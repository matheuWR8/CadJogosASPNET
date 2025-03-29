using System.Data.SqlClient;
using System.Data;

namespace CadJogosASPNET.DAO
{
    public static class HelperDAO
    {
        /// <summary>
        /// Método estático que executa um comando SQL (insert, update, delete)
        /// </summary>
        /// <param name="sql">Comando sql</param>
        /// <param name="parametros">Lista de parâmetros a ser incluído no comando</param>
        public static void ExecutarSQL(string sql, SqlParameter[] parametros = null)
        {
            using (SqlConnection conexao = ConexaoDB.GetConexao())
            {
                using (var comando = new SqlCommand(sql, conexao))
                {
                    if (parametros != null)
                        comando.Parameters.AddRange(parametros);

                    comando.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Método estático que executa um comando de select
        /// </summary>
        /// <param name="sql">Comando SQL de select</param>
        /// <param name="parametros">Parâmetros opcionais a serem incluídos</param>
        /// <returns>Tabela contendo os dados da consulta</returns>
        public static DataTable ExecutarSelect(string sql, SqlParameter[] parametros = null)
        {
            using (SqlConnection conexao = ConexaoDB.GetConexao())
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, conexao))
                {
                    if (parametros != null)
                        adapter.SelectCommand.Parameters.AddRange(parametros);

                    DataTable tabela = new DataTable();
                    adapter.Fill(tabela);
                    return tabela;
                }
            }
        }

        /// <summary>
        /// Executa uma stored procedure
        /// </summary>
        /// <param name="nomeProcedure">Nome da SP a ser executada</param>
        /// <param name="parametros">Parâmetros a serem incluídos no comando</param>
        public static void ExecutarProcedure(string nomeProcedure, SqlParameter[] parametros)
        {
            using (SqlConnection conexao = ConexaoDB.GetConexao())
            {
                using (SqlCommand comando = new SqlCommand(nomeProcedure, conexao))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    if (parametros != null)
                        comando.Parameters.AddRange(parametros);
                    comando.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Executa uma stored procedure com comando SELECT
        /// </summary>
        /// <param name="nomeProcedure">Nome da SP a ser executada</param>
        /// <param name="parametros">Parâmetros a serem incluídos no comando</param>
        /// <returns>Tabela contendo os dados da consulta</returns>
        public static DataTable ExecutarProcedureSelect(string nomeProcedure, SqlParameter[] parametros)
        {
            using (SqlConnection conexao = ConexaoDB.GetConexao())
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(nomeProcedure, conexao))
                {
                    if (parametros != null)
                        adapter.SelectCommand.Parameters.AddRange(parametros);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    DataTable tabela = new DataTable();
                    adapter.Fill(tabela);
                    return tabela;
                }
            }
        }


    }
}
