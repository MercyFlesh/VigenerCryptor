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
using VigenerCryptor.Models;
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
        public string Crypt(CryptRequest request)
        {

            switch (request.Mode)
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
        public string UploadFile(IFormFile file)
        {
            string format = file.FileName.Split('.')[1];
            string text = "";

            switch (format)
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

        public static Dictionary<string, string> formatsContentType = new Dictionary<string, string>()
        {
            {"txt", "text/plain"},
            {"docx",  "application/vnd.openxmlformats-officedocument.wordprocessingml.document"}
        };

        [HttpPost]
        public FileResult Download(DownloadRequest request)
        {
            string filename = $"{request.FileName}.{request.Format}";
            string contentType = formatsContentType[request.Format];

            switch(request.Format)
            {
                case ("txt"):
                    UnicodeEncoding uni = new UnicodeEncoding();
                    return File(uni.GetBytes(request.Text), contentType, filename);
                case ("docx"):
                    return File(textToDocBytes(request.Text), contentType, filename);
                default:
                    throw new ArgumentException("invalid format");
            }
        }

        public byte[] textToDocBytes(string text)
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                WordprocessingDocument doc = WordprocessingDocument.Create(memStream, WordprocessingDocumentType.Document, true);
                
                doc.AddMainDocumentPart().Document = new Document();
                var body = doc.MainDocumentPart.Document.AppendChild(new Body());
                
                foreach(string parag in text.Split("\r\n"))
                {
                    var paragraph = body.AppendChild(new Paragraph());
                    var run = paragraph.AppendChild(new Run());
                    run.AppendChild(new Text(parag));
                }

                doc.Close();
                return memStream.ToArray();
            } 
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
            List<string> result = new List<string>();

            using (WordprocessingDocument doc = WordprocessingDocument.Open(file.OpenReadStream(), false))
            {
                var paragraphs = doc.MainDocumentPart.RootElement.Descendants<Paragraph>();
                foreach (var paragraph in paragraphs)
                {
                    result.Add(paragraph.InnerText);
                }
            }

            return string.Join("\r\n", result);
        }
    }
}
