using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Test_WPF
{
    internal class AppContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Document_type> Document_types { get; set; }
        public DbSet<Marriage_act> Product_marriage_acts { get; set; }
        public DbSet<Batch_report> Batch_report { get; set; }
        public DbSet<Accompanying_documents> Accompanying_documents { get; set; }
        public DbSet<Final_report> Final_report { get; set; }
        public DbSet<Productions> Productions { get; set; }

        public AppContext() : base("DefaultConnection") { }
    }   

}
