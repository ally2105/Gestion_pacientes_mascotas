namespace Gestion_pacientes_mascotas.Models;

using Gestion_pacientes_mascotas.Interfaces;

public class Owner : INotifiable
{
    // Design: Owner is a data model (POCO). It does not implement IRegisterable
    // because registration (console input, additional validation, persistence)
    // is considered application logic and is centralized in OwnerService.
    // This keeps the entity clean and facilitates testing and reuse.

    // Encapsulation: sensitive fields (address, phone) are private
    // and exposed through properties with validation to prevent invalid states.

    private string address = string.Empty;
    private string phone = string.Empty;

    public string Name { get; set; }

    private int age;
    public int Age
    {
        get { return age; }
        set
        {
            if (value >= 0)
                age = value;
            else
                throw new Exception("Age cannot be negative");
        }
    }

    public string Address
    {
        get { return address; }
        set
        {
            if (!string.IsNullOrEmpty(value))
                address = value;
            else
                throw new Exception("Address cannot be empty");
        }
    }

    public string Phone
    {
        get { return phone; }
        set
        {
            if (value.Length == 10)
                phone = value;
            else
                throw new Exception("Phone must have 10 digits");
        }
    }

    public List<Pet> Pets { get; set; }

    public Owner(string name, int age, string address, string phone)
    {
        Name = name;
        Age = age;
        Address = address;
        Phone = phone;
        Pets = new List<Pet>();
    }

    public void ShowInformation()
    {
        // Show basic information; for security, we don't reveal the full phone number in the output
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Age: {Age}");
        Console.WriteLine($"Address: {Address}");
        Console.WriteLine($"Phone: {ObfuscatePhone(Phone)}");
    }

    public void ShowPets()
    {
        if (Pets == null || Pets.Count == 0)
        {
            Console.WriteLine("No pets registered.");
            return;
        }
        Console.WriteLine($"Pets of {Name}:");
        foreach (var pet in Pets)
        {
            pet.ShowInformation();
            Console.WriteLine("---");
        }
    }

    /// <summary>
    /// Sends a notification to the owner.
    /// This is a simple console implementation of the INotifiable interface.
    /// In a real application, this would delegate to an external service (e.g., SMS, Email).
    /// </summary>
    public void SendNotification(string message)
    {
        var visiblePhone = ObfuscatePhone(Phone);
        Console.WriteLine($"[Notification] Sending to {Name} ({visiblePhone}): {message}");
    }

    private string ObfuscatePhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone)) return "(no number)";
        var digits = phone.Trim();
        if (digits.Length <= 2) return new string('*', digits.Length);
        var visible = digits.Substring(digits.Length - 2);
        return new string('*', digits.Length - 2) + visible;
    }
}
