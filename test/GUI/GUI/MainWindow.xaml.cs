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
        Crypto Crypt_class_AES = new AESCrypt();
        Crypto Crypt_class_RSA = new RSACrypt();
        Crypto Crypt_class_RC2 = new RC2Crypt();
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
                        Base_Mode(Crypt_class_RSA);
                        break;
                    }
                case "AES":
                    {
                        Base_Mode(Crypt_class_AES);
                        break;
                    }
                case "RC2":
                    {
                        Base_Mode(Crypt_class_RC2);
                        break;
                    }
                default:
                    {
                        MessageBox.Show("You must choose algorithm.", "Error");
                        break;
                    }
            }
        }

        private void Base_Mode(Crypto Crypt_class)
        {
            switch (RadioButtonText)
            {
                case "Encrypt":
                    {
                        EncryptedData.Text = Crypt_class.Encrypt_Data(DataToEncrypt.Text);
                        break;
                    }
                case "Decrypt":
                    {
                        DecryptedData.Text = Crypt_class.Decrypt_Data(EncryptedData.Text);
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
                        Create_Key(Crypt_class_RSA);
                        break;
                    }
                case "AES":
                    {
                        Create_Key(Crypt_class_AES);
                        break;
                    }
                case "RC2":
                    {
                        Create_Key(Crypt_class_RC2);
                        break;
                    }
                default:
                    {
                        MessageBox.Show("You must choose algorithm.", "Error");
                        break;
                    }
        }
        }

        private void Create_Key(Crypto Crypt_class)
        {
            Crypt_class.Create_Key();
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
    }
}
