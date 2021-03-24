using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
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

    public class Dis
    {
        public string Subjucts { get; set; }
    }

    public sealed partial class AdminPage : Page
    {
        StorageFolder folder = ApplicationData.Current.LocalFolder;
        string str;
        StorageFile file;
        List<Dis> DisList;
        XmlSerializer serializer = new XmlSerializer(typeof(List<Dis>));
        XmlSerializer serializer1 = new XmlSerializer(typeof(List<UserData>));
        TextBox name;
        TextBox textBox;
        List<UserData> userDatas;

        public AdminPage()
        {
            this.InitializeComponent();
        }
        private async void Delate_Click(object sender, RoutedEventArgs e)
        {
            if (text.SelectedIndex != -1)
            {
                DisList.RemoveAt(text.SelectedIndex);
                text.Items.RemoveAt(text.SelectedIndex);

                file = await folder.CreateFileAsync("Dis.xml", CreationCollisionOption.ReplaceExisting);

                using (var stream = await file.OpenStreamForWriteAsync())
                {
                    serializer.Serialize(stream, DisList);
                }
            }

        }

        private void text_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
        private async void Add_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog content = new ContentDialog();
            name = new TextBox();
            content.Content = name;
            content.Title = "Название дисциплины";
            content.CloseButtonText = "Выйти";
            content.PrimaryButtonText = "Добаваить дисциплину";
            content.PrimaryButtonClick += async (es, s) =>
            {
                text.Items.Add(name.Text);

                file = await folder.CreateFileAsync("Dis.xml", CreationCollisionOption.OpenIfExists);

                Dis disclass = new Dis() { Subjucts = name.Text };

                DisList.Add(disclass);


                using (var stream = await file.OpenStreamForWriteAsync())
                {
                    serializer.Serialize(stream, DisList);
                }
            };
            await content.ShowAsync();


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
                text.Items.Add(item.Subjucts);
            }


            StorageFile file1 = await folder.CreateFileAsync("db.xml", CreationCollisionOption.OpenIfExists);

            XmlSerializer xml = new XmlSerializer(typeof(List<UserData>));

            try
            {
                using (Stream stream = await file1.OpenStreamForReadAsync())
                {
                    userDatas = (List<UserData>)xml.Deserialize(stream);
                    DataGrid.ItemsSource = userDatas;
                }

            }
            catch (Exception ex)
            {
                MessageDialog msg = new MessageDialog(ex.Message);
                await msg.ShowAsync();
            }


        }

        private async void Rename_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog content = new ContentDialog();
            textBox = new TextBox();
            content.Content = textBox;
            content.Title = "Новое название дисциплины";
            content.CloseButtonText = "Выйти";
            content.PrimaryButtonText = "Изменить";
            content.PrimaryButtonClick += async (es, s) =>
            {
                if (text.SelectedIndex != -1)
                {
                    DisList[text.SelectedIndex].Subjucts = textBox.Text;
                    text.Items[text.SelectedIndex] = textBox.Text;

                    file = await folder.CreateFileAsync("Dis.xml", CreationCollisionOption.ReplaceExisting);

                    using (var stream = await file.OpenStreamForWriteAsync())
                    {
                        serializer.Serialize(stream, DisList);
                    }
                }
            };
            await content.ShowAsync();
        }

        private void Cancel_click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
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

        private async void Add_user_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog content = new ContentDialog();
            TextBox[] textBoxes = new TextBox[7];
            StackPanel stack = new StackPanel();
            ComboBox box = new ComboBox();
            ComboBox combo = new ComboBox();
            box.Items.Add("Студент");
            box.Items.Add("Преподаватель");
            box.Margin = new Thickness(10, 10, 10, 10);
            combo.Margin = new Thickness(10, 10, 10, 10);
            stack.Orientation = Orientation.Vertical;
            box.HorizontalAlignment = HorizontalAlignment.Center;
            combo.HorizontalAlignment = HorizontalAlignment.Center;
            foreach (var item in DisList)
            {
                combo.Items.Add(item.Subjucts);
            }

            for (int i = 0; i < 7; i++)
            {
                textBoxes[i] = new TextBox();
                textBoxes[i].Margin = new Thickness(10, 10, 10, 10);
                stack.Children.Add(textBoxes[i]);
            }

            textBoxes[4].Visibility = Visibility.Collapsed;
            textBoxes[5].Visibility = Visibility.Collapsed;


            stack.Children.Add(box);
            stack.Children.Add(combo);

            textBoxes[1].PlaceholderText = "Имя";
            textBoxes[0].PlaceholderText = "Фамилия";
            textBoxes[2].PlaceholderText = "Отчество";
            textBoxes[3].PlaceholderText = "Логин";
            textBoxes[4].PlaceholderText = "Пароль";
            textBoxes[5].PlaceholderText = "Группа";
            textBoxes[6].PlaceholderText = "Курс";
            textBoxes[3].Name = "Passwodr";
            box.SelectionChanged += (es, s) =>
            {
                if (box.SelectedIndex == 0)
                {
                    textBoxes[3].Visibility = Visibility.Visible;
                    textBoxes[4].Visibility = Visibility.Visible;
                    combo.Visibility = Visibility.Collapsed;
                }
                else
                {
                    textBoxes[3].Visibility = Visibility.Collapsed;
                    textBoxes[4].Visibility = Visibility.Collapsed;
                    combo.Visibility = Visibility.Visible;
                }
            };
            content.Content = stack;
            content.Title = "Добавление пользователя";
            content.CloseButtonText = "Выйти";
            content.PrimaryButtonText = "Добаваить ";
            content.PrimaryButtonClick += async (es, s) =>
            {


                file = await folder.CreateFileAsync("db.xml", CreationCollisionOption.OpenIfExists);
                if (box.SelectedIndex != 0)
                {
                    textBoxes[3].Text = "";
                    textBoxes[4].Text = "";
                }

                UserData user = new UserData()
                {

                    AccountType = box.SelectedIndex,
                    Name = textBoxes[1].Text,
                    Surname = textBoxes[0].Text,
                    LastName = textBoxes[2].Text,
                    Group = textBoxes[4].Text,
                    Login = textBoxes[5].Text,
                    Kurs = textBoxes[3].Text,
                    Password = GetMd5Hash(textBoxes[6].Text)


                };
                if (box.SelectedIndex == 1)
                {
                    user.Subjects = combo.Items[combo.SelectedIndex].ToString();
                }
                else
                {
                    user.Subjects = "";
                }
                textBoxes[0].Text = textBoxes[0].Text.Replace(" ", "");
                textBoxes[1].Text = textBoxes[1].Text.Replace(" ", "");
                textBoxes[2].Text = textBoxes[2].Text.Replace(" ", "");
                textBoxes[3].Text = textBoxes[3].Text.Replace(" ", "");
                textBoxes[4].Text = textBoxes[4].Text.Replace(" ", "");
                textBoxes[5].Text = textBoxes[5].Text.Replace(" ", "");
                textBoxes[5].Text = textBoxes[5].Text.Replace(" ", "");

                if (textBoxes[0].Text != "" && textBoxes[1].Text != "" && textBoxes[5].Text != "" && textBoxes[6].Text != "" && box.SelectedIndex != -1)
                {
                    if ((box.SelectedIndex == 0 && textBoxes[4].Text != "" && textBoxes[3].Text != "") || (box.SelectedIndex == 1 && textBoxes[4].Text == "" && textBoxes[3].Text == ""))
                    {
                        userDatas.Add(user);
                        using (var stream = await file.OpenStreamForWriteAsync())
                        {
                            serializer1.Serialize(stream, userDatas);

                            DataGrid.ItemsSource = new List<UserData>();
                            DataGrid.ItemsSource = userDatas;


                        }
                    }
                }
                else
                {

                }



            };
            await content.ShowAsync();


        }

        private async void Delate_user_Click(object sender, RoutedEventArgs e)
        {
            int test = DataGrid.SelectedIndex;
            if (DataGrid.SelectedIndex != -1)
            {
                userDatas.RemoveAt(DataGrid.SelectedIndex);
                DataGrid.ItemsSource = new List<UserData>();
                DataGrid.ItemsSource = userDatas;
                file = await folder.CreateFileAsync("db.xml", CreationCollisionOption.ReplaceExisting);
                using (var stream = await file.OpenStreamForWriteAsync())
                {
                    serializer1.Serialize(stream, userDatas);
                }
            }


        }

        private void DataGrid_BeginningEdit(object sender, Microsoft.Toolkit.Uwp.UI.Controls.DataGridBeginningEditEventArgs e)
        {
            var itemPassword = DataGrid.SelectedItem as UserData;
            if (e.Column.Header.ToString() == "Password")
            {
                savePass = itemPassword.Password;
                itemPassword.Password = "";
            }
            if (e.Column.Header.ToString() == "AccountType")
            {
                saveId = itemPassword.AccountType.ToString();
            }
            if (e.Column.Header.ToString() == "Name") { saveName = itemPassword.Name.ToString(); }
            if (e.Column.Header.ToString() == "Login") { saveLog = itemPassword.Login.ToString(); }
            if (e.Column.Header.ToString() == "Surname") { saveSur = itemPassword.Surname.ToString(); }
            if (e.Column.Header.ToString() == "Group") { saveGrup = itemPassword.Group.ToString(); }
            if (e.Column.Header.ToString() == "Kurs") { saveKurs = itemPassword.Kurs.ToString(); }
            if (e.Column.Header.ToString() == "Subjects") { savesubjects = itemPassword.Subjects.ToString(); }
            if (e.Column.Header.ToString() == "LastName") { saveLast = itemPassword.LastName.ToString(); }
        }
        string savePass = "";
        string saveId = "";
        string saveName = "";
        string saveSur = "";
        string saveLog = "";
        string saveGrup = "";
        string saveKurs = "";
        string savesubjects = "";
        string saveLast = "";
        private async void DataGrid_CellEditEnded(object sender, Microsoft.Toolkit.Uwp.UI.Controls.DataGridCellEditEndedEventArgs e)
        {

            var itemPassword = DataGrid.SelectedItem as UserData;
            itemPassword.Password = itemPassword.Password.Replace(" ", "");
            if (itemPassword.Password == "")
            {

                itemPassword.Password = savePass;
            }


            if (e.Column.Header.ToString() == "Password" && savePass != itemPassword.Password)
            {

                itemPassword.Password = GetMd5Hash(itemPassword.Password);
                userDatas[DataGrid.SelectedIndex].Password = itemPassword.Password;

            }
            if (e.Column.Header.ToString() == "Group" && itemPassword.AccountType == 1 || itemPassword.AccountType == 2)
            {
                itemPassword.Group = "";
            }
            if (e.Column.Header.ToString() == "Subjects" )
            {
                itemPassword.Subjects = savesubjects;
            }
            if (e.Column.Header.ToString() == "Kurs" && itemPassword.AccountType == 1 || itemPassword.AccountType == 2)
            {
                itemPassword.Kurs = "";
            }
            if (e.Column.Header.ToString() == "AccountType")
            {
                itemPassword.AccountType = Convert.ToInt32(saveId);
            }

            switch (e.Column.Header)
            {

                case "Group":
                    {
                        itemPassword.Group = itemPassword.Group.Replace(" ", "");
                        if (itemPassword.Group == "")
                        {
                            itemPassword.Group = saveGrup;
                        }
                        userDatas[DataGrid.SelectedIndex].Group = itemPassword.Group;
                    }
                    break;

                case "Login":
                    {
                        itemPassword.Login = itemPassword.Login.Replace(" ", "");
                        if (itemPassword.Login == "")
                        {
                            itemPassword.Login = saveLog;
                        }
                        userDatas[DataGrid.SelectedIndex].Login = itemPassword.Login;
                    }
                    break;
                case "Kurs":
                    {
                        itemPassword.Kurs = itemPassword.Kurs.Replace(" ", "");
                        if (itemPassword.Kurs == "")
                        {
                            itemPassword.Kurs = saveKurs;
                        }
                        userDatas[DataGrid.SelectedIndex].Kurs = itemPassword.Kurs;
                    }
                    break;
               

                case "Name":
                    {
                        itemPassword.Name = itemPassword.Name.Replace(" ", "");
                        if (itemPassword.Name == "")
                        {
                            itemPassword.Name = saveName;
                        }
                        userDatas[DataGrid.SelectedIndex].Name = itemPassword.Name;
                    }
                    break;

                case "Surname":
                    {
                        itemPassword.Surname = itemPassword.Surname.Replace(" ", "");
                        if (itemPassword.Surname == "")
                        {
                            itemPassword.Surname = saveSur;
                        }
                        userDatas[DataGrid.SelectedIndex].Surname = itemPassword.Surname;
                    }
                    break;
                case "LastName":
                    {
                        itemPassword.LastName = itemPassword.LastName.Replace(" ", "");
                        if (itemPassword.LastName == "")
                        {
                            itemPassword.LastName = saveLast;
                        }
                        userDatas[DataGrid.SelectedIndex].LastName = itemPassword.LastName;
                    }
                    break;
            }
            file = await folder.CreateFileAsync("db.xml", CreationCollisionOption.ReplaceExisting);
            using (var stream = await file.OpenStreamForWriteAsync())
            {
                serializer1.Serialize(stream, userDatas);




            }

        }

        private void PivotItem_Tapped(object sender, RoutedEventArgs e)
        {
            Frame frame = new Frame();
            Groups.Content = frame;
            frame.Navigate(typeof(Group));
        }
    }
}

