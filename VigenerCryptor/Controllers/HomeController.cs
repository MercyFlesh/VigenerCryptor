using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using VigenerCryptor.Services;



namespace VigenerCryptor.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<string> CryptAsync(string text, string key, string mode, string langMode="ru")
        {
            switch (mode)
            {
                case ("encrypt"):
                    return await new Cryptor(Cryptor.alphabets[langMode]).Encrypt(text, key);
                case ("decrypt"):
                    return await new Cryptor(Cryptor.alphabets[langMode]).Decrypt(text, key);
                default:
                    throw new ArgumentException("invalid format");
            }
        }

        [HttpPost]
        public async Task<string> UploadFileAsync(IFormFile file)
        {
            string format = file.FileName.Split('.')[1];
            string text = "";

            switch (format)
            {
                case ("txt"):
                    text = await DocumentService.UploadTxt(file.OpenReadStream());
                    break;
                case ("docx"):
                    text = await DocumentService.UploadDocx(file.OpenReadStream());
                    break;
            }

            return text;
        }

        [HttpPost]
        public async Task<FileResult> DownloadAsync(string text, string filename, string format)
        {
            string fullName = $"{filename}.{format}";
            string contentType = DocumentService.formatsContentType[format];

            switch(format)
            {
                case ("txt"):
                    UnicodeEncoding uni = new UnicodeEncoding();
                    return File(uni.GetBytes(text), contentType, fullName);
                case ("docx"):
                    return File(await DocumentService.GetDocxBytes(text), contentType, fullName);
                default:
                    throw new ArgumentException("invalid format");
            }
        }
    }
}
