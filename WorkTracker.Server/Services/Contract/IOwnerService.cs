using WorkTracker.Database.DTO;
using WorkTracker.Database.Models;

namespace WorkTracker.Server.Services
{
    public interface IOwnerService
    {
        /// <summary>
        /// Adds the owner 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="encryptedPassword"></param>
        /// <returns></returns>
        OwnerDTO AddOwner(string name, string email, string encryptedPassword);

        /// <summary>
        /// Returns the owner having given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        OwnerDTO RetrieveOwnerById(int id);

        /// <summary>
        /// Returns the owner having given email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        OwnerDTO RetrieveOwnerByEmail(string email);

        /// <summary>
        /// Updates the owner 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        OwnerDTO UpdateOwner(OwnerDTO obj);

        /// <summary>
        /// Authorizes the user and send back the JWT token
        /// </summary>
        /// <param name="email"></param>
        /// <param name="encryptedPassword"></param>
        /// <returns></returns>
        bool Authenticate(string email, string encryptedPassword);
    }
}