using Microsoft.EntityFrameworkCore;
using Notlarim.Core.DataAccess;
using Notlarim.Core.DataAccess.EntityFramework;
using Notlarim.Data.Abstract;
using Notlarim.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim.Data.Concrete
{
    public class EfNoteRepository : EfCoreGenericRepository<Note>, INoteRepository
    {
        protected NoteContext NoteContext
        {
            get { return (NoteContext)context; }
        }
        public EfNoteRepository(NoteContext context) : base(context) { }
        public async Task<List<Note>> AdminNoteDetails()
        {
            
                var noteDetails = NoteContext.Notes
                    .Include(n => n.Category)
                    .Include(n => n.Member)
                    .ToList();
                return noteDetails;
            
        }

        //public Note GetNoteDetails(int noteId)
        //{
            
        //        var noteDetails = NoteContext.Notes.Where(n => n.NoteId == noteId)
        //            .Include(n => n.Category)
        //            .Include(n => n.Member)
        //            .FirstOrDefault();
        //        return noteDetails;
            
        //}

        //public List<Note> GetSearch(string search)
        //{
           
        //        var searchNote = NoteContext.Notes.Where(n => n.Title.ToLower().Contains(search.ToLower()) || n.Title.ToUpper().Contains(search.ToUpper()));
        //        return searchNote.ToList();

            
        //}


        public async Task<Note> GetNoteDetails(int noteId)
        {
            var noteDetails = NoteContext.Notes.Where(n => n.NoteId == noteId)
                    .Include(n => n.Category)
                    .Include(n => n.Member)
                    .FirstOrDefault();
            return noteDetails;
        }

        async Task<List<Note>> INoteRepository.GetSearch(string search)
        {
            var searchNote = NoteContext.Notes.Where(n => n.Title.ToLower().Contains(search.ToLower()) || n.Title.ToUpper().Contains(search.ToUpper()));
            return searchNote.ToList();
        }
    }
}
