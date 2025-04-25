using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;
using CadJogosASPNET.Models;
using Microsoft.CodeAnalysis;

namespace CadJogosASPNET.DAO
{
    public class JogoDAO : PadraoDAO<JogoViewModel>
    {
        ///// <summary>
        ///// Insere um jogo na tabela
        ///// </summary>
        ///// <param name="jogo">DTO do jogo</param>
        //public void Inserir(JogoViewModel jogo)
        //{
        //    SqlParameter[] parametros = CriarParametros(jogo);
        //    HelperDAO.ExecutarProcedure("spIncluirJogo", parametros);
        //}

        ///// <summary>
        ///// Altera os dados de um jogo na tabela
        ///// </summary>
        ///// <param name="jogo">DTO do jogo</param>
        //public void Alterar(JogoViewModel jogo)
        //{
        //    SqlParameter[] parametros = CriarParametros(jogo);
        //    HelperDAO.ExecutarProcedure("spAlterarJogo", parametros);
        //}

        ///// <summary>
        ///// Apaga um registro da tabela
        ///// </summary>
        ///// <param name="id">Código do jogo</param>
        //public void Excluir(int id)
        //{
        //    SqlParameter[] parametro = { new SqlParameter("id", id) };
        //    HelperDAO.ExecutarProcedure("spExcluirJogo", parametro);
        //}

        ///// <summary>
        ///// Busca um jogo pelo seu ID
        ///// </summary>
        ///// <param name="id">Código do jogo</param>
        ///// <returns>DTO contendo os dados do jogo</returns>
        //public JogoViewModel Consultar(int id)
        //{
        //    SqlParameter[] parametro = { new SqlParameter("id", id) };
        //    DataTable tabela = HelperDAO.ExecutarProcedureSelect("spConsultarJogo", parametro);

        //    if (tabela.Rows.Count == 0)
        //        return null;
        //    else
        //    {
        //        DataRow registro = tabela.Rows[0];
        //        return MontarModel(registro);
        //    }
        //}

        ///// <summary>
        ///// Retorna uma lista contendo todos os registros da tabela
        ///// </summary>
        ///// <returns>Lista de DTOs com todos os registros</returns>
        //protected override List<JogoViewModel> Listar()
        //{
        //    List<JogoViewModel> lista = new List<JogoViewModel>();

        //    DataTable tabela = HelperDAO.ExecutarProcedureSelect("spListarJogos", null);

        //    foreach (DataRow registro in tabela.Rows)
        //        lista.Add(MontarModel(registro));

        //    return lista;
        //}

        ///// <summary>
        ///// Busca o maior id da tabela e retorna o seguinte a ele
        ///// </summary>
        ///// <returns>Maior id + 1</returns>
        //public int ProximoId()
        //{
        //    SqlParameter[] parametro = { new SqlParameter("tabela", "jogos") };
        //    DataTable tabela = HelperDAO.ExecutarProcedureSelect("spProximoId", parametro);
        //    return Convert.ToInt32(tabela.Rows[0]["MAIOR"]); 
        //}

        /// <summary>
        /// </summary>
        /// <param name="jogo">DTO do jogo</param>
        /// <returns>Lista de parâmetros SQL</returns>
        protected override SqlParameter[] CriarParametros(JogoViewModel jogo)
        {
            SqlParameter[] parametros = new SqlParameter[5];
            parametros[0] = new SqlParameter("@id", jogo.Id);
            parametros[1] = new SqlParameter("@descricao", jogo.Descricao);
            if (jogo.ValorLocacao != null)
                parametros[2] = new SqlParameter("@valor_locacao", jogo.ValorLocacao);
            else
                parametros[2] = new SqlParameter("@valor_locacao", DBNull.Value);
            parametros[3] = new SqlParameter("@data_aquisicao", jogo.DataAquisicao);
            parametros[4] = new SqlParameter("@categoriaID", jogo.CategoriaId);

            return parametros;
        }

        /// <summary>
        /// Transfere os dados de um DataRow para um DTO
        /// </summary>
        /// <param name="registro">DataRow contendo o resultado da consulta</param>
        /// <returns>DTO com os dados da linha</returns>
        protected override JogoViewModel MontarModel(DataRow registro)
        {
            JogoViewModel jogo = new JogoViewModel();

            jogo.Id = Convert.ToInt32(registro["id"]);
            jogo.Descricao = registro["descricao"].ToString();
            if (registro["valor_locacao"] != DBNull.Value)
                jogo.ValorLocacao = Convert.ToDouble(registro["valor_locacao"]);
            jogo.DataAquisicao = Convert.ToDateTime(registro["data_aquisicao"]);
            jogo.CategoriaId = Convert.ToInt32(registro["categoriaID"]);
            if (registro.Table.Columns.Contains("NomeCategoria"))
                jogo.NomeCategoria = registro["NomeCategoria"].ToString();

            return jogo;
        }

        protected override void SetTabela()
        {
            Tabela = "Jogos";
        }

        public JogoDAO()
        {
            NomeSpListar = "spListarJogos";
        }

    }
}
