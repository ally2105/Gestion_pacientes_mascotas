using System;
using System.Collections.Generic;
using Gestion_pacientes_mascotas.Models;

class Program
{
    static void Main()
    {
        List<Paciente> listaPacientes = new List<Paciente>();
        string opcion = "";

        while (opcion.ToLower() != "salir")
        {
            Console.WriteLine("=== Clínica Salud - Menú Principal ===");
            Console.WriteLine("1. Registrar Paciente");
            Console.WriteLine("2. Ver Pacientes");
            Console.WriteLine("3. Buscar Paciente");
            Console.WriteLine("Escribe 'salir' para salir");
            Console.Write("Seleccione una opción: ");
            opcion = Console.ReadLine();

            Console.WriteLine();

            switch (opcion)
            {
                case "1":
                    PacienteService.RegistrarPaciente(listaPacientes);
                    Console.WriteLine("Paciente registrado con éxito.");
                    break;

                case "2":
                    PacienteService.VerPacientes(listaPacientes);

                    bool continuarSubMenu = true;
                    while (continuarSubMenu)
                    {
                        Console.WriteLine("Escribe 1. para agregar otro paciente");
                        Console.WriteLine("Escribe 2. para editar un paciente");
                        Console.WriteLine("Escribe 3. para eliminar un paciente");
                        Console.WriteLine("Escribe 4. para volver al menú principal");
                        string subOpcion = Console.ReadLine();

                        switch (subOpcion)
                        {
                            case "1":
                                PacienteService.RegistrarPaciente(listaPacientes);
                                break;

                            case "2":
                                Console.Write("Ingrese el ID del paciente a editar: ");
                                string idEdicion = Console.ReadLine();
                                var pacienteAEditar = listaPacientes.Find(p => p.Id.ToString() == idEdicion);
                                if (pacienteAEditar != null)
                                {
                                    Console.Write("Nuevo nombre (deje vacío para no cambiar): ");
                                    string nuevoNombre = Console.ReadLine();
                                    if (!string.IsNullOrEmpty(nuevoNombre))
                                        pacienteAEditar.Nombre = nuevoNombre;

                                    Console.Write("Nueva edad (deje vacío para no cambiar): ");
                                    string nuevaEdadInput = Console.ReadLine();
                                    if (!string.IsNullOrEmpty(nuevaEdadInput))
                                    {
                                        try
                                        {
                                            byte nuevaEdad = byte.Parse(nuevaEdadInput);
                                            pacienteAEditar.Edad = nuevaEdad;
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Error: La edad debe ser un número válido.");
                                            continue;
                                        }
                                        catch (OverflowException)
                                        {
                                            Console.WriteLine("Error: La edad está fuera del rango permitido (0-255).");
                                            continue;
                                        }
                                    }

                                    Console.Write("Nuevos síntomas (deje vacío para no cambiar): ");
                                    string nuevosSintomas = Console.ReadLine();
                                    if (!string.IsNullOrEmpty(nuevosSintomas))
                                        pacienteAEditar.Sintomas = nuevosSintomas;

                                    Console.WriteLine("Paciente actualizado con éxito.");
                                }
                                else
                                {
                                    Console.WriteLine("Paciente no encontrado.");
                                }
                                break;

                            case "3":
                                Console.Write("Ingrese el ID del paciente a eliminar: ");
                                string idEliminacion = Console.ReadLine();
                                var pacienteAEliminar = listaPacientes.Find(p => p.Id.ToString() == idEliminacion);
                                if (pacienteAEliminar != null)
                                {
                                    listaPacientes.Remove(pacienteAEliminar);
                                    Console.WriteLine("Paciente eliminado con éxito.");
                                }
                                else
                                {
                                    Console.WriteLine("Paciente no encontrado.");
                                }
                                break;

                            case "4":
                                continuarSubMenu = false; // salir del submenú
                                break;

                            default:
                                Console.WriteLine("Opción no válida. Intente de nuevo.");
                                break;
                        }
                    }
                    break;

                case "3":
                    PacienteService.BuscarPaciente(listaPacientes);
                    break;

                case "salir":
                    Console.WriteLine("Saliendo del programa...");
                    break;

                default:
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    break;
            }

            if (opcion.ToLower() != "salir")
            {
                Console.WriteLine("\nPresione una tecla para continuar...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
