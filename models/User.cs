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
    

