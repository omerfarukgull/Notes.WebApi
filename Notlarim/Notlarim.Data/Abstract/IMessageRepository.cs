using MyBlog.Entities.Concrete;
using Notlarim.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim.Data.Abstract
{
    public interface IMessageRepository : IRepository<Messages>
    {
        void MessageStatusUpdate(int id);
    }
}
