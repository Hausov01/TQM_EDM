using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Xml.Linq;

namespace Test_WPF
{
    internal class Documents
    {
        [Key]
        public int document_id { get; set; }
        private int document_type, status, creator, registred;
        private string date_change, date_creation;

        public int Document_type { get { return document_type; } set { document_type = value; } }
        public string Date_creation { get { return date_creation; } set { date_creation = value; } }
        public string Date_change { get { return date_change; } set { date_change = value; } }
        public int Registred { get { return registred; } set { registred = value; } }
        public int Status { get { return status; } set { status = value; } }
        public int Creator { get { return creator; } set { creator = value; } }

        int next_id;

        public Documents()
        {

        }

        public Documents(int Type, int Creator)
        {
            //индексация
            this.document_id = next_id;
            next_id++;
            // автоматическое назначение переменным значений
            this.document_type = Type;
            this.date_creation = Convert.ToString(DateTime.Now);
            this.date_change = Convert.ToString(DateTime.Now);
            this.Registred = 1;
            this.status = 1;
            this.creator = Creator;
        }

        public string GetName()
        {
            if (this.document_type == 1){ return "Сопроводительные документы";}
            else if (this.document_type == 2){ return "Итоговый отчёт"; }
            else if (this.document_type == 3) { return "Отчёт о проверке партии"; }
            else if (this.document_type == 4) { return "Акт о браке продукции"; }
            return "id типа документа некорректен";
        }

    }

    class Marriage_act : Documents
    {
        private int Production_UIN;
        private string Conclusion;
        public int production_UIN { get; set; }
        public string conclusion { get; set; }
        public Marriage_act(){ }

        public Marriage_act(int Type, int Creator, int Production_UIN)
            : base(Type, Creator)
        {
            this.production_UIN = Production_UIN;

        }
        public void Action(int Action_id)
        {
            switch (Action_id)
            {
                case 1:
                    Status = 2;
                    break;
                case 2:
                    Status = 3;
                    break;
                case 3:
                    Status = 1;
                    break;
                case 4:
                    Status = 8;
                    break;
            }
        }

        public DockPanel DocTab() // метод пример как работать с вкладками
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
        }
    }


    class Batch_report : Documents
    {
        bool GIIS_Accepted = false;
        bool Has_mairrage = false;

        public Batch_report(int Type, int Creator) : base(Type, Creator) {}
        public Batch_report() { }
        public Documents Action(int Action_id)
        {
            switch (Action_id)
            {
                case 1:
                    Status = 4;
                    return null;
                case 2:
                    Status = 2;
                    return null;
                case 3:
                    Status = 3;
                    return null;
                case 4:
                    Status = 1;
                    return null;
                case 5:
                    return new Accompanying_documents(1, 1);
                case 6:
                    return new Marriage_act(1, 1, 1);
                case 7:
                    return new Final_report(1, 1);
                default: 
                    return null;
            }
        }

        public DockPanel DocTab() // метод пример как работать с вкладками
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
        }
    }

    class Accompanying_documents : Documents
    {
        public Accompanying_documents(int Type, int Creator) : base(Type, Creator) {}

        public Accompanying_documents() { }

        public void Action(int Action_id)
        {
            switch (Action_id)
            {
                case 1:
                    Status = 2;
                    break;
                case 2:
                    Status = 3;
                    break;
                case 3:
                    Status = 1;
                    break;
            }
        }
        public DockPanel DocTab() // метод пример как работать с вкладками
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
        }
    }


    class Final_report : Documents
    {
        public Final_report(int Type, int Creator) : base(Type, Creator) { }

        public Final_report() { }

        public void Action(int Action_id)
        {
            switch (Action_id)
            {
                case 1:
                    Status = 2;
                    break;
                case 2:
                    Status = 3;
                    break;
                case 3:
                    Status = 7;
                    break;
                case 4:
                    Status = 1;
                    break;
            }

        }
        public DockPanel DocTab() // метод пример как работать с вкладками
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
        }
    }
}
