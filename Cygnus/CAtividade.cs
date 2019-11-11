using System;

namespace Cygnus
{
    class CAtividade
    {
        public string Codigo { get; set; }
        public string Localizacao { get; set; }
        public DateTime DataInicio { get; set; }
        public int Turno { get; set; }
        public string Frequencia { get; set; }

        public CAtividade(string id, string pos, DateTime date, int turn, string freq)
        {
            Codigo = id;
            Localizacao = pos;
            DataInicio = date;
            Turno = turn;
            Frequencia = freq;
        }

        public override string ToString()
        {
            return this.Codigo.ToString();
        }
    }
}