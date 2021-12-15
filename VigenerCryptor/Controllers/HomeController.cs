using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            return Cryptor.Decrypt(request.Text, request.Key);
        }
    }
}
