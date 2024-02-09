using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication3.ViewModels;
using IdentityUser = Microsoft.AspNetCore.Identity.IdentityUser;

namespace WebApplication3.Pages
{
    public class LoginModel : PageModel
    {
		[BindProperty]
		public Login LModel { get; set; }

		private readonly SignInManager<IdentityUser> signInManager;
		public LoginModel(SignInManager<IdentityUser> signInManager)
		{
			this.signInManager = signInManager;
		}
		public void OnGet()
        {
        }

		public async Task<IActionResult> OnPostAsync()
		{
			if (ModelState.IsValid)
			{
				var identityResult = await signInManager.PasswordSignInAsync(LModel.Email, LModel.Password,
			   LModel.RememberMe, false);
				if (identityResult.Succeeded)
				{
					return RedirectToPage("Index");
				}
				Console.WriteLine("error in logging in");
				ModelState.AddModelError("", "Username or Password incorrect");
			}
			return Page();
		}
	}
}
