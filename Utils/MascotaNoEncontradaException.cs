using System;

namespace Gestion_pacientes_mascotas.Models
{
    // Excepción personalizada para cuando no se encuentra una mascota
    public class MascotaNoEncontradaException : Exception
    {
        public MascotaNoEncontradaException() { }
        public MascotaNoEncontradaException(string message) : base(message) { }
        public MascotaNoEncontradaException(string message, Exception inner) : base(message, inner) { }
    }
}
