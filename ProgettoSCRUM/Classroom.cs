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
        //public List<Voto> voti;


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


        public void AggiungiVoto(string idStudente, Materia materia, double valore, DateTime data)
        {
            Student studente = SearchStudent(idStudente);
            if (studente != null)
            {
                studente.AggiungiVoto(materia, valore, data);
                Console.WriteLine($"Voto aggiunto a {studente.Name} {studente.Surname} per {materia}: {valore}");
            }
            else
            {
                Console.WriteLine("Studente non trovato.");
            }
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

       
    }
}
