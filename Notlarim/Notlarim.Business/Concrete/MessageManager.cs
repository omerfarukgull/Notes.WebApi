using MyBlog.Entities.Concrete;
using Notlarim.Business.Abstract;
using Notlarim.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim.Business.Concrete
{
    public class MessageManager : IMessageService
    {
        private IMessageRepository _messageRepository;
        public MessageManager(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public void Add(Messages messages)
        {
            _messageRepository.Create(messages);
        }

        public async Task<Messages> AddAsync(Messages messages)
        {
            await _messageRepository.CreateAsync(messages);
            return messages;
        }

        public void Delete(Messages messages)
        {
            _messageRepository.Delete(messages);
        }

        public async Task<List<Messages>> GetAll()
        {
            return await _messageRepository.GetList();
        }

        public async Task<Messages> GetById(int id)
        {
            return await _messageRepository.Get(m => m.MessagesId == id);
        }

        public void MessageStatusUpdate(int id)
        {
            _messageRepository.MessageStatusUpdate(id);
        }

        public void Update(Messages messages)
        {
            _messageRepository.Update(messages);
        }
    }
}
