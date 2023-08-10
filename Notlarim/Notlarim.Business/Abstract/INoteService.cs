using Notlarim.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim.Business.Abstract
{
    public interface INoteService
    {
        Task<List<Note>> GetAll();
        Task<List<Note>> GetAllIsApproved();
        Task<List<Note>> GetAllIsNotApproved();
        Task<List<Note>> UserNotes(int userId);
        Task<List<Note>> GetAll(string search);
        Task<List<Note>> GetByCategory(int categoryId);

        void Add(Note note);
        void Update(Note note);
        void Delete(Note note);

        Task<Note> AddAsync(Note note);
        Task<Note> DeleteAsync(Note note);
        Task<Note> UpdateAsync(Note entityToUpdate, Note note);
   
        Task<Note> GetById(int noteId);
        Task<Note> GetNoteDetails(int noteId);
        Task<List<Note>> AdminNoteDetails();

        //Buraya IRepository yazmış olduğumuz 5 metot yani Crud işlemleri dışında özel metotları yazıyoruz
    }
}
