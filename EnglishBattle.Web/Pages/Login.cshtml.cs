using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using EnglishBattle.Web.Resources;
using System.Security.AccessControl;

namespace EnglishBattle.Web.Pages.Shared
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        [Display(Name = nameof(SharedResources.Username), ResourceType = typeof(SharedResources))]
        [Required(ErrorMessageResourceName = nameof(SharedResources.UsernameRequired), ErrorMessageResourceType = typeof(SharedResources))]
        public string UserName { get; set; }
        [BindProperty]
        [Display(Name = nameof(SharedResources.Password), ResourceType = typeof(SharedResources))]
        [Required(ErrorMessageResourceName = nameof(SharedResources.PasswordRequired), ErrorMessageResourceType = typeof(SharedResources))]
        public string Password { get; set; }

        public LoginModel()
        {
            UserName = string.Empty;
            Password = string.Empty;
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                return;
            }
        }
    }
}
