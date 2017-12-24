using System.ComponentModel.DataAnnotations;

namespace TasksManager.Entities
{
    public class PermissionType:DomainObject
    {
        [Required]
        public string Name { get; set; }
    }
}