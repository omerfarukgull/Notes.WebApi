using Notlarim.Core.DataAccess;
using Notlarim.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim.Data.Abstract
{
    public interface INoteRepository : IRepository<Note>
    {
        Task<List<Note>> GetSearch(string search);
        Task<Note> GetNoteDetails(int noteId);
        Task<List<Note>> AdminNoteDetails();
        //Buraya IRepository yazmış olduğumuz 5 metot yani Crud işlemleri dışında özel metotları yazıyoruz
    }
}
