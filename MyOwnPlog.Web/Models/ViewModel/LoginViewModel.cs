using System.ComponentModel.DataAnnotations;

namespace MyOwnPlog.Web.Models.ViewModel
{
	public class LoginViewModel
	{
		[Required]
		public string username { get; set; }
		[Required]
		[MinLength(6, ErrorMessage ="the password has to be at least 6 number")]
		public string password { get; set; }

		public string? ReturnUrl { get; set; }
	}
}
