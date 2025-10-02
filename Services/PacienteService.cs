using System;
using System.Collections.Generic;
using System.Linq;

namespace Gestion_pacientes_mascotas.Models
{
    public class PacienteService : IRegistrable
    {
        private List<Paciente> pacientes = new List<Paciente>();

        public void Registrar()
        {
            Console.WriteLine("=== Registro de nuevo paciente ===");
            Console.Write("Ingrese el nombre del paciente: ");
            string nombre = Console.ReadLine() ?? "";

            // 🔹 Validación robusta para la edad del paciente
            int edad;
            while (true)
            {
                Console.Write("Ingrese la edad del paciente: ");
                if (int.TryParse(Console.ReadLine(), out edad) && edad >= 0)
                {
                    break; // válido → salimos del bucle
                }
                else
                {
                    Console.WriteLine("⚠️ Error: La edad debe ser un número válido y mayor o igual a 0.");
                }
            }

            Console.Write("Ingrese la dirección del paciente: ");
            string direccion = Console.ReadLine() ?? "";
            while (string.IsNullOrWhiteSpace(direccion))
            {
                Console.WriteLine("⚠️ Error: La dirección no puede estar vacía.");
                Console.Write("Ingrese la dirección del paciente: ");
                direccion = Console.ReadLine() ?? "";
            }
            Console.Write("Ingrese el teléfono del paciente: ");
            string telefono = Console.ReadLine() ?? "";
            while (telefono.Length != 10 || !telefono.All(char.IsDigit))
            {
                Console.WriteLine("⚠️ Error: El teléfono debe tener 10 dígitos numéricos.");
                Console.Write("Ingrese el teléfono del paciente: ");
                telefono = Console.ReadLine() ?? "";
            }
            Paciente nuevoPaciente = new Paciente(nombre, edad, direccion, telefono);

            Console.WriteLine("¿Desea agregar mascotas para este paciente? (si/no)");
            string respuestaMascotas = Console.ReadLine() ?? "no";
            while (respuestaMascotas.ToLower() == "si")
            {
                Console.Write("Nombre de la mascota: ");
                string nombreMascota = Console.ReadLine() ?? "";
                Console.WriteLine(string.IsNullOrWhiteSpace(nombreMascota) ? "⚠️ Error: El nombre de la mascota no puede estar vacío." : "");
                while (string.IsNullOrWhiteSpace(nombreMascota))
                {
                    Console.Write("Nombre de la mascota: ");
                    nombreMascota = Console.ReadLine() ?? "";
                }
                Console.Write("Ingrese la especie de la mascota: ");
                string especieMascota = Console.ReadLine() ?? "";
                while (string.IsNullOrWhiteSpace(especieMascota))
                {
                    Console.WriteLine("⚠️ Error: La especie no puede estar vacía.");
                    Console.Write("Especie: ");
                    especieMascota = Console.ReadLine() ?? "";
                }
                Console.Write("Raza: ");
                string razaMascota = Console.ReadLine() ?? "";
                while (string.IsNullOrWhiteSpace(razaMascota))
                {
                    Console.WriteLine("⚠️ Error: La raza no puede estar vacía.");
                    Console.Write("Raza: ");
                    razaMascota = Console.ReadLine() ?? "";
                }

                // 🔹 Validación robusta para la edad de la mascota
                int edadMascota;
                while (true)
                {
                    Console.Write("Edad: ");
                    if (int.TryParse(Console.ReadLine(), out edadMascota) && edadMascota >= 0)
                    {
                    }
                    else
                    {
                        Console.WriteLine("⚠️ Error: La edad debe ser un número válido y mayor o igual a 0.");
                    }
                }

                Mascota mascota = new Mascota(nombreMascota, especieMascota, razaMascota, edadMascota, nuevoPaciente.Nombre);
                nuevoPaciente.Mascotas.Add(mascota);

                Console.WriteLine("¿Desea agregar otra mascota? (si/no)");
                respuestaMascotas = Console.ReadLine() ?? "no";

                pacientes.Add(nuevoPaciente);
                Console.WriteLine("Paciente registrado exitosamente.");
            }
        }

        public void VerPacientes()
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
