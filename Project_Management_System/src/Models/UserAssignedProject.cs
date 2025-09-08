using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Management_System.Models;

public class UserAssignedProject : BaseModel
{
    public Guid UserID { get; set; }
    public User User { get; set; }
    
    public Guid ProjectID { get; set; }
    public Project Project { get; set; }
    
    public DateTime AssignDate { get; set; }
    public DateTime EndDate { get; set; }
}