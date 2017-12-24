using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TasksManager.Entities
{
    public class ObjectPermission:DomainObject
    {
        public int ObjectId { get; set; }
        [Required]
        public string ObjectType { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public PermissionType PermissionType { get; set; }
    }
}
