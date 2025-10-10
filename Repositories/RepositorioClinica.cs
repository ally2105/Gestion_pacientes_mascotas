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
            if (mascota == null)
            {
                Console.WriteLine($"No se encontró mascota con Id {id}");
                return;
            }
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
        //---------------------Veterinarios---------------------
        private readonly List<Veterinario> veterinarios = new List<Veterinario>();
        public void AgregarVeterinario(Veterinario v)
        {
            veterinarios.Add(v);
            Logger.LogInfo($"Veterinario agregado: {v.Nombre}", "RepositorioClinica");
        }
        public List<Veterinario> ObtenerVeterinarios() => veterinarios;
        //----------------------Citas-------------------------------
        private readonly List<Cita> citas = new List<Cita>();
        public void AgregarCita(Cita c)
        {
            citas.Add(c);
            Logger.LogInfo($"Cita agregada para la mascota: {c.Mascota.Nombre}", "RepositorioClinica");
        }
        public List<Cita> ObtenerCitas() => citas;
    }
}
