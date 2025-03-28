using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Test_WPF
{
    internal class AppContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Document_type> Document_types { get; set; }
        public DbSet<Marriage_act> Product_marriage_acts { get; set; }
        public DbSet<Batch_report> Batch_report { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<Final_report> Final_report { get; set; }
        public DbSet<Productions> Productions { get; set; }

        public AppContext() : base("DefaultConnection") { }



    }   
    class Productions
    {
        [Key]
        public int Production_UIN { get; set; }
        private int Batch_id, Product_type_id, Cost, Main_insert, Additional_insert, Metall_id, Weight, Is_marriage;
        private string Description_marriage, Short_description_marriage, Product_article, Conclusion;

        public int batch_id { get { return Batch_id; } set { Batch_id = value; } }
        public int product_type_id { get { return Product_type_id; } set { Product_type_id = value; } }
        public int cost { get { return Cost; } set { Cost = value; } }
        public int main_insert { get { return Main_insert; } set { Main_insert = value; } }
        public int additional_insert { get { return Additional_insert; } set { Additional_insert = value; } }
        public int weight { get { return Weight; } set { Weight = value; } }
        public int is_marriage { get { return Is_marriage; } set { Is_marriage = value; } }
        public int metall_id { get { return Metall_id; } set { Metall_id = value; } }
        public string description_marriage { get { return Description_marriage; } set { Description_marriage = value; } }
        public string short_description_marriage { get { return Short_description_marriage; } set { Short_description_marriage = value; } }
        //public string product_article { get { return Product_article; } set { Product_article = value; } } // TODO починить вывод
        public string conclusion { get { return Conclusion; } set { Conclusion = value; } }

        public int production_UIN { get; set; }
    }
    
}
