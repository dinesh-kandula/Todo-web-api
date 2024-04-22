using TodoModels.Models;

namespace MvcTodoApp.Models
{
    public class Custom
    {
        public required User User { get; set; }

        public required Credential Credential { get; set; }
    }
}
