using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BE_TaskManager.Models;

public class User
{
    [JsonIgnore] public Guid Id { get; set; }
    [Required] public string Username { get; set; }
    [Required] public string Password { get; set;}
    [Required] public string Email { get; set;}

    public User(string username, string password, string email){
        Id = new Guid();
        Username = username;
        Password = password;
        Email = email;
    }

}