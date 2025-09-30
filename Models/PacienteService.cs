using System;
using System.Collections.Generic;
using System.Linq;

namespace Gestion_pacientes_mascotas.Models
{
    public class PacienteService
    {
    public static void RegistrarPaciente(List<Paciente> pacientes)
        {
            Console.WriteLine("=== Registro de nuevo paciente ===");

            Console.Write("Ingrese el nombre del paciente: ");
            string nombre = Console.ReadLine() ?? "";

            int edad;
            try
            {
                Console.Write("Ingrese la edad del paciente: ");
                edad = int.Parse(Console.ReadLine() ?? "0");
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: La edad debe ser un número válido.");
                return;
            }
            catch (OverflowException)
            {
                Console.WriteLine("Error: La edad está fuera del rango permitido.");
                return;
            }

            Console.Write("Ingrese la dirección del paciente: ");
            string direccion = Console.ReadLine() ?? "";

            Console.Write("Ingrese el teléfono del paciente: ");
            string telefono = Console.ReadLine() ?? "";

            Paciente nuevoPaciente = new Paciente(nombre, edad, direccion, telefono);

            Console.WriteLine("¿Desea agregar mascotas para este paciente? (si/no)");
            string respuestaMascotas = Console.ReadLine() ?? "no";
            while (respuestaMascotas.ToLower() == "si")
            {
                Console.Write("Nombre de la mascota: ");
                string nombreMascota = Console.ReadLine() ?? "";
                Console.Write("Especie: ");
                string especieMascota = Console.ReadLine() ?? "";
                Console.Write("Raza: ");
                string razaMascota = Console.ReadLine() ?? "";
                Console.Write("Edad: ");
                int edadMascota = 0;
                int.TryParse(Console.ReadLine(), out edadMascota);
                Mascota mascota = new Mascota(nombreMascota, especieMascota, razaMascota, edadMascota);
                nuevoPaciente.Mascotas.Add(mascota);
                Console.WriteLine("¿Desea agregar otra mascota? (si/no)");
                respuestaMascotas = Console.ReadLine() ?? "no";
            }

            pacientes.Add(nuevoPaciente);
            Console.WriteLine("Paciente registrado exitosamente.");
        }

        public static void VerPacientes(List<Paciente> pacientes)
        {
            Console.WriteLine("=== Lista de Pacientes Registrados ===");
            if (pacientes.Count == 0)
            {
                Console.WriteLine("No hay pacientes registrados.");
                return;
            }
            foreach (var paciente in pacientes)
            {
                paciente.MostrarInformacion();
                paciente.MostrarMascotas();
                Console.WriteLine("-------------------------------");
            }
        }

        public static void BuscarPaciente(List<Paciente> pacientes)
        {
            Console.Write("Ingrese el nombre del paciente a buscar: ");
            string nombreBusqueda = Console.ReadLine() ?? "";
            var pacientesEncontrados = pacientes
                .Where(p => p.Nombre.Equals(nombreBusqueda, StringComparison.OrdinalIgnoreCase))
                .ToList();
            if (pacientesEncontrados.Count == 0)
            {
                Console.WriteLine("No se encontraron pacientes con ese nombre.");
                return;
            }
            Console.WriteLine($"=== Resultados de la búsqueda para '{nombreBusqueda}' ===");
            var result = pacientesEncontrados.OrderBy(p => p.Nombre).ToList();
            foreach (var paciente in result)
            {
                paciente.MostrarInformacion();
                paciente.MostrarMascotas();
                Console.WriteLine("-------------------------------");
            }
        }
    }
}
