using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WebApplication3.ViewModels
{
	public class Register
	{
		[Required]
		[Display(Name = "First Name")]
		public string FirstName { get; set; }

		[Required]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		[Required]
		[Display(Name = "Gender")]
		public string Gender { get; set; }

		[Required]
		[RegularExpression(@"(?i)^[STFG]\d{7}[A-Z]$", ErrorMessage = "NRIC must be in the format S/T/F/G followed by 7 digits and an uppercase letter")]
		[Display(Name = "NRIC")]
		public string NRIC { get; set; }

		[Required]
		[DataType(DataType.EmailAddress)]
		[Display(Name = "Email Address")]
		public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [MinLength(12, ErrorMessage = "Password must be at least 12 characters long")]
        public string Password { get; set; }

        [Required]
		[DataType(DataType.Password)]
		[Compare(nameof(Password), ErrorMessage = "Password and confirmation password do not match")]
		[Display(Name = "Confirm Password")]
		public string ConfirmPassword { get; set; }

		[Required]
		[DataType(DataType.Date)]
		[Display(Name = "Date of Birth")]
		public DateTime DateOfBirth { get; set; }
	}
}
