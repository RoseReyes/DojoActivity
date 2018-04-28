using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using System;

namespace dojoactivity.Models

{
    public class AddActivityViewModel
    {
        
        [Required(ErrorMessage="Title is required")]
        [Display(Name="Title")]
        [MinLength(2)]
        public string Title { get; set; }
        
        [Required(ErrorMessage="Time is required")]
        [Display(Name="Time")]
        public TimeSpan Time { get; set; }
        
        [Required(ErrorMessage="Date is required")]
        [Display(Name="Date")]
        public DateTime Date { get; set; }
        
        [Required(ErrorMessage="Description is required")]
        [MinLength(10)]
        [Display(Name="Description")]
        public string Description { get; set; }
        
        [Required(ErrorMessage="Duration is required")]
        [Display(Name="Duration")]
        public int Duration { get; set; }
        
        [Required(ErrorMessage="Hours is required")]
        [Display(Name="Hours")]
        public string Hours { get; set; }
    }
}
