using System;
using System.Linq;
using System.Collections.Generic;
using Gestion_pacientes_mascotas.Database;
using Gestion_pacientes_mascotas.Utils;

namespace Gestion_pacientes_mascotas.Models
{
    public class PacienteService : IRegistrable
    {
        private readonly DataContext _context;
        public MascotaService? MascotaService { get; set; }

        public PacienteService(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // ===========================================
        // 🟢 CREATE - Registrar Paciente
        // ===========================================
        public void Registrar()
        {
            try
            {
                Console.WriteLine("=== Registro de nuevo paciente ===");
                Console.Write("Ingrese el nombre del paciente: ");
                string nombre = Console.ReadLine() ?? "";

                if (_context.Pacientes.Any(p => p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine("⚠️ Ya existe un paciente con ese nombre. Use otro o actualice el existente.");
                    return;
                }

                int edad;
                while (true)
                {
                    Console.Write("Ingrese la edad del paciente: ");
                    if (int.TryParse(Console.ReadLine(), out edad) && edad >= 0)
                        break;
                    Console.WriteLine("⚠️ Error: La edad debe ser un número válido y mayor o igual a 0.");
                }

                Console.Write("Ingrese la dirección del paciente: ");
                string direccion = Console.ReadLine() ?? "";
                while (string.IsNullOrWhiteSpace(direccion))
                {
                    Console.WriteLine("⚠️ Error: La dirección no puede estar vacía.");
                    direccion = Console.ReadLine() ?? "";
                }

                Console.Write("Ingrese el teléfono del paciente (10 dígitos): ");
                string telefono = Console.ReadLine() ?? "";
                while (telefono.Length != 10 || !telefono.All(char.IsDigit))
                {
                    Console.WriteLine("⚠️ Error: El teléfono debe tener 10 dígitos numéricos.");
                    telefono = Console.ReadLine() ?? "";
                }

                var nuevoPaciente = new Paciente(nombre, edad, direccion, telefono);

                // Registro de mascotas vinculadas (opcional)
                Console.WriteLine("¿Desea agregar mascotas para este paciente? (si/no)");
                string respuestaMascotas = Console.ReadLine() ?? "no";
                while (respuestaMascotas.Trim().ToLower() == "si")
                {
                    Console.Write("Nombre de la mascota: ");
                    string nombreMascota = Console.ReadLine() ?? "";
                    while (string.IsNullOrWhiteSpace(nombreMascota))
                    {
                        Console.WriteLine("⚠️ Error: El nombre de la mascota no puede estar vacío.");
                        nombreMascota = Console.ReadLine() ?? "";
                    }

                    Console.Write("Ingrese la especie: ");
                    string especieMascota = Console.ReadLine() ?? "";
                    while (string.IsNullOrWhiteSpace(especieMascota))
                    {
                        Console.WriteLine("⚠️ Error: La especie no puede estar vacía.");
                        especieMascota = Console.ReadLine() ?? "";
                    }

                    Console.Write("Raza: ");
                    string razaMascota = Console.ReadLine() ?? "";
                    while (string.IsNullOrWhiteSpace(razaMascota))
                    {
                        Console.WriteLine("⚠️ Error: La raza no puede estar vacía.");
                        razaMascota = Console.ReadLine() ?? "";
                    }

                    int edadMascota;
                    while (true)
                    {
                        Console.Write("Edad: ");
                        if (int.TryParse(Console.ReadLine(), out edadMascota) && edadMascota >= 0)
                            break;
                        Console.WriteLine("⚠️ Error: La edad debe ser válida.");
                    }

                    var mascota = new Mascota(Guid.NewGuid(), nombreMascota, especieMascota, razaMascota, edadMascota, nuevoPaciente.Nombre);
                    nuevoPaciente.Mascotas.Add(mascota);
                    _context.Mascotas.Add(mascota);

                    Console.WriteLine("¿Desea agregar otra mascota? (si/no)");
                    respuestaMascotas = Console.ReadLine() ?? "no";
                }

                _context.Pacientes.Add(nuevoPaciente);
                Logger.LogInfo($"Paciente registrado: {nuevoPaciente.Nombre}", "PacienteService.Registrar");
                Console.WriteLine("✅ Paciente registrado exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Ocurrió un error durante el registro.");
                Logger.LogError(ex, "PacienteService.Registrar");
            }
            finally
            {
                Console.WriteLine("(Registro finalizado)\n");
            }
        }

        // ===========================================
        // 🟡 READ - Ver todos los pacientes
        // ===========================================
        public void VerPacientes()
        {
            Console.WriteLine("=== Lista de Pacientes Registrados ===");
            if (!_context.Pacientes.Any())
            {
                Console.WriteLine("No hay pacientes registrados.");
                return;
            }

            foreach (var paciente in _context.Pacientes)
            {
                paciente.MostrarInformacion();
                paciente.MostrarMascotas();
                Console.WriteLine("-------------------------------");
            }
        }

        // ===========================================
        // 🔵 READ - Buscar paciente por nombre
        // ===========================================
        public void BuscarPacientePorNombre()
        {
            Console.Write("Ingrese el nombre del paciente a buscar: ");
            string nombreBuscar = Console.ReadLine() ?? "";

            var paciente = _context.Pacientes.FirstOrDefault(p =>
                p.Nombre.Equals(nombreBuscar, StringComparison.OrdinalIgnoreCase));

            if (paciente == null)
            {
                Console.WriteLine("⚠️ Paciente no encontrado.");
                return;
            }

            paciente.MostrarInformacion();
            paciente.MostrarMascotas();
        }

        // ===========================================
        // 🟠 UPDATE - Actualizar paciente
        // ===========================================
        public void ActualizarPaciente()
        {
            Console.WriteLine("=== Actualizar Paciente ===");

            if (!_context.Pacientes.Any())
            {
                Console.WriteLine("No hay pacientes registrados.");
                return;
            }

            Console.Write("Ingrese el nombre del paciente a actualizar: ");
            string nombreBuscar = Console.ReadLine() ?? "";

            var paciente = _context.Pacientes.FirstOrDefault(p =>
                p.Nombre.Equals(nombreBuscar, StringComparison.OrdinalIgnoreCase));

            if (paciente == null)
            {
                Console.WriteLine("⚠️ Paciente no encontrado.");
                return;
            }

            Console.WriteLine($"Paciente encontrado: {paciente.Nombre}");
            Console.WriteLine("Deje vacío cualquier campo que no desee modificar.");

            Console.Write("Nuevo nombre: ");
            string nuevoNombre = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(nuevoNombre))
                paciente.Nombre = nuevoNombre;

            Console.Write("Nueva edad: ");
            string nuevaEdad = Console.ReadLine() ?? "";
            if (int.TryParse(nuevaEdad, out int edad))
                paciente.Edad = edad;

            Console.Write("Nueva dirección: ");
            string nuevaDireccion = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(nuevaDireccion))
                paciente.Direccion = nuevaDireccion;

            Console.Write("Nuevo teléfono: ");
            string nuevoTelefono = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(nuevoTelefono))
                paciente.Telefono = nuevoTelefono;

