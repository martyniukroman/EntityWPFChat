using EntityWPFChat.models;
using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Timer = System.Threading.Timer;

namespace EntityWPFChat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    ///

    public class AligmentConverter : IMultiValueConverter {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {

            if ((values[0] as Person)?.Name == (values[1] as Person)?.Name) {
                return HorizontalAlignment.Right;
            }
            else {
                return HorizontalAlignment.Left;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    public partial class MainWindow : MetroWindow {

        public ModelContext db = new ModelContext();
        public static object locker = new object();
        public Person validPerson;

        public bool Valid = true;
        public bool Valid1 = true;
        public bool Valid2 = true;

        public string link;

        Person CurrentLoginedUser;
        Message message = new Message();

        public static string[] accentsArr = new string[] { "Red", "Green", "Blue", "Purple", "Orange", "Lime", "Emerald", "Teal", "Cyan", "Cobalt", "Indigo", "Violet", "Pink", "Magenta", "Crimson", "Amber", "Yellow", "Brown", "Olive", "Steel", "Mauve", "Taupe", "Sienna" };
        public static string[] colors = new string[] { "Red", "Green", "Blue", "Purple", "Orange", "Pink", "Brown", "Olive", "Black", "Gray", "Tomato", "Coral", "Navy", "Orchid", "Plum", "Peru", "Silver", "Tan", "Teal",  };

        public void UpdateUI(string Accent, string Theme) {
            ThemeManager.ChangeAppStyle(this,
                                   ThemeManager.GetAccent(Accent),
                                   ThemeManager.GetAppTheme(Theme));
        }
        
        public void UpdateContent() {

            ProgressRingLoading.IsActive = true;
         //   this.Title = CurrentLoginedUser.Name;
           // System.Threading.Thread.Sleep(1000);

            try {
              ListViewMessages.ItemsSource = db.Messages.ToList();
              UsersFrom.ItemsSource = db.People.ToList();
           //   UsersFromDelete.ItemsSource = db.People.ToList();

                try {
                    UsersFrom.Columns[2].Visibility = Visibility.Collapsed;
                    UsersFrom.Columns[0].Visibility = Visibility.Collapsed;
                    UsersFrom.Columns[3].Visibility = Visibility.Collapsed;
                    // UsersFromDelete.Columns[2].Visibility = Visibility.Collapsed;
                }
                catch (Exception) {
                    
                }

            }
            catch (Exception) {
               
            }
            finally {
                Scroll.ScrollToEnd();
                ProgressRingLoading.IsActive = false;
            }
        }

        public MainWindow(){

            InitializeComponent();
            FlyOutRegistr.IsOpen = true;

            UpdateContent();

            validPerson = new Person();
            this.DataContext = validPerson;
            DropDownAccents.ItemsSource = accentsArr;

            DataGridCommandsInfo.ItemsSource = colors;

            ImageSource aaa = new BitmapImage(new Uri(@"https://www.pixel-creation.com/wp-content/uploads/dragon-full-hd-wallpaper-and-background-image-1920x1080-id441572-1.jpg", UriKind.Absolute));

            GridMain.Background = new ImageBrush(aaa);

            System.Threading.Thread.Sleep(1000);
            CurrentLoginedUser = null;
            UpdateContent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {

            Color tmpcolor = ColorPicker.Color;

            message.MessageContent = null;
            message.PictureLink = null;
            message.Color = null;

            string tmp = TextBoxMain.Text;

            if (CurrentLoginedUser == null) {
                this.ShowMessageAsync("Error", "You must login before sending message");
                return;
            }

            UsersFrom.SelectedItem = CurrentLoginedUser;

            message.Sender = (UsersFrom.SelectedItem as Person);
            message.Receiver = UsersFrom.SelectedItem as Person;
            message.dateTime = DateTime.Now;
            message.PictureLink = link;
            message.Color = ColorPicker.Color.ToString();

            CurrentLoginedUser.MessageCount++;

            //if (TextBoxMain.Text.ToLower().Contains("pink") {
            //  char.IsLetter(TextBoxMain.Text.IndexOf("!") + 1
            //}
            string res = "";
            foreach (var item in colors) {
                if (TextBoxMain.Text.Contains("!" + item.ToLower())) {
                    message.Color = item;
                }
                //if (char.IsLetter(TextBoxMain.Text[TextBoxMain.Text.IndexOf("!") + 1]) == true) { // kostil
                  
                //}
            }

            try {
                for (int i = 0; i < tmp.Length; i++) {
                    if (tmp[i] == '!' && char.IsLetter(tmp[i + 1]))
                        continue;
                    else
                        res += tmp[i];
                }
            }
            catch (Exception) {

            }

            if (TextBoxMain.Text.Length <= 1 || TextBoxMain.Text == null) {
                this.ShowMessageAsync("Error", "You wrote nothing");
                return;
            }


            //foreach(char c in tmp) {
            //    if (c == '!')
            //        continue;
            //    else
            //        res += c;
            //}


            message.MessageContent = res;

            db.Messages.Add(message);
            db.SaveChanges();

            TextBoxMain.Clear();
            link = string.Empty;
            ButtonPhoto.Content = "🔗";
            ColorPicker.Color = tmpcolor;

            UpdateContent();
        }

        private void MenuRegistr_Click(object sender, RoutedEventArgs e) {
            FlyOutRegistr.IsOpen = true;
        }

        private void MenuRemove_Click(object sender, RoutedEventArgs e) {
            LabelOnDeleteUserName.Content = CurrentLoginedUser.Name;
            FlyOutRemove.IsOpen = true;
        }

        private void TextBox_Error(object sender, ValidationErrorEventArgs e) {

            if (System.Windows.Controls.Validation.GetHasError(TextBoxNameRegistr))
                Valid = false;
            else
                Valid = true;
        }

        private void ButtonUserAdd_Click(object sender, RoutedEventArgs e) {

            try {
                if (TextBoxNameRegistr.Text == string.Empty || TextBoxNameRegistrPswd.Password == string.Empty) {
                    throw new Exception("Empty Fields");
                }
                foreach (Person item in UsersFrom.Items) {
                    if (item.Name == TextBoxNameRegistr.Text && TextBoxNameRegistrPswd.Password != item.Password) {
                        throw new Exception("This name is already taken or password is invalid");
                    }
                }

                foreach (Person item in UsersFrom.Items) {
                    if (TextBoxNameRegistr.Text == item.Name && TextBoxNameRegistrPswd.Password == item.Password) {
                        CurrentLoginedUser = item;
                        UpdateContent();
                        TextBoxNameRegistr.Clear();
                        TextBoxNameRegistrPswd.Clear();
                        UsersFrom.SelectedItem = CurrentLoginedUser;
                        FlyOutRegistr.IsOpen = false;
                        return;
                    }
                }

                if (Valid && Valid1) {
                    Person tmp = new Person();

                    tmp.Name = TextBoxNameRegistr.Text;
                    tmp.Password = TextBoxNameRegistrPswd.Password;
                    CurrentLoginedUser = tmp;
                    db.People.Add(tmp);
                    db.SaveChanges();

                    UpdateContent();
                    TextBoxNameRegistr.Clear();
                    TextBoxNameRegistrPswd.Clear();
                    UsersFrom.SelectedItem = CurrentLoginedUser;
                    FlyOutRegistr.IsOpen = false;
                }
                else {
                    throw new Exception("Validation error");
                }

            }
            catch (Exception ex) {
                this.ShowMessageAsync("Error", ex.Message);
            }

            
        }

        private void TextBoxNameRegistr_TextChanged(object sender, TextChangedEventArgs e) {       

            if (System.Windows.Controls.Validation.GetHasError(TextBoxNameRegistr))  
                 Valid = false;
            else
                Valid = true;
        }

        private void ButtonUserRemove_Click(object sender, RoutedEventArgs e) {
             
            try {

                UpdateContent();

                if (CurrentLoginedUser.Password == TextBoxConfirmPass.Password) {

                    foreach (Message item in db.Messages) { // ex
                   //         MessageBox.Show(item.Sender.Name, CurrentLoginedUser.Name);
                        if (item.Sender.Name == CurrentLoginedUser.Name) {
                //            MessageBox.Show("Within if", CurrentLoginedUser.Name);
                            db.Messages.Remove(item);
                        }
                    }

                    db.People.Remove(CurrentLoginedUser);
                    db.SaveChanges();
                    UpdateContent();
                    FlyOutRemove.IsOpen = false;
                }
                else {
                    this.ShowMessageAsync("Error", "You can not delete root user\nor password wasn't confirmed");
                }

            }
            catch (Exception ex) {
                this.ShowMessageAsync("Fatal Error", ex.Message);
            }
            finally {
                FlyOutRegistr.IsOpen = true;
            }

           
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e) {
            UpdateContent();
        }

        private void MenuSettings_Click(object sender, RoutedEventArgs e) {
            FlyOutStyle.IsOpen = true;       
        }

        private void DropDownAccents_SelectionChanged(object sender, SelectionChangedEventArgs e) {

            if (ThemeSwitch.IsChecked == false)
                UpdateUI(DropDownAccents.SelectedItem.ToString(), "BaseDark");
            else
                UpdateUI(DropDownAccents.SelectedItem.ToString(), "BaseLight");
        }

        private void ToggleSwitch_Click(object sender, RoutedEventArgs e) {
            if (ThemeSwitch.IsChecked == false) 
                UpdateUI(DropDownAccents.SelectedItem.ToString(), "BaseDark");       
            else
                UpdateUI(DropDownAccents.SelectedItem.ToString(), "BaseLight");
        }

        private void TextBoxNameRegistrPswd_TextChanged(object sender, TextChangedEventArgs e) {

            if (System.Windows.Controls.Validation.GetHasError(TextBoxNameRegistrPswd))
                Valid = false;
            else
                Valid = true;
        }

        private void MetroWindow_SizeChanged(object sender, SizeChangedEventArgs e) {
            FlyOutRegistr.Width = this.ActualWidth;
            FlyOutRegistr.Height = this.ActualHeight;

            FatherWin.Tag = FatherWin.ActualWidth / 2;
            
        }

        private void MetroWindow_StateChanged(object sender, EventArgs e) {

            FlyOutRegistr.Width = this.ActualWidth;
            FlyOutRegistr.Height = this.ActualHeight;

        }

        private void ButtonPhoto_Click(object sender, RoutedEventArgs e) {
            FlyOutAttach.IsOpen = true;
        }

        private void TextBoxLinkToImage_TextChanged(object sender, TextChangedEventArgs e) {
            try {
                   ImageAttachPreview.Source = new BitmapImage(new Uri(TextBoxLinkToImage.Text));
            }
            catch (Exception) {

            }

        }

        private void ButtonAttach_Click(object sender, RoutedEventArgs e) {
            link = TextBoxLinkToImage.Text;
            ButtonPhoto.Foreground = new SolidColorBrush(ColorPicker.Color);
            TextBoxLinkToImage.Clear();
            ImageAttachPreview.Source = null;

            if (link != string.Empty)
                ButtonPhoto.Content += "*";
            else
                ButtonPhoto.Content = "🔗";

            FlyOutAttach.IsOpen = false;
            
        }

        private void ButtonAttachCencel_Click(object sender, RoutedEventArgs e) {
            link = null;
            TextBoxLinkToImage.Clear();
            ImageAttachPreview.Source = null;
            ColorPicker.Color = Color.FromRgb(46, 78, 132);
            ButtonPhoto.Content = "🔗";
            ButtonPhoto.Foreground = new SolidColorBrush(Colors.Gray);
            FlyOutAttach.IsOpen = false;

        }

        private void ButtonRestoreDefColor_Click(object sender, RoutedEventArgs e) {
            link = null;
            TextBoxLinkToImage.Clear();
            ImageAttachPreview.Source = null;
            ColorPicker.Color = Color.FromRgb(46, 78, 132);
            ButtonPhoto.Content = "🔗";
            ButtonPhoto.Foreground = new SolidColorBrush(Colors.Gray);
        }

        private void FlyOutStyle_MouseLeave(object sender, MouseEventArgs e) {
            (sender as Flyout).IsOpen = false;
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e) {
            db.Messages.Remove(ListViewMessages.SelectedItem as Message);
            ButtonDelete.Visibility = Visibility.Collapsed;

            CurrentLoginedUser.MessageCount--;
            UpdateContent();
            db.SaveChanges();
            UpdateContent();
        }

        private void ListViewMessages_SelectionChanged(object sender, SelectionChangedEventArgs e) {

            if ((ListViewMessages.SelectedItem as Message).Sender.Name == CurrentLoginedUser.Name) {
                ButtonDelete.Visibility = Visibility.Visible;
            }
            else {
                ButtonDelete.Visibility = Visibility.Collapsed;
            }

        }

        private void UsersFrom_SelectionChanged(object sender, SelectionChangedEventArgs e) {

            //int counter = 0;

            //foreach (Message item in db.Messages) {
            //    if ((UsersFrom.SelectedItem as Person).Name == item.Sender.Name) {
            //        counter++;
            //    }
            //}

        }

        private void TextBlockContent_MouseEnter(object sender, MouseEventArgs e) {
            if ((sender as TextBlock).Text.Contains("https://")) {
                this.Cursor = Cursors.Hand;
            }
        }

        private void TextBlockContent_MouseRightButtonDown(object sender, MouseButtonEventArgs e) {
            if ((sender as TextBlock).Text.Contains("https://")) {
                System.Diagnostics.Process.Start((sender as TextBlock).Text);
            }
        }

        private void TextBlockContent_MouseLeave(object sender, MouseEventArgs e) {
            this.Cursor = Cursors.Arrow;
        }
    }
}
