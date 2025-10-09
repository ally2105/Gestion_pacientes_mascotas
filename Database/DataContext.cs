using Gestion_pacientes_mascotas.Models;

namespace Gestion_pacientes_mascotas.Database
{
    public class DataContext
    {
        public List<Paciente> Pacientes { get; set; } = new();
        public List<Mascota> Mascotas { get; set; } = new();
        public List<Veterinario> Veterinarios { get; set; } = new();
        public List<Cita> Citas { get; set; } = new();

        public DataContext()
        {
            // Ejemplo opcional de datos iniciales (puedes quitarlo)
            // var mascotaDemo = new Mascota(Guid.NewGuid(), "Luna", "Gato", "Siames", 3, "Ana");
            // var pacienteDemo = new Paciente("Ana", 28, "Calle 123", "3001234567");
            // pacienteDemo.Mascotas.Add(mascotaDemo);
            // Pacientes.Add(pacienteDemo);
            // Mascotas.Add(mascotaDemo);
        }

        // Punto de extensión para persistencia futura (archivo/BD)
        public void GuardarCambios()
        {
            // Por ahora es simulación; aquí podrías serializar a JSON, etc.
            Console.WriteLine("💾 DataContext: cambios guardados en memoria.");
        }
    }
}
