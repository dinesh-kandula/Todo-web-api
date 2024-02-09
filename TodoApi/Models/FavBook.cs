using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class FavBook
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required Guid UserId { get; set; }

        public User User { get; set; }

        [Required]
        public required Guid BookId { get; set; }

        public Book Book { get; set; }
    }
}
