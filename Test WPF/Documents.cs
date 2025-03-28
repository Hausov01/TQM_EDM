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
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Media;

namespace Test_WPF
{
    internal class Documents
    {
        [Key]
        public int Document_id { get; set; }
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

        public Documents(int Creator)
        {
            //индексация
            this.Document_id = next_id;
            next_id++;
            // автоматическое назначение переменным значений
            //this.document_type = Type;
            this.date_creation = Convert.ToString(DateTime.Now);
            this.date_change = Convert.ToString(DateTime.Now);
            this.Registred = 1;
            this.status = 1;
            this.creator = Creator;
        }

        public string GetName()
        {
            if (this.document_type == 1) { return "Накладная"; }
            else if (this.document_type == 2) { return "Итоговый отчёт"; }
            else if (this.document_type == 3) { return "Отчёт о проверке партии"; }
            else if (this.document_type == 4) { return "Акт о браке продукции"; }
            return "id типа документа некорректен id: " + Convert.ToString(this.document_type);
        }
        public StackPanel ProductPaste()// метод для вывода списка изделий
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
            if (this.GetType() == typeof(Marriage_act))
            {
                using (var context = new AppContext())
                {
                    var DocBody = context.Productions
                        .Select(p => new
                        {
                            product_type_id = p.product_type_id,
                            main_insert = p.main_insert,
                            additional_insert = p.additional_insert,
                            weight = p.weight,
                            metall_id = p.metall_id,
                            is_marriage = p.is_marriage,
                            short_description_marriage = p.short_description_marriage,
                            description_marriage = p.description_marriage,
                        })
                        .ToList();
                    datagrid.ItemsSource = DocBody;
                }
            }
            if (this.GetType() == typeof(Invoice))
            {
                using (var context = new AppContext())
                {
                    var DocBody = context.Productions
                        .Select(p => new
                        {
                            
                        })
                        .ToList();
                    datagrid.ItemsSource = DocBody;
                }
            }
            else 
            {
                using (var context = new AppContext())
                {
                    var DocBody = context.Productions
                        .Select(p => new
                        {
                            batch_id = p.batch_id,
                            product_type_id = p.product_type_id,
                            main_insert = p.main_insert,
                            cost = p.cost,
                            additional_insert = p.additional_insert,
                            weight = p.weight,
                            metall_id = p.metall_id,
                            is_marriage = p.is_marriage,
                        })
                        .ToList();
                    datagrid.ItemsSource = DocBody;
                }
            }
            

