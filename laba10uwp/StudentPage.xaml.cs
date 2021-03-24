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
    /// 

    public class Marks_stud
    {
        public string subjects { get; set; }
        public string date { get; set; }
        public string mark { get; set; }

    }
    public class Info_Student
    {
        public string MARKS { get;set; }
        public string DIS { get;set; }
        public string DATE { get;set; }
    }
    public sealed partial class StudentPage : Page
    {
        StorageFolder folder = ApplicationData.Current.LocalFolder;
        string str;
        StorageFile file;
        List<Dis> DisList;
        XmlSerializer serializer = new XmlSerializer(typeof(List<Dis>));
        StorageFile file_group;
        string str_Group;
        XmlSerializer xml_Group = new XmlSerializer(typeof(List<Ocenivanie>));

        List<Ocenivanie> ocenivanies;


        List<Info_Student> students = new List<Info_Student>();

        
        public StudentPage()
        {
            this.InitializeComponent();
        }
        Save_Student save;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
               save = (Save_Student)e.Parameter;
                
                stud.Text = save.Fio;
                grup.Text = save.gruppa;

            }
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Dis>));
            file = await folder.CreateFileAsync("Dis.xml", CreationCollisionOption.OpenIfExists);
            str = await FileIO.ReadTextAsync(file);

            using (var stream = await file.OpenStreamForReadAsync())
            {
                if (str.Length != 0)
                {
                    DisList = (List<Dis>)serializer.Deserialize(stream);
                }
                else
                {
                    DisList = new List<Dis>();
                }
            }

            foreach (var item in DisList)
            {
                Dis.Items.Add(item.Subjucts);
            }
            file_group = await folder.CreateFileAsync($"{save.gruppa}.xml", CreationCollisionOption.OpenIfExists);
            str_Group = await FileIO.ReadTextAsync(file_group);
            using (var stream = await file_group.OpenStreamForReadAsync())
            {
                if (str_Group.Length != 0)
                {
                    ocenivanies = (List<Ocenivanie>)xml_Group.Deserialize(stream);
                }
                else
                {
                    ocenivanies = new List<Ocenivanie>();
                }
            }



            for (int i = 0; i < ocenivanies.Count; i++)
            {
                Info_Student info_Student = new Info_Student();
                info_Student.DATE = ocenivanies[i].date;
                info_Student.DIS = ocenivanies[i].subjec;
                for (int J = 0; J < ocenivanies[i].student.Count; J++)
                {
                    if (ocenivanies[i].student[J] == save.Fio)
                    {
                        info_Student.MARKS = ocenivanies[i].marks[J];
                        students.Add(info_Student);
                        break;
                    }
                }
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void Dis_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Marks_stud> students1 = new List<Marks_stud>();

            if (Dis.SelectedIndex != -1)
            {
                for (int i = 0; i < students.Count; i++)
                {
                    if (students[i].DIS == Dis.Items[Dis.SelectedIndex].ToString())
                    {
                        students1.Add(new Marks_stud { date = students[i].DATE, subjects = students[i].DIS, mark = students[i].MARKS });
                    }
                }
                grid.ItemsSource = students1;
            }
        }
    }
}
