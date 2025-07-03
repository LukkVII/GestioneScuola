using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgettoSCRUM
{
    public class Student
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int? BirthYear { get; set; }
        public string IdStudente { get; set; }

        public List<Voto> Voti { get; set; } = new List<Voto>();



        public Student(string name, string surname, int birthYear, string idStudente)
        {
            Name = name;
            Surname = surname;
            BirthYear = birthYear;
            IdStudente = idStudente;
        }

        public static Student CreateFromFile(string row)
        {
            row = row.Trim('*');
            int temp;

            string[] campi = row.Split(";");

            if (campi.Length != 4) return null;
            if (!int.TryParse(campi[2], out temp)) return null;
            if (campi[0] == "" || campi[1] == "" || campi[3] == "") return null;

            return new Student(campi[0], campi[1], int.Parse(campi[2]), campi[3]);
        }

        

        public override string ToString()
        {
            return $"Nome: {Name}, Cognome: {Surname}, Anno di nascita: {BirthYear}, ID: {IdStudente}";
        }


        public void AggiungiVoto(Materia materia, double valore, DateTime data)
        {
            Voti.Add(new Voto(materia, valore, data));
        }

        public void MostraTuttiIVoti()
        {
            if (Voti.Count == 0)
            {
                Console.WriteLine("Nessun voto disponibile.");
                return;
            }

            foreach (var voto in Voti)
            {
                Console.WriteLine(voto.ToString());
            }
        }

        public void MostraVotiPerMateria(string nomeMateria)
        {
            var votiMateria = Voti.Where(v => v.Materia.Nome.Equals(nomeMateria, StringComparison.OrdinalIgnoreCase)).ToList();

            if (votiMateria.Count == 0)
            {
                Console.WriteLine($"Nessun voto trovato per la materia {nomeMateria}.");
                return;
            }

            foreach (var voto in votiMateria)
            {
                Console.WriteLine(voto.ToString());
            }
        }
    }

    }

