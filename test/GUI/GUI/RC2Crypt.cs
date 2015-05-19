using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Security.Cryptography;
using System.IO;

namespace GUI
{
    class RC2Crypt : Crypto
    {
        private static object LockObj = new object();
        public void Create_Key()
        {
            lock (LockObj)
            {
                using (RC2CryptoServiceProvider RC2 = new RC2CryptoServiceProvider())
                {
                    File.WriteAllText("RC2KEY.txt", Convert.ToBase64String(RC2.Key));
                    File.WriteAllText("RC2IV.txt", Convert.ToBase64String(RC2.IV));
                }
            }
        }
        public string Encrypt_Data(string p)
        {
            lock (LockObj)
            {
                if (File.Exists("RC2KEY.txt") && File.Exists("RC2IV.txt"))
                {
                    using (RC2CryptoServiceProvider RC2 = new RC2CryptoServiceProvider())
                    {
                        RC2.Key = Convert.FromBase64String(File.ReadAllText("RC2KEY.txt"));
                        RC2.IV = Convert.FromBase64String(File.ReadAllText("RC2IV.txt"));
                        byte[] DataToEncrypt = Encoding.Unicode.GetBytes(p);
                        ICryptoTransform encryptor = RC2.CreateEncryptor(RC2.Key, RC2.IV);
                        using (MemoryStream msEncrypt = new MemoryStream())
                        {
                            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                            {
                                csEncrypt.Write(DataToEncrypt, 0, DataToEncrypt.Length);
                                csEncrypt.FlushFinalBlock();
                                return Convert.ToBase64String(msEncrypt.ToArray());
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("You must create keys", "Error");
                    return null;
                }
            }
        }

        public string Decrypt_Data(string p)
        {
            lock (LockObj)
            {
                if (File.Exists("RC2KEY.txt") && File.Exists("RC2IV.txt"))
                {
                    using (RC2CryptoServiceProvider RC2 = new RC2CryptoServiceProvider())
                    {
                        RC2.Key = Convert.FromBase64String(File.ReadAllText("RC2KEY.txt"));
                        RC2.IV = Convert.FromBase64String(File.ReadAllText("RC2IV.txt"));
                        byte[] EncryptedData = Convert.FromBase64String(p);
                        ICryptoTransform decryptor = RC2.CreateDecryptor(RC2.Key, RC2.IV);
                        using (MemoryStream msDecrypt = new MemoryStream(EncryptedData))
                        {
                            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                            {
                                StringBuilder roundtrip = new StringBuilder();
                                int b = 0;
                                do
                                {
                                    b = csDecrypt.ReadByte();
                                    if (b != -1)
                                    {
                                        roundtrip.Append((char)b);
                                    }
                                } while (b != -1);
                                return Encoding.Unicode.GetString(Encoding.ASCII.GetBytes(roundtrip.ToString()));
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("You must create keys", "Error");
                    return null;
                }
            }
        }
    }
}
