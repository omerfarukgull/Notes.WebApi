using Notlarim.Business.Abstract;
using Notlarim.Data.Abstract;
using Notlarim.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim.Business.Concrete
{
    public class NoteManager : INoteService
    {
        private INoteRepository _noteRepository;
        public NoteManager(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public void Add(Note note)
        {
            _noteRepository.Create(note);
        }

        public async Task<Note> AddAsync(Note note)
        {
           await _noteRepository.CreateAsync(note);
            return note;
        }

        public async Task<List<Note>> AdminNoteDetails()
        {
            return await _noteRepository.AdminNoteDetails();
        }

        public void Delete(Note note)
        {
            _noteRepository.Delete(note);
        }

        public async Task<Note> DeleteAsync(Note note)
        {
            _noteRepository.Delete(note);
            return note;
        }

        public async Task<List<Note>> GetAll()
        {
            return await _noteRepository.GetList();
        }

        public async Task<List<Note>> GetAll(string search)
        {
            return await _noteRepository.GetSearch(search);
        }

        public async Task<List<Note>> GetAllIsApproved()
        {
            return await _noteRepository.GetList(n => n.IsApproved == true);
        }

        public async Task<List<Note>> GetAllIsNotApproved()
        {
            return await _noteRepository.GetList(n => n.IsApproved == false);
        }

        public async Task<List<Note>> GetByCategory(int categoryId)
        {
            return await _noteRepository.GetList(n => (n.CategoryId == categoryId || categoryId == 0) && n.IsApproved == true);
        }

        public async Task<Note> GetById(int noteId)
        {
            return await _noteRepository.Get(n => n.NoteId == noteId);
        }
        public void Update(Note note)
        {
            _noteRepository.Update(note);
        }

        public async Task<Note> UpdateAsync(Note entityToUpdate, Note note)
        {
            entityToUpdate.Title = note.Title;
            entityToUpdate.Content= note.Content;
            entityToUpdate.NotePdfUrl = note.NotePdfUrl;
            entityToUpdate.IsApproved = note.IsApproved;
            entityToUpdate.CategoryId = note.CategoryId;
            entityToUpdate.MemberId = note.MemberId;
            entityToUpdate.NoteImgUrl = note.NoteImgUrl;

            await _noteRepository.UpdateAsync(entityToUpdate);
            return note;
        }

        public async Task<List<Note>> UserNotes(int userId)
        {
            return await _noteRepository.GetList(n => n.MemberId == userId);
        }

        async Task<Note> INoteService.GetNoteDetails(int noteId)
        {
            return await _noteRepository.GetNoteDetails(noteId);
        }
    }
}
