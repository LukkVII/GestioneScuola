using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgettoSCRUM
{
    public class Materia
    {
        public string Nome { get; set; }
        public string Codice { get; set; }

        List<Materia> materie = new List<Materia>();

        public Materia(string nome, string codice)
        {
            Nome = nome;
            Codice = codice;
        }


        public override string ToString()
        {
            return $"{Nome} ({Codice})";
        }
    }
}
