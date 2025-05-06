using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CadJogosASPNET.Controllers;
using CadJogosASPNET.DAO;
using CadJogosASPNET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CadJogos.Controllers
{
    public class CategoriaController : PadraoControler<CategoriaViewModel>
    {
        public CategoriaController(){
            DAO = new CategoriaDAO();
            GerarProximoId = true;
        }

        protected override void ValidarDados(CategoriaViewModel model, string operacao)
        {
            base.ValidarDados(model, operacao);
            if (string.IsNullOrEmpty(model.Descricao))
                ModelState.AddModelError("Descricao", "Preencha a descricao.");
        }
    }
}