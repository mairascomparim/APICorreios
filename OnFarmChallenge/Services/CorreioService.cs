using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnFarmChallenge.Services
{
    public class CorreioService
    {
        public String ConsultaObjeto(string CodigoRastreio)
        {
            try
            {
                wsCorreio.ServiceClient ws = new wsCorreio.ServiceClient();
                wsCorreio.sroxml wsCorreioResponse = ws.buscaEventos("ECT", "SRO", "L", "T", "101", CodigoRastreio);

                if (wsCorreioResponse.objeto == null)
                {
                    return "Código Não Encontrado";
                }
                else
                {
                    return wsCorreioResponse.objeto[0].evento[0].descricao;
                }
            } catch (Exception)
            {
                return "Error";
            }

        }
    }
}
