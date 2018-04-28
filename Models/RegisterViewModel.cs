using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using System;

namespace dojoactivity.Models

{
    public class RegisterViewModel
    {
        
        [Required(ErrorMessage="Email is required")]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9.+_-]+@[a-zA-Z0-9._-]+\.[a-zA-Z]+$")]
        [Display(Name="Email")]
        public string Email { get; set; }
        
        [Required(ErrorMessage="First name is required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First Name can only contain letters")]
        [Display(Name="First Name")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage="Last name is required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last Name can only contain letters")]
        [Display(Name="Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage="Password is required")]
        [RegularExpression(@"^[a-zA-Z0-9.+_-]+@[a-zA-Z0-9._-]+$", ErrorMessage="Password should contain 1 leter, 1 digit and 1 special characters")]
        [MinLength(8)]
        [Display(Name="Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Display(Name="Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirmation must match.")]
        public string ConfirmPassword { get; set; }
        
    }
}
