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
    public class MemberManager : IMemberService
    {
        private IMemberRepository _memberRepository;
        public MemberManager(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public void Add(Member member)
        {
            _memberRepository.Create(member);
        }

        public async Task<Member> AddAsync(Member member)
        {
            await _memberRepository.CreateAsync(member);
            return member;
        }

        public void Delete(Member member)
        {
            _memberRepository.Delete(member);
        }

        public async Task<List<Member>> GetAll()
        {
            return await _memberRepository.GetList();
        }

        public async Task<Member> GetById(int id)
        {
            return await _memberRepository.Get(m => m.MemberId == id);
        }

        public Member LoginUser(string mail, string password)
        {
            return _memberRepository.LoginUser(mail, password);
        }

        public void Update(Member member)
        {
            _memberRepository.Update(member);
        }
        public bool UserCheckMail(string mail)
        {
            return _memberRepository.UserCheckMail(mail);
        }
    }
}