            Console.WriteLine("✅ Paciente actualizado correctamente.");
            Logger.LogInfo($"Paciente actualizado: {paciente.Nombre}", "PacienteService.ActualizarPaciente");
        }

        // ===========================================
        // 🔴 DELETE - Eliminar paciente
        // ===========================================
        public void EliminarPaciente()
        {
            Console.WriteLine("=== Eliminar Paciente ===");

            if (!_context.Pacientes.Any())
            {
                Console.WriteLine("No hay pacientes registrados.");
                return;
            }

            Console.Write("Ingrese el nombre del paciente a eliminar: ");
            string nombreBuscar = Console.ReadLine() ?? "";

            var paciente = _context.Pacientes.FirstOrDefault(p =>
                p.Nombre.Equals(nombreBuscar, StringComparison.OrdinalIgnoreCase));

            if (paciente == null)
            {
                Console.WriteLine("⚠️ Paciente no encontrado.");
                return;
            }

            Console.WriteLine($"¿Está seguro de eliminar al paciente {paciente.Nombre}? (si/no)");
            string confirmacion = Console.ReadLine() ?? "no";
            if (confirmacion.ToLower() != "si")
            {
                Console.WriteLine("❌ Operación cancelada.");
                return;
            }

            // También eliminar sus mascotas del contexto
            foreach (var mascota in paciente.Mascotas.ToList())
                _context.Mascotas.Remove(mascota);

            _context.Pacientes.Remove(paciente);
            Console.WriteLine("✅ Paciente eliminado correctamente.");
            Logger.LogInfo($"Paciente eliminado: {paciente.Nombre}", "PacienteService.EliminarPaciente");
        }

        // ===========================================
        // 🧩 Utilidad adicional: Remover asociación mascota
        // ===========================================
        public void RemoverAsociacionMascota(Guid id)
        {
            foreach (var paciente in _context.Pacientes)
            {
                var m = paciente.Mascotas.FirstOrDefault(x => x.Id == id);
                if (m != null)
                {
                    paciente.Mascotas.Remove(m);
                    Console.WriteLine($"Asociación: mascota con Id {id} removida del paciente {paciente.Nombre}.");
                    return;
                }
            }
        }
    }
}
