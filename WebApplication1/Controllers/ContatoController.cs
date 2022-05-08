using MasterCoinTest.Models;
using MasterCoinTest.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ContatoController : Controller
    {

        private readonly IContatoRepositorio _contatoRepositorio;
        public ContatoController(IContatoRepositorio contatoRepositorio)
        {
            _contatoRepositorio = contatoRepositorio;
        }

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

           List<ContatoTable> contatos =_contatoRepositorio.BuscarTodos();

            return View(contatos);
        }

        public IActionResult Criar()
        {
            return View();
        }


        public IActionResult CriarUsuario()
        {
            return View();
        }


        public IActionResult Cotacao()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Inverter(ContatoModel palavras)
        {
            string texto = palavras.Palavra;

            string texto1 = InverteString(texto);


            TempData["msg"] = "Sua palavra invertida é: " + texto1;

            return View();
        }

        [HttpPost]
        public IActionResult Criar(ContatoModel contato)
        {
            int idade = DateTime.Now.Year - contato.DatadeNascimento.Year;

            if
                (DateTime.Now.DayOfYear < contato.DatadeNascimento.DayOfYear)
            {
                idade = idade - 1;
            }

            if (contato.DatadeNascimento == DateTime.Now)
                TempData["msg"] = "Eba! Hoje é seu Aniversário!";

            else
                TempData["msg"] = "Sua idade é " + idade + "anos.";

            return View();

        }

        [HttpPost]
        public IActionResult Cotacao(ContatoModel cotacao)
        {
            string strURL = "https://api.hgbrasil.com/finance?array_limit=1&fields=only_results,USD&key=12234bfa";

            using (HttpClient client = new HttpClient())
            {

                var response = client.GetAsync(strURL).Result;

                if (response.IsSuccessStatusCode == true)
                {
                    var result = response.Content.ReadAsStringAsync().Result;

                    Market market = JsonConvert.DeserializeObject<Market>(result);

                    decimal real = decimal.Round(cotacao.Real / market.Currency.Buy, 3);

                    TempData["msg"] = "A cotação atual do dolar é : " + market.Currency.Buy + " e  o valor em reais convertido do dolar é : " + real;


                }

                return View();


            }
        }

        [HttpPost]
        public IActionResult Criar2(ContatoTable contato)
        {
            _contatoRepositorio.Adicionar(contato);

            return RedirectToAction("Index");

        }

           
    }
}
