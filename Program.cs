using System;
using System.Collections.Generic;
using System.Linq;

public class Paciente
{
    public int Id { get; set; }
    public string Nombre { get; set; }
}

class Program
{
    static void Main()
    {
        // Conjunto total de pacientes ficticios
        var todosPacientes = new HashSet<Paciente>();
        for (int i = 1; i <= 500; i++)
        {
            todosPacientes.Add(new Paciente { Id = i, Nombre = "Paciente " + i });
        }

        // Conjunto de pacientes vacunados con Pfizer (75 pacientes)
        var vacunadosPfizer = new HashSet<Paciente>(todosPacientes.Take(75));

        // Conjunto de pacientes vacunados con AstraZeneca (75 pacientes)
        var vacunadosAstraZeneca = new HashSet<Paciente>(todosPacientes.Skip(75).Take(75));

        // Conjunto de pacientes vacunados con ambas dosis (50 pacientes)
        var vacunadosAmbas = new HashSet<Paciente>(todosPacientes.Skip(150).Take(50));

        // --- Operaciones de teoría de conjuntos ---
        // Unión de todos los vacunados
        var todosVacunados = new HashSet<Paciente>(vacunadosPfizer);
        todosVacunados.UnionWith(vacunadosAstraZeneca);
        todosVacunados.UnionWith(vacunadosAmbas);

        // Diferencia: pacientes no vacunados
        var noVacunados = new HashSet<Paciente>(todosPacientes);
        noVacunados.ExceptWith(todosVacunados);

        // Intersección: pacientes con ambas dosis
        var ambasDosis = new HashSet<Paciente>(vacunadosPfizer);
        ambasDosis.IntersectWith(vacunadosAstraZeneca);
        ambasDosis.UnionWith(vacunadosAmbas); // también agregamos los que se marcaron directamente como "Ambas"

        // Diferencia: solo Pfizer
        var soloPfizer = new HashSet<Paciente>(vacunadosPfizer);
        soloPfizer.ExceptWith(vacunadosAstraZeneca);
        soloPfizer.ExceptWith(vacunadosAmbas);

        // Diferencia: solo AstraZeneca
        var soloAstraZeneca = new HashSet<Paciente>(vacunadosAstraZeneca);
        soloAstraZeneca.ExceptWith(vacunadosPfizer);
        soloAstraZeneca.ExceptWith(vacunadosAmbas);

        // --- Resultados ---
        Console.WriteLine(" Resultados de la campaña de vacunación COVID-19");
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine("Pacientes NO vacunados: " + noVacunados.Count);
        Console.WriteLine("Pacientes con ambas dosis: " + ambasDosis.Count);
        Console.WriteLine("Pacientes solo Pfizer: " + soloPfizer.Count);
        Console.WriteLine("Pacientes solo AstraZeneca: " + soloAstraZeneca.Count);

        // Ejemplo de listados detallados
        Console.WriteLine("\nEjemplo de pacientes NO vacunados:");
        foreach (var p in noVacunados.Take(10))
            Console.WriteLine($"{p.Id} - {p.Nombre}");

        Console.WriteLine("\nEjemplo de pacientes con ambas dosis:");
        foreach (var p in ambasDosis.Take(10))
            Console.WriteLine($"{p.Id} - {p.Nombre}");

        Console.WriteLine("\nEjemplo de pacientes solo Pfizer:");
        foreach (var p in soloPfizer.Take(10))
            Console.WriteLine($"{p.Id} - {p.Nombre}");

        Console.WriteLine("\nEjemplo de pacientes solo AstraZeneca:");
        foreach (var p in soloAstraZeneca.Take(10))
            Console.WriteLine($"{p.Id} - {p.Nombre}");
    }
}