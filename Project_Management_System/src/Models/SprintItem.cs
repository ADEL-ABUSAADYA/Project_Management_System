using Project_Management_System.Models.Enums;

namespace Project_Management_System.Models;

public class SprintItem : BaseModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public SprintItemStatus Status { get; set; }
    
    public Guid ProjectID { get; set; }
    public Project Project { get; set; }
    
    public ICollection<UserSprintItem> UserSprintItems { get; set; }
}