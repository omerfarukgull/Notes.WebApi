using Notlarim.Core.DataAccess;
using Notlarim.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim.Data.Abstract
{
    public interface IMemberRepository : IRepository<Member>
    {
        Member LoginUser(string mail, string password);
        public bool UserCheckMail(string mail);
    }
}
