using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;
using CadJogosASPNET.Models;

namespace CadJogosASPNET.DAO
{
    public class JogoDAO
    {
        /// <summary>
        /// Insere um jogo na tabela
        /// </summary>
        /// <param name="jogo">DTO do jogo</param>
        public void Inserir(JogoViewModel jogo)
        {
            SqlParameter[] parametros = CriarParametros(jogo);
            string sql =
                "insert into jogos(id, descricao, valor_locacao, data_aquisicao, categoriaID)" +
                "values (@id, @descricao, @valor_locacao, @data_aquisicao, @categoriaID)";

            HelperDAO.ExecutarSQL(sql, parametros);
        }

        /// <summary>
        /// Altera os dados de um jogo na tabela
        /// </summary>
        /// <param name="jogo">DTO do jogo</param>
        public void Alterar(JogoViewModel jogo)
        {
            SqlParameter[] parametros = CriarParametros(jogo);
            string sql = "set dateformat dmy; update jogos set " +
                "descricao = @descricao, " +
                "valor_locacao = @valor_locacao, " +
                "data_aquisicao = @data_aquisicao, " +
                "categoriaID = @categoriaID " +
                "where id = @id";

            HelperDAO.ExecutarSQL(sql, parametros);
        }

        /// <summary>
        /// Apaga um registro da tabela
        /// </summary>
        /// <param name="id">Código do jogo</param>
        public void Excluir(int id)
        {
            string sql = $"delete jogos where id = " + id;

            HelperDAO.ExecutarSQL(sql);
        }

        /// <summary>
        /// Busca um jogo pelo seu ID
        /// </summary>
        /// <param name="id">Código do jogo</param>
        /// <returns>DTO contendo os dados do jogo</returns>
        public JogoViewModel Consultar(int id)
        {
            string sql = "select * from jogos where id = " + id;
            DataTable tabela = HelperDAO.ExecutarSelect(sql);
            if (tabela.Rows.Count == 0)
                return null;
            else
            {
                DataRow registro = tabela.Rows[0];
                return MontarModel(registro);
            }
        }

        /// <summary>
        /// Retorna uma lista contendo todos os registros da tabela
        /// </summary>
        /// <returns>Lista de DTOs com todos os registros</returns>
        public List<JogoViewModel> Listar()
        {
            List<JogoViewModel> lista = new List<JogoViewModel>();

            string sql = "select * from jogos";
            DataTable tabela = HelperDAO.ExecutarSelect(sql);

            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontarModel(registro));

            return lista;
        }

        public int ProximoId()
        {
            string sql = "select isnull(max(id) +1, 1) as 'MAIOR' from jogos";
            DataTable tabela = HelperDAO.ExecutarSelect(sql, null);
            return Convert.ToInt32(tabela.Rows[0]["MAIOR"]); 
        }

        /// <summary>
        /// </summary>
        /// <param name="jogo">DTO do jogo</param>
        /// <returns>Lista de parâmetros SQL</returns>
        private SqlParameter[] CriarParametros(JogoViewModel jogo)
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
        private JogoViewModel MontarModel(DataRow registro)
        {
            JogoViewModel jogo = new JogoViewModel();

            jogo.Id = Convert.ToInt32(registro["id"]);
            jogo.Descricao = registro["descricao"].ToString();
            if (registro["valor_locacao"] != DBNull.Value)
                jogo.ValorLocacao = Convert.ToDouble(registro["valor_locacao"]);
            jogo.DataAquisicao = Convert.ToDateTime(registro["data_aquisicao"]);
            jogo.CategoriaId = Convert.ToInt32(registro["categoriaID"]);

            return jogo;
        }

    }
}
