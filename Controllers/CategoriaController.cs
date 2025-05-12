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
using Microsoft.AspNetCore.Http;
using System.IO;

namespace CadJogos.Controllers
{
    public class CategoriaController : PadraoControler<CategoriaViewModel>
    {

        public CategoriaController(){
            DAO = new CategoriaDAO();
            GeraProximoId = true;
        }

        public byte[] ConvertImageToByte(IFormFile file){
            if (file !=  null)
                using (var memStream = new MemoryStream()){
                    file.CopyTo(memStream);
                    return memStream.ToArray();
                }
            
            return null;
        }


        protected override void ValidarDados(CategoriaViewModel model, string operacao)
        {
            base.ValidarDados(model, operacao);
            if (string.IsNullOrEmpty(model.Descricao))
                ModelState.AddModelError("Descricao", "Preencha a descricao.");

            //Imagem será obrigatio apenas na inclusão.
            //Na alteração iremos considerar a que já estava salva.
            if (model.Imagem == null && operacao == "I")
                ModelState.AddModelError("Imagem", "Escolha uma imagem.");

            if (model.Imagem != null && model.Imagem.Length / 1024 / 1024 >= 2)
                ModelState.AddModelError("Imagem", "Imagem limitada a 2MB.");

            if (ModelState.IsValid)
            {
                //na alteração, se não foi informada a imagem, iremos manter a que já estava salva.
                if (operacao == "A" && model.Imagem == null)
                {
                    CategoriaViewModel cid = DAO.Consultar(model.Id);
                    model.ImagemEmByte = cid.ImagemEmByte;
                }
                else
                {
                    model.ImagemEmByte = ConvertImageToByte(model.Imagem);
                }
            }
        }

    }

}