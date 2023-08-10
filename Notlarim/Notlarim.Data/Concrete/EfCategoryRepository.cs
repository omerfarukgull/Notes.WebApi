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
    public class EfCategoryRepository : EfCoreGenericRepository<Category>, ICategoryRepository
    {
        protected NoteContext NoteContext
        {
            get { return (NoteContext)context; }
        }
        public EfCategoryRepository(NoteContext context) : base(context) { }
    }
}
