using System.ComponentModel.DataAnnotations;

namespace StudentManagementApi.Dto
{
    public class AddStudentDto
    {
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "First name must be between 1 and 255 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Last name must be between 1 and 255 characters.")]
        public string LastName { get; set; }

        [StringLength(255, MinimumLength = 1, ErrorMessage = "Other name must be between 1 and 255 characters.")]
        [Required(AllowEmptyStrings = true, ErrorMessage = "Other name must be between 1 and 255 characters.")]
        public string? OtherNames { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.DateTime, ErrorMessage = "Date of birth must be a valid date.")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(13, ErrorMessage = "Invalid phone number.Phone number must be at most 13 characters long.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; }

        [Required]
        [RegularExpression("^(male|female)$", ErrorMessage = "Gender must be either 'male' or 'female'.")]
        public string Gender { get; set; }

        [Required]
        public string Address { get; set; }
    }
}