using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using WorkTracker.Database.DTO;
using WorkTracker.Database.Interfaces;
using WorkTracker.Database.Models;
using WorkTracker.Server.Services.Contract;

namespace WorkTracker.Server.Services
{
    public class OwnerService : IOwnerService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IHelper _helper;


        public OwnerService(IUnitOfWork unitOfWork, IMapper mapper, IHelper helper)
        {
            _mapper = mapper;
            _helper = helper;
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
                Email = email,
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

            throw new Exception("User not found");
        }

        /// <summary>
        /// Returns the owner having given email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public OwnerDTO RetrieveOwnerByEmail(string email)
        {
            var user = _unitOfWork.Owners.Get(o => string.Equals(o.Email, email))?.FirstOrDefault();
            if (user != null)
                return _mapper.Map<OwnerDTO>(user);

            throw new Exception("User not found");
        }

        /// <summary>
        /// Updates the owner 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public OwnerDTO UpdateOwner(OwnerDTO obj)
        {
            var user = _unitOfWork.Owners.GetByID(obj.Id);
            if (user != null)
            {
                _unitOfWork.Owners.Update(new Owner()
                {
                    Id = obj.Id,
                    Name = obj.Name,
                    Email = obj.Email,
                });
                var updatedUser = _unitOfWork.Owners.GetByID(obj.Id);
                return _mapper.Map<OwnerDTO>(updatedUser);
            }

            throw new Exception("User not found");
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
            var user = _unitOfWork.Owners.Get(x => x.Email == email).FirstOrDefault();
            if (user == null)
                throw new Exception("Invalid email");

            user.IsEmailVerified = true;

            _unitOfWork.Owners.Update(user);
            _unitOfWork.Commit();
        }
    }
}
