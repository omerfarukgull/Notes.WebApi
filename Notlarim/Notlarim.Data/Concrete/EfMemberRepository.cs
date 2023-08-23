using Microsoft.EntityFrameworkCore;
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
    public class EfMemberRepository : EfCoreGenericRepository<Member>, IMemberRepository
    {
        public EfMemberRepository(NoteContext context) : base(context)
        {
        }

        private NoteContext NoteContext
        {
            get { return (NoteContext)context; }
        }

        public async Task<Member> GetByIdUserProfile(int id)
        {
            var userProfile = await NoteContext.Members.FirstOrDefaultAsync(m => m.MemberId == id);
            return userProfile;
        }

        public Member LoginUser(string mail, string password)
        {

            var loginUser = NoteContext.Members.FirstOrDefault(m => m.Email == mail && m.Password == password);
            return loginUser;

        }
        public bool UserCheckMail(string mail)
        {

            return NoteContext.Members.Any(m => m.Email == mail);

        }
    }
}
