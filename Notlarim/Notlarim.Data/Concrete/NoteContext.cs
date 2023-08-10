using Microsoft.EntityFrameworkCore;
using MyBlog.Entities.Concrete;
using Notlarim.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim.Data.Concrete
{
    public class NoteContext : DbContext
    {
        public NoteContext(DbContextOptions options) : base(options) { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<Note> Notes { get; set; }
       

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=ADMINISTRATOR;Database=MyNotesDb;Trusted_Connection=true;TrustServerCertificate=True");
        //}
    }
}
