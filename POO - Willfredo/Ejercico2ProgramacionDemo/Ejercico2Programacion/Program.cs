using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Bienvenido al sistema de cálculo de seguros");

        Console.WriteLine("Ingrese los datos de su vehículo");
        Console.Write("Marca: ");
        string? marca = Console.ReadLine();

        Console.Write("Modelo: ");
        string? modelo = Console.ReadLine();

        Console.Write("Año: ");
        int año = int.Parse(Console.ReadLine());

        Console.Write("Cantidad de puertas (para automóviles) o cilindrada (para motocicletas): ");
        int atributoExtra = int.Parse(Console.ReadLine());

        Console.Write("¿Es un automóvil o una motocicleta? (A/M): ");
        char tipo = char.ToUpper(Console.ReadLine()[0]);

        Vehiculo vehiculo;
        if (tipo == 'A')
        {
            vehiculo = new Automovil(marca, modelo, año, atributoExtra);
        }
        else if (tipo == 'M')
        {
            vehiculo = new Motocicleta(marca, modelo, año, atributoExtra);
        }
        else
        {
            Console.WriteLine("Tipo de vehículo no válido. Saliendo del programa.");
            return;
        }

        double costoSeguro = vehiculo.CalcularCostoSeguro();

        Console.WriteLine($"El costo del seguro para su vehículo es: ${costoSeguro}");
    }
}

public abstract class Vehiculo
{
    public string? Marca { get; set; }
    public string? Modelo { get; set; }
    public int Año { get; set; }

    protected Vehiculo(string marca, string modelo, int año)
    {
        Marca = marca;
        Modelo = modelo;
        Año = año;
    }

    public abstract double CalcularCostoSeguro();
}

public class Automovil : Vehiculo
{
    public int CantidadPuertas { get; set; }

    public Automovil(string marca, string modelo, int año, int cantidadPuertas)
        : base(marca, modelo, año)
    {
        CantidadPuertas = cantidadPuertas;
    }

    public override double CalcularCostoSeguro()
    {
        return (CantidadPuertas * 10) + (DateTime.Now.Year - Año) * 10;
    }
}

public class Motocicleta : Vehiculo
{
    public int Cilindrada { get; set; }

    public Motocicleta(string marca, string modelo, int año, int cilindrada)
        : base(marca, modelo, año)
    {
        Cilindrada = cilindrada;
    }

    public override double CalcularCostoSeguro()
    {
        return 100 + (Cilindrada / 100) * 50 + (DateTime.Now.Year - Año) * 5;
    }
}

public class CompaniaSeguros
{
    public static double CalcularCostoTotalSeguro(Vehiculo[] vehiculos)
    {
        double costoTotal = 0;

        foreach (var vehiculo in vehiculos)
        {
            costoTotal += vehiculo.CalcularCostoSeguro();
        }

        return costoTotal;
    }
}