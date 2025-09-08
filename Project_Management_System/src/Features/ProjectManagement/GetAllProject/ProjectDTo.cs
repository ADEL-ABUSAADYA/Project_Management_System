﻿using Project_Management_System.Models.Enums;

namespace Project_Management_System.Features.ProjectManagement.GetAllProject
{
    public class ProjectDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ProjectStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; } 
    
        public string CreatorName { get; set; }
    }
}
