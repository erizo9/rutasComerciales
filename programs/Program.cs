using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using rutasComerciales;

namespace RutasComerciales
{
    class Program
    {
        static List<User> users = new List<User>();
        static List<Product> products = new List<Product>();
        static User? currentUser;
        static string logFilePath = "error_log.txt";

               static void Main()
        {
            // Inicializar el registro de errores
            File.WriteAllText(logFilePath, "Inicio del registro de errores\n");

            InitializeProducts();
            users = User.LoadUsersFromFile();

            while (true)
            {
                Console.WriteLine("1. Iniciar sesión");
                Console.WriteLine("2. Registrarse");
                Console.WriteLine("3. Zona pública");
                Console.WriteLine("4. Salir");
                string? choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            currentUser = User.Login(users);
                            if (currentUser != null)
                            {
                                MainMenu();
                            }
                            break;
                        case "2":
                            Register();
                            break;
                        case "3":
                            PublicZone();
                            break;
                        case "4":
                            Environment.Exit(0);
                            break;
                        default:
                            Log("Opción no válida. Inténtalo de nuevo.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Log($"Error en el menú principal: {ex.Message}");
                }
            }
        }

        static void InitializeProducts()
        {
            Product product1 = new Product { Name = "Producto1", Price = 199 };
            products.Add(product1);
        }

        static void Log(string message)
        {
            File.AppendAllText(logFilePath, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}\n");
        }

        static void Register()
        {
            try
            {
                User newUser = User.RegisterUser();
                users.Add(newUser);
                User.SaveUsersToFile(users);

                Console.WriteLine("Registro exitoso. Inicia sesión con tus nuevas credenciales.");
            }
            catch (Exception ex)
            {
                Log($"Error en el registro: {ex.Message}");
            }
        }

        static void MainMenu()
        {
            while (true)
            {
                Console.WriteLine($"Bienvenido, {currentUser?.Username ?? "Invitado"}!");
                Console.WriteLine("1. Ver Visitas");
                Console.WriteLine("2. Agregar Visita");

                if (currentUser?.IsBoss() == true)
                {
                    Console.WriteLine("3. Ver Rutas de Usuarios");
                }

                Console.WriteLine("4. Marcar Visita como Realizada");
                Console.WriteLine("5. Zona privada");
                Console.WriteLine("6. Cerrar Sesión");

                string? choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            currentUser.ViewVisits();
                            break;
                        case "2":
                            currentUser.AddVisit(users);
                            break;
                        case "3":
                            if (currentUser?.IsBoss() == true)
                            {
                                ViewUserRoutes();
                            }
                            else
                            {
                                Console.WriteLine("No tienes permisos para esto");
                            }
                            break;
                        case "4":
                            currentUser.MarkVisitCompleted();
                            break;
                        case "5":
                            currentUser.PrivateZone();
                            break;
                        case "6":
                            currentUser = null;
                            return;
                        default:
                            Log("Opción no válida. Inténtalo de nuevo.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Log($"Error en el menú principal: {ex.Message}");
                }
            }
        }

        static void ViewUserRoutes()
        {
            Console.WriteLine("Rutas de Usuarios:");

            foreach (var user in users)
            {
                Console.WriteLine($"{user.Username}:");
                
                if (user.Visits != null && user.Visits.Count > 0)
                {
                    foreach (var visit in user.Visits)
                    {
                        Console.WriteLine($"  - {visit.Date.ToShortDateString()} - {visit.Location} {(visit.Completed ? "(Completada)" : "(Pendiente)")}");
                    }
                }
                else
                {
                    Console.WriteLine("  - Sin visitas registradas");
                }
                
                Console.WriteLine();
            }
        }

        static void PublicZone()
        {
            Console.WriteLine("Bienvenido a la Zona Pública!");

            while (true)
            {
                Console.WriteLine("1. Ver Productos Públicos");
                Console.WriteLine("2. Volver al Menú Principal");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ViewPublicProducts();
                        break;
                    case "2":
                        return;  // Volver al menú principal
                    default:
                        Console.WriteLine("Opción no válida. Inténtalo de nuevo.");
                        break;
                }
            }
        }

        static void ViewPublicProducts()
        {
            if (products == null || products.Count == 0)
            {
                Console.WriteLine("No hay productos disponibles en la Zona Pública.");
                return;
            }

            Console.WriteLine("Productos Públicos:");

            foreach (var product in products)
            {
                product.ViewProduct();
            }
        }
    }
}
