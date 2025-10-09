using Gestion_pacientes_mascotas.Models;
using Gestion_pacientes_mascotas.Utils;

namespace Gestion_pacientes_mascotas.Repositories
{
    public class RepositorioClinica
    {
        private readonly List<Paciente> pacientes = new List<Paciente>();
        private readonly List<Mascota> mascotas = new List<Mascota>();

        // -------------------- PACIENTES --------------------

        public void AgregarPaciente(Paciente p)
        {
            pacientes.Add(p);
            Logger.LogInfo($"Paciente agregado: {p.Nombre}", "RepositorioClinica");
        }

        public List<Paciente> ObtenerPacientes() => pacientes;

        // -------------------- MASCOTAS --------------------

        public void AgregarMascota(Mascota m)
        {
            mascotas.Add(m);
            Logger.LogInfo($"Mascota agregada: {m.Nombre}", "RepositorioClinica");
        }

        public List<Mascota> ObtenerMascotas() => mascotas;

        public Mascota? BuscarMascotaPorId(Guid id)
        {
            var mascota = mascotas.FirstOrDefault(m => m.Id == id);
            if (mascota == null)
                throw new MascotaNoEncontradaException($"No se encontró mascota con Id {id}");
            return mascota;
        }

        public void EditarMascota(Guid id)
        {
            var mascota = BuscarMascotaPorId(id);
            Console.WriteLine($"Editando mascota: {mascota.Nombre}");
            Utils.MascotaInteractor.EditarCamposInteractivos(mascota);
            Logger.LogInfo($"Mascota {id} actualizada correctamente", "RepositorioClinica");
            Console.WriteLine("✅ Mascota actualizada con éxito.");
        }

        public void EliminarMascota(Guid id)
        {
            var mascota = BuscarMascotaPorId(id);
            if (mascota != null)
            {
                mascotas.Remove(mascota);
                Logger.LogInfo($"Mascota eliminada: {mascota.Nombre}", "RepositorioClinica");
                Console.WriteLine($"✅ Mascota {mascota.Nombre} eliminada correctamente.");
            }
        }
    }
}
