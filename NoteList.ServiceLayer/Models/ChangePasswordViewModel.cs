using System.ComponentModel.DataAnnotations;

namespace NoteList.ServiceLayer.Models
{
    public class ChangePasswordViewModel
    {

        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        public string ConfirmPassword { get; set; }
    }

}
