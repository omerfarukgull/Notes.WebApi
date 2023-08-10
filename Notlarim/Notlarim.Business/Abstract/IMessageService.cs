using MyBlog.Entities.Concrete;
using Notlarim.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim.Business.Abstract
{
    public interface IMessageService
    {
        Task<List<Messages>> GetAll();
        void Add(Messages messages);
        Task<Messages> AddAsync(Messages messages);
        void Update(Messages messages);
        void Delete(Messages messages);
        Task<Messages> GetById(int id);
        void MessageStatusUpdate(int id);
    }
}