            dockpanel.Children.Add(ToolBar);
            dockpanel.Children.Add(datagrid);
            return dockpanel;
        }
        public static T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);
            while (parent != null && !(parent is T))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            return parent as T;
        }
    }

    class Marriage_act : Documents
    {
        private int Production_UIN;
        private string Conclusion;
        public int production_UIN { get; set; }
        public string conclusion { get; set; }
        public Marriage_act(){ }

        public Marriage_act(int Creator, int Production_UIN)
            : base(Creator)
        {
            this.production_UIN = Production_UIN;
            this.Document_type = 4;
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
            List<Marriage_act> data;
            using (var context = new AppContext()) 
            { 
                data = context.Product_marriage_acts.ToList();
            }

            if (data != null && data.Any())
            {
                Production_UIN = data[0].Production_UIN;
                Conclusion = data[0].Conclusion;
            }
            else
            {
                // Обработка случая, когда данные отсутствуют
                MessageBox.Show("Данные не найдены в таблице Product_marriage_acts.");
            }
            
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
            StackPanel roster = new StackPanel();// контейнер для списка изделий
            roster = ProductPaste();

            // строка заголовка
            StackPanel TitleRow = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0, 0, 0, 10)
            };

            StackPanel ToolBar = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(10)
            };

            TextBlock textBlock_Tital = new TextBlock { Margin = new Thickness(3) }; textBlock_Tital.Text = "Акт о браке продукции";
            textBlock_Tital.FontWeight = FontWeights.Bold;
            TitleRow.Children.Add(textBlock_Tital);

            // Первая строка: горизонтальный StackPanel с двумя TextBox
            StackPanel firstRow = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0, 0, 0, 10)
            };

            TextBlock textBlock1 = new TextBlock { Margin = new Thickness(3) }; textBlock1.Text = "Номер партии";
            TextBox textBox1 = new TextBox { Margin = new Thickness(5), Width = 100, Text = Convert.ToString(this.Document_id) };

            firstRow.Children.Add(textBlock1);
            firstRow.Children.Add(textBox1);

            // Вторая строка: заголовок вывода изделий
            StackPanel secondRow = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Margin = new Thickness(0, 0, 0, 10)
            };
            TextBlock textBlock2 = new TextBlock { Margin = new Thickness(3) }; textBlock2.Text = "Список изделий:";
            TextBlock textBlock3 = new TextBlock { Margin = new Thickness(3) }; textBlock3.Text = "Заключение:";
            TextBox textBox2 = new TextBox { Margin = new Thickness(5), Width = 500, Height=150, Text = Convert.ToString(this.Conclusion), HorizontalAlignment = HorizontalAlignment.Left };
            

            secondRow.Children.Add(textBlock2);
            secondRow.Children.Add(roster);
            secondRow.Children.Add(textBlock3);
            secondRow.Children.Add(textBox2);

            // Добавляем строки в контейнер текстовых полей
            textFieldsPanel.Children.Add(TitleRow);
            textFieldsPanel.Children.Add(firstRow);
            textFieldsPanel.Children.Add(secondRow);

            // Добавляем контейнер текстовых полей в DockPanel
            DockPanel.SetDock(textFieldsPanel, Dock.Top); // Привязываем к верхней части
            mainDockPanel.Children.Add(textFieldsPanel);

            StackPanel Basement = new StackPanel// нижняя часть вкладки
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(5)
            };

            Button SaveDoc = new Button//кнопка сохранения 
            { 
                Content = "Сохранить", 
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom,
            };
            Basement.Children.Add(SaveDoc);

            SaveDoc.Click += (sender, e) => { };


            DockPanel.SetDock(SaveDoc, Dock.Bottom); // Привязываем к нижней части
            mainDockPanel.Children.Add(Basement);

            // Устанавливаем главный DockPanel как содержимое окна
            return mainDockPanel;
        }
    }

    class Batch_report : Documents
    {
        bool GIIS_Accepted = false;
        bool Has_mairrage = false;
        public int Batch_id { get; set; }
        public string Supplier = @"АО 'Смоленский ювелирный завод'";

        public Batch_report(int Creator) : base(Creator) 
        { 
            this.Document_type = 3; 
            this.Batch_id = 1;
        }
        public Batch_report() {}
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
                    return new Invoice(1);
                case 6:
                    return new Marriage_act(1, 1);
                case 7:
                    return new Final_report(1);
                default: 
                    return null;
            }
        }

        public DockPanel DocTab() // метод пример как работать с вкладками
        {
            List<Marriage_act> data;
            using (var context = new AppContext())
            {
                data = context.Product_marriage_acts.ToList();
            }


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
            StackPanel roster = new StackPanel();// контейнер для списка изделий
            roster = ProductPaste();

            // строка заголовка
            StackPanel TitleRow = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0, 0, 0, 10)
            };

            StackPanel ToolBar = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(10)
            };

            TextBlock textBlock_Tital = new TextBlock { Margin = new Thickness(3) }; textBlock_Tital.Text = "Отчёт о проверке партии";
            textBlock_Tital.FontWeight = FontWeights.Bold;
            TitleRow.Children.Add(textBlock_Tital);

            // Первая строка: горизонтальный StackPanel
            StackPanel firstRow = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0, 0, 0, 10)
            };

            TextBlock textBlock1 = new TextBlock { Margin = new Thickness(3) }; textBlock1.Text = "Номер партии";
            TextBox textBox1 = new TextBox { Margin = new Thickness(5), Width = 60, Text = Convert.ToString(this.Batch_id) };
            TextBlock textBlock12 = new TextBlock { Margin = new Thickness(3) }; textBlock12.Text = "Наименование поставщика";
            TextBox textBox12 = new TextBox { Margin = new Thickness(5), Width = 200, Text = Convert.ToString(Supplier) };
            TextBlock textBlock13 = new TextBlock { Margin = new Thickness(3) }; textBlock13.Text = "Номер накладной";
            TextBox textBox13 = new TextBox { Margin = new Thickness(5), Width = 60, Text = Convert.ToString("3") };


            firstRow.Children.Add(textBlock1);
            firstRow.Children.Add(textBox1);
            firstRow.Children.Add(textBlock12);
            firstRow.Children.Add(textBox12);
            firstRow.Children.Add(textBlock13);
            firstRow.Children.Add(textBox13);

            StackPanel ChoiceRow = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0, 0, 0, 10)
            };

            Button buttonBatch = new Button
            {
                Content = "Выбрать партию",
                Margin = new Thickness(5),
                Height = 20,
                Width = 100,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom,
                // Выравнивание по левому краю
            };

            Button buttonSupplier = new Button
            {
                Content = "Выбрать поставщика",
                Margin = new Thickness(5),
                Height = 20,
                Width = 130,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom,
                // Выравнивание по левому краю
            };

            Button buttonInvoice = new Button
            {
                Content = "Выбрать накладную",
                Margin = new Thickness(5),
                Height = 20,
                Width = 140,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom,
                // Выравнивание по левому краю
            };
            buttonInvoice.Click += (sender, e) =>
            {
                Invoice document1 = new Invoice(1);
                new Tab(FindVisualParent<TabControl>(buttonInvoice), document1);
            };

            ChoiceRow.Children.Add(buttonBatch);
            ChoiceRow.Children.Add(buttonSupplier);
            ChoiceRow.Children.Add(buttonInvoice);

            // Вторая строка: заголовок вывода изделий
            StackPanel secondRow = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Margin = new Thickness(0, 0, 0, 10)
            };
            TextBlock textBlock2 = new TextBlock { Margin = new Thickness(3) }; textBlock2.Text = "Список изделий:";
            TextBlock textBlock3 = new TextBlock { Margin = new Thickness(3) }; textBlock3.Text = "Заключение:";
            TextBox textBox2 = new TextBox { Margin = new Thickness(5), Width = 500, Height = 150, Text = Convert.ToString(""), HorizontalAlignment = HorizontalAlignment.Left };


            secondRow.Children.Add(textBlock2);
            secondRow.Children.Add(roster);
            secondRow.Children.Add(textBlock3);
            secondRow.Children.Add(textBox2);

            // Добавляем строки в контейнер текстовых полей
            textFieldsPanel.Children.Add(TitleRow);
            textFieldsPanel.Children.Add(firstRow);
            textFieldsPanel.Children.Add(ChoiceRow);
            textFieldsPanel.Children.Add(secondRow);

            // Добавляем контейнер текстовых полей в DockPanel
            DockPanel.SetDock(textFieldsPanel, Dock.Top); // Привязываем к верхней части
            mainDockPanel.Children.Add(textFieldsPanel);

            StackPanel Basement = new StackPanel// нижняя часть вкладки
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(5)
            };

            Button SaveDoc = new Button//кнопка сохранения 
            {
                Content = "Сохранить",
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom,
            };
            Basement.Children.Add(SaveDoc);

            SaveDoc.Click += (sender, e) => { };


            DockPanel.SetDock(SaveDoc, Dock.Bottom); // Привязываем к нижней части
            mainDockPanel.Children.Add(Basement);

            // Устанавливаем главный DockPanel как содержимое окна
            return mainDockPanel;
        }
    }

    [Table("Invoice")]
    class Invoice : Documents
    {
        public Invoice(int Creator) : base(Creator) 
        {
            this.Document_type = 1;
        }

        public Invoice() { }

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
            List<Marriage_act> data;
            using (var context = new AppContext())
            {
                data = context.Product_marriage_acts.ToList();
            }

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
            StackPanel roster = new StackPanel();// контейнер для списка изделий
            roster = ProductPaste();

            // строка заголовка
            StackPanel TitleRow = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0, 0, 0, 10)
            };

            StackPanel ToolBar = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(10)
            };

            TextBlock textBlock_Tital = new TextBlock { Margin = new Thickness(3) }; textBlock_Tital.Text = "Наклаадная";
            textBlock_Tital.FontWeight = FontWeights.Bold;
            TitleRow.Children.Add(textBlock_Tital);

            // Первая строка: горизонтальный StackPanel с двумя TextBox
            StackPanel firstRow = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0, 0, 0, 10)
            };

            TextBlock textBlock1 = new TextBlock { Margin = new Thickness(3) }; textBlock1.Text = "Номер партии";
            TextBox textBox1 = new TextBox { Margin = new Thickness(5), Width = 60, Text = Convert.ToString("") };
            TextBlock textBlock12 = new TextBlock { Margin = new Thickness(3) }; textBlock12.Text = "Наименование поставщика";
            TextBox textBox12 = new TextBox { Margin = new Thickness(5), Width = 200, Text = Convert.ToString("") };
            TextBlock textBlock13 = new TextBlock { Margin = new Thickness(3) }; textBlock13.Text = "Номер накладной";
            TextBox textBox13 = new TextBox { Margin = new Thickness(5), Width = 60, Text = Convert.ToString("") };


            firstRow.Children.Add(textBlock1);
            firstRow.Children.Add(textBox1);
            firstRow.Children.Add(textBlock12);
            firstRow.Children.Add(textBox12);
            firstRow.Children.Add(textBlock13);
            firstRow.Children.Add(textBox13);

            // Вторая строка: заголовок вывода изделий
            StackPanel secondRow = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Margin = new Thickness(0, 0, 0, 10)
            };
            TextBlock textBlock2 = new TextBlock { Margin = new Thickness(3) }; textBlock2.Text = "Список изделий:";
            TextBlock textBlock3 = new TextBlock { Margin = new Thickness(3) }; textBlock3.Text = "Заключение:";
            TextBox textBox2 = new TextBox { Margin = new Thickness(5), Width = 500, Height = 150, Text = Convert.ToString(""), HorizontalAlignment = HorizontalAlignment.Left };


            secondRow.Children.Add(textBlock2);
            secondRow.Children.Add(roster);
            secondRow.Children.Add(textBlock3);
            secondRow.Children.Add(textBox2);

            // Добавляем строки в контейнер текстовых полей
            textFieldsPanel.Children.Add(TitleRow);
            textFieldsPanel.Children.Add(firstRow);
            textFieldsPanel.Children.Add(secondRow);

            // Добавляем контейнер текстовых полей в DockPanel
            DockPanel.SetDock(textFieldsPanel, Dock.Top); // Привязываем к верхней части
            mainDockPanel.Children.Add(textFieldsPanel);

            StackPanel Basement = new StackPanel// нижняя часть вкладки
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(5)
            };

            Button SaveDoc = new Button//кнопка сохранения 
            {
                Content = "Сохранить",
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom,
            };
            Basement.Children.Add(SaveDoc);

            SaveDoc.Click += (sender, e) => { };


            DockPanel.SetDock(SaveDoc, Dock.Bottom); // Привязываем к нижней части
            mainDockPanel.Children.Add(Basement);

            // Устанавливаем главный DockPanel как содержимое окна
            return mainDockPanel;
        }
    }

    class Final_report : Documents
    {
        public Final_report(int Creator) : base(Creator) 
        {
            this.Document_type = 2; 
        }

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

            //строка заголовка
            StackPanel TitleRow = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0, 0, 0, 10)
            };
            TextBlock textBlock_Tital = new TextBlock { Margin = new Thickness(3) }; textBlock_Tital.Text = "Итоговый отчёт";
            textBlock_Tital.FontWeight = FontWeights.Bold;
            TitleRow.Children.Add(textBlock_Tital);

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
            textFieldsPanel.Children.Add(TitleRow);
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
