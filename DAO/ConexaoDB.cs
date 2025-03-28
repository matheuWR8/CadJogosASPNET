using System.Data.SqlClient;

namespace CadJogosASPNET.DAO
{
    /// <summary>
    /// Classe estática com método para estabelecimento de uma conexão SQL
    /// </summary>
    public static class ConexaoDB
    {
        /// <summary>
        /// Método estático que retorna uma conexão SQL com o DB AulaDB
        /// </summary>
        /// <returns>Conexão aberta</returns>
        public static SqlConnection GetConexao()
        {
            string stringConexao = "Data Source=LOCALHOST; Database=AULADB; user id=sa; password=123456";
            SqlConnection conexao = new SqlConnection(stringConexao);
            conexao.Open();
            return conexao;
        }
    }
}
