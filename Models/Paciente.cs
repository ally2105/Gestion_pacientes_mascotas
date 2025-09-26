namespace Gestion_pacientes_mascotas.Models
{
    public class Paciente
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public Byte Edad { get; set; }
        public string Sintomas { get; set; }
        public string Especie { get; set; }
    }
}