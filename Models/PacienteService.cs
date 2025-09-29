using System;
using System.Collections.Generic;
using System.Linq;

namespace Gestion_pacientes_mascotas.Models
{
    public class PacienteService
    {
        public static void RegistrarPaciente(Dictionary<Guid, Paciente> pacientesDic)
        {
            Console.WriteLine("=== Registro de nuevo paciente ===");

            Console.Write("Ingrese el nombre del paciente: ");
            string nombre = Console.ReadLine();

            byte edad;
            try
            {
                Console.Write("Ingrese la edad del paciente: ");
                edad = byte.Parse(Console.ReadLine() ?? "0");
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: La edad debe ser un número válido.");
                return;
            }
            catch (OverflowException)
            {
                Console.WriteLine("Error: La edad está fuera del rango permitido (0-255).");
                return;
            }

            Console.Write("Ingrese la especie del paciente: ");
            string especie = Console.ReadLine();

            Console.Write("Ingrese los síntomas del paciente: ");
            string sintomas = Console.ReadLine();

            Paciente nuevoPaciente = new Paciente
            {
                Id = Guid.NewGuid(),
                Nombre = nombre,
                Edad = edad,
                Especie = especie,
                Sintomas = sintomas
            };

            pacientesDic.Add(nuevoPaciente.Id, nuevoPaciente);
            Console.WriteLine($"Paciente registrado con ID: {nuevoPaciente.Id}");
        }

        public static void VerPacientes(Dictionary<Guid, Paciente> pacientesDic)
        {
            Console.WriteLine("=== Lista de Pacientes Registrados ===");

            if (pacientesDic.Count == 0)
            {
                Console.WriteLine("No hay pacientes registrados.");
                return;
            }

            foreach (var paciente in pacientesDic.Values)
            {
                Console.WriteLine($"ID: {paciente.Id}");
                Console.WriteLine($"Nombre: {paciente.Nombre}");
                Console.WriteLine($"Edad: {paciente.Edad}");
                Console.WriteLine($"Especie: {paciente.Especie}");
                Console.WriteLine($"Síntomas: {paciente.Sintomas}");
                Console.WriteLine("-------------------------------");
            }
        }

        public static void BuscarPaciente(Dictionary<Guid, Paciente> pacientesDic)
        {
            Console.Write("Ingrese el nombre del paciente a buscar: ");
            string nombreBusqueda = Console.ReadLine();

            var pacientesEncontrados = pacientesDic.Values
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
                Console.WriteLine($"ID: {paciente.Id}");
                Console.WriteLine($"Nombre: {paciente.Nombre}");
                Console.WriteLine($"Edad: {paciente.Edad}");
                Console.WriteLine($"Especie: {paciente.Especie}");
                Console.WriteLine($"Síntomas: {paciente.Sintomas}");
                Console.WriteLine("-------------------------------");
            }
        }
    }
}
