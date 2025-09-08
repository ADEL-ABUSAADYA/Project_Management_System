namespace Project_Management_System.Models;

public class UserSprintItem :BaseModel
{
    public Guid UserID { get; set; }
    public User User { get; set; }
    
    public Guid SprintItemID { get; set; }
    public SprintItem SprintItem { get; set; }
}