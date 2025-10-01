namespace Gestion_pacientes_mascotas.Models
{
        public class Mascota : Animal
        {
            private string raza = string.Empty;
            private string dueno = string.Empty;

            public string Raza
            {
                get { return raza; }
                set { raza = !string.IsNullOrEmpty(value) ? value : throw new Exception("La raza no puede estar vacía"); }
            }

            public string Dueno
            {
                get { return dueno; }
                set { dueno = !string.IsNullOrEmpty(value) ? value : throw new Exception("El dueño no puede estar vacío"); }
            }

            public Mascota(string nombre, string especie, string raza, int edad, string dueno = "") : base(nombre, especie, edad)
            {
                Raza = raza;
                Dueno = dueno;
            }

            public override void EmitirSonido()
            {
                switch (Especie.ToLower())
                {
                    case "perro":
                        Console.WriteLine("Guau");
                        break;
                    case "gato":
                        Console.WriteLine("Miau");
                        break;
                    case "pájaro":
                        Console.WriteLine("Pío");
                        break;
                    case "vaca":
                        Console.WriteLine("Muu");
                        break;
                    case "oveja":
                        Console.WriteLine("Beee");
                        break;
                    case "cerdo":
                        Console.WriteLine("Oink");
                        break;
                    default:
                        Console.WriteLine("El animal emite un sonido.");
                        break;
                }
            }

            public void MostrarInformacion()
            {
                Console.WriteLine($"Nombre: {Nombre}\nEspecie: {Especie}\nRaza: {Raza}\nEdad: {Edad}");
            }
        }
}
