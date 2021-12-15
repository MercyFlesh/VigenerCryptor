using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VigenerCryptor.Models
{
    public class CryptRequest
    {
        public string Key { set; get; }
        public string Text { set; get; }
    }
}
