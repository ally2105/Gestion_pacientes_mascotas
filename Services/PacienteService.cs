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
        // Enlace opcional al servicio global de mascotas para sincronizar registros
        public MascotaService? MascotaService { get; set; }

        public PacienteService(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Registrar()
        {
            try
            {
                Console.WriteLine("=== Registro de nuevo paciente ===");
                Console.Write("Ingrese el nombre del paciente: ");
                string nombre = Console.ReadLine() ?? "";

                int edad;
                while (true)
                {
                    Console.Write("Ingrese la edad del paciente: ");
                    if (int.TryParse(Console.ReadLine(), out edad) && edad >= 0)
                    {
                        break;
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

                    int edadMascota;
                    while (true)
                    {
                        Console.Write("Edad: ");
                        if (int.TryParse(Console.ReadLine(), out edadMascota) && edadMascota >= 0)
                            break;
                        Console.WriteLine("⚠️ Error: La edad debe ser un número válido y mayor o igual a 0.");
                    }

                    var mascota = new Mascota(Guid.NewGuid(), nombreMascota, especieMascota, razaMascota, edadMascota, nuevoPaciente.Nombre);
                    nuevoPaciente.Mascotas.Add(mascota);

                    // Agregar la mascota al DataContext (fuente única)
                    _context.Mascotas.Add(mascota);

                    Console.WriteLine("¿Desea agregar otra mascota? (si/no)");
                    respuestaMascotas = Console.ReadLine() ?? "no";
                }

                // Agregar paciente al contexto
                _context.Pacientes.Add(nuevoPaciente);
                Logger.LogInfo($"Paciente registrado: {nuevoPaciente.Nombre}", "PacienteService.Registrar");
                Console.WriteLine("✅ Paciente registrado exitosamente.");

#if DEBUG
                Console.WriteLine("¿Desea forzar un error para practicar depuración? (si /no)");
                string forzarError = Console.ReadLine() ?? "no";
                if (forzarError.ToLower() == "si")
                {
                    ForzarErrorParaDepuracion();
                }
#endif
            }
            catch (MascotaNoEncontradaException mex)
            {
                Console.WriteLine($"⚠️ Mascota no encontrada: {mex.Message}");
                Logger.LogWarning(mex.Message, "PacienteService.Registrar");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Ocurrió un error durante el registro. Verifique su entrada e intente nuevamente.");
                Console.WriteLine($"Detalles (para desarrolladores): {ex.Message}");
                Logger.LogError(ex, "PacienteService.Registrar");
            }
            finally
            {
                Console.WriteLine("(Registro finalizado)\n");
            }
        }

#if DEBUG
        private void ForzarErrorParaDepuracion()
        {
            int cero = 0;
            int resultado = 1 / cero;
            Console.WriteLine($"Resultado (no debería verse): {resultado}");
        }
#endif

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

        // Remueve la asociación de una mascota de cualquier paciente que la tenga.
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
