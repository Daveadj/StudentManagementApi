using System.ComponentModel.DataAnnotations;

namespace StudentManagementApi.Dto
{
    public class LoginDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}