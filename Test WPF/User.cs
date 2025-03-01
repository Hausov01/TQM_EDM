using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Test_WPF
{
    class User
    {

        //[Key] private string login;
        [Key] public string login { get; set; }
        private string password, name, last_name, patronymic, phone;
        private int position_id;

        //public string Login { get { return login; } set { login = value; } }
        public string Password { get { return password; } set { password = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string Last_name { get { return last_name; } set { last_name = value; } }
        public string Patronymic { get { return patronymic; } set { patronymic = value; } }
        public string Phone { get { return phone; } set { phone = value; } }
        public int Position_id { get { return position_id; } set { position_id = value; } }


        public User() { }

        public User(string login, string password, string name, string last_name, string patronymic, string phone, int position_id)
        {
            this.login = login;
            this.password = password;
            this.name = name;
            this.last_name = last_name;
            this.patronymic = patronymic;
            this.phone = phone;
            this.position_id = position_id;

        }
    }

    class Document_type
    {

        [Key] public int id { get; set; }
        private string name;

        public string Name { get { return name; } set { name = value; } }


        public Document_type() { }

        public Document_type(string name)
        {
            this.name = name;
        }
    }

    class Productions
    {
        [Key] public int Production_UIN { get; set; }
        private int batch_id;
        private int product_type_id;
        private int cost;
        private int main_insert;
        private int additional_insert;
        private int metall_id;
        private int weight;
        private string description_marriage;
        private string short_description_marriage;
        private string product_article;
        private string conclusion;
        public int Batch_id { get { return batch_id; } set { batch_id = value; } }
        public int Product_type_id { get { return product_type_id; } set { product_type_id = value; } }
        public int Cost { get { return cost; } set { cost = value; } }
        public int Main_insert { get { return main_insert; } set { main_insert = value; } }
        public int Additional_insert { get { return additional_insert; } set { additional_insert = value; } }
        public int Metall_id { get { return metall_id; } set { metall_id = value; } }
        public int Weight { get { return weight; } set { weight = value; } }
        public string Description_marriage { get { return description_marriage; } set { description_marriage = value; } }
        public string Short_description_marriage { get { return short_description_marriage; } set { short_description_marriage = value; } }
        public string Product_article { get { return product_article; } set { product_article = value; } }
        public string Conclusion { get { return conclusion; } set { conclusion = value; } }


        public Productions() { }
        public Productions(int batch_id, int product_type_id, int cost, int main_insert, int additional_insert, 
            int metall_id, int weight, string description_marriage, string short_description_marriage, 
            string product_article, string conclusion)
        {
            this.batch_id = batch_id;
            this.product_type_id = product_type_id;
            this.cost = cost;
            this.main_insert = main_insert;
            this.additional_insert = additional_insert;
            this.metall_id = metall_id;
            this.weight = weight;
            this.description_marriage = description_marriage;
            this.short_description_marriage = short_description_marriage;
            this.product_article = product_article;
            this.conclusion = conclusion;
        }
    }
}
