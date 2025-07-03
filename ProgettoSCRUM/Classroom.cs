using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgettoSCRUM
{
    public class Classroom
    {
        private List<Student> students = new List<Student>();
        private List<Materia> materie = new List<Materia>();
        //public List<Voto> voti;

        public Classroom()
        {
            materie.Add(new Materia("Matematica", "MAT01"));
            materie.Add(new Materia("Italiano", "ITA02"));
            materie.Add(new Materia("Inglese", "ING03"));
        }



        public bool AddStudent(Student student)
        {
            if (students.Any(x => x.IdStudente == student.IdStudente)) return false;
            students.Add(student);
            return true;
        }


        public bool RemoveStudent(string idStudente)
        {
            if (students.Any(x => x.IdStudente == idStudente))
            {
                students = students.Where(x => x.IdStudente != idStudente).ToList(); 
                return true;
            }
            return false;
        }


        public Student SearchStudent(string idStudente)
        {
            return students.Find(s => s.IdStudente.Equals(idStudente, StringComparison.OrdinalIgnoreCase));
        }



        public string[] GenerateRows()
        {
            string[] rows = new string[students.Count];
            for (int i = 0; i < rows.Length; i++)
            {
                rows[i] = "*" + students[i].Name + ";" + students[i].Surname + ";" + students[i].BirthYear + ";" + students[i].IdStudente;
            }

            return rows;
        }


        //ordina studenti
        public void Order(string parameter)
        {
            if (parameter == "Name")
            {
                students = students.OrderBy(x => x.Name).ToList();
            }

            if (parameter == "Surname")
            {
                students = students.OrderBy(x => x.Surname).ToList();
            }

            if (parameter == "BirthYear")
            {
                students = students.OrderBy(x => x.BirthYear).ToList();
            }
        }


        //elenco studenti
        public override string ToString()
        {
            string ris = "";
            foreach (Student student in students)
            {
                ris += student.ToString() + "\n";
            }
            return ris;
        }

        public List<Student> GetStudents()
        {
            return students;
        }


        public void AggiungiVoto(string idStudente, string nomeMateria, double valore, DateTime data)
        {
            Student studente = SearchStudent(idStudente);
            Materia materia = CercaMateria(nomeMateria);

            if (studente == null)
            {
                Console.WriteLine("Studente non trovato.");
                return;
            }

            if (materia == null)
            {
                Console.WriteLine("Materia non trovata.");
                return;
            }

            studente.AggiungiVoto(materia, valore, data);
            Console.WriteLine($"\nVoto {valore} aggiunto a {studente.Name} {studente.Surname} per {materia.Nome}.");
        }

        public void MostraVotiStudente(string idStudente, string materia = null)
        {
            Student studente = SearchStudent(idStudente);

            if (studente == null)
            {
                Console.WriteLine("Studente non trovato.");
                return;
            }

            if (materia == null)
            {
                studente.MostraTuttiIVoti();
            }
            else
            {
                studente.MostraVotiPerMateria(materia);
            }
        }

        public bool AggiungiMateria(Materia materia)
        {
            if (materie.Any(m => m.Nome.Equals(materia.Nome, StringComparison.OrdinalIgnoreCase)))
                return false;

            materie.Add(materia);
            return true;
        }

        public List<Materia> GetMaterie()
        {
            return materie;
        }

        public Materia CercaMateria(string nomeMateria)
        {
            return materie.FirstOrDefault(m => m.Nome.Equals(nomeMateria, StringComparison.OrdinalIgnoreCase));
        }
    }
}
