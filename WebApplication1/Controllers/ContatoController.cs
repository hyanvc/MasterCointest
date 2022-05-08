﻿using MasterCoinTest.Models;
using MasterCoinTest.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
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

        public static bool checkPalindrome(string mainString)
        {
            string firstHalf = mainString.Substring(0, mainString.Length / 2);
            char[] arr = mainString.ToCharArray();

            Array.Reverse(arr);

            string temp = new string(arr);
            string secondHalf = temp.Substring(0, temp.Length / 2);

            return firstHalf.Equals(secondHalf);
        }


        public IActionResult Inverter()
        {
            return View();
        }


        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Index1()
        {

            List<ContatoTable> contatos = _contatoRepositorio.BuscarTodos();

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

            bool texto2 = checkPalindrome(texto);

            if(texto2 == true)
            {
                TempData["palindromo"] = " sua palavra invertida é : " + texto1 + " e sua palavra é Palindromo!";
            }



            TempData["msg1"] = "Sua palavra invertida é: " + texto1;

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

            if (contato.Name.Contains("mc") ||
              (contato.Name.Contains("mastercoin")))
            {


                TempData["nome"] = ("Seu Nome de Usuario Nao Pode Conter  mc/mastercoin");
                return RedirectToAction("CriarUsuario");
            }

            Regex rx = new Regex("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])[a-zA-Z0-9]{6,32}$");
            Match match = rx.Match(contato.Senha);

            if (!match.Success)
            {


                TempData["nome"] = ("A senha deve conter, no mínimo, uma letra maiúscula, uma letra minúscula e um número A mesma não pode ter nenhum caractere de pontuação, acentuação ou espaço Além disso, a senha pode ter de 6 a 32 caracteres  ");
                return RedirectToAction("CriarUsuario");
            }
            _contatoRepositorio.Adicionar(contato);

            return RedirectToAction("Index1");

        }

           
    }
}
