
using System.ComponentModel.DataAnnotations;

namespace Munch.Models
{
    public class AddReview
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter your name")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Please enter your comment")]
        public string? Comment { get; set; }

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int Rating { get; set; }
    }
}