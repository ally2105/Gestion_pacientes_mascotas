using System;

namespace Gestion_pacientes_mascotas.Models
{
    public class Cita
    {
        public DateTime FechaHora { get; set; }
        public string Motivo { get; set; }
        public Mascota Mascota { get; set; }
        public Veterinario Veterinario { get; set; }

        public Cita(DateTime fechaHora, string motivo, Mascota mascota, Veterinario veterinario)
        {
            FechaHora = fechaHora;
            Motivo = motivo;
            Mascota = mascota;
            Veterinario = veterinario;
        }

        public void MostrarInformacion()
        {
            Console.WriteLine($"Fecha y Hora: {FechaHora}\nMotivo: {Motivo}\n--- Información de la Mascota ---");
            Mascota.MostrarInformacion();
            Console.WriteLine("--- Información del Veterinario ---");
            Veterinario.MostrarInformacion();
        }
    }
}
