using System;
using System.Collections.Generic;   
using Gestion_pacientes_mascotas.Models;

namespace Gestion_pacientes_mascotas.Services
{
    public class VeterinarioService : ServicioVeterinario
    {
        private readonly List<Veterinario> veterinarios = new();

        // Implementación del método abstracto
        public override void Atender()
        {
            Console.WriteLine("🩺 El veterinario está atendiendo a un paciente...");
        }

        // Crear un nuevo veterinario
        public void AgregarVeterinario()
        {
            Console.WriteLine("\n=== Registro de Veterinario ===");

            Console.Write("Nombre: ");
            string? nombre = Console.ReadLine();
            Console.Write("Especialidad: ");
            string? especialidad = Console.ReadLine();
            Console.Write("Teléfono: ");
            string? telefono = Console.ReadLine();
            Console.Write("Email: ");
            string? email = Console.ReadLine();

            // Validación de datos obligatorios
            if (string.IsNullOrWhiteSpace(nombre) ||
                string.IsNullOrWhiteSpace(especialidad) ||
                string.IsNullOrWhiteSpace(telefono) ||
                string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("⚠️ Todos los campos son obligatorios. No se pudo registrar el veterinario.");
                return;
            }

            // Crear y agregar el veterinario
            var veterinario = new Veterinario(nombre, especialidad, telefono, email);
            veterinarios.Add(veterinario);

            Console.WriteLine("✅ Veterinario registrado correctamente.");
        }

        // Mostrar todos los veterinarios registrados
        public void ListarVeterinarios()
        {
            Console.WriteLine("\n=== Lista de Veterinarios ===");

            if (veterinarios.Count == 0)
            {
                Console.WriteLine("No hay veterinarios registrados.");
                return;
            }

            int contador = 1;
            foreach (var v in veterinarios)
            {
                Console.WriteLine($"\n#{contador++}");
                v.MostrarInformacion();
            }
        }

        // Buscar un veterinario por nombre
        public void BuscarVeterinario()
        {
            Console.Write("\nIngrese el nombre del veterinario a buscar: ");
            string? nombre = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("⚠️ Debe ingresar un nombre válido.");
                return;
            }

            var veterinario = veterinarios.Find(v =>
                v.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));

            if (veterinario != null)
            {
                Console.WriteLine("\n✅ Veterinario encontrado:");
                veterinario.MostrarInformacion();
            }
            else
            {
                Console.WriteLine("❌ Veterinario no encontrado.");
            }
        }

        // Editar datos de un veterinario existente
        public void EditarVeterinario()
        {
            Console.Write("\nIngrese el nombre del veterinario a editar: ");
            string? nombre = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("⚠️ Debe ingresar un nombre válido.");
                return;
            }

            var veterinario = veterinarios.Find(v =>
                v.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));

            if (veterinario == null)
            {
                Console.WriteLine("❌ Veterinario no encontrado.");
                return;
            }

            Console.WriteLine("\n=== Editar Veterinario ===");
            Console.Write($"Nuevo nombre (actual: {veterinario.Nombre}): ");
            string? nuevoNombre = Console.ReadLine();
            Console.Write($"Nueva especialidad (actual: {veterinario.Especialidad}): ");
            string? nuevaEspecialidad = Console.ReadLine();
            Console.Write($"Nuevo teléfono (actual: {veterinario.Telefono}): ");
            string? nuevoTelefono = Console.ReadLine();
            Console.Write($"Nuevo email (actual: {veterinario.Email}): ");
            string? nuevoEmail = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(nuevoNombre))
                veterinario.Nombre = nuevoNombre;
            if (!string.IsNullOrWhiteSpace(nuevaEspecialidad))
                veterinario.Especialidad = nuevaEspecialidad;
            if (!string.IsNullOrWhiteSpace(nuevoTelefono))
                veterinario.Telefono = nuevoTelefono;
            if (!string.IsNullOrWhiteSpace(nuevoEmail))
                veterinario.Email = nuevoEmail;

            Console.WriteLine("✅ Datos del veterinario actualizados correctamente.");
        }

        // Eliminar un veterinario por nombre
        public void EliminarVeterinario()
        {
            Console.Write("\nIngrese el nombre del veterinario a eliminar: ");
            string? nombre = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("⚠️ Debe ingresar un nombre válido.");
                return;
            }

            var veterinario = veterinarios.Find(v =>
                v.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));

            if (veterinario != null)
            {
                veterinarios.Remove(veterinario);
                Console.WriteLine("✅ Veterinario eliminado correctamente.");
            }
            else
            {
                Console.WriteLine("❌ Veterinario no encontrado.");
            }
        }
    }
}
