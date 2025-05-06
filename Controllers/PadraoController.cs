using Microsoft.AspNetCore.Mvc;
using CadJogosASPNET.Models;
using CadJogosASPNET.DAO;
using System;

namespace CadJogosASPNET.Controllers
{
    public class PadraoControler<T> : Controller where T : PadraoViewModel
    {
        protected PadraoDAO<T> DAO { get; set; }
        protected bool GerarProximoId { get; set; }
        protected string NomeViewIndex { get; set; } = "Index";
        protected string NomeViewForm { get; set; } = "Form";
        protected string NomeViewError { get; set; } = "Error";


        public virtual IActionResult Index()
        {
            try
            {
                var lista = DAO.Listar();
                return View(NomeViewIndex, lista);
            }
            catch (Exception erro)
            {
                return View(NomeViewError, new ErrorViewModel(erro.ToString()));
            }
        }

        public virtual IActionResult Create()
        {
            try
            {
                ViewBag.Operacao = "I";
                T model = Activator.CreateInstance<T>();
                PreencherDadosParaView("I", model);
                return View("Form", model);
            }
            catch (Exception erro)
            {
                return View(NomeViewError, new ErrorViewModel(erro.ToString()));
            }
        }

        public virtual IActionResult Salvar(T model, string operacao)
        {
            try
            {
                ValidarDados(model, operacao);
                if (!ModelState.IsValid)
                {
                    ViewBag.Operacao = operacao;
                    PreencherDadosParaView(operacao, model);
                    return View(NomeViewForm, model);
                }

                if (operacao == "I")
                    DAO.Insert(model);
                else
                    DAO.Update(model);

                return RedirectToAction(NomeViewIndex);
            }
            catch (Exception erro)
            {
                return View(NomeViewError, new ErrorViewModel(erro.ToString()));
            }

        }

        public IActionResult Edit(int id)
        {
            try
            {
                ViewBag.Operacao = "A";
                var model = DAO.Consultar(id);

                if (model == null)
                    return RedirectToAction(NomeViewIndex);
                else
                {
                    PreencherDadosParaView("A", model);
                    return View(NomeViewForm, model);
                }
            }
            catch (Exception erro)
            {
                return View(NomeViewError, new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                DAO.Delete(id);
                return RedirectToAction(NomeViewIndex);
            }
            catch (Exception erro)
            {
                return View(NomeViewError, new ErrorViewModel(erro.ToString()));
            }
        }


        protected virtual void PreencherDadosParaView(string Operacao, T model)
        {
            if (GerarProximoId && Operacao == "I")
                model.Id = DAO.ProximoId();
        }

        protected virtual void ValidarDados(T model, string operacao)
        {
            ModelState.Clear();

            if (model.Id <= 0)
                ModelState.AddModelError("Id", "Código inválido!");

            if (operacao == "I" && DAO.Consultar(model.Id) != null)
                ModelState.AddModelError("Id", "Código já está em uso.");

            if (operacao == "A" && DAO.Consultar(model.Id) == null)
                ModelState.AddModelError("Id", "Este registro não existe.");

        }



    }
}