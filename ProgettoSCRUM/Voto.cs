using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgettoSCRUM
{
    public class Voto
    {
        public Materia Materia { get; set; }
        public double Valore { get; set; }
        public DateTime Data { get; set; }

        public Voto(Materia materia, double valore, DateTime data)
        {
            Materia = materia;
            Valore = valore;
            Data = data;
        }

        public override string ToString()
        {
            return $"{Materia.Nome} - Voto: {Valore} ({Data.ToShortDateString()})";
        }


    }
}
