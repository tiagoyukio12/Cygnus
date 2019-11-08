using System;
using System.Collections.Generic;
using System.Text;

namespace Cygnus
{
    public class CVoluntario
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Endereco { get; set; }

        public override string ToString()
        {
            return this.Nome + " - " + this.DataNascimento.ToString("MM/dd/yyyy") + " - " + this.Endereco;
        }
    }
}
