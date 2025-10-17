using System;

namespace Gestion_pacientes_mascotas.Models
{
    // Custom exception for when a pet is not found
    public class PetNotFoundException : Exception
    {
        public PetNotFoundException() { }
        public PetNotFoundException(string message) : base(message) { }
        public PetNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}