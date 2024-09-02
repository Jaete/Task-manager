using System.Text.Json.Serialization;

namespace BE_TaskManager.Models;

public class User
{
    [JsonIgnore] public Guid Id { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set;}
    public string? Email { get; set;}

    [JsonIgnore] public List<Task>? Tasks { get; set; }

    public User(string username, string password, string email){
        Id = new Guid();
        Username = username;
        Password = password;
        Email = email;
    }

}