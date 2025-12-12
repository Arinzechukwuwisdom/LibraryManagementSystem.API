using BCrypt.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SureLbraryAPI.Context;
using SureLbraryAPI.DTOs;
using SureLbraryAPI.Interfaces;
using SureLbraryAPI.Models;
using SureLbraryAPI.Utilities;
using System.ComponentModel.DataAnnotations;

namespace SureLbraryAPI.Repository
{
    public class UserRepository : IUserService
    {   
        private readonly LibraryContext _context;
        public UserRepository(LibraryContext context)
        {
            _context = context;
        }
        
        public async Task<ResponseDetails<GetUserDTO>> CreateUserAsync(CreateUserDTO userDetail)
        {
            try
            {
                var userAlreadyExists=await _context.Users.AnyAsync(u=>u.Email==userDetail.Email);
                if (userAlreadyExists)
                {
                    return ResponseDetails<GetUserDTO>.Failed("Conflict", $"User with Email:{userDetail.Email} already Exists", 409);
                }
                var lastestMembershipNumber = await _context.Users.Select(u => u.MembershipNumber).MaxAsync(); 
                var user = new User
                {
                    Email = userDetail.Email,
                    Name = userDetail.Name,
                    Address = userDetail.Address,
                    Password=BCrypt.Net.BCrypt.HashPassword(userDetail.Password),
                    MembershipNumber = lastestMembershipNumber + 1,
                    Role=userDetail.Role
                };  
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                var userDTO = new GetUserDTO    
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    MembershipCode=user.MembershipCode,
                    Address = user.Address,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt,
                };
                    return ResponseDetails<GetUserDTO>.Success(userDTO, "User successfully Created", 201);

            }
            catch (Exception ex) 
            {
               return ResponseDetails<GetUserDTO>.Failed("An Exception was Caught",ex.Message,ex.HResult);
            }
        }

        // This is delete
        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    return false;
                }
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ResponseDetails<List<GetUserDTO>>> GetAllUsersAsync()
        {
            try
            {
                var users=await _context.Users.Select(x => new GetUserDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    MembershipCode = x.MembershipCode,
                    Address = x.Address,
                    CreatedAt= x.CreatedAt,
                    UpdatedAt= x.UpdatedAt,
                }).ToListAsync();
                if(users.Count == 0)
                {
                    return ResponseDetails<List<GetUserDTO>>.Success([],"No User Found",204);
                }
                return ResponseDetails<List<GetUserDTO>>.Success(users," Users retrieved successfully",200);
            
            }
            catch (Exception ex) 
            {
                return ResponseDetails<List<GetUserDTO>>.Failed("Caught Exception", ex.Message,ex.HResult);
            }
        }

        public async Task<ResponseDetails<GetUserDTO>> GetUserByIdAsync(int id)
        {
            try
            {
               var user= await _context.Users.FindAsync(id);
                if(user is null)
                {
                    return ResponseDetails<GetUserDTO>.Failed();
                }
                var userDTO = new GetUserDTO
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    MembershipCode = user.MembershipCode,
                    Address = user.Address,
                };
                return ResponseDetails<GetUserDTO>.Success(userDTO, "User Found Successfully",200);
            }
            catch(Exception ex)
            {
                return ResponseDetails<GetUserDTO>.Failed("Caught Exception", ex.Message,ex.HResult);
            }
        }

        

        //public async Task<ResponseDetails<GetUserDTO>> UpdateUsersAsync(CreateUserDTO userDetail, int id)
        //{
        //    try
        //    {
        //        var user = await _context.Users.FindAsync(id);
        //        if (user is null)
        //        {
        //            return ResponseDetails<GetUserDTO>.Failed("Not Found", $"User with ID:{id} not found", 400);
        //        }
        //        var EmailExists = await _context.Users.AnyAsync(u => u.Email == userDetail.Email && u.Id != id);
        //        if (EmailExists)
        //        {
        //            return ResponseDetails<GetUserDTO>.Failed("Conflict", $"User with Email{userDetail.Email} already Exists", 409);
        //        }
        //        user.Name = userDetail.Email ?? user.Email;
        //        user.Address = userDetail.Address ?? user.Address;
        //        user.Email = userDetail.Email ?? user.Email;
        //        user.Password = userDetail.Password is not null ? BCrypt.Net.BCrypt.HashPassword(userDetail.Password) : user.Password;
        //        user.UpdatedAt = DateTime.UtcNow;

        //        var getUserDTO = new GetUserDTO
        //        {
                    
        //        };


        //    }
        //    catch (Exception ex)
        //    {
        //        return ResponseDetails<GetUserDTO>.Failed("Caught Exception", ex.Message, ex.HResult);

        //    }
        //}
    }
}
