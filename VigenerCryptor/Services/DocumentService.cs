﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace VigenerCryptor.Services
{
    public class DocumentService
    {
        public static Dictionary<string, string> formatsContentType = new Dictionary<string, string>()
        {
            {"txt", "text/plain"},
            {"docx",  "application/vnd.openxmlformats-officedocument.wordprocessingml.document"}
        };

        public async static Task<byte[]> GetDocxBytes(string text)
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                WordprocessingDocument doc = WordprocessingDocument.Create(memStream, WordprocessingDocumentType.Document, true);

                doc.AddMainDocumentPart().Document = new Document();
                var body = doc.MainDocumentPart.Document.AppendChild(new Body());

                await Task.Run(() =>
                {
                    foreach (string parag in text.Split("\r\n"))
                    {
                        var paragraph = body.AppendChild(new Paragraph());
                        var run = paragraph.AppendChild(new Run());
                        run.AppendChild(new Text(parag));
                    }
                });

                doc.Close();
                return memStream.ToArray();
            }
        }

        public async static Task<string> UploadTxt(Stream fileStream)
        {
            string result = "";
            using (StreamReader stream = new StreamReader(fileStream))
            {
               result = await Task.Run(stream.ReadToEnd);
            }

            return result;
        }

        public async static Task<string> UploadDocx(Stream fileStream)
        {
            List<string> result = new List<string>();

            using (WordprocessingDocument doc = WordprocessingDocument.Open(fileStream, false))
            {
                await Task.Run(() =>
                {
                    var paragraphs = doc.MainDocumentPart.RootElement.Descendants<Paragraph>();
                    foreach (var paragraph in paragraphs)
                    {
                        result.Add(paragraph.InnerText);
                    }
                });
            }

            return string.Join("\r\n", result);
        }
    }
}
