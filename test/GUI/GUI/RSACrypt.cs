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
    class RSACrypt : Crypto
    {
        private static object LockObj = new object();
        public void Create_Key()
        {
            lock (LockObj)
            {
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    File.WriteAllText("RSAKEY.xml", RSA.ToXmlString(true));
                    File.WriteAllText("RSAPUBKEY.xml", RSA.ToXmlString(false));
                }
            }
        }
        public string Encrypt_Data(string Data)
        {
            lock (LockObj)
            {
                if (File.Exists("RSAKEY.xml"))
                {
                    using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                    {
                        byte[] DataToEncrypt = Encoding.Unicode.GetBytes(Data);
                        string rsaKey = File.ReadAllText("RSAKEY.xml");
                        RSA.FromXmlString(rsaKey);
                        return RSAEncrypt(DataToEncrypt, RSA.ExportParameters(false), false);
                    }
                }
                else
                {
                    MessageBox.Show("Key not found.", "Error");
                    return null;
                }
            }
        }
        private static string RSAEncrypt(byte[] dataToEncrypt, RSAParameters rSAParameters, bool p)
        {
            byte[] encryptedData;
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSA.ImportParameters(rSAParameters);
                encryptedData = RSA.Encrypt(dataToEncrypt, p);
                return Convert.ToBase64String(encryptedData);
            }
        }

        public string Decrypt_Data(string Data)
        {
            lock (LockObj)
            {
                if (File.Exists("RSAKEY.xml"))
                {
                    using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                    {
                        byte[] encryptedData = Convert.FromBase64String(Data);
                        string rsaKey = File.ReadAllText("RSAKEY.xml");
                        RSA.FromXmlString(rsaKey);
                        return RSADecrypt(encryptedData, RSA.ExportParameters(true), false);
                    }
                }
                else
                {
                    MessageBox.Show("Key not found.", "Error");
                    return null;
                }
            }
        }
        private static string RSADecrypt(byte[] encryptedData, RSAParameters rSAParameters, bool p)
        {
            byte[] decryptedData;
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSA.ImportParameters(rSAParameters);
                decryptedData = RSA.Decrypt(encryptedData, p);
                return Encoding.Unicode.GetString(decryptedData);
            }
        }
        public static string EncryptWithPubKey(string pathPubKey, byte[] DataToEncrypt)
        {
            lock (LockObj)
            {
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    string PubKey = File.ReadAllText(pathPubKey);
                    RSA.FromXmlString(PubKey);
                    return RSAEncrypt(DataToEncrypt, RSA.ExportParameters(false), false);
                }
            }
        }
    }
}
