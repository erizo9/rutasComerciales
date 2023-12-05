using Newtonsoft.Json;
 class User
    {
public string? Username { get; set; }
        public string? Password { get; set; }
        public bool Jefe { get; set; }
        public List<Visit>? Visits { get; set; } = new List<Visit>();
        public string? PrivateInformation { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }
        public DateTime RegistrationDate { get; set; }


        public bool IsBoss()
        {
            return Jefe;
        }

        public static User RegisterUser()
        {
            Console.Write("Nuevo Usuario: ");
            string? username = Console.ReadLine();
            Console.Write("Contraseña: ");
            string? password = Console.ReadLine();

            return new User { Username = username, Password = password };
        }

        public static void SaveUsersToFile(List<User> users)
        {
            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText("users.json", json);
        }

        public static List<User> LoadUsersFromFile()
        {
            if (File.Exists("users.json"))
            {
                string json = File.ReadAllText("users.json");
                return JsonConvert.DeserializeObject<List<User>>(json);
            }

            return new List<User>();
        }

        public static User? Login(List<User> users)
        {
            Console.Write("Usuario: ");
            string? username = Console.ReadLine();
            Console.Write("Contraseña: ");
            string? password = Console.ReadLine();

            User? currentUser = users.Find(u => u.Username == username && u.Password == password);

            if (currentUser != null)
            {
                Console.WriteLine($"Inicio de sesión exitoso, ¡bienvenido {currentUser.Username}!");
            }
            else
            {
                Console.WriteLine("Inicio de sesión fallido. Verifica tus credenciales.");
            }

            return currentUser;
        }

        public void AddVisit(List<User> users)
        {
            if (Visits == null)
            {
                Visits = new List<Visit>();
            }

            if (IsBoss())
            {
                Console.Write("Nombre de usuario del destinatario: ");
                string? recipientUsername = Console.ReadLine();

                User? recipientUser = users.Find(u => u.Username == recipientUsername);

                if (recipientUser != null)
                {
                    Console.Write($"Fecha de la Visita para {recipientUser.Username} (MM/DD/AAAA): ");
                    DateTime date;
                    while (!DateTime.TryParse(Console.ReadLine(), out date))
                    {
                        Console.WriteLine("Formato de fecha no válido. Inténtalo de nuevo.");
                    }

                    Console.Write("Ubicación: ");
                    string? location = Console.ReadLine();

                    AddVisit(recipientUser, date, location);
                }
                else
                {
                    Console.WriteLine("Usuario destinatario no encontrado.");
                }
            }
            else
            {
                Console.WriteLine("No tienes permisos para agregar visitas a otros usuarios.");
            }
        }

        public void AddVisit(User recipientUser, DateTime date, string location)
        {
            Visits ??= new List<Visit>();

            Visit newVisit = new Visit { Date = date, Location = location };

            recipientUser.Visits ??= new List<Visit>();
            recipientUser.Visits.Add(newVisit);

            Console.WriteLine("Visita agregada correctamente al usuario.");
        }

        public void ViewVisits()
        {
            Console.WriteLine("Visitas Pendientes:");

            if (Visits == null)
            {
                Console.WriteLine("No hay visitas pendientes.");
                return;
            }

            foreach (var visit in Visits)
            {
                if (!visit.Completed)
                {
                    Console.WriteLine($"{visit.Date.ToShortDateString()} - {visit.Location}");
                }
            }
        }

        public void MarkVisitCompleted()
        {
            ViewVisits();

            Console.Write("Ingrese la fecha de la visita que desea marcar como realizada (MM/DD/AAAA): ");
            DateTime date;
            while (!DateTime.TryParse(Console.ReadLine(), out date))
            {
                Console.WriteLine("Formato de fecha no válido. Inténtalo de nuevo.");
            }

            var visit = Visits?.Find(v => v.Date.Date == date.Date);

            if (visit != null)
            {
                visit.MarkCompleted();
            }
            else
            {
                Console.WriteLine("No se encontró ninguna visita para la fecha proporcionada.");
            }
        }

        public void PrivateZone()
        {
            Console.WriteLine($"Bienvenido a la Zona Privada, {Username ?? "Invitado"}!");
            Console.WriteLine($"Información Privada: {PrivateInformation}");

            while (true)
            {
                Console.WriteLine("1. Ver Historial de Visitas");
                Console.WriteLine("2. Volver al Menú Principal");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ViewVisitHistory();
                        break;
                    case "2":
                        return;
                    default:
                        Console.WriteLine("Opción no válida. Inténtalo de nuevo.");
                        break;
                }
            }
        }

        public void ViewVisitHistory()
        {
            if (Visits == null || Visits.Count == 0)
            {
                Console.WriteLine("No hay historial de visitas disponible.");
                return;
            }

            Console.WriteLine("Historial de Visitas:");

            foreach (var visit in Visits)
            {
                visit.ViewVisit();
            }
        }
    }
