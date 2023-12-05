using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using rutasComerciales;

namespace RutasComerciales
{
    class Program
    {

        static string logFilePath = "error_log.txt";

               static void Main()
        {
            // Inicializar el registro de errores
            File.WriteAllText(logFilePath, "Inicio del registro de errores\n");



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
  
                            break;
                        case "2":
              
                            break;
                        case "3":
                   
                            break;
                        case "4":
                     
                            break;
             
                       
                    }
                }
                catch (Exception ex)
                {
            
                }
            }
        }