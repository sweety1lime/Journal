using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Serialization;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace BrainDead
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    
    public class Ocenivanie
    {

        public string date { get; set; }

        public string subjec { get; set; }
        public List<string> marks { get; set; }
        public List<string> student { get; set; }

    }
    public class Otmetka
    {
        public string student { get; set; }
        public string marks { get; set; }
    }
    public sealed partial class TeacherPage : Page
    {
        StorageFolder folder;
        StorageFile[] files = new StorageFile[4];
        XmlSerializer[] serializers = new XmlSerializer[4];
        string[] stroka = new string[4];
        List<Kurs> kurs;
        string str;
        StorageFile file;
        StorageFile fll;
        string filstr;
        List<Dis> DisList;
        XmlSerializer serializer = new XmlSerializer(typeof(List<Dis>));
        XmlSerializer serializer1 = new XmlSerializer(typeof(List<Ocenivanie>));

        public TeacherPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                Save save = (Save)e.Parameter;
                subject.Text = save.dis;
                Name_Prepod.Text = save.Prepod;

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


            foreach (var item in kurs)
            {
                group.Items.Add(item.Name_Group);
            }

            Date.Date = DateTime.Now;

        }

        private async void Kurs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (Kurs.SelectedIndex)
            {
                case 0:
                    {
                        if (group != null)
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

                            listbox.Items.Clear();
                            group.Items.Clear();
                            foreach (var item in kurs)
                            {
                                group.Items.Add(item.Name_Group);
                            }
                        }
                    }
                    break;
                case 1:
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
                        listbox.Items.Clear();
                        group.Items.Clear();
                        foreach (var item in kurs)
                        {
                            group.Items.Add(item.Name_Group);
                        }
                    }
                    break;
                case 2:
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
                        listbox.Items.Clear();
                        group.Items.Clear();
                        foreach (var item in kurs)
                        {
                            group.Items.Add(item.Name_Group);
                        }
                    }
                    break;
                case 3:
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
                        listbox.Items.Clear();
                        group.Items.Clear();
                        foreach (var item in kurs)
                        {
                            group.Items.Add(item.Name_Group);
                        }
                    }
                    break;

                default:
                    break;
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private async void group_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (group.SelectedIndex != -1)
            {

                switch (Kurs.SelectedIndex)
                {
                    case 0:
                        {
                            files[0] = await folder.CreateFileAsync("Kurs1.xml", CreationCollisionOption.OpenIfExists);
                            using (var stream = await files[0].OpenStreamForWriteAsync())
                            {
                                serializers[0].Deserialize(stream);
                            }

                            listbox.Items.Clear();

                            foreach (var item in kurs[group.SelectedIndex].name_student)
                            {
                                listbox.Items.Add(item);
                            }
                            fll = await folder.CreateFileAsync($"{kurs[group.SelectedIndex].Name_Group}.xml", CreationCollisionOption.OpenIfExists);
                            filstr = await FileIO.ReadTextAsync(fll);
                            using (var stream = await fll.OpenStreamForReadAsync())
                            {
                                if (filstr.Length != 0)
                                {
                                    ocenivanies = (List<Ocenivanie>)serializer1.Deserialize(stream);
                                }
                                else
                                {
                                    ocenivanies = new List<Ocenivanie>();
                                }
                            }
                            adw.Date = null;
                            Otmetka.ItemsSource = "";
                        }
                        break;
                    case 1:
                        {
                            files[1] = await folder.CreateFileAsync("Kurs2.xml", CreationCollisionOption.OpenIfExists);
                            using (var stream = await files[1].OpenStreamForWriteAsync())
                            {
                                serializers[1].Deserialize(stream);
                            }

                            listbox.Items.Clear();

                            foreach (var item in kurs[group.SelectedIndex].name_student)
                            {
                                listbox.Items.Add(item);
                            }
                            fll = await folder.CreateFileAsync($"{kurs[group.SelectedIndex].Name_Group}.xml", CreationCollisionOption.OpenIfExists);
                            filstr = await FileIO.ReadTextAsync(fll);
                            using (var stream = await fll.OpenStreamForReadAsync())
                            {
                                if (filstr.Length != 0)
                                {
                                    ocenivanies = (List<Ocenivanie>)serializer1.Deserialize(stream);
                                }
                                else
                                {
                                    ocenivanies = new List<Ocenivanie>();
                                }
                            }
                            adw.Date = null;
                            Otmetka.ItemsSource = "";
                        }
                        break;
                    case 2:
                        {
                            files[2] = await folder.CreateFileAsync("Kurs3.xml", CreationCollisionOption.OpenIfExists);
                            using (var stream = await files[2].OpenStreamForWriteAsync())
                            {
                                serializers[2].Deserialize(stream);
                            }

                            listbox.Items.Clear();
                            foreach (var item in kurs[group.SelectedIndex].name_student)
                            {
                                listbox.Items.Add(item);
                            }
                            fll = await folder.CreateFileAsync($"{kurs[group.SelectedIndex].Name_Group}.xml", CreationCollisionOption.OpenIfExists);
                            filstr = await FileIO.ReadTextAsync(fll);
                            using (var stream = await fll.OpenStreamForReadAsync())
                            {
                                if (filstr.Length != 0)
                                {
                                    ocenivanies = (List<Ocenivanie>)serializer1.Deserialize(stream);
                                }
                                else
                                {
                                    ocenivanies = new List<Ocenivanie>();
                                }
                            }
                            adw.Date = null;
                            Otmetka.ItemsSource = "";
                        }
                        break;
                    case 3:
                        {
                            files[3] = await folder.CreateFileAsync("Kurs4.xml", CreationCollisionOption.OpenIfExists);
                            using (var stream = await files[3].OpenStreamForWriteAsync())
                            {
                                serializers[3].Deserialize(stream);
                            }

                            listbox.Items.Clear();
                            foreach (var item in kurs[group.SelectedIndex].name_student)
                            {
                                listbox.Items.Add(item);
                            }
                            fll = await folder.CreateFileAsync($"{kurs[group.SelectedIndex].Name_Group}.xml", CreationCollisionOption.OpenIfExists);
                            filstr = await FileIO.ReadTextAsync(fll);
                            using (var stream = await fll.OpenStreamForReadAsync())
                            {
                                if (filstr.Length != 0)
                                {
                                    ocenivanies = (List<Ocenivanie>)serializer1.Deserialize(stream);
                                }
                                else
                                {
                                    ocenivanies = new List<Ocenivanie>();
                                }
                            }
                            adw.Date = null;
                            Otmetka.ItemsSource = "";
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        List<Ocenivanie> ocenivanies = new List<Ocenivanie>();
        private async void Tapped_mark_Click(object sender, RoutedEventArgs e)
        {
            int count = -1;
            if (Box_Mark.SelectedIndex != -1 && listbox.SelectedIndex != -1)
            {
                Ocenivanie ocenivanie = new Ocenivanie();
                if (ocenivanies.Count != 0)
                {
                    for (int i = 0; i < ocenivanies.Count; i++)
                    {
                        if (ocenivanies[i].subjec == subject.Text && Date.Date.Value.Date.ToString().Replace("0:00:00", "") == ocenivanies[i].date)
                        {
                            count = i;
                        }
                    }
                    if (count == -1)
                    {

                        ocenivanie.marks = new List<string>();
                        ocenivanie.student = new List<string>();
                        ocenivanie.subjec = subject.Text;
                        ocenivanie.date = Date.Date.Value.Date.ToString().Replace("0:00:00", "");

                        foreach (var item in listbox.Items)
                        {
                            ocenivanie.student.Add(item.ToString());
                            ocenivanie.marks.Add("");
                        }
                        ocenivanies.Add(ocenivanie);
                        var text = Box_Mark.Items[Box_Mark.SelectedIndex] as ComboBoxItem;
                        ocenivanies[ocenivanies.Count - 1].marks[listbox.SelectedIndex] = text.Content.ToString();
                    }


                    else
                    {
                        var text = Box_Mark.Items[Box_Mark.SelectedIndex] as ComboBoxItem;
                        ocenivanies[count].marks[listbox.SelectedIndex] = text.Content.ToString();
                    }
                }
                else
                {
                    ocenivanie.marks = new List<string>();
                    ocenivanie.student = new List<string>();
                    ocenivanie.subjec = subject.Text;
                    ocenivanie.date = Date.Date.Value.Date.ToString().Replace("0:00:00", "");

                    foreach (var item in listbox.Items)
                    {
                        ocenivanie.student.Add(item.ToString());
                        ocenivanie.marks.Add("");
                    }
                    ocenivanies.Add(ocenivanie);
                var text = Box_Mark.Items[Box_Mark.SelectedIndex] as ComboBoxItem;
                ocenivanies[ocenivanies.Count - 1].marks[listbox.SelectedIndex] = text.Content.ToString();
                }


                fll = await folder.CreateFileAsync($"{kurs[group.SelectedIndex].Name_Group}.xml", CreationCollisionOption.ReplaceExisting);
                using (var stream = await fll.OpenStreamForWriteAsync())
                {
                    serializer1.Serialize(stream, ocenivanies);
                }
            }
        }

        private void adw_Closed(object sender, object e)
        {
            List<Otmetka> Otmetkas = new List<Otmetka>();

            int op = -1;
            for (int i = 0; i < ocenivanies.Count; i++)
            {
                if (adw.Date.Value.Date.ToString().Replace("0:00:00", "")  == ocenivanies[i].date && subject.Text == ocenivanies[i].subjec)
                {
                    op = i;
                }
            }
            if (op != -1)
            {

            for (int i = 0; i < ocenivanies[op].student.Count; i++)
            {
                Otmetkas.Add(new Otmetka { marks = ocenivanies[op].marks[i], student = ocenivanies[op].student[i] });
            }
            Otmetka.ItemsSource = Otmetkas;
            }
            
        }
    }

}
