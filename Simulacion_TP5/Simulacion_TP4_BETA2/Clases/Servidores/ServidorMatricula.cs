using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion_TP1.Clases
{
    public class ServidorMatricula : Servidor
    {
        string estado;

        public ServidorMatricula(string estado)
        {
            this.Estado = estado;
        }

        public string Estado { get => estado; set => estado = value; }
    }
}
