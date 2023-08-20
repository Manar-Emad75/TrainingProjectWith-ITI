using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Sunrise.Models
{
    public class Employee
    {
        
        public int Id { get; set; }

        [Required (ErrorMessage ="you have to provide a valid full name")]
        [MinLength(12 , ErrorMessage ="Full Name must be greater than 12 characters.")]
        [MaxLength(70, ErrorMessage = "Full Name must be 1ess than 70 characters.")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "you have to provide a valid Position")]
        [MinLength(2, ErrorMessage = "Position must be greater than 2 characters.")]
        [MaxLength(20, ErrorMessage = "Position must be 1ess than 20 characters.")]
        public string Position { get; set; }
        [Required(ErrorMessage = "you have to provide a valid Salary")]
        //[Range(5500,55000,ErrorMessage ="Salary must be between 5500 EGP and 55000 EGP")]
        public decimal Salary { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime JoinDate { get; set; }

        [DisplayName("Image")]
        [ValidateNever]
        public string ImageUrl { get; set; }

        // foreign key
        [Range(1,int.MaxValue,ErrorMessage="Choose a valid Department.")]
        [DisplayName("Department")]
        public int DepartmentId { get; set; }

        [ValidateNever]
          public Department Department { get; set; }

	}
}
