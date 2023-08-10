using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notlarim.Business.Abstract;
using Notlarim.Entities;

namespace Notlarim.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private INoteService _noteService;
        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var userId = HttpContext.Session.GetInt32("userıd");
            var noteList = await _noteService.UserNotes((int)userId);
            return Ok(noteList);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var noteId = await _noteService.GetById(id);
            if (noteId == null)
            {
                return NotFound();
            }
            return Ok(noteId);
        }
        [HttpPost]
        public async Task<IActionResult> CreateNote(Note note)
        {
            await _noteService.AddAsync(note);
            return CreatedAtAction(nameof(GetProduct), new { id = note.NoteId }, note);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNote(int id,Note note)
        {
            if (id != note.NoteId)
            {
                return BadRequest();
            }
            var entity = await _noteService.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }
            await _noteService.UpdateAsync(entity,note);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            var note = await _noteService.GetById(id);
            if (note == null)
            {
                return NotFound();
            }
            await _noteService.DeleteAsync(note);
            return NoContent();
        }
    }
}
