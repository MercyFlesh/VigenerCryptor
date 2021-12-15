using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VigenerCryptor.Services
{
    public interface ICrypting
    {
        string Encrypted();
        string Decrypted();
    }
}
