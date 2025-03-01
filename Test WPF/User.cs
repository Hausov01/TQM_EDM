using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

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
}
