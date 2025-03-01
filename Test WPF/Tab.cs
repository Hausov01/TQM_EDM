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
        public Tab(TabControl TabsContainer, Documents document, object selectedItem) // конструктор документа
        {
            db = new AppContext();
            ListBox notesList = new ListBox();

            DockPanel Contetnt = new DockPanel();
            // Приведение типов
            if (document is Marriage_act)
            {
                Marriage_act MAct = (Marriage_act)document; 
                Contetnt = MAct.DocTab();
            }
            if (document is Batch_report)
            {
                Batch_report MAct = (Batch_report)document;
                Contetnt = MAct.DocTab();
            }
            if (document is Accompanying_documents)
            {
                Accompanying_documents MAct = (Accompanying_documents)document;
                Contetnt = MAct.DocTab();
            }
            if (document is Final_report)
            {
                Final_report MAct = (Final_report)document;
                Contetnt = MAct.DocTab();
            }

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

            DockPanel roster = new DockPanel();
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

        

        
        public DockPanel RosterPaste(Documents document, TabControl TabsContainer)
        {
            DockPanel dockpanel = new DockPanel { LastChildFill = true };
            DataGrid datagrid = new DataGrid
            {
                AutoGenerateColumns = true, // Автогенерация столбцов
                IsReadOnly = true, // Запрет редактирования
                SelectionMode = DataGridSelectionMode.Single // Разрешить выбор только одной строки
            };

            // Загрузка данных в зависимости от типа документа
            if (document.Document_type == 1)
            {
                using (var context = new AppContext())
                {
                    var MActs = context.Accompanying_documents.ToList();
                    datagrid.ItemsSource = MActs;
                }
            }
            else if (document.Document_type == 2)
            {
                using (var context = new AppContext())
                {
                    var MActs = context.Final_report.ToList();
                    datagrid.ItemsSource = MActs;
                }
            }
            else if (document.Document_type == 3)
            {
                using (var context = new AppContext())
                {
                    var MActs = context.Batch_report.ToList();
                    datagrid.ItemsSource = MActs;
                }
            }
            else if (document.Document_type == 4)
            {
                using (var context = new AppContext())
                {
                    var MActs = context.Product_marriage_acts.ToList();
                    datagrid.ItemsSource = MActs;
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
                    HandleItemClick(selectedItem, document, TabsContainer); // Вызов метода обработки нажатия
                }
            };

            dockpanel.Children.Add(datagrid);
            return dockpanel;
        }// метод для вывода списка экземпляров документа
        
        private void HandleItemClick(object selectedItem, Documents document, TabControl TabsContainer)// Метод для обработки нажатия на экземпляр документа
        {
            // Здесь можно добавить логику обработки выбранного элемента
            //MessageBox.Show($"Выбран элемент: {selectedItem}");
            switch (document.GetName())
            {
                case "Сопроводительные документы":
                    Accompanying_documents document1 = new Accompanying_documents(1, 1);
                    new Tab(TabsContainer, document, selectedItem);
                    break;
                case "Отчёт о проверке партии":
                    Batch_report document2 = new Batch_report(3, 1);
                    new Tab(TabsContainer, document, selectedItem);
                    break;
                case "Акт о браке продукции":
                    Marriage_act document3 = new Marriage_act(4, 1, 1);
                    new Tab(TabsContainer, document, selectedItem);
                    break;
                case "Итоговый отчёт":
                    Final_report document4 = new Final_report(2, 1);
                    new Tab(TabsContainer, document, selectedItem);
                    break;
                default:
                    MessageBox.Show("Ошибка при создании вкладки");
                    break;
            }
            
        }

        
        /*public DockPanel UsualTab() // метод пример как работать с вкладками
        {
            // Создаем главный контейнер - DockPanel
            DockPanel mainDockPanel = new DockPanel
            {
                LastChildFill = true // Последний элемент заполняет оставшееся пространство
            };

            // Создаем контейнер для текстовых полей (вертикальный StackPanel)
            StackPanel textFieldsPanel = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Margin = new Thickness(10)
            };

            // Первая строка: горизонтальный StackPanel с двумя TextBox
            StackPanel firstRow = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0, 0, 0, 10)
            };

            TextBox textBox1 = new TextBox { Margin = new Thickness(5), Width = 100 };
            TextBox textBox2 = new TextBox { Margin = new Thickness(5), Width = 100 };
            TextBlock textBlock1 = new TextBlock { Margin = new Thickness(3) }; textBlock1.Text = "123";
            TextBlock textBlock2 = new TextBlock { Margin = new Thickness(3) }; textBlock2.Text = "456";

            firstRow.Children.Add(textBlock1);
            firstRow.Children.Add(textBox1);
            firstRow.Children.Add(textBlock2);
            firstRow.Children.Add(textBox2);

            // Вторая строка: горизонтальный StackPanel с двумя TextBox
            StackPanel secondRow = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0, 0, 0, 10)
            };

            TextBox textBox3 = new TextBox { Margin = new Thickness(5), Width = 100 };
            TextBox textBox4 = new TextBox { Margin = new Thickness(5), Width = 100 };

            secondRow.Children.Add(textBox3);
            secondRow.Children.Add(textBox4);

            // Добавляем строки в контейнер текстовых полей
            textFieldsPanel.Children.Add(firstRow);
            textFieldsPanel.Children.Add(secondRow);

            // Добавляем контейнер текстовых полей в DockPanel
            DockPanel.SetDock(textFieldsPanel, Dock.Top); // Привязываем к верхней части
            mainDockPanel.Children.Add(textFieldsPanel);

            // Создаем кнопку и привязываем ее к нижней части DockPanel
            Button button = new Button
            {
                Content = "Кнопка",
                Margin = new Thickness(5),
                Height = 20,
                Width = 100,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom,
                // Выравнивание по левому краю
            };

            DockPanel.SetDock(button, Dock.Bottom); // Привязываем к нижней части
            mainDockPanel.Children.Add(button);

            // Устанавливаем главный DockPanel как содержимое окна
            return mainDockPanel;
        }*/

    }
}
