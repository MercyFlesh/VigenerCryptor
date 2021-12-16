using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Web;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VigenerCryptor.Models;
using VigenerCryptor.Services;
using System.Diagnostics;

namespace VigenerCryptor.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public string Crypt(CryptRequest request)
        {
            
            switch(request.Mode)
            {
                case (0):
                    return Cryptor.Encrypt(request.Text, request.Key);
                case (1):
                    return Cryptor.Decrypt(request.Text, request.Key);
                default:
                    return "";
            }
        }

        [HttpPost]
        public string UploadFile(IFormFile  file)
        {
            string format = file.FileName.Split('.')[1];
            string text = "";
            
            switch(format)
            {
                case ("txt"):
                    text = UploadTxt(file);
                    break;
                case ("docx"):
                    text = UploadDocx(file);
                    break;
            }

            return text;
        }

        [NonAction]
        private string UploadTxt(IFormFile file)
        {
            string result = "";
            using (StreamReader stream = new StreamReader(file.OpenReadStream()))
            {
                result = stream.ReadToEnd();
            }

            return result;
        }

        [NonAction]
        private string UploadDocx(IFormFile file)
        {
            string result = "";
            

            return result;
        }
    }
}
