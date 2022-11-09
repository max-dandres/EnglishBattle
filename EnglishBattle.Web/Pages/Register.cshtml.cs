using EnglishBattle.Web.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace EnglishBattle.Web.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        [Display(Name = nameof(SharedResources.Username), ResourceType = typeof(SharedResources))]
        [Required(ErrorMessageResourceName = nameof(SharedResources.UsernameRequired), ErrorMessageResourceType = typeof(SharedResources))]
        public string UserName { get; set; } = "";
        [BindProperty]
        [Display(Name = nameof(SharedResources.Password), ResourceType = typeof(SharedResources))]
        [Required(ErrorMessageResourceName = nameof(SharedResources.PasswordRequired), ErrorMessageResourceType = typeof(SharedResources))]
        public string Password { get; set; } = "";
        [BindProperty]
        [Display(Name = nameof(Register.ConfirmPassword), ResourceType = typeof(Register))]
        [Required(ErrorMessageResourceName = nameof(Register.ConfirmPasswordRequired), ErrorMessageResourceType = typeof(Register))]
        [Compare(nameof(Password), ErrorMessageResourceName = nameof(Register.ConfirmPasswordMismatch), ErrorMessageResourceType = typeof(Register))]
        public string ConfirmPassword { get; set; } = "";

        public RegisterModel()
        {

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
