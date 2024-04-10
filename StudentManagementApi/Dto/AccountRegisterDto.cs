using System.ComponentModel.DataAnnotations;

namespace StudentManagementApi.Dto
{
    public class AccountRegisterDto
    {
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "First name must be between 1 and 255 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Last name must be between 1 and 255 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }
    }
}