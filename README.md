# Gestion_pacientes_mascotas

Este repositorio contiene un pequeño sistema para gestionar pacientes y
sus mascotas. Aquí se documentan decisiones de diseño relacionadas con el
uso de clases abstractas e interfaces, ejemplos prácticos y recomendaciones.

## Diferencia entre clase abstracta e interfaz

- Clase abstracta:
	- Puede contener implementación compartida y campos protegidos.
	- Útil cuando hay comportamiento común que varias subclases deben heredar.
	- Se usa cuando se desea proporcionar estado o métodos con implementación.

- Interfaz:
	- Define solo el contrato (métodos, propiedades), sin implementación.
	- Permite múltiples implementaciones y es ideal para desacoplar componentes.
	- Facilita tests y la inyección de dependencias.

## Qué se usó en este proyecto y por qué

- `Animal` (en `Models/Animal.cs`) es una clase abstracta: representa un
	concepto general (nombre, especie, edad) y proporciona `EmitirSonido()` con
	implementación por defecto; las subclases (p. ej. `Mascota`) sobrescriben
	este método para comportamientos particulares (polimorfismo).

- `ServicioVeterinario` (en `Services/ServicioVeterinario.cs`) es abstracta
	porque puede contener lógica o estado compartido en el futuro y fuerza a
	las subclases a implementar `Atender()`.

- `IRegistrable` (en `Models/IRegistrable.cs`) es una interfaz que define el
	contrato `Registrar()`. Se implementa en los servicios (`PacienteService`,
	`MascotaService`) para permitir múltiples estrategias de registro
	(memoria, persistencia, API) y facilitar pruebas.

## Dónde una interfaz aporta mayor flexibilidad

- Servicios de registro (Paciente/Mascota): usar `IRegistrable` permite pasar
	diferentes implementaciones según el entorno (tests, producción, mocks).

- Repositorios o adaptadores: si más adelante añades una capa de persistencia,
	define `IRepository<T>` y crea implementaciones para la base de datos o para
	una versión en memoria usada en tests.

## Ejemplos prácticos

- Polimorfismo con `EmitirSonido()` (en `Program.cs` puedes crear):

```csharp
var animales = new List<Animal>
{
		new Mascota("Firulais", "Perro", "Labrador", 5, "Juan"),
		new Mascota("Misu", "Gato", "Siames", 3, "Ana")
};
foreach (var a in animales)
		a.EmitirSonido(); // Guau, Miau
```

- Registro centralizado (ya implementado en `Services/PacienteService.cs`):
	la entrada por consola y la construcción de objetos ocurren dentro del
	servicio; las entidades (`Paciente`, `Mascota`) se mantienen como POCOs.

## Recomendaciones

- Mantén la lógica de E/S y validación en servicios; las entidades deben ser
	modelos de datos limpios.
- Añade interfaces para puntos de extensión (servicios, repositorios) para
	facilitar la inyección de dependencias y testing.
- Usa clases abstractas para compartir código entre subclases cuando exista
	comportamiento común.

---

Si quieres, puedo:
- Añadir el snippet de polimorfismo directamente a `Program.cs`.
- Crear un `IRepository<T>` y ejemplo de implementación en memoria.
- Generar tests unitarios básicos.
