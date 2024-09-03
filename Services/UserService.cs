using System.ComponentModel.DataAnnotations;
using System.Net;
using BCrypt.Net;
using BE_TaskManager.Context;
using BE_TaskManager.Models;
using BE_TaskManager.Models.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BE_TaskManager.Services{
    public class UserServices {

        private readonly TaskManager _context;

        public UserServices(TaskManager taskManagerContext)
        {
            _context = taskManagerContext;
        }

        public async Task<Response> Create([FromBody] User request) 
        {
            PasswordHasher<string> hasher = new();

            var validatedUser = ValidationService.RequiredFieldMissing(request);

            if(validatedUser.StatusCode != HttpStatusCode.OK){
                return validatedUser;
            }

            if(!ValidationService.IsValidEmail(request.Email)){
                return new Response(
                    HttpStatusCode.BadRequest, 
                    "Email is not valid.");
            }

            var user = new User(
                request.Username,
                BCrypt.Net.BCrypt.EnhancedHashPassword(request.Password),
                request.Email
            );
            
            _context.Add(user);
            await _context.SaveChangesAsync();
            
            return new Response(
                HttpStatusCode.Created,
                "User created sucessfully."
            );
        }

        public async Task<Response> Update(Guid id, [FromBody] User request) 
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);

            if(user == null){
                return new Response(
                    HttpStatusCode.NotFound,
                    "User not found."
                );
            }

            user.Username = request.Username ?? user.Username;
            user.Password = request.Password ?? user.Password;
            if(request.Email == null || request.Email.Length == 0){
                user.Email = user.Password;
            }
            else if(ValidationService.IsValidEmail(request.Email)){
                user.Email = request.Email;
            }else{
                return new Response(
                    HttpStatusCode.BadRequest, 
                    "Email is not valid."
                );
            }

            _context.Update(user);
            await _context.SaveChangesAsync();

            return new Response(
                HttpStatusCode.OK,
                "User updated sucessfully."
            );
        }

        public async Task<Response> GetAll() 
        {
            /* FETCH USERS */
            var users = await _context.Users.ToListAsync();

            if(users.Count == 0){
                return new Response(
                    HttpStatusCode.NotFound,
                    "No user found."
                );
            }

            /* DOES NOT RETURN USERS PASSWORDS */
            List<UserResponse> response = new();

            foreach(var user in users){
                var userResponse = new UserResponse
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                };
                response.Add(userResponse);
            }

            return new FetchResponse<List<UserResponse>>(
                response,
                HttpStatusCode.OK,
                "Users found."
            );
        }

        public async Task<Response> GetById(Guid id) 
        {
            /* FETCH USER */
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);

            if(user == null){
                return new Response(
                    HttpStatusCode.NotFound, 
                    "User not found."
                );
            }

            /* DOES NOT RETURN USER'S PASSWORD */
            UserResponse response = new()
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };

            return new FetchResponse<UserResponse>(
                response,
                HttpStatusCode.OK,
                "User found."
            );
        }

        public async Task<Response> GetByEmail(string email){

            /* FETCH USER */
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == email);

            if(user == null){
                return new Response(
                    HttpStatusCode.NotFound, 
                    "User not found."
                );
            }

            /* DOES NOT RETURN USER'S PASSWORD */
            UserResponse response = new()
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };

            return new FetchResponse<UserResponse>(
                response,
                HttpStatusCode.OK,
                "User found."
            );
        }

        public async Task<Response> Delete(Guid id){
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);

            if(user == null){
                return new Response(
                    HttpStatusCode.NotFound,
                    "User not found."
                );
            }

            _context.Remove (user);
            await _context.SaveChangesAsync();

            return new Response(
                HttpStatusCode.OK,
                "User deleted sucessfully."
            );
        }

    }
}