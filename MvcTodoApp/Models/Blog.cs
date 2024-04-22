using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcTodoApp.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        
        [StringLength(60, MinimumLength = 3)]
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [DisplayName("Cover Picture")]
        public string? Cover { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public required string Author { get; set; }
    }
}
