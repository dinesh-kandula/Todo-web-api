using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TodoApi.Models
{
    public enum Gender
    {
        MALE, FEMALE, OTHER
    }
    public class User
    {
        [Key]
        public Guid UserId { get; set; }

        [DisplayName("User Name"), Required, StringLength(60, MinimumLength = 3)]
        public required string Name { get; set; }

        [DataType(DataType.Date), DisplayName("Date Of Birth")]
        public DateTime DOB { get; set; }

        public Gender Gender { get; set; }

        [DisplayName("Profile Picture")]
        public string? Profile { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        // One to One Realationship b/w User and credentials each user have only one credentials details
        // and each credentails details are belong to one user only
        public Credential Credential { get; set; }

        // One to Many ==> One user Many Todo
        public ICollection<Todo> Todo { get; set; }

        // Many to Many ==> Many Users Many FavBook
        public ICollection<FavBook> FavBook { get; set; }
    }
}
