using System.ComponentModel.DataAnnotations;

namespace TasksManager.ViewModel.Projects
{
    public class DeleteProjectRequest
    {
        [Required]
        public byte[] RowVersion { get; set; }
    }
}
