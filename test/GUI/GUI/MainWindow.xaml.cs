using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string RadioButtonText;
        string RSAPubKey;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void EncryptDecrypt_Button(object sender, RoutedEventArgs e)
        {
            if (RadioButtonText=="Encrypt")
            {
                EncryptedData.Text = RSACrypt.Encrypt_Text(Encoding.Unicode.GetBytes(DataToEncrypt.Text));
            }
            else if (RadioButtonText=="Decrypt")
            {
                DecryptedData.Text = RSACrypt.Decrypt_Text(Convert.FromBase64String(EncryptedData.Text));
            }
        }
        private void CreateKey_Button(object sender, RoutedEventArgs e)
        {
            RSACrypt.Create_Key();
        }
        private void Encrypt(object sender, RoutedEventArgs e)
        {
            var button = sender as RadioButton;
            RadioButtonText = button.Content.ToString();
        }

        private void ImportPubKey_Button(object sender, RoutedEventArgs e)
        {
            RSAPubKey = ImportPubKey.Text;
        }

        private void EncryptWithPubKey_Button(object sender, RoutedEventArgs e)
        {
            EncryptedData.Text = RSACrypt.EncryptWithPubKey(RSAPubKey, Encoding.Unicode.GetBytes(DataToEncrypt.Text));
        }

        private void DataToEncrypt_Box(object sender, TextChangedEventArgs e)
        {

        }

        private void DecryptedData_Box(object sender, TextChangedEventArgs e)
        {

        }

        private void EncryptedData_Box(object sender, TextChangedEventArgs e)
        {

        }
    }
}
