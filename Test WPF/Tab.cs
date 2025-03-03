using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Data.SQLite;
using System.Xml.Linq;
using System.Windows;
using System.Windows.Media;

namespace Test_WPF
{
    internal class Tab
    {
        AppContext db;
        public Tab(TabControl TabsContainer, Object document, string something) // конструктор нового документа
        {
            db = new AppContext();
            ListBox notesList = new ListBox();
            var tabText = new TextBlock();

            DockPanel Contetnt = new DockPanel();
            // Приведение типов
            if (document is Marriage_act)
            {
                Marriage_act DocBody = (Marriage_act)document; 
                Contetnt = DocBody.DocTab();
                tabText = new TextBlock { Text = "Акт о браке продукции"};// Текст вкладки
            }
            if (document is Batch_report)
            {
                Batch_report DocBody = (Batch_report)document;
                Contetnt = DocBody.DocTab();
                tabText = new TextBlock { Text = "Отчёт о проверке партии" };// Текст вкладки
            }
            if (document is Invoice)
            {
                Invoice DocBody = (Invoice)document;
                Contetnt = DocBody.DocTab();
                tabText = new TextBlock { Text = "Накладная" };// Текст вкладки
            }
            if (document is Final_report)
            {
                Final_report DocBody = (Final_report)document;
                Contetnt = DocBody.DocTab();
                tabText = new TextBlock { Text = "Итоговый отчёт" };// Текст вкладки
            }

            // Создаем кастомный заголовок вкладки
            var headerPanel = new StackPanel { Orientation = Orientation.Horizontal };

            // Кнопка закрытия
            var closeButton = new Button
            {
                Content = "X",
                Width = 15,
                Height = 15,
                Margin = new Thickness(3, 0, 0, 0),
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent,
                Padding = new Thickness(0),
                Cursor = Cursors.Hand
            };

            // Добавляем элементы в заголовок
            headerPanel.Children.Add(tabText);
            headerPanel.Children.Add(closeButton);

            // Создаем вкладку
            var tabItem = new TabItem
            {
                Header = headerPanel, // Устанавливаем кастомный заголовок
                Content = Contetnt      // Устанавливаем содержимое вкладки
            };
            
            // Добавляем обработчик события для кнопки закрытия
            closeButton.Click += (sender, e) =>
            {
                TabsContainer.Items.Remove(tabItem); // Удаляем вкладку при нажатии на кнопку
            };

            TabsContainer.Items.Add(tabItem);// Добавляем вкладку в TabControl
            TabsContainer.SelectedItem = tabItem;// делаем вкладку активной
        }

        public Tab(TabControl TabsContainer, Documents document)// конструктор с списком
        {
            db = new AppContext();
            ListBox notesList = new ListBox();

            StackPanel roster = new StackPanel();
            roster = RosterPaste(document, TabsContainer);

            // Создаем кастомный заголовок вкладки
            var headerPanel = new StackPanel { Orientation = Orientation.Horizontal };

            // Текст вкладки
            var tabText = new TextBlock { Text = document.GetName() };

            // Кнопка закрытия
            var closeButton = new Button
            {
                Content = "X",
                Width = 15,
                Height = 15,
                Margin = new Thickness(3, 0, 0, 0),
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent,
                Padding = new Thickness(0),
                Cursor = Cursors.Hand
            };

            // Добавляем элементы в заголовок
            headerPanel.Children.Add(tabText);
            headerPanel.Children.Add(closeButton);

            // Создаем вкладку
            var tabItem = new TabItem
            {
                Header = headerPanel, // Устанавливаем кастомный заголовок
                Content = roster      // Устанавливаем содержимое вкладки
            };

            // Добавляем обработчик события для кнопки закрытия
            closeButton.Click += (sender, e) =>
            {
                TabsContainer.Items.Remove(tabItem); // Удаляем вкладку при нажатии на кнопку
            };

            // Добавляем вкладку в TabControl
            TabsContainer.Items.Add(tabItem);
            TabsContainer.SelectedItem = tabItem;// делаем вкладку активной
        }

        

        
        public StackPanel RosterPaste(Documents document, TabControl TabsContainer)// метод для вывода списка экземпляров документа
        {
            StackPanel dockpanel = new StackPanel 
            {
                CanVerticallyScroll = true,
            };

            DataGrid datagrid = new DataGrid
            {
                AutoGenerateColumns = true, // Автогенерация столбцов
                IsReadOnly = true, // Запрет редактирования
                SelectionMode = DataGridSelectionMode.Single, // Разрешить выбор только одной строки
                HorizontalAlignment = HorizontalAlignment.Stretch, // Растягиваем по горизонтали
                VerticalAlignment = VerticalAlignment.Stretch,
            };

            StackPanel ToolBar = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(5)
            };

            Button CreateDoc = new Button { Content = "Создать", Margin = new Thickness(10) };
            Button DeleteDoc = new Button { Content = "Удалить", Margin = new Thickness(10) };
            ToolBar.Children.Add(CreateDoc);
            ToolBar.Children.Add(DeleteDoc);

            // Загрузка данных в зависимости от типа документа
            if (document.Document_type == 1)
            {
                using (var context = new AppContext())
                {
                    var DocBody = context.Invoice.ToList();
                    datagrid.ItemsSource = DocBody;
                }
            }
            else if (document.Document_type == 2)
            {
                using (var context = new AppContext())
                {
                    var DocBody = context.Final_report.ToList();
                    datagrid.ItemsSource = DocBody;
                }
            }
            else if (document.Document_type == 3)
            {
                using (var context = new AppContext())
                {
                    var DocBody = context.Batch_report.ToList();
                    datagrid.ItemsSource = DocBody;
                }
            }
            else if (document.Document_type == 4)
            {
                using (var context = new AppContext())
                {
                    var DocBody = context.Product_marriage_acts.ToList();
                    datagrid.ItemsSource = DocBody;
                }
            }
            else
            {
                MessageBox.Show("Ошибка открытия вкладки");
            }


            // Добавляем обработчик события выбора строки
            datagrid.SelectionChanged += (sender, e) =>
            {
                var selectedItem = datagrid.SelectedItem;
                if (selectedItem != null)
                {
                    new Tab(TabsContainer, selectedItem, "something"); //Открываем вкладку с данными документа
                    datagrid.SelectedItem = null;
                }
            };

            CreateDoc.Click += (sender, e) =>
            {
                switch (document.GetName())
                {
                    case "Накладная":
                        Invoice document1 = new Invoice(1, 1);
                        new Tab(TabsContainer, document, "something");
                        break;
                    case "Отчёт о проверке партии":
                        Batch_report document2 = new Batch_report(3, 1);
                        new Tab(TabsContainer, document, "something");
                        break;
                    case "Акт о браке продукции":
                        Marriage_act document3 = new Marriage_act(4, 1, 1);
                        new Tab(TabsContainer, document, "something");
                        break;
                    case "Итоговый отчёт":
                        Final_report document4 = new Final_report(2, 1);
                        new Tab(TabsContainer, document, "something");
                        break;
                    default:
                        MessageBox.Show("Ошибка при создании вкладки");
                        break;
                }
            };
            
            dockpanel.Children.Add(ToolBar);
            dockpanel.Children.Add(datagrid);
            return dockpanel;
        }
    }
}
