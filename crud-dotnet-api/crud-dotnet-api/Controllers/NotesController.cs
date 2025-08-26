using crud_dotnet_api.CQRS.Commands.CreateNote;
using crud_dotnet_api.CQRS.Commands.DeleteNote;
using crud_dotnet_api.CQRS.Commands.UpdateNote;
using crud_dotnet_api.CQRS.Queries.GetNotesByUser;
using crud_dotnet_api.Data;
using crud_dotnet_api.Model;
using crud_dotnet_api.Model.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace crud_dotnet_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {

        private readonly IMediator _mediator;
        public NotesController( IMediator mediator)
        {
            
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        [Route("AddNote")]
        public async Task<IActionResult> CreateNote([FromBody] NoteDTO noteDTO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _mediator.Send(new CreateNoteCommand(noteDTO));
            return Ok();
        }

        [Authorize]
        [HttpGet]
        [Route("GetNotesByUserId")]
        public async Task<IActionResult> GetNotesByUserId(Guid userId)
        {
            var notes = await _mediator.Send(new GetNotesByUserQuery(userId));
            if (notes == null || !notes.Any())
                return NotFound(new { Message = "No notes found for the given user." });

            return Ok(notes);
        }




        [HttpPut]
        [Route("UpdateNote")]
        public async Task<IActionResult> UpdateNote([FromBody] NoteEditDTO editNoteDTO)
        {
           
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _mediator.Send(new UpdateNoteCommand(editNoteDTO));
                return Ok();
            }

        [HttpDelete]
        [Route("DeleteNoteById")]
        public async Task<IActionResult> DeleteNote(Guid noteId)
        {
            try
            {

                await _mediator.Send(new DeleteNoteCommand(noteId));
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
