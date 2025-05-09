using Microsoft.AspNetCore.Http;
using System;

namespace CadJogosASPNET.Controllers
{
    public class HelperControllers
    {

        public static Boolean UserEstaLogado(ISession session)
        {
            string logado = session.GetString("Logado");
            if (logado == null)
                return false;
            else
                return true;
        }

    }
}
