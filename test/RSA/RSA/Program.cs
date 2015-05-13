using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace RSA
{
    class Program
    {
        static void Main(string[] argv)
        {
            byte[] encryptedData;
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                switch (argv[0])
                {
                    case "Encrypt":
                        string str = File.ReadAllText("DataToEncrypt.txt",Encoding.GetEncoding(1251));
                        byte[] dataToEncrypt = Encoding.Unicode.GetBytes(str);
                        File.WriteAllText("RSAKEY.xml",RSA.ToXmlString(true));
                        RSAEncrypt(dataToEncrypt, RSA.ExportParameters(false), false);
                        break;
                    case "Decrypt":
                        string rsaKey = File.ReadAllText("RSAKEY.xml");
                        RSA.FromXmlString(rsaKey);
                        encryptedData = Convert.FromBase64String(File.ReadAllText("EncryptedData.txt"));
                        RSADecrypt(encryptedData, RSA.ExportParameters(true), false);
                        break;
                    default :
                        throw new ArgumentException();
                }
            }
        }

        private static void RSADecrypt(byte[] encryptedData, RSAParameters rSAParameters, bool p)
        {
            byte[] decryptedData;
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSA.ImportParameters(rSAParameters);
                decryptedData = RSA.Decrypt(encryptedData, p);
                File.WriteAllText("DecryptedData.txt", Encoding.Unicode.GetString(decryptedData));
            }
        }

        private static void RSAEncrypt(byte[] dataToEncrypt, RSAParameters rSAParameters, bool p)
        {
            byte[] encryptedData;
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSA.ImportParameters(rSAParameters);
                encryptedData = RSA.Encrypt(dataToEncrypt, p);
                File.WriteAllText("EncryptedData.txt", Convert.ToBase64String(encryptedData));
            }
        }
    }
}
