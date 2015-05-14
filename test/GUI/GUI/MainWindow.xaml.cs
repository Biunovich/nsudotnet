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
using System.IO;

namespace GUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string RadioButtonText;
        string RSAPubKey;
        string RadioButtonMode;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void EncryptDecrypt_Button(object sender, RoutedEventArgs e)
        {
            switch (RadioButtonMode)
            {
                case "RSA":
                    {
                        RSA_Mode();
                        break;
                    }
                case "AES":
                    {
                        AES_Mode();
                        break;
                    }
                case "RC2":
                    {
                        RC2_Mode();
                        break;
                    }
                default:
                    {
                        MessageBox.Show("You must choose algorithm.", "Error");
                        break;
                    }
            }
        }

        private void RC2_Mode()
        {
            switch(RadioButtonText)
            {
                case "Encrypt":
                    {
                        EncryptedData.Text = RC2Crypt.Encrypt_Data(DataToEncrypt.Text);
                        break;
                    }
                case "Decrypt":
                    {
                        DecryptedData.Text = RC2Crypt.Decrypt_Data(EncryptedData.Text);
                        break;
                    }
                default:
                    {
                        MessageBox.Show("You must choose mode", "Error");
                        break;
                    }
            }
        }

        private void AES_Mode()
        {
            switch (RadioButtonText)
            {
                case "Encrypt":
                    {
                        EncryptedData.Text = AESCrypt.Encrypt_Data(DataToEncrypt.Text);
                        break;
                    }
                case "Decrypt":
                    {
                        DecryptedData.Text = AESCrypt.Decrypt_Data(EncryptedData.Text);
                        break;
                    }
                default:
                    {
                        MessageBox.Show("You must choose mode", "Error");
                        break;
                    }
            }
        }
        private void CreateKey_Button(object sender, RoutedEventArgs e)
        {
            switch (RadioButtonMode) {
                case "RSA":
                    {
                        RSACrypt.Create_Key();
                        break;
                    }
                case "AES":
                    {
                        AESCrypt.Create_Keys();
                        break;
                    }
                case "RC2":
                    {
                        RC2Crypt.Create_Key();
                        break;
                    }
                default:
                    {
                        MessageBox.Show("You must choose algorithm.", "Error");
                        break;
                    }
        }
        }
        private void Encrypt(object sender, RoutedEventArgs e)
        {
            var button = sender as RadioButton;
            RadioButtonText = button.Content.ToString();
        }

        private void ImportPubKey_Button(object sender, RoutedEventArgs e)
        {
            if (File.Exists(ImportPubKey.Text))
            {
                RSAPubKey = ImportPubKey.Text;
            }
            else
            {
                MessageBox.Show("Public key not found", "Error");
            }
        }

        private void EncryptWithPubKey_Button(object sender, RoutedEventArgs e)
        {
            if (RadioButtonMode == "RSA")
                EncryptedData.Text = RSACrypt.EncryptWithPubKey(RSAPubKey, Encoding.Unicode.GetBytes(DataToEncrypt.Text));
            else
            {
                MessageBox.Show("You can use this type encryption only with RSA algorithm.", "Error");
            }
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

        private void Algorithm(object sender, RoutedEventArgs e)
        {
            var button = sender as RadioButton;
            RadioButtonMode = button.Content.ToString();
        }
        private void RSA_Mode()
        {
            switch (RadioButtonText)
            {
                case "Encrypt":
                    {
                        EncryptedData.Text = RSACrypt.Encrypt_Text(Encoding.Unicode.GetBytes(DataToEncrypt.Text));
                        break;
                    }
                case "Decrypt":
                    {
                        DecryptedData.Text = RSACrypt.Decrypt_Text(Convert.FromBase64String(EncryptedData.Text));
                        break;
                    }
                default:
                    {
                        MessageBox.Show("You must choose mode.", "Error");
                        break;
                    }
            }
        }
    }
}
