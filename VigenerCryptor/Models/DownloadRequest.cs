using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VigenerCryptor.Models
{
    public class DownloadRequest
    {
        public string Text { set; get; }
        public string FileName { set; get; }
        public string Format { set; get; }
    }
}
