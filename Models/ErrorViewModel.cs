using System;

namespace CadJogosASPNET.Models
{
    public class ErrorViewModel
    {
        public string Erro { get; set; }
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);


        public ErrorViewModel(string erro)
        {
            this.Erro = erro;
        }

        public ErrorViewModel()
        {
        }


    }
}
