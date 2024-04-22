using System.ComponentModel.DataAnnotations;

namespace TodoModels.Models
{
    public enum Status
    {
        TODO, WORKING, DONE
    }

    public enum Priority
    {
        LOW, MEDIUM, HIGH
    }

    public class Todo
    {
        [Key]
        public Guid TodoId { get; set; }

        [Required, StringLength(120, MinimumLength = 3)]
        public required string Title { get; set; }

        public string? Description { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public Priority Priority { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }

        [Required]
        public required Guid UserId { get; set; }

        // Each Todo Belong to one user only ==> Many to One
        public User? User { get; set; }
    }
}
