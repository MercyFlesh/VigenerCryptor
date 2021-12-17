using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VigenerCryptor.Services
{
    public interface ICrypting
    {
        string Encrypt(string text, string key);
        string Decrypt(string text, string key);
    }
}
