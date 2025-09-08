﻿// using Microsoft.EntityFrameworkCore;
// using Project_Management_System.Models;
//
// namespace Project_Management_System.Data.Repositories
// {
//     public class UserRepository : Repository<User> , IUserRepository
//     {
//         public UserRepository(Context context) : base(context)
//         {
//         }
//
//         public async Task<(int, bool)> LogInUser(string email, string password)
//         {
//             var userData = await _context.Users
//             .AsNoTracking()
//             .Where(u => u.Email == email && u.Password == password) 
//             .Select(u => new { u.ID, u.TwoFactorAuthEnabled })
//             .FirstOrDefaultAsync();
//
//             if (userData is null)
//             {
//                 return (0, false);
//             }
//
//             return (userData.ID, userData.TwoFactorAuthEnabled);
//         }
//     }
// }
