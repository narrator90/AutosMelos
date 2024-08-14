using Autokereskedes_BS.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Autokereskedes_BS.Services
{
    public class ExtUserService
    {

        /// <summary>
        /// Represents a user with additional details.
        /// </summary>
        public record Felhasznalo(string Id, string FullName, bool lockedout, string UserName, bool active, int carCount, bool admin);
        /// <summary>
        /// Represents a user scheduled for deletion.
        /// </summary>
        public record TorlendoFelhasznalo(string id, string FullName, string UserName, DateTime requestTime);
        /// <summary>
        /// Represents the model for adding or editing a user.
        /// </summary>
        public record AddOrEditUserModel(
            string Id,
            string FullName,
            string UserName,
            string Password,
            string EMail,
            bool Active
        );


        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        /// <summary>
        /// Initializes a new instance of the ExtUserService class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        /// <param name="um">The user manager for handling user-related operations.</param>
        public ExtUserService(ApplicationDbContext context, UserManager<ApplicationUser> um)
        {
            _context = context;
            _userManager = um;
        }
        /// <summary>
        /// Changes the admin role for a user.
        /// </summary>
        /// <param name="f">The user to change the admin role for.</param>
        public async Task ChangeAdminRole(Felhasznalo f)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == f.Id);
            if (await _userManager.IsInRoleAsync(user, "Administrator"))
                await _userManager.RemoveFromRoleAsync(user, "Administrator");
            else
                await _userManager.AddToRoleAsync(user, "Administrator");
        }
        /// <summary>
        /// Gets a list of users who have requested account deletion.
        /// </summary>
        /// <returns>A list of users who have requested deletion.</returns>
        public List<ApplicationUser> GetUsersWhoRequestDelete() => _context.Users.Where(x => x.Active && x.DeleteRequested).ToList();
        /// <summary>
        /// Marks a user account as requested for deletion.
        /// </summary>
        /// <param name="user">The user requesting deletion.</param>
        public async Task DeleteRequest(ApplicationUser user)
        {
            var u = _context.Users.FirstOrDefault(x => x == user);
            u.DeleteRequested = true;
            u.DeleteRequestTime = DateTime.Now;
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Deletes a user based on the provided information.
        /// </summary>
        /// <param name="tf">The information of the user to be deleted.</param>
        public void DeleteUser(TorlendoFelhasznalo tf) {

            var user = _context.Users.FirstOrDefault(x => x.Id == tf.id);
            _userManager.DeleteAsync(user); 
        }
        /// <summary>
        /// Adds or updates a user based on the provided model.
        /// </summary>
        /// <param name="m">The model containing user information.</param>
        public void AddOrUpdateUser(AddOrEditUserModel m)
        {
            ApplicationUser user = string.IsNullOrEmpty(m.Id) ? Activator.CreateInstance<ApplicationUser>() : _context.Users.FirstOrDefault(x => x.Id == m.Id);
            user.FullName = m.FullName;
            user.UserName = m.UserName;
            user.Email = m.EMail;
            user.Active = m.Active;
            user.EmailConfirmed = true;
            if (string.IsNullOrEmpty(m.Id))
                _userManager.CreateAsync(user, m.Password);
            else
                _userManager.UpdateAsync(user);
        }
        /// <summary>
        /// Retrieves a user by their username.
        /// </summary>
        /// <param name="name">The username of the user to retrieve.</param>
        /// <returns>The user with the specified username, or null if not found.</returns>
        public ApplicationUser GetUserByUserName(string name)
        {
            return _context.Users.FirstOrDefault(x => x.UserName == name);
        }
        /// <summary>
        /// Retrieves a model for adding or editing a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve the model for.</param>
        /// <returns>The AddOrEditUserModel for the specified user.</returns>
        public AddOrEditUserModel GetUserModelById(string id)
        {
            var result = _context.Users.FirstOrDefault(x => x.Id == id);
            return new AddOrEditUserModel(result.Id, result.FullName, result.UserName, "", result.Email, result.Active);
        }
        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The user with the specified ID, or null if not found.</returns>
        public ApplicationUser GetUserbyId(string id)
        {
            var result = _context.Users.FirstOrDefault(x => x.Id == id);
            return result;
        }
        /// <summary>
        /// Retrieves a list of users who have requested account deletion.
        /// </summary>
        /// <returns>A list of TorlendoFelhasznalo objects representing users requesting deletion.</returns>
        public List<TorlendoFelhasznalo> GetTorlendoFelhasznaloLista()
        {
            List<TorlendoFelhasznalo> result = new List<TorlendoFelhasznalo>();
            var f_lista = _context.Users.Where(x => x.DeleteRequested).ToList();
            foreach ( var f in f_lista )
            {
                result.Add(new TorlendoFelhasznalo(
                       f.Id,
                       f.FullName,
                       f.UserName,
                       f.DeleteRequestTime
                    ));
            }
            return result;
        }
        /// <summary>
        /// Retrieves a list of users who are locked out of their accounts.
        /// </summary>
        /// <returns>A list of locked-out users.</returns>
        public List<ApplicationUser> GetLockedOutUsers()
        {
            return _context.Users.Include(x => x.Cars).Where(x => x.LockoutEnd != null).ToList();
        }
        /// <summary>
        /// Unlocks a user account.
        /// </summary>
        /// <param name="user">The user to unlock.</param>
        public void UnlockUser(ApplicationUser user) => _userManager.SetLockoutEndDateAsync(user, null);
        /// <summary>
        /// Retrieves a list of users with additional details, including their roles and car count.
        /// </summary>
        /// <returns>A list of Felhasznalo objects representing users.</returns>
        public async Task<List<Felhasznalo>> GetUserLista()
        {
            List<Felhasznalo> result = new List<Felhasznalo>();
            var f_lista = _context.Users.Include(x=>x.Cars).ToList();
            foreach (var user in f_lista)
            {
                var isAdmin = await _userManager.IsInRoleAsync(user, "Administrator");
                result.Add(new Felhasznalo(
                    user.Id,
                    user.FullName, 
                    user.LockoutEnabled, 
                    user.UserName, 
                    user.Active, 
                    (user.Cars == null ? 0 : user.Cars.Count()),
                    isAdmin
                ));
            }
            return result;
        }
            
    }
}
