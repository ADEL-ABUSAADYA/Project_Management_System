using Project_Management_System.Common.Data.Enums;

namespace Project_Management_System.Models
{
    public class UserFeature : BaseModel
    { 
        public Guid UserID { get; set; }
        public User user { get; set; }
        public Feature Feature { get; set; }
    }
}
