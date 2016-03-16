using System.ComponentModel.DataAnnotations;

namespace WebApplication3.ViewModels.Account
{
	public class ExternalLoginConfirmationViewModel
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
	}
}