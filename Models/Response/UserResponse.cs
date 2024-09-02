
using System.Text.Json.Serialization;

namespace BE_TaskManager.Models.Response{

    public class UserResponse{
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        [JsonIgnore] public List<Task>? Tasks{ get; set; }
    }

}