using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Sunrise.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "you have to provide a valid full name")]
        [MinLength(2, ErrorMessage = "Department Name must be greater than 1 characters.")]
        [MaxLength(70, ErrorMessage = "Department Name must be 1ess than 70 characters.")]
        public string FullName { get; set; }

		[Required(ErrorMessage = "you have to provide a valid Description for this department")]
		[MinLength(2, ErrorMessage = "Department Description must be greater than 1 characters.")]
		[MaxLength(70, ErrorMessage = "Department Description must be 1ess than 70 characters.")]
		public string Description { get; set; }
        [ValidateNever]
        public List<Employee>Employees { get; set; }
    }
}
