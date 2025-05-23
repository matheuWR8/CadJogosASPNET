﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CadJogosASPNET.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FazerLogin(string usuario, string senha)
        {
            //Este é apenas um exemplo, aqui você deve consultar na sua tabela de usuários
            //se existe esse usuário e senha
            if (usuario == "admin" && senha == "1234")
            {
                HttpContext.Session.SetString("Logado", "true");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Erro = "Usuário ou senha inválidos!";
                return View("Index");
            }
        }
        public IActionResult LogOff()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
