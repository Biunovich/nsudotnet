using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Windows;

namespace GUI
{
    class AESCrypt
    {
        public static void Create_Keys()
        {
            using (AesCryptoServiceProvider AES = new AesCryptoServiceProvider())
            {
                File.WriteAllText("AESIV.xml", Convert.ToBase64String(AES.IV));
                File.WriteAllText("AESKEY.xml", Convert.ToBase64String(AES.Key));
            }
        }

        internal static string Encrypt_Data(string DataToEncrypt)
        {
            if (File.Exists("AESKEY.xml") && File.Exists("AESIV.xml"))
            {
                using (AesCryptoServiceProvider AES = new AesCryptoServiceProvider())
                {
                    AES.Key = Convert.FromBase64String(File.ReadAllText("AESKEY.xml"));
                    AES.IV = Convert.FromBase64String(File.ReadAllText("AESIV.xml"));
                    ICryptoTransform encryptor = AES.CreateEncryptor(AES.Key, AES.IV);
                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(DataToEncrypt);
                            }
                            return Convert.ToBase64String(msEncrypt.ToArray());
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("You must create keys.", "Error");
                return null;
            }
        }

        internal static string Decrypt_Data(string p)
        {
            if (File.Exists("AESKEY.xml") && File.Exists("AESIV.xml"))
            {
                using (AesCryptoServiceProvider AES = new AesCryptoServiceProvider())
                {
                    byte[] EncryptedData = Convert.FromBase64String(p);
                    AES.Key = Convert.FromBase64String(File.ReadAllText("AESKEY.xml"));
                    AES.IV = Convert.FromBase64String(File.ReadAllText("AESIV.xml"));
                    ICryptoTransform decryptor = AES.CreateDecryptor(AES.Key, AES.IV);
                    using (MemoryStream msDecrypt = new MemoryStream(EncryptedData))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt,decryptor,CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                return srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("You must create keys.", "Error");
                return null;
            }
        }
    }
}
