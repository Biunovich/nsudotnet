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
    class AESCrypt : Crypto
    {
        private static object LockObj = new object();
        public void Create_Key()
        {
            lock (LockObj)
            {
                using (AesCryptoServiceProvider AES = new AesCryptoServiceProvider())
                {
                    File.WriteAllText("AESIV.txt", Convert.ToBase64String(AES.IV));
                    File.WriteAllText("AESKEY.txt", Convert.ToBase64String(AES.Key));
                }
            }
        }

        public string Encrypt_Data(string DataToEncrypt)
        {
            lock (LockObj)
            {
                if (File.Exists("AESKEY.txt") && File.Exists("AESIV.txt"))
                {
                    using (AesCryptoServiceProvider AES = new AesCryptoServiceProvider())
                    {
                        AES.Key = Convert.FromBase64String(File.ReadAllText("AESKEY.txt"));
                        AES.IV = Convert.FromBase64String(File.ReadAllText("AESIV.txt"));
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
        }

        public string Decrypt_Data(string p)
        {
            lock (LockObj)
            {
                if (File.Exists("AESKEY.txt") && File.Exists("AESIV.txt"))
                {
                    using (AesCryptoServiceProvider AES = new AesCryptoServiceProvider())
                    {
                        byte[] EncryptedData = Convert.FromBase64String(p);
                        AES.Key = Convert.FromBase64String(File.ReadAllText("AESKEY.txt"));
                        AES.IV = Convert.FromBase64String(File.ReadAllText("AESIV.txt"));
                        ICryptoTransform decryptor = AES.CreateDecryptor(AES.Key, AES.IV);
                        using (MemoryStream msDecrypt = new MemoryStream(EncryptedData))
                        {
                            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
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
}
