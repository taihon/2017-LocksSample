using System;
using System.Collections.Generic;
using System.Text;

namespace TasksManager.Entities
{
    public class ObjectPermission:DomainObject
    {
        public int ObjectId { get; set; }
        public string ObjectType { get; set; }
        public int UserId { get; set; }
        public PermissionType PermissionType { get; set; }
    }
}
