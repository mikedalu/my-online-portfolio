using System.ComponentModel.DataAnnotations;

namespace my_online_portfolio.Models
{
  public class ContactViewModel
  {
    // The [Required] attribute ensures the field is not empty
    [Required(ErrorMessage = "Please enter your name")]
    public string Name { get; set; }

    // [EmailAddress] automatically validates the format (e.g., has '@' and '.')
    [Required]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

    // Limits the message length to prevent massive inputs
    [Required]
    [StringLength(500, MinimumLength = 10, ErrorMessage = "Message must be between 10 and 500 characters")]
    public string Message { get; set; }
  }
}
