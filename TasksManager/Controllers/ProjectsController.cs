using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TasksManager.DataAccess.Projects;
using TasksManager.ViewModel;
using TasksManager.ViewModel.Projects;

namespace TasksManager.Controllers
{
    [Route("api/[controller]")]
    public class ProjectsController : Controller
    {
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ListResponse<ProjectResponse>))]
        public async Task<IActionResult> GetProjectsListAsync(ProjectFilter filter, ListOptions options, [FromServices]IProjectsListQuery query)
        {
            var response = await query.RunAsync(filter, options);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ProjectResponse))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateProjectAsync([FromBody]CreateProjectRequest request, [FromServices]ICreateProjectCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ProjectResponse response = await command.ExecuteAsync(request);
            return Ok(response); // This is wrong, it should return 401
        }

        [HttpGet("{projectId}")]
        [ProducesResponseType(200, Type = typeof(ProjectResponse))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetProjectAsync(int projectId, [FromServices]IProjectQuery query)
        {
            ProjectResponse response = await query.RunAsync(projectId);
            return response == null
                ? (IActionResult)NotFound()
                : Ok(response);
        }

        [HttpPut("{projectId}")]
        [ProducesResponseType(200, Type = typeof(ProjectResponse))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateProjectAsync(int projectId, [FromBody]UpdateProjectRequest request, [FromServices]IUpdateProjectCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await command.ExecuteAsync(projectId, request);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPut("{projectId}/lock")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> LockProjectAsync(int projectId, [FromServices]ILockProjectCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = 1; // From identity
            var response = await command.ExecuteAsync(projectId, userId);
            if (!response)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPut("{projectId}/unlock")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UnlockProjectAsync(int projectId, [FromServices]IUnlockProjectCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = 1; // From identity
            await command.ExecuteAsync(projectId, userId);

            return Ok();
        }

        [HttpDelete("{projectId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteProjectAsync(int projectId, [FromBody]DeleteProjectRequest request, [FromServices]IDeleteProjectCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await command.ExecuteAsync(projectId, request);
            if (!response)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
