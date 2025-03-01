using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Test_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AppContext db;
        public MainWindow()
        {
            InitializeComponent();

            db = new AppContext();

        }

        private void Authorization(object sender, RoutedEventArgs e)
        {
            string login = InputLogin.Text.Trim();
            string password = InputPassword.Password.Trim();

            User authUser = null;
            using (AppContext context = new AppContext())
            {
                authUser = db.Users.Where(b => b.login == login && b.Password == password).FirstOrDefault();
            }
            if (authUser != null)
            {
                MessageBox.Show("Авторизовано");
                Main_menu Menu = new Main_menu();
                Menu.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Логин или пароль некорректны");
            }
            /*
            if (login.Length > 5 && login.Length < 18)
            {
                InputLogin.Background = Brushes.Transparent;
                InputLogin.ToolTip = "";
            }
            else
            {
                InputPassword.ToolTip = "";
                InputLogin.ToolTip = "Логин слишком короткий или длинный";
                InputLogin.Background = Brushes.DarkRed;
            }
            if (password.Length > 5 && password.Length < 18)
            {

                InputPassword.Background = Brushes.Transparent;

                User user = new User(login, password, "Имя", "Фамилия", "Отчество", "89065650007", "Должность");
                // db.Users.Add(user);
                // db.SaveChanges();

                using (var context = new AppContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            context.Users.Add(user);
                            context.SaveChanges();
                            transaction.Commit(); // Завершаем транзакцию
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }

                MessageBox.Show("Авторизовано");
            }
            else
            {
                InputPassword.ToolTip = "Пароль слишком короткий или длинный";
                InputPassword.Background = Brushes.DarkRed;
            }
            */
            }
    }
}
