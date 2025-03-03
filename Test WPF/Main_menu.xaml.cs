using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Test_WPF
{
    /// <summary>
    /// Логика взаимодействия для Main_menu.xaml
    /// </summary>
    public partial class Main_menu : Window
    {
        public Main_menu()
        {
            InitializeComponent();
        }

        public void NewTab(object sender, RoutedEventArgs e)
        {
            TextBlock clickedTextBlock = (TextBlock)sender;
            switch (clickedTextBlock.Text)
            {
                case "Накладная":
                    Invoice document1 = new Invoice(1,1);
                    new Tab(TabsContainer, document1);
                    break;
                case "Отчёт о проверке партии":
                    Batch_report document2 = new Batch_report(3, 1);
                    new Tab(TabsContainer, document2);
                    break;
                case "Акт о браке продукции":
                    Marriage_act document3 = new Marriage_act(4, 1, 1);
                    new Tab(TabsContainer, document3);
                    break;
                case "Итоговый отчёт":
                    Final_report document4 = new Final_report(2, 1);
                    new Tab(TabsContainer, document4);
                    break;
                default: 
                    MessageBox.Show("Ошибка при создании вкладки");
                    break;
            }

        }
        private void DeleteDoc(object sender, RoutedEventArgs e) 
        { 

        }
        private void CreateDoc(object sender, RoutedEventArgs e)
        {

        }

    }
}
