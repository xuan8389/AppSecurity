using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using WebApplication3.ViewModels;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication3.Pages
{


    public class IndexModel : PageModel
    {
        public string DecryptedNric { get; set; } // Property to hold decrypted NRIC
        private RSACryptoServiceProvider rsa { get; }

        public string PasswordHash { get; set; } // Property to hold decrypted NRIC


        private readonly ILogger<IndexModel> _logger;
        private readonly UserManager<IdentityUser> userManager;


        public IndexModel(ILogger<IndexModel> logger, UserManager<IdentityUser> userManager, RSACryptoServiceProvider rsa)
        {
            _logger = logger;
            this.userManager = userManager;
            this.rsa = rsa;


        }

        public string Decrypt(string encryptedNric)
        {
            
            byte[] byteText = Encoding.UTF8.GetBytes(encryptedNric);
            byte[] plainText = rsa.Decrypt(byteText, false);
            return Encoding.UTF8.GetString(plainText);


        }



        public async Task OnGet()
        {
            

            

        }

            
        }

    }
