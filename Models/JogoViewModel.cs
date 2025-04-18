using System;

namespace CadJogosASPNET.Models
{
    public class JogoViewModel
    {
        public int Id { get; set; }
        public String Descricao { get; set; }
        public double? ValorLocacao { get; set; }
        public DateTime DataAquisicao { get; set; }
        public int CategoriaId { get; set; }
        public string NomeCategoria { get; set; }
    }
}
