﻿using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;
using Project_Management_System.Data;

namespace Project_Management_System.Features.SprintItemManagement.AddSprintItem
{
    public class AddSprintItemRequestViewModel
    {
        public string Title { get;  set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
    }
    
    public class ForgetPasswordViewModelValidator : AbstractValidator<AddSprintItemRequestViewModel>
    {
        public ForgetPasswordViewModelValidator ()
        {
         
        }
    }
}
