using Notlarim.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim.Business.Abstract
{
    public interface IMemberService
    {
        Task<Member> GetById(int id);
        Task<Member> GetByIdUserProfile(int id);
        Task<List<Member>> GetAll();
        public void Add(Member member);
        Task<Member> AddAsync(Member member);
        public void Delete(Member member);
        public void Update(Member member);
        Member LoginUser(string mail,string password);
        public bool UserCheckMail(string mail);
    }
}
