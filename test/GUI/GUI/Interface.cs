using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    public interface Crypto
    {
        void Create_Key();
        string Encrypt_Data(string DataToEncrypt);
        string Decrypt_Data(string Encrypt_Data);
    }
}
