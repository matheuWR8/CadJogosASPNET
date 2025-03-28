using CadJogosASPNET.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using CadJogosASPNET.DAO;

namespace CadJogosASPNET.Controllers
{
    public class JogoController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                JogoDAO dao = new JogoDAO();
                var lista = dao.Listar();
                return View("index", lista);
            }
            catch (Exception erro)
            {
                return View("error", new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult Create()
        {
            try
            {
                JogoViewModel jogo = new JogoViewModel();

                jogo.DataAquisicao = DateTime.Now;

                JogoDAO dao = new JogoDAO();
                jogo.Id = dao.ProximoId();

                return View("Form", jogo);
            }
            catch (Exception erro)
            {
                return View("error", new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult Salvar(JogoViewModel aluno)
        { 
            try
            {
                JogoDAO dao = new JogoDAO();
                if (dao.Consultar(aluno.Id) == null)
                    dao.Inserir(aluno);
                else
                    dao.Alterar(aluno); 
                return RedirectToAction("index");
            }
            catch (Exception erro)
            {
                return View("error", new ErrorViewModel(erro.ToString()));
            }

        }

        public IActionResult Edit(int id)
        {
            try
            {
                JogoDAO dao = new JogoDAO();
                JogoViewModel aluno = dao.Consultar(id);
                if (aluno == null)
                    return RedirectToAction("index");
                else
                    return View("form", aluno);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                JogoDAO dao = new JogoDAO();
                dao.Excluir(id);
                return RedirectToAction("index");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
    }
}
