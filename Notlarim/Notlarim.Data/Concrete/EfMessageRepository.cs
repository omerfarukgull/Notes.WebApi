using Microsoft.EntityFrameworkCore;
using MyBlog.Entities.Concrete;
using Notlarim.Core.DataAccess.EntityFramework;
using Notlarim.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim.Data.Concrete
{
    public class EfMessageRepository : EfCoreGenericRepository<Messages>, IMessageRepository
    {
        protected NoteContext NoteContext
        {
            get { return (NoteContext)context; }
        }
        public EfMessageRepository(NoteContext context) : base(context) { }
        public void MessageStatusUpdate(int id)
        {

            var cmd = "update Messages set MessageStatus=1 where MessagesId=@p0 ";
            NoteContext.Database.ExecuteSqlRaw(cmd, id);

        }
    }
}
