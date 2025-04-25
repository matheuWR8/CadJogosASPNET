using System;

namespace CadJogosASPNET.Models
{
    public class JogoViewModel : PadraoViewModel
    {
        public String Descricao { get; set; }
        public double? ValorLocacao { get; set; }
        public DateTime DataAquisicao { get; set; }
        public int CategoriaId { get; set; }

        /// <summary>
        /// Nome da categoria de de respectivo CategoriaID para exibição; não é inserido na tabela de jogos
        /// </summary>
        public string NomeCategoria { get; set; }
    }
}
