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
    class RC2Crypt
    {
        public static void Create_Key()
        {
            using (RC2CryptoServiceProvider RC2 = new RC2CryptoServiceProvider())
            {
                File.WriteAllText("RC2KEY.xml", Convert.ToBase64String(RC2.Key));
                File.WriteAllText("RC2IV.xml", Convert.ToBase64String(RC2.IV));
            }
        }
        internal static string Encrypt_Data(string p)
        {
            if (File.Exists("RC2KEY.xml") && File.Exists("RC2IV.xml"))
            {
                using (RC2CryptoServiceProvider RC2 = new RC2CryptoServiceProvider())
                {
                    RC2.Key = Convert.FromBase64String(File.ReadAllText("RC2KEY.xml"));
                    RC2.IV = Convert.FromBase64String(File.ReadAllText("RC2IV.xml"));
                    byte[] DataToEncrypt = Encoding.Unicode.GetBytes(p);
                    ICryptoTransform encryptor = RC2.CreateEncryptor(RC2.Key, RC2.IV);
                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt,encryptor,CryptoStreamMode.Write))
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

        internal static string Decrypt_Data(string p)
        {
            if (File.Exists("RC2KEY.xml") && File.Exists("RC2IV.xml"))
            {
                using (RC2CryptoServiceProvider RC2 = new RC2CryptoServiceProvider())
                {
                    RC2.Key = Convert.FromBase64String(File.ReadAllText("RC2KEY.xml"));
                    RC2.IV = Convert.FromBase64String(File.ReadAllText("RC2IV.xml"));
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
