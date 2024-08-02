using System.ComponentModel.DataAnnotations;

namespace NoteList.ServiceLayer.Models
{
    public class RegisterViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
    }
}
