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
            SqlParameter[] parametros =
            {
                new SqlParameter("id", model.Id),
                new SqlParameter("descricao", model.Descricao)
            };

            return parametros;
        }


        protected override CategoriaViewModel MontarModel(DataRow registro)
        {
            CategoriaViewModel categoria = new CategoriaViewModel()
            {
                Id = Convert.ToInt32(registro["id"]),
                Descricao = registro["descricao"].ToString()
            };

            return categoria;
        }

        protected override void SetTabela()
        {
            Tabela = "Categorias";
        }
    }
}