using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BE_TaskManager.Models;

public class Task
{   
    public const int TASK_STATUS_PENDING = 0;
    public const int TASK_STATUS_DONE = 1;
    public const int TASK_STATUS_FAILED = 2;
    [JsonIgnore] public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime? Deadline { get; set; } 
    public int Status { get; set; }
    [JsonIgnore] public DateTime CreatedAt { get; set; }
    [JsonIgnore] public DateTime UpdatedAt { get; set; }
    public Task(string name, string description, DateTime? deadline){
        Name = name;
        Description = description;
        Deadline = deadline;
        Status = TASK_STATUS_PENDING;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }
}