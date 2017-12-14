using System.ComponentModel.DataAnnotations;

namespace TasksManager.Entities
{
    public class User : DomainObject
    {
        [Required]
        [MaxLength(64)]
        public string Login { get; set; }
    }
}
