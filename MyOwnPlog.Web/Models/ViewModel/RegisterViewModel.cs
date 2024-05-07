using System.ComponentModel.DataAnnotations;

namespace MyOwnPlog.Web.Models.ViewModel
{
	public class RegisterViewModel
	{

		[Required]
		public string username { get; set; }

        [Required]
		[EmailAddress]
        public string Email { get; set; }
        [Required]
		[MinLength(6, ErrorMessage ="the minimum length should be 6")]
        public string password { get; set; }
	}
}
