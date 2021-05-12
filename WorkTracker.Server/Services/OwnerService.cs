using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using WorkTracker.Database.DTO;
using WorkTracker.Database.Interfaces;
using WorkTracker.Database.Models;
using WorkTracker.Server.Exceptions;
using WorkTracker.Server.Services.Contract;

namespace WorkTracker.Server.Services
{
    public class OwnerService : IOwnerService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IHelper _helper;
        private readonly IStringLocalizer<Resource> _stringLocalizer;


        public OwnerService(IUnitOfWork unitOfWork, IMapper mapper, IHelper helper, IStringLocalizer<Resource> stringLocalizer)
        {
            _mapper = mapper;
            _helper = helper;
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Adds the owner 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="encryptedPassword"></param>
        /// <returns></returns>
        public OwnerDTO AddOwner(string name, string email, string encryptedPassword)
        {
            var ownerToInsert = new Owner()
            {
                Workers = null,
                Jobs = null,
                Name = name,
                Email = email.ToLower(),
                EncryptedPassword = encryptedPassword
            };

            _unitOfWork.Owners.Insert(ownerToInsert);
            _unitOfWork.Commit();

            return _mapper.Map<OwnerDTO>(ownerToInsert);
        }

        /// <summary>
        /// Returns the owner having given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OwnerDTO RetrieveOwnerById(int id)
        {
            var user = _unitOfWork.Owners.GetByID(id);
            if (user != null)
                return _mapper.Map<OwnerDTO>(user);

            throw new WtException(_stringLocalizer["OwnerNotFound"], Constants.OWNER_NOT_FOUND);
        }

        /// <summary>
        /// Returns the owner having given email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public OwnerDTO RetrieveOwnerByEmail(string email)
        {
            var user = _unitOfWork.Owners.Get(o => string.Equals(o.Email, email.ToLower()))?.FirstOrDefault();
            if (user != null)
                return _mapper.Map<OwnerDTO>(user);

            throw new WtException(_stringLocalizer["OwnerNotFound"], Constants.OWNER_NOT_FOUND);
        }

        /// <summary>
        /// Updates the owner 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public OwnerDTO UpdateOwner(OwnerDTO obj, string encryptedPassword)
        {
            var user = _unitOfWork.Owners.GetByID(obj.Id);
            if (user != null)
            {
                _unitOfWork.Owners.Update(new Owner()
                {
                    Name = obj.Name,
                    Email = obj.Email,
                    EncryptedPassword = encryptedPassword
                });
                var updatedUser = _unitOfWork.Owners.GetByID(obj.Id);
                return _mapper.Map<OwnerDTO>(updatedUser);
            }

            throw new WtException(_stringLocalizer["OwnerNotFound"], Constants.OWNER_NOT_FOUND);
        }

        /// <summary>
        /// Authorizes the user and send back the JWT token
        /// </summary>
        /// <param name="email"></param>
        /// <param name="encryptedPassword"></param>
        /// <returns></returns>
        public bool Authenticate(string email, string encryptedPassword)
        {
            var user = _unitOfWork.Owners.Get(x => x.Email == email && x.IsEmailVerified == true).FirstOrDefault();
            if (user == null)
            {
                return false;
            }
            return string.Equals(encryptedPassword, user.EncryptedPassword);
        }

        public void VerifyEmail(string email)
        {
            var user = _unitOfWork.Owners.Get(x => string.Equals(x.Email.ToLower(), email.ToLower()) &&
                                                   !x.IsEmailVerified).FirstOrDefault();
            if (user == null)
                throw new WtException(_stringLocalizer["ErrorInvalidEmail"], Constants.INVALID_EMAIL);

            user.IsEmailVerified = true;

            try
            {
                _unitOfWork.Owners.Update(user);
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw;
            }

        }
    }
}
