using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;
using System.Xml.Serialization;
using System.Diagnostics;
using Microsoft.UI.Xaml.Controls;
using OfficeOpenXml;
using Windows.UI.Notifications;
using System.Xml.Linq;


// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace BrainDead
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>

    public class Kurs
    {
        public string Name_Group { get; set; }
        public List<string> name_student;

    }

    public sealed partial class Group : Page
    {
        TextBox name;
        StorageFolder folder;
        StorageFile[] files = new StorageFile[4];
        XmlSerializer[] serializers = new XmlSerializer[4];
        string[] stroka = new string[4];
        List<Kurs> kurs;

        public Group()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            this.InitializeComponent();
        }




        private async void Add_Group_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog content = new ContentDialog();
            name = new TextBox();
            content.Content = name;
            content.Title = "Название Группы";
            content.CloseButtonText = "Выйти";
            content.PrimaryButtonText = "Добаваить Группу";
            content.PrimaryButtonClick += async (es, s) =>
            {
                var deb = Pivo.SelectedItem as TabViewItem;


                switch (deb.Header)
                {
                    case "1 Курс":
                        {

                            group.Items.Add(name.Text);
                            Kurs kurs_class = new Kurs();
                            kurs_class.name_student = new List<string>();
                            kurs_class.Name_Group = name.Text.Trim();
                            kurs.Add(kurs_class);

                            using (var stream = await files[0].OpenStreamForWriteAsync())
                            {
                                serializers[0].Serialize(stream, kurs);
                            }
                        }
                        break;
                    case "2 Курс":
                        {
                            group.Items.Add(name.Text);
                            Kurs kurs_class = new Kurs();
                            kurs_class.name_student = new List<string>();
                            kurs_class.Name_Group = name.Text.Trim();
                            kurs.Add(kurs_class);

                            using (var stream = await files[1].OpenStreamForWriteAsync())
                            {
                                serializers[1].Serialize(stream, kurs);
                            }
                        }
                        break;
                    case "3 Курс":
                        {
                            group.Items.Add(name.Text);
                            Kurs kurs_class = new Kurs();
                            kurs_class.name_student = new List<string>();
                            kurs_class.Name_Group = name.Text.Trim();
                            kurs.Add(kurs_class);

                            using (var stream = await files[2].OpenStreamForWriteAsync())
                            {
                                serializers[2].Serialize(stream, kurs);
                            }

                        }
                        break;
                    case "4 Курс":
                        {
                            group.Items.Add(name.Text);
                            Kurs kurs_class = new Kurs();
                            kurs_class.name_student = new List<string>();
                            kurs_class.Name_Group = name.Text.Trim();
                            kurs.Add(kurs_class);

                            using (var stream = await files[3].OpenStreamForWriteAsync())
                            {
                                serializers[3].Serialize(stream, kurs);
                            }
                        }
                        break;
                    default:
                        {

                        }
                        break;
                }
            };
            await content.ShowAsync();
        }

        private async void Delate_Group_Click(object sender, RoutedEventArgs e)
        {

            var deb = Pivo.SelectedItem as TabViewItem;


            switch (deb.Header)
            {
                case "1 Курс":
                    {
                        if (group.SelectedIndex != -1)
                        {

                            files[0] = await folder.CreateFileAsync("Kurs1.xml", CreationCollisionOption.ReplaceExisting);

                            kurs.RemoveAt(group.SelectedIndex);
                            group.Items.RemoveAt(group.SelectedIndex);

                            using (var stream = await files[0].OpenStreamForWriteAsync())
                            {
                                serializers[0].Serialize(stream, kurs);
                            }
                        }
                    }
                    break;
                case "2 Курс":
                    {
                        if (group.SelectedIndex != -1)
                        {

                            files[1] = await folder.CreateFileAsync("Kurs2.xml", CreationCollisionOption.ReplaceExisting);

                            kurs.RemoveAt(group.SelectedIndex);
                            group.Items.RemoveAt(group.SelectedIndex);

                            using (var stream = await files[1].OpenStreamForWriteAsync())
                            {
                                serializers[1].Serialize(stream, kurs);
                            }
                        }
                    }
                    break;
                case "3 Курс":
                    {
                        if (group.SelectedIndex != -1)
                        {

                            files[2] = await folder.CreateFileAsync("Kurs3.xml", CreationCollisionOption.ReplaceExisting);

                            kurs.RemoveAt(group.SelectedIndex);
                            group.Items.RemoveAt(group.SelectedIndex);

                            using (var stream = await files[2].OpenStreamForWriteAsync())
                            {
                                serializers[2].Serialize(stream, kurs);
                            }
                        }

                    }
                    break;
                case "4 Курс":
                    {
                        if (group.SelectedIndex != -1)
                        {

                            files[3] = await folder.CreateFileAsync("Kurs4.xml", CreationCollisionOption.ReplaceExisting);

                            kurs.RemoveAt(group.SelectedIndex);
                            group.Items.RemoveAt(group.SelectedIndex);

                            using (var stream = await files[3].OpenStreamForWriteAsync())
                            {
                                serializers[3].Serialize(stream, kurs);
                            }
                        }
                    }
                    break;
                default:
                    {

                    }
                    break;
            }
        }

        private async void Add_student_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog content = new ContentDialog();
            TextBox[] textBoxes = new TextBox[3];
            StackPanel stack = new StackPanel();
            stack.Orientation = Orientation.Vertical;

            for (int i = 0; i < 3; i++)
            {
                textBoxes[i] = new TextBox();
                textBoxes[i].Margin = new Thickness(10, 10, 10, 10);
                stack.Children.Add(textBoxes[i]);
            }

            textBoxes[0].PlaceholderText = "Фамилия";
            textBoxes[1].PlaceholderText = "Имя";
            textBoxes[2].PlaceholderText = "Отчество";

            content.Content = stack;
            content.Title = "Добавление студента";
            content.CloseButtonText = "Выйти";
            content.PrimaryButtonText = "Добаваить студента";
            content.PrimaryButtonClick += async (es, s) =>
            {


                var deb = Pivo.SelectedItem as TabViewItem;


                switch (deb.Header)
                {
                    case "1 Курс":
                        {
                            if (group.SelectedIndex != -1)
                            {

                                kurs[group.SelectedIndex].name_student.Add($"{textBoxes[0].Text.ToUpper().Trim()} {textBoxes[1].Text.ToUpper().Trim()} {textBoxes[2].Text.ToUpper().Trim()}");
                                Kurs1.Items.Clear();
                                foreach (var item in kurs[group.SelectedIndex].name_student)
                                {
                                    Kurs1.Items.Add(item);
                                }
                                files[0] = await folder.CreateFileAsync("Kurs1.xml", CreationCollisionOption.OpenIfExists);
                                using (var stream = await files[0].OpenStreamForWriteAsync())
                                {
                                    serializers[0].Serialize(stream, kurs);
                                }
                            }
                        }
                        break;
                    case "2 Курс":
                        {
                            if (group.SelectedIndex != -1)
                            {

                                kurs[group.SelectedIndex].name_student.Add($"{textBoxes[0].Text.ToUpper().Trim()} {textBoxes[1].Text.ToUpper().Trim()} {textBoxes[2].Text.ToUpper().Trim()}");
                                Kurs1.Items.Clear();
                                foreach (var item in kurs[group.SelectedIndex].name_student)
                                {
                                    Kurs1.Items.Add(item);
                                }
                                files[1] = await folder.CreateFileAsync("Kurs2.xml", CreationCollisionOption.OpenIfExists);
                                using (var stream = await files[1].OpenStreamForWriteAsync())
                                {
                                    serializers[1].Serialize(stream, kurs);
                                }
                            }
                        }
                        break;
                    case "3 Курс":
                        {
                            if (group.SelectedIndex != -1)
                            {

                                kurs[group.SelectedIndex].name_student.Add($"{textBoxes[0].Text.ToUpper().Trim()} {textBoxes[1].Text.ToUpper().Trim()} {textBoxes[2].Text.ToUpper().Trim()}");
                                Kurs1.Items.Clear();
                                foreach (var item in kurs[group.SelectedIndex].name_student)
                                {
                                    Kurs1.Items.Add(item);
                                }
                                files[2] = await folder.CreateFileAsync("Kurs3.xml", CreationCollisionOption.OpenIfExists);
                                using (var stream = await files[2].OpenStreamForWriteAsync())
                                {
                                    serializers[2].Serialize(stream, kurs);
                                }
                            }
                        }
                        break;
                    case "4 Курс":
                        {
                            if (group.SelectedIndex != -1)
                            {

                                kurs[group.SelectedIndex].name_student.Add($"{textBoxes[0].Text.ToUpper().Trim()} {textBoxes[1].Text.ToUpper().Trim()} {textBoxes[2].Text.ToUpper().Trim()}");
                                Kurs1.Items.Clear();
                                foreach (var item in kurs[group.SelectedIndex].name_student)
                                {
                                    Kurs1.Items.Add(item);
                                }
                                files[3] = await folder.CreateFileAsync("Kurs4.xml", CreationCollisionOption.OpenIfExists);
                                using (var stream = await files[3].OpenStreamForWriteAsync())
                                {
                                    serializers[3].Serialize(stream, kurs);
                                }
                            }
                        }
                        break;
                    default:
                        {

                        }
                        break;
                }
            }; await content.ShowAsync();
        }



        private async void group_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            {
                var deb = Pivo.SelectedItem as TabViewItem;
                Debug.WriteLine(deb.Header);

                switch (deb.Header)
                {
                    case "1 Курс":
                        {
                            if (group.SelectedIndex != -1)
                            {

                                Kurs1.Items.Clear();
                                foreach (var item in kurs[group.SelectedIndex].name_student)
                                {
                                    Kurs1.Items.Add(item);
                                }
                            }

                        }
                        break;
                    case "2 Курс":
                        {
                            if (group.SelectedIndex != -1)
                            {

                                Kurs2.Items.Clear();
                                foreach (var item in kurs[group.SelectedIndex].name_student)
                                {
                                    Kurs2.Items.Add(item);
                                }
                            }
                        }
                        break;
                    case "3 Курс":
                        {
                            if (group.SelectedIndex != -1)
                            {

                                Kurs3.Items.Clear();
                                foreach (var item in kurs[group.SelectedIndex].name_student)
                                {
                                    Kurs3.Items.Add(item);
                                }
                            }
                        }
                        break;
                    case "4 Курс":
                        {
                            if (group.SelectedIndex != -1)
                            {

                                Kurs3.Items.Clear();
                                foreach (var item in kurs[group.SelectedIndex].name_student)
                                {
                                    Kurs3.Items.Add(item);
                                }
                            }
                        }
                        break;
                    default:
                        {

                        }
                        break;
                }

            }
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {

            folder = ApplicationData.Current.LocalFolder;
            for (int i = 0; i < 4; i++)
            {
                files[i] = await folder.CreateFileAsync($"Kurs{i + 1}.xml", CreationCollisionOption.OpenIfExists);
                stroka[i] = await FileIO.ReadTextAsync(files[i]);
                serializers[i] = new XmlSerializer(typeof(List<Kurs>));
            }
            using (var stream = await files[0].OpenStreamForReadAsync())
            {
                if (stroka[0].Length != 0)
                {
                    kurs = (List<Kurs>)serializers[0].Deserialize(stream);
                }
                else
                {
                    kurs = new List<Kurs>();
                }
            }
            //foreach (var item in kurs)
            //{
            //    group.Items.Add(item.Name_Group);
            //}
        }



        private async void Pivo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var deb = Pivo.SelectedItem as TabViewItem;
            Debug.WriteLine(deb.Header);
            if (files[0] != null)
            {
                switch (deb.Header)
                {
                    case "1 Курс":
                        {

                            using (var stream = await files[0].OpenStreamForReadAsync())
                            {
                                if (stroka[0].Length != 0)
                                {
                                    kurs = (List<Kurs>)serializers[0].Deserialize(stream);
                                }
                                else
                                {
                                    kurs = new List<Kurs>();
                                }
                            }
                            Kurs1.Items.Clear();
                            group.Items.Clear();
                            foreach (var item in kurs)
                            {
                                group.Items.Add(item.Name_Group);
                            }

                        }
                        break;
                    case "2 Курс":
                        {
                            using (var stream = await files[1].OpenStreamForReadAsync())
                            {
                                if (stroka[1].Length != 0)
                                {
                                    kurs = (List<Kurs>)serializers[1].Deserialize(stream);
                                }
                                else
                                {
                                    kurs = new List<Kurs>();
                                }
                            }
                            group.Items.Clear();
                            foreach (var item in kurs)
                            {
                                group.Items.Add(item.Name_Group);
                            }
                        }
                        break;
                    case "3 Курс":
                        {
                            using (var stream = await files[2].OpenStreamForReadAsync())
                            {
                                if (stroka[2].Length != 0)
                                {
                                    kurs = (List<Kurs>)serializers[2].Deserialize(stream);
                                }
                                else
                                {
                                    kurs = new List<Kurs>();
                                }
                            }
                            group.Items.Clear();
                            foreach (var item in kurs)
                            {
                                group.Items.Add(item.Name_Group);
                            }
                        }
                        break;
                    case "4 Курс":
                        {
                            using (var stream = await files[3].OpenStreamForReadAsync())
                            {
                                if (stroka[3].Length != 0)
                                {
                                    kurs = (List<Kurs>)serializers[3].Deserialize(stream);
                                }
                                else
                                {
                                    kurs = new List<Kurs>();
                                }
                            }
                            group.Items.Clear();
                            foreach (var item in kurs)
                            {
                                group.Items.Add(item.Name_Group);
                            }
                        }
                        break;
                    default:
                        {

                        }
                        break;
                }

            }

        }

        private async void Del_student_Click(object sender, RoutedEventArgs e)
        {
            var deb = Pivo.SelectedItem as TabViewItem;

            switch (deb.Header)
            {
                case "1 Курс":
                    {
                        if (group.SelectedIndex != -1 && Kurs1.SelectedIndex != -1)
                        {
                            kurs[group.SelectedIndex].name_student.RemoveAt(Kurs1.SelectedIndex);
                            Kurs1.Items.RemoveAt(Kurs1.SelectedIndex);

                            files[0] = await folder.CreateFileAsync("Kurs1.xml", CreationCollisionOption.ReplaceExisting);
                            using (var stream = await files[0].OpenStreamForWriteAsync())
                            {
                                serializers[0].Serialize(stream, kurs);
                            }
                            files[0] = await folder.CreateFileAsync("Kurs1.xml", CreationCollisionOption.OpenIfExists);
                        }
                        break;
                    };
                case "2 Курс":
                    {
                        if (group.SelectedIndex != -1 && Kurs2.SelectedIndex != -1)
                        {
                            kurs[group.SelectedIndex].name_student.RemoveAt(Kurs2.SelectedIndex);
                            Kurs2.Items.RemoveAt(Kurs2.SelectedIndex);

                            files[1] = await folder.CreateFileAsync("Kurs2.xml", CreationCollisionOption.ReplaceExisting);
                            using (var stream = await files[1].OpenStreamForWriteAsync())
                            {
                                serializers[1].Serialize(stream, kurs);
                            }
                            files[1] = await folder.CreateFileAsync("Kurs2.xml", CreationCollisionOption.OpenIfExists);
                        }
                        break;
                    };
                case "3 Курс":
                    {
                        kurs[group.SelectedIndex].name_student.RemoveAt(Kurs3.SelectedIndex);
                        Kurs3.Items.RemoveAt(Kurs3.SelectedIndex);

                        files[2] = await folder.CreateFileAsync("Kurs3.xml", CreationCollisionOption.ReplaceExisting);
                        using (var stream = await files[2].OpenStreamForWriteAsync())
                        {
                            serializers[2].Serialize(stream, kurs);
                        }
                        files[2] = await folder.CreateFileAsync("Kurs3.xml", CreationCollisionOption.OpenIfExists);
                        break;
                    };

                case "4 Курс":
                    {
                        kurs[group.SelectedIndex].name_student.RemoveAt(Kurs3.SelectedIndex);
                        Kurs3.Items.RemoveAt(Kurs3.SelectedIndex);

                        files[3] = await folder.CreateFileAsync("Kurs4.xml", CreationCollisionOption.ReplaceExisting);
                        using (var stream = await files[3].OpenStreamForWriteAsync())
                        {
                            serializers[3].Serialize(stream, kurs);
                        }
                        files[3] = await folder.CreateFileAsync("Kurs4.xml", CreationCollisionOption.OpenIfExists);
                        break;
                    };
                default:
                    {
                        break;
                    }
            }
        }

        private async void rename_student_Click(object sender, RoutedEventArgs e)
        {
            StackPanel panel = new StackPanel();
            panel.Orientation = Orientation.Vertical;
            TextBox[] boxes = new TextBox[3];

            string[] text = new string[] { "Фамилия", "Имя", "Отчество" };

            for (int i = 0; i < 3; i++)
            {
                boxes[i] = new TextBox();
                boxes[i].PlaceholderText = text[i];
                boxes[i].Margin = new Thickness(10, 10, 10, 10);
                panel.Children.Add(boxes[i]);
            }

            ContentDialog content = new ContentDialog()
            {
                PrimaryButtonText = "Изменить студент",
                CloseButtonText = "Закрыть",
                Content = panel
            };

            var deb = Pivo.SelectedItem as TabViewItem;

            switch (deb.Header)
            {
                case "1 Курс":
                    {
                        content.PrimaryButtonClick += async (a, b) =>
                        {
                            if (group.SelectedIndex != -1 && boxes[0].Text.Trim() != "" && boxes[1].Text.Trim() != "")
                            {
                                kurs[group.SelectedIndex].name_student[Kurs1.SelectedIndex] = $"{boxes[0].Text.Trim().ToUpper()} {boxes[1].Text.Trim().ToUpper()} {boxes[2].Text.Trim().ToUpper()}";
                                Kurs1.Items[Kurs1.SelectedIndex] = $"{boxes[0].Text.Trim().ToUpper()} {boxes[1].Text.Trim().ToUpper()} {boxes[2].Text.Trim().ToUpper()}";

                                files[0] = await folder.CreateFileAsync("Kurs1.xml", CreationCollisionOption.ReplaceExisting);

                                using (var stream = await files[0].OpenStreamForWriteAsync())
                                {
                                    serializers[0].Serialize(stream, kurs);
                                }

                                files[0] = await folder.CreateFileAsync("Kurs1.xml", CreationCollisionOption.OpenIfExists);
                            }
                        };
                        break;
                    };
                case "2 Курс":
                    {
                        content.PrimaryButtonClick += async (a, b) =>
                        {
                            if (group.SelectedIndex != -1 && boxes[0].Text != "" && boxes[1].Text != "")
                            {
                                kurs[group.SelectedIndex].name_student[Kurs2.SelectedIndex] = $"{boxes[0].Text.Trim().ToUpper()} {boxes[1].Text.Trim().ToUpper()} {boxes[2].Text.Trim().ToUpper()}";
                                Kurs2.Items[Kurs2.SelectedIndex] = $"{boxes[0].Text.Trim().ToUpper()} {boxes[1].Text.Trim().ToUpper()} {boxes[2].Text.Trim().ToUpper()}";

                                files[1] = await folder.CreateFileAsync("Kurs2.xml", CreationCollisionOption.ReplaceExisting);

                                using (var stream = await files[1].OpenStreamForWriteAsync())
                                {
                                    serializers[1].Serialize(stream, kurs);
                                }

                                files[1] = await folder.CreateFileAsync("Kurs2.xml", CreationCollisionOption.OpenIfExists);
                            }
                        };
                        break;
                    };
                case "3 Курс":
                    {
                        content.PrimaryButtonClick += async (a, b) =>
                        {
                            if (group.SelectedIndex != -1 && boxes[0].Text.Trim() != "" && boxes[1].Text.Trim() != "")
                            {
                                kurs[group.SelectedIndex].name_student[Kurs3.SelectedIndex] = $"{boxes[0].Text.Trim().ToUpper()} {boxes[1].Text.Trim().ToUpper()} {boxes[2].Text.Trim().ToUpper()}";
                                Kurs3.Items[Kurs3.SelectedIndex] = $"{boxes[0].Text.Trim().ToUpper()} {boxes[1].Text.Trim().ToUpper()} {boxes[2].Text.Trim().ToUpper()}";

                                files[2] = await folder.CreateFileAsync("Kurs3.xml", CreationCollisionOption.ReplaceExisting);

                                using (var stream = await files[2].OpenStreamForWriteAsync())
                                {
                                    serializers[2].Serialize(stream, kurs);
                                }

                                files[2] = await folder.CreateFileAsync("Kurs3.xml", CreationCollisionOption.OpenIfExists);
                            }
                        };
                        break;
                    };

                case "4 Курс":
                    {
                        content.PrimaryButtonClick += async (a, b) =>
                        {
                            if (group.SelectedIndex != -1 && boxes[0].Text.Trim() != "" && boxes[1].Text.Trim() != "")
                            {
                                kurs[group.SelectedIndex].name_student[Kurs4.SelectedIndex] = $"{boxes[0].Text.Trim().ToUpper()} {boxes[1].Text.Trim().ToUpper()} {boxes[2].Text.Trim().ToUpper()}";
                                Kurs4.Items[Kurs4.SelectedIndex] = $"{boxes[0].Text.Trim().ToUpper()} {boxes[1].Text.Trim().ToUpper()} {boxes[2].Text.Trim().ToUpper()}";

                                files[3] = await folder.CreateFileAsync("Kurs4.xml", CreationCollisionOption.ReplaceExisting);

                                using (var stream = await files[3].OpenStreamForWriteAsync())
                                {
                                    serializers[3].Serialize(stream, kurs);
                                }

                                files[3] = await folder.CreateFileAsync("Kurs4.xml", CreationCollisionOption.OpenIfExists);
                            }
                        };
                        break;
                    };
                default:
                    {
                        break;
                    }
            }
            await content.ShowAsync();
        }

        private async void Export_Click(object sender, RoutedEventArgs e)
        {
            if (group.SelectedIndex != -1)
            {

                var Name_Group = $"{group.Items[group.SelectedIndex]}.xml";

                var files = await folder.GetFilesAsync();

                bool prov = false;


                foreach (var item in files)
                {
                    if (item.Name == Name_Group)
                    {

                        prov = true;
                    }
                }
                if (prov)
                {

                    StorageFile File_Group = await folder.GetFileAsync(Name_Group);

                    List<Ocenivanie> students;

                    XmlSerializer xml_group = new XmlSerializer(typeof(List<Ocenivanie>));

                    string File_Length;

                    File_Length = await FileIO.ReadTextAsync(File_Group);

                    using (var stream = await File_Group.OpenStreamForReadAsync())
                    {

                        if (File_Length.Length != 0)
                        {

                            students = (List<Ocenivanie>)xml_group.Deserialize(stream);
                        }
                        else
                        {

                            students = new List<Ocenivanie>();
                        }
                    }

                    ToastNotifier toastNotifier = ToastNotificationManager.CreateToastNotifier();
                    Windows.Data.Xml.Dom.XmlDocument xml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText02);
                    Windows.Data.Xml.Dom.XmlNodeList nodes = xml.GetElementsByTagName("text");
                    nodes.Item(0).AppendChild(xml.CreateTextNode("Генерация Excel файла"));

                    using (ExcelPackage excelPackage = new ExcelPackage())
                    {
                        try{ 
                        excelPackage.Workbook.Properties.Title = "Wa???";
                        for (int i = 0; i < students.Count; i++)
                        {
                            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add($"{students[i].date} {students[i].subjec}");
                            worksheet.Cells[1, 1].Value = "Студент";
                            worksheet.Cells[1, 2].Value = "Оценка";
                            for (int j = 0; j < students[i].student.Count; j++)
                            {
                                worksheet.Cells[j + 2, 1].Value = students[i].student[j];
                                worksheet.Cells[j + 2, 2].Value = students[i].marks[j];
                            }
                        }

                        FileInfo fi = new FileInfo(ApplicationData.Current.LocalFolder.Path + @"\" + Name_Group.Replace("xml", "xlsx"));
                        excelPackage.SaveAs(fi);
                          
                            nodes.Item(1).AppendChild(xml.CreateTextNode("Exсel файл успешно создан! "));
                        }
                        catch
                        {
                            nodes.Item(1).AppendChild(xml.CreateTextNode("Произошла ошибка в создании файла"));
                        }
                        ToastNotification toast = new ToastNotification(xml);
                        toast.ExpirationTime = DateTime.Now.AddSeconds(4);
                        toastNotifier.Show(toast);
                    }

                }
            }
        }


    }
}

