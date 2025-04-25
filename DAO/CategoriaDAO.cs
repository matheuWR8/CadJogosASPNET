using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CadJogosASPNET.Models;

namespace CadJogosASPNET.DAO
{
    public class CategoriaDAO : PadraoDAO<CategoriaViewModel>
    {
        //public List<CategoriaViewModel> Listar()
        //{
        //    List<CategoriaViewModel> lista = new List<CategoriaViewModel>();

        //    DataTable tabela = HelperDAO.ExecutarProcedureSelect("spListarCategorias", null);

        //    foreach (DataRow registro in tabela.Rows)
        //        lista.Add(MontarModel(registro));

        //    return lista;
        //}


        protected override SqlParameter[] CriarParametros(CategoriaViewModel model)
        {
            throw new NotImplementedException();
        }


        protected override CategoriaViewModel MontarModel(DataRow registro)
        {
            CategoriaViewModel categoria = new CategoriaViewModel();

            categoria.Id = Convert.ToInt32(registro["id"]);
            categoria.Nome = registro["descricao"].ToString();

            return categoria;
        }

        protected override void SetTabela()
        {
            Tabela = "Categorias";
        }
    }
}