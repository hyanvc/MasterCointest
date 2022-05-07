using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ContatoController : Controller
    {

        public static string InverteString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }


        public IActionResult Inverter()
        {
            return View();
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Criar()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Inverter(ContatoModel palavras)
        {
            string texto = palavras.Palavra;

            string texto1 = InverteString(texto);


            TempData["msg"] =  "Sua palavra invertida é: " +  texto1;





            return View();
        }

        [HttpPost]
        public IActionResult Criar(ContatoModel contato)
        {
            int idade = DateTime.Now.Year - contato.DatadeNascimento.Year;

            if
                (DateTime.Now.DayOfYear < contato.DatadeNascimento.DayOfYear);
            {
                idade = idade - 1;
            }

            if (contato.DatadeNascimento == DateTime.Now)
                TempData["msg"] = "Eba! Hoje é seu Aniversário!";

            else
                TempData["msg"] = "Sua idade é " + idade + "anos.";

            return View();   
            
        
        }

    }
}
