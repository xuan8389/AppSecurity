using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using WebApplication3.ViewModels;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication3.Pages
{
    public class RegisterModel : PageModel
    {
        private UserManager<IdentityUser> userManager { get; }
        private SignInManager<IdentityUser> signInManager { get; }



        [BindProperty]
        public Register RModel { get; set; }
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

        public RegisterModel(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, RSACryptoServiceProvider rsa)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.rsa = rsa;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            RModel.FirstName = SanitizeText(RModel.FirstName);
            RModel.LastName = SanitizeText(RModel.LastName);
            RModel.NRIC = SanitizeText(RModel.NRIC);
        

            byte[] byteText = Encoding.UTF8.GetBytes(RModel.NRIC);
            byte[] encryptedText = rsa.Encrypt(byteText, false);

            string nric = Convert.ToBase64String(encryptedText);

            if (RModel.DateOfBirth > DateTime.Today)
            {
                ModelState.AddModelError("DateOfBirth", "Date of birth cannot be after today.");
            }
            if (ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    UserName = RModel.Email,
                    Email = RModel.Email,
                    PhoneNumber = nric
                };
                var result = await userManager.CreateAsync(user, RModel.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToPage("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return Page();
        }

        private string SanitizeText(string input)
        {
            input = input.Replace("<", "&lt;").Replace(">", "&gt;");
            input = Regex.Replace(input, @"[^a-zA-Z\s]", "");

            return input; // Allow only letters and spaces
        }


    }
}
