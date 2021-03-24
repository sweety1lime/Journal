using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace BrainDead
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public class UserData
    {
        public int AccountType { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string LastName { get; set; }
        public string Group { get; set; }
        public string Kurs { get; set; }

        public string Subjects { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
    public class Save
    {
        public string Prepod;
        public string dis;
    }
    public class Save_Student
    {
        public string Fio;
        public string gruppa;
    }

    public sealed partial class MainPage : Page
    {
        StorageFolder folder;
        StorageFile[] files = new StorageFile[4];
        XmlSerializer[] serializers = new XmlSerializer[4];
        string[] stroka = new string[4];
        List<Kurs> kurs;

        List<UserData> list = new List<UserData>();
        int count = 0;
        public MainPage()
        {
            this.InitializeComponent();
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RegGrid.Visibility = Visibility.Collapsed;
            LoginGrid.Visibility = Visibility.Visible;
            RegLogButton.Content = "Войти";

        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LoginGrid.Visibility = Visibility.Collapsed;
            RegGrid.Visibility = Visibility.Visible;
            RegLogButton.Content = "Зарегистрироваться";
        }
        private  void RegLogButton_Click(object sender, RoutedEventArgs e)
        {
            if (RegGrid.Visibility == Visibility.Visible)
            {
                if (RegNameBox.Text.Length == 0)
                {
                    var message = new MessageDialog("Поле имени не может быть пустым.").ShowAsync();
                    return;
                }

                if (RegSurNameBox.Text.Length == 0)
                {
                    var message = new MessageDialog("Поле фамилии не может быть пустым.").ShowAsync();
                    return;
                }
                if (RegLastNameBox.Text.Length == 0)
                {
                    var message = new MessageDialog("Поле Отчества не может быть пустым.").ShowAsync();
                    return;
                }
                if (RegTextBox.Text.Length == 0)
                {
                    var message = new MessageDialog("Поле логина не может быть пустым.").ShowAsync();
                    return;
                }

                if (RegPasswordbx.Password == "")
                {
                    var message = new MessageDialog("Поле пароля не может быть пустым.").ShowAsync();
                    return;
                }
                if (RegTypeBox.SelectedIndex < 0)
                {
                    var message = new MessageDialog("Выберете тип учетной записи").ShowAsync();
                    return;
                }
                bool prov = false;

                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Login == RegTextBox.Text)
                    {
                        prov = true;
                        break;
                    }
                }
                if (!prov)
                {
                    otpravitel();

                }
            }
            else
            {
                poluchatel();
            }
        }

        static string GetMd5Hash(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sBuilder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                return sBuilder.ToString();
            }
        }

        async void otpravitel()
        {
            bool prov = false;
            string FIO = $"{RegSurNameBox.Text.Trim().ToUpper()} {RegNameBox.Text.Trim().ToUpper()} {RegLastNameBox.Text.Trim().ToUpper()}";
            if (RegTypeBox.SelectedIndex == 0)
            {
                if (Kurs1.IsChecked == true)
                {
                    if (RegTypeBox.SelectedIndex != 0)
                    {
                        Group.Text = "";
                    }
                    if (Group.SelectedIndex != -1)
                    {
                        for (int i = 0; i <
                            kurs[Group.SelectedIndex].name_student.Count; i++)
                        {
                            if (FIO ==kurs[Group.SelectedIndex].name_student[i])
                            {
                                prov = true;
                                break;
                            }
                        }
                    }

                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i].Name == RegSurNameBox.Text.Trim().ToUpper() && list[i].Surname == RegNameBox.Text.Trim().ToUpper() && list[i].LastName == RegLastNameBox.Text.Trim().ToUpper() && list[i].Group == Group.Items[Group.SelectedIndex].ToString())
                        {
                            prov = false;
                            break;
                        }
                    }

                    UserData data = new UserData();
                    data.Login = RegTextBox.Text;
                    data.Password = GetMd5Hash(RegPasswordbx.Password);
                    data.Name = RegNameBox.Text;
                    data.Surname = RegSurNameBox.Text;
                    data.LastName = RegLastNameBox.Text;
                    data.AccountType = RegTypeBox.SelectedIndex;
                    data.Group = Group.Items[Group.SelectedIndex].ToString();
                    data.Kurs = "1 Курс";
                    data.Subjects = "";
                    list.Add(data);

                    StorageFolder folder = ApplicationData.Current.LocalFolder;

                    StorageFile file = await folder.CreateFileAsync("db.xml", CreationCollisionOption.OpenIfExists);

                    XmlSerializer xml = new XmlSerializer(typeof(List<UserData>));

                    Stream stream = await file.OpenStreamForWriteAsync();
                    if (prov == true)
                    {

                        xml.Serialize(stream, list);

                        stream.Close();

                        MessageDialog msg = new MessageDialog(file.Path);
                        await msg.ShowAsync();
                    }

                }
                if (Kurs2.IsChecked == true)
                {
                    if (RegTypeBox.SelectedIndex != 0)
                    {
                        Group.Text = "";
                    }
                    if (Group.SelectedIndex != -1)
                    {
                        for (int i = 0; i <
                            kurs[Group.SelectedIndex].name_student.Count; i++)
                        {
                            if (FIO ==
                                kurs[Group.SelectedIndex].name_student[i])
                            {
                                prov = true;
                                break;
                            }
                        }
                    }
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i].Name == RegSurNameBox.Text.Trim().ToUpper() && list[i].Surname == RegNameBox.Text.Trim().ToUpper() && list[i].LastName == RegLastNameBox.Text.Trim().ToUpper() && list[i].Group == Group.Items[Group.SelectedIndex].ToString())
                        {
                            prov = false;
                            break;
                        }
                    }
                    UserData data = new UserData();
                    data.Login = RegTextBox.Text;
                    data.Password = GetMd5Hash(RegPasswordbx.Password);
                    data.Name = RegNameBox.Text;
                    data.Surname = RegSurNameBox.Text;
                    data.LastName = RegLastNameBox.Text;
                    data.AccountType = RegTypeBox.SelectedIndex;
                    data.Group = Group.Items[Group.SelectedIndex].ToString();
                    data.Kurs = "2 Курс";
                    data.Subjects = "";
                    list.Add(data);

                    StorageFolder folder = ApplicationData.Current.LocalFolder;

                    StorageFile file = await folder.CreateFileAsync("db.xml", CreationCollisionOption.OpenIfExists);

                    XmlSerializer xml = new XmlSerializer(typeof(List<UserData>));

                    Stream stream = await file.OpenStreamForWriteAsync();
                    if (prov == true)
                    {

                        xml.Serialize(stream, list);

                        stream.Close();

                        MessageDialog msg = new MessageDialog(file.Path);
                        await msg.ShowAsync();
                    }
                }
                if (Kurs3.IsChecked == true)
                {
                    if (RegTypeBox.SelectedIndex != 0)
                    {
                        Group.Text = "";
                    }
                    if (Group.SelectedIndex != -1)
                    {
                        for (int i = 0; i <
                            kurs[Group.SelectedIndex].name_student.Count; i++)
                        {
                            if (FIO ==
                                kurs[Group.SelectedIndex].name_student[i])
                            {
                                prov = true;
                                break;
                            }
                        }
                    }

                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i].Name == RegSurNameBox.Text.Trim().ToUpper() && list[i].Surname == RegNameBox.Text.Trim().ToUpper() && list[i].LastName == RegLastNameBox.Text.Trim().ToUpper() && list[i].Group == Group.Items[Group.SelectedIndex].ToString())
                        {
                            prov = false;
                            break;
                        }
                    }
                    UserData data = new UserData();
                    data.Login = RegTextBox.Text;
                    data.Password = GetMd5Hash(RegPasswordbx.Password);
                    data.Name = RegNameBox.Text;
                    data.Surname = RegSurNameBox.Text;
                    data.LastName = RegLastNameBox.Text;
                    data.AccountType = RegTypeBox.SelectedIndex;
                    data.Group = Group.Items[Group.SelectedIndex].ToString();
                    data.Kurs = "3 Курс";
                    data.Subjects = "";
                    list.Add(data);

                    StorageFolder folder = ApplicationData.Current.LocalFolder;

                    StorageFile file = await folder.CreateFileAsync("db.xml", CreationCollisionOption.OpenIfExists);

                    XmlSerializer xml = new XmlSerializer(typeof(List<UserData>));

                    Stream stream = await file.OpenStreamForWriteAsync();

                    if (prov == true)
                    {

                        xml.Serialize(stream, list);

                        stream.Close();

                        MessageDialog msg = new MessageDialog(file.Path);
                        await msg.ShowAsync();
                    }

                }
                if (Kurs4.IsChecked == true)
                {
                    if (RegTypeBox.SelectedIndex != 0)
                    {
                        Group.Text = "";
                    }
                    if (Group.SelectedIndex != -1)
                    {
                        for (int i = 0; i <
                            kurs[Group.SelectedIndex].name_student.Count; i++)
                        {
                            if (FIO ==
                                kurs[Group.SelectedIndex].name_student[i])
                            {
                                prov = true;
                                break;
                            }
                        }
                    }
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i].Name == RegSurNameBox.Text.Trim().ToUpper() && list[i].Surname == RegNameBox.Text.Trim().ToUpper() && list[i].LastName == RegLastNameBox.Text.Trim().ToUpper() && list[i].Group == Group.Items[Group.SelectedIndex].ToString())
                        {
                            prov = false;
                            break;
                        }
                    }
                    UserData data = new UserData();
                    data.Login = RegTextBox.Text;
                    data.Password = GetMd5Hash(RegPasswordbx.Password);
                    data.Name = RegNameBox.Text;
                    data.Surname = RegSurNameBox.Text;
                    data.LastName = RegLastNameBox.Text;
                    data.AccountType = RegTypeBox.SelectedIndex;
                    data.Group = Group.Items[Group.SelectedIndex].ToString();
                    data.Kurs = "4 Курс";
                    data.Subjects = "";
                    list.Add(data);

                    StorageFolder folder = ApplicationData.Current.LocalFolder;

                    StorageFile file = await folder.CreateFileAsync("db.xml", CreationCollisionOption.OpenIfExists);

                    XmlSerializer xml = new XmlSerializer(typeof(List<UserData>));

                    Stream stream = await file.OpenStreamForWriteAsync();
                    if (prov == true)
                    {

                        xml.Serialize(stream, list);

                        stream.Close();

                        MessageDialog msg = new MessageDialog(file.Path);
                        await msg.ShowAsync();
                    }
                }
            }
            if (RegTypeBox.SelectedIndex == 2)
            {

                UserData data = new UserData();
                data.Login = RegTextBox.Text;
                data.Password = GetMd5Hash(RegPasswordbx.Password);
                data.Name = RegNameBox.Text;
                data.Surname = RegSurNameBox.Text;
                data.LastName = RegLastNameBox.Text;
                data.AccountType = RegTypeBox.SelectedIndex;

                data.Group = "";
                data.Kurs = "";
                data.Subjects = "";

                list.Add(data);

                StorageFolder folder = ApplicationData.Current.LocalFolder;

                StorageFile file = await folder.CreateFileAsync("db.xml", CreationCollisionOption.OpenIfExists);

                XmlSerializer xml = new XmlSerializer(typeof(List<UserData>));

                Stream stream = await file.OpenStreamForWriteAsync();

                xml.Serialize(stream, list);

                stream.Close();

                MessageDialog msg = new MessageDialog(file.Path);
                await msg.ShowAsync();

            }
            if (subjects.SelectedIndex != -1)
            {
                if (RegTypeBox.SelectedIndex == 1)
                {

                    UserData data = new UserData();
                    data.Login = RegTextBox.Text;
                    data.Password = GetMd5Hash(RegPasswordbx.Password);
                    data.Name = RegNameBox.Text;
                    data.Surname = RegSurNameBox.Text;
                    data.LastName = RegLastNameBox.Text;
                    data.AccountType = RegTypeBox.SelectedIndex;
                    data.Subjects = subjects.Items[subjects.SelectedIndex].ToString();
                    data.Group = "";
                    data.Kurs = "";


                    list.Add(data);

                    StorageFolder folder = ApplicationData.Current.LocalFolder;

                    StorageFile file = await folder.CreateFileAsync("db.xml", CreationCollisionOption.OpenIfExists);

                    XmlSerializer xml = new XmlSerializer(typeof(List<UserData>));

                    Stream stream = await file.OpenStreamForWriteAsync();

                    xml.Serialize(stream, list);

                    stream.Close();

                    MessageDialog msg = new MessageDialog(file.Path);
                    await msg.ShowAsync();

                }
            }

        }
        private void RegTypeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string type = "";
            if (RegTypeBox.SelectedIndex == 0) type = "Студент";
            if (RegTypeBox.SelectedIndex == 1) type = "Преподаватель";
            if (RegTypeBox.SelectedIndex == 2) type = "Админ";
        }
        async void poluchatel()
        {
            UserData data = new UserData();

            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.CreateFileAsync("db.xml", CreationCollisionOption.OpenIfExists);

            XmlSerializer xml = new XmlSerializer(typeof(List<UserData>));

            try
            {
                using (Stream stream = await file.OpenStreamForWriteAsync())
                {
                    list = (List<UserData>)xml.Deserialize(stream);
                }
                string psswrd = GetMd5Hash(Passwordbx.Password);
                for (int i = 0; i < list.Count; i++)
                {
                    if (LoginTextBox.Text == list[i].Login && psswrd == list[i].Password)
                    {
                        if (list[i].AccountType == 0)
                        {
                            Save_Student save = new Save_Student();

                            save.Fio = $"{list[i].Surname} {list[i].Name} {list[i].LastName}";
                            save.gruppa = list[i].Group;
                            this.Frame.Navigate(typeof(StudentPage), save);
                        }
                        if (list[i].AccountType == 1)
                        {
                            Save save = new Save();
                            save.dis = list[i].Subjects;
                            save.Prepod = $"{list[i].Surname} {list[i].Name} {list[i].LastName}";
                            this.Frame.Navigate(typeof(TeacherPage), save);
                        }
                        if (list[i].AccountType == 2)
                        {
                            this.Frame.Navigate(typeof(AdminPage));
                        }
                    }
                }

            }
            catch (Exception e)
            {
                MessageDialog msg = new MessageDialog(e.Message);
                await msg.ShowAsync();
            }


        }

        private void RegTypeBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (RegTypeBox.SelectedIndex == 0)
            {
                Kurs1.IsChecked = false;
                Kurs2.IsChecked = false;
                Kurs3.IsChecked = false;
                Kurs4.IsChecked = false;
                Group.Visibility = Visibility.Visible;
                Kurses.Visibility = Visibility.Visible;
            }
            else
            {
                Group.Visibility = Visibility.Collapsed;
                Kurses.Visibility = Visibility.Collapsed;
            }
            if (RegTypeBox.SelectedIndex == 1)
            {
                subjects.Visibility = Visibility.Visible;
            }
            else
            {
                subjects.Visibility = Visibility.Collapsed;
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UserData data = new UserData();

            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.CreateFileAsync("db.xml", CreationCollisionOption.OpenIfExists);

            XmlSerializer xml = new XmlSerializer(typeof(List<UserData>));
            folder = ApplicationData.Current.LocalFolder;
            for (int i = 0; i < 4; i++)
            {
                files[i] = await folder.CreateFileAsync($"Kurs{i + 1}.xml", CreationCollisionOption.OpenIfExists);
                stroka[i] = await FileIO.ReadTextAsync(files[i]);
                serializers[i] = new XmlSerializer(typeof(List<Kurs>));
            }
            using (var stream = await file.OpenStreamForReadAsync())
            {
                try
                {
                    list = (List<UserData>)xml.Deserialize(stream);
                }
                catch
                {
                    list = new List<UserData>();
                }
            }
            List<Dis> DisList;
            XmlSerializer serializer2 = new XmlSerializer(typeof(List<Dis>));
            string str1;
            file = await folder.CreateFileAsync("Dis.xml", CreationCollisionOption.OpenIfExists);
            str1 = await FileIO.ReadTextAsync(file);

            using (var stream = await file.OpenStreamForReadAsync())
            {
                if (str1.Length != 0)
                {
                    DisList = (List<Dis>)serializer2.Deserialize(stream);
                }
                else
                {
                    DisList = new List<Dis>();
                }
            }

            foreach (var item in DisList)
            {
                subjects.Items.Add(item.Subjucts);
            }
        }

        private async void Kurs1_Checked(object sender, RoutedEventArgs e)
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
            Group.Items.Clear();
            foreach (var item in kurs)
            {
                Group.Items.Add(item.Name_Group);
            }
        }

        private async void Kurs2_Checked(object sender, RoutedEventArgs e)
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
            Group.Items.Clear();
            foreach (var item in kurs)
            {
                Group.Items.Add(item.Name_Group);
            }
        }

        private async void Kurs3_Checked(object sender, RoutedEventArgs e)
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
            Group.Items.Clear();
            foreach (var item in kurs)
            {
                Group.Items.Add(item.Name_Group);
            }
        }

        private async void Kurs4_Checked(object sender, RoutedEventArgs e)
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
            Group.Items.Clear();
            foreach (var item in kurs)
            {
                Group.Items.Add(item.Name_Group);
            }
        }

        private void subjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
