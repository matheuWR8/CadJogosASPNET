using CadJogosASPNET.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using CadJogosASPNET.DAO;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Security.Cryptography.Xml;

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
                return View("Index", lista);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult Create()
        {
            try
            {
                ViewBag.Operacao = "I";
                JogoViewModel jogo = new JogoViewModel();
                PrepararListaCategorias();

                jogo.DataAquisicao = DateTime.Now;

                JogoDAO dao = new JogoDAO();
                jogo.Id = dao.ProximoId();

                return View("Form", jogo);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult Salvar(JogoViewModel jogo, string operacao)
        { 
            try
            {
                ValidarDados(jogo, operacao);
                if (!ModelState.IsValid)
                {
                    ViewBag.Operacao = operacao;
                    PrepararListaCategorias();
                    return View("Form", jogo);
                }

                JogoDAO dao = new JogoDAO();
                if (operacao == "I")
                    dao.Insert(jogo);
                else
                    dao.Update(jogo); 

                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }

        }

        public IActionResult Edit(int id)
        {
            try
            {
                ViewBag.Operacao = "A";
                JogoDAO dao = new JogoDAO();
                JogoViewModel jogo = dao.Consultar(id);
                PrepararListaCategorias();

                if (jogo == null)
                    return RedirectToAction("Index");
                else
                    return View("Form", jogo);
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
                dao.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        private void ValidarDados(JogoViewModel jogo, string operacao) 
        {
            ModelState.Clear();
            JogoDAO dao = new JogoDAO();

            if (jogo.Id <=0)
                ModelState.AddModelError("Id", "Código inválido!");
            else
            {
                if (operacao == "I" && dao.Consultar(jogo.Id) != null)
                    ModelState.AddModelError("Id", "Código já está em uso.");

                if (operacao == "A" && dao.Consultar(jogo.Id) == null)
                    ModelState.AddModelError("Id", "Jogo não está cadastrado.");
            }

            if (string.IsNullOrEmpty(jogo.Descricao)) 
                ModelState.AddModelError("Descricao", "Preencha a descrição.");

            if (jogo.ValorLocacao < 0)
                ModelState.AddModelError("ValorLocacao", "Campo obrigatório.");

            if (jogo.CategoriaId < 0)
                ModelState.AddModelError("CategoriaId", "Informe o código da categoria.");

            if (jogo.DataAquisicao > DateTime.Now)
                ModelState.AddModelError("DataAquisicao", "Data inválida!");
        }

        private void PrepararListaCategorias(){
            CategoriaDAO categoriaDao = new CategoriaDAO();
            var categorias = categoriaDao.Listar();
            List<SelectListItem> listaCategorias = new List<SelectListItem>();

            listaCategorias.Add(new SelectListItem("Selecione uma categoria...", "0"));

            foreach (var categoria in categorias){
                SelectListItem item = new SelectListItem(categoria.Descricao, categoria.Id.ToString());
                listaCategorias.Add(item);
            }

            ViewBag.Categorias = listaCategorias;
        }
    }
}
