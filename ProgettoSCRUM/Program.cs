using ProgettoSCRUM;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static ProgettoSCRUM.Classroom;
using static ProgettoSCRUM.Student;


namespace ProgettoSCRUM
{
    public class Program 
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Gestione studenti");

            Classroom classroom = new Classroom();
            List<Voto> voti = new List<Voto>();
            List<Student> students;

            List<Materia> materie = new List<Materia>();


            while (true)
            {
                int choice;

                Console.WriteLine("\nMENU");
                Console.WriteLine("1. Aggiungi studente");
                Console.WriteLine("2. Rimuovi studente");
                Console.WriteLine("3. Cerca studente per numero di matricola");
                Console.WriteLine("4. Visualizza elenco studenti");
                Console.WriteLine("5. Upload file archivio");
                Console.WriteLine("6. Aggiornamento file archivio");
                Console.WriteLine("7. Ordina secondo criterio selezionato");
                Console.WriteLine("8. Inserisci voto");
                Console.WriteLine("9. Visualizza voti per studente");
                Console.WriteLine("0. Esci\n");


                int.TryParse(Console.ReadLine(), out choice);
                if (choice > 9 && choice < 0)
                {
                    Console.WriteLine("Inserimento non valido");
                    continue;
                }

                if (choice == 0) break;

                switch (choice)
                {
                    case 1:
                        if (AddStudent(classroom))
                            Console.WriteLine("\nStudente aggiunto con successo!");
                        else
                            Console.WriteLine("\nStudente già presente, impossibile aggiungere");
                        break;

                    case 2:
                        if (RemoveStudent(classroom))
                            Console.WriteLine("\nStudente rimosso con successo!");
                        else
                            Console.WriteLine("\nStudente non presente, impossibile rimuovere.");
                        break;

                    case 3:
                        SearchStudent(classroom);
                        break;

                    case 4:
                        Console.WriteLine(classroom.ToString());
                        break;

                    case 5:
                        FileUpload(classroom);
                        break;

                    case 6:
                        FileUpdate(classroom);
                        Console.WriteLine("\nAggiornamento completato!");
                        break;

                    case 7:
                        PrintOrdered(classroom);
                        break;

                    case 8:
                        Console.WriteLine("\nMaterie disponibili:");
                        foreach (var mat in classroom.GetMaterie())
                        {
                            Console.WriteLine($"- {mat.Nome}");
                        }

                        Console.Write("\nInserisci materia: ");
                        string materiaInput = Console.ReadLine();

                        Materia materiaSelezionata = classroom.CercaMateria(materiaInput);

                        if (materiaSelezionata == null)
                        {
                            Console.WriteLine("Materia non valida.");
                        }
                        else
                        {
                            Console.WriteLine("\nStudenti disponibili:");
                            foreach (var studente in classroom.GetStudents())
                            {
                                Console.WriteLine($"{studente.Name} {studente.Surname} --> ID: {studente.IdStudente}");
                            }

                            Console.Write("\nInserisci ID Studente: ");
                            string idStudenteInput = Console.ReadLine();

                            Student studenteSelezionato = classroom.SearchStudent(idStudenteInput);

                            if (studenteSelezionato == null)
                            {
                                Console.WriteLine("ID Studente non valido o studente non trovato.");
                                return;
                            }

                            Console.Write("Inserisci voto: ");
                            double votoValore = double.Parse(Console.ReadLine());

                            classroom.AggiungiVoto(idStudenteInput, materiaInput, votoValore, DateTime.Now);
                        }
                        break;

                    case 9:
                        Console.Write("Inserisci ID studente per vedere i voti: ");
                        string idInput = Console.ReadLine();

                        classroom.VisualizzaVotiStudente(idInput);
                        break;

                }
            }
            }

        public static bool AddStudent(Classroom classroom)
        {
            Console.WriteLine("\nInserire nome: ");
            string name;

            while (true)
            {
                name = Console.ReadLine();
                if (name != "") break;
                Console.WriteLine("Inserimento non corretto, riprova");
            }

            Console.WriteLine("\nInserire cognome: ");
            string surname;

            while (true)
            {
                surname = Console.ReadLine();
                if (surname != "") break;
                Console.WriteLine("Inserimento non corretto, riprova");
            }

            Console.WriteLine("\nInserire anno di nascita: ");
            int birthYear;

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out birthYear)) break;
                Console.WriteLine("Inserimento non corretto, riprova");
            }


            Console.WriteLine("\nInserire ID: ");
            string studentID;

            while (true)
            {
                studentID = Console.ReadLine();
                if (studentID != "") break;
                Console.WriteLine("Inserimento non corretto, riprova");
            }

            if (classroom.AddStudent(new Student(name, surname, birthYear, studentID))) return true;
            return false;

        }

        public static bool RemoveStudent(Classroom classroom)
        {
            Console.WriteLine("Inserire ID dello studente da rimuovere: ");
            string ID;

            while (true)
            {
                if ((ID = Console.ReadLine()) != "") break;
                Console.WriteLine("Inserimento non corretto, riprova");
            }

            if (classroom.RemoveStudent(ID)) return true;
            return false;
        }

        public static void SearchStudent(Classroom classroom)
        {
            Console.WriteLine("Inserire ID dello studente da cercare: ");
            string ID;

            while (true)
            {
                if ((ID = Console.ReadLine()) != "") break;
                Console.WriteLine("Inserimento non corretto, riprova");
            }

            Console.WriteLine(classroom.SearchStudent(ID));
        }

        public static void FileUpload(Classroom classroom)
        {
            string[] rows = File.ReadAllLines("elencoStudenti.txt");
            foreach (string row in rows)
            {
                if (row.StartsWith("*") && Student.CreateFromFile(row) != null)
                {
                    classroom.AddStudent(Student.CreateFromFile(row));
                }
            }
            Console.WriteLine("Upload completato");
        }

        public static void FileUpdate(Classroom classroom)
        {
            File.WriteAllLines("elencoStudenti.txt", classroom.GenerateRows());
        }

        public static void PrintOrdered(Classroom classroom)
        {
            Console.WriteLine("\nScegli criterio ordinamento: ");
            Console.WriteLine("1. Name");
            Console.WriteLine("2. Surname");
            Console.WriteLine("3. BirthYear");

            int choice;

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out choice) && choice < 4 && choice > 0) break;
                Console.WriteLine("Scelta non valida, riprova");
            }

            switch (choice)
            {
                case 1:
                    classroom.Order("Name");
                    break;

                case 2:
                    classroom.Order("Surname");
                    break;

                case 3:
                    classroom.Order("BirthYear");
                    break;
            }

            Console.WriteLine(classroom.ToString());
        }

    }
    }
