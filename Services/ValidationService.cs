using System.Net;
using System.Text.RegularExpressions;
using BE_TaskManager.Models;
using BE_TaskManager.Models.Response;

namespace BE_TaskManager.Services{

    public class ValidationService{

        public static Response RequiredFieldMissing(User user){
            if(user.Email == null || Equals("", user.Email)){
                return new Response(HttpStatusCode.BadRequest,"Email cannot be empty.");
            }
            if(user.Password == null || user.Password.Length == 0){
                return new Response(HttpStatusCode.BadRequest,"Password cannot be empty.");
            }
            if(user.Username == null || user.Username.Length == 0){
                return new Response(HttpStatusCode.BadRequest,"Username cannot be empty.");
            }
            return new Response(HttpStatusCode.OK, "User properties are correct.");
        }

        public static bool IsValidEmail(string email){
            Match match = Regex.Match(email, @"[a-zA-Z0-9]+@[a-zA-Z0-9]+\.[a-zA-Z]{2,}");
            if(match.Success){
                return true;
            }
            return false;
        }
    }

}