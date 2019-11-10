using System;

namespace Cygnus
{
    class CAtividade
    {
        public int Codigo { get; set; }
        public string Localizacao { get; set; }
        public DateTime DataInicio { get; set; }
        public int Turno { get; set; }
        public string Frequencia { get; set; }

        public override string ToString()
        {
            return this.Codigo.ToString();
        }
    }
}