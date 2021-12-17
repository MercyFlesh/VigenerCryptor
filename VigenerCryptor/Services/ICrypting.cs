using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VigenerCryptor.Services
{
    public interface ICrypting
    {
        Task<string> Encrypt(string text, string key);
        Task<string> Decrypt(string text, string key);
    }
}
