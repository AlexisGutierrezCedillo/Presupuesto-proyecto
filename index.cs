using System;
using System.Collections.Generic;

class Program
{
    // Función recursiva que acumula todas las combinaciones de 'count' números que sumen 'target'
    static void EncontrarCombinaciones(List<double> numeros, int count, double target, int inicio, List<double> combinacionActual, List<List<double>> resultados)
    {
        // Caso base: cuando no se requieren más números
        if (count == 0)
        {
            if (Math.Abs(target) < 1e-6) // Tolerancia para comparar con 0
            {
                // Se agrega una copia de la combinación actual a los resultados
                resultados.Add(new List<double>(combinacionActual));
            }
            return;
        }

        // Se recorre la lista de números a partir del índice 'inicio'
        for (int i = inicio; i < numeros.Count; i++)
        {
            combinacionActual.Add(numeros[i]);
            // Se llama recursivamente reduciendo la cantidad a buscar y actualizando el target
            EncontrarCombinaciones(numeros, count - 1, target - numeros[i], i + 1, combinacionActual, resultados);
            // Se elimina el último número para explorar otras combinaciones
            combinacionActual.RemoveAt(combinacionActual.Count - 1);
        }
    }

    static void Main(string[] args)
    {
        List<double> numeros = new List<double>();
        Console.WriteLine("Ingrese números (enteros o decimales). Escriba 'listo' para finalizar:");

        // Entrada de números
        while (true)
        {
            string entrada = Console.ReadLine();
            if (entrada.ToLower() == "listo")
                break;
            
            double num; // Declaración fuera del if
            if (double.TryParse(entrada, out num))
            {
                numeros.Add(num);
            }
            else
            {
                Console.WriteLine("Entrada inválida, intente nuevamente.");
            }
        }

        // Solicita el objetivo de la suma
        Console.WriteLine("Suma objetivo:");
        double objetivo;
        while (!double.TryParse(Console.ReadLine(), out objetivo))
        {
            Console.WriteLine("Entrada inválida, ingrese un número para la suma objetivo:");
        }
        
        // Solicita la cantidad de números a sumar
        Console.WriteLine("Cantidad de números a sumar:");
        int cantidad;
        while (!int.TryParse(Console.ReadLine(), out cantidad))
        {
            Console.WriteLine("Entrada inválida, ingrese un número entero para la cantidad de números a sumar:");
        }
        
        // Lista para acumular todas las combinaciones válidas
        List<List<double>> resultados = new List<List<double>>();
        EncontrarCombinaciones(numeros, cantidad, objetivo, 0, new List<double>(), resultados);
        
        // Mostrar resultados
        if (resultados.Count > 0)
        {
            Console.WriteLine("Se encontraron las siguientes combinaciones:");
            foreach (var combinacion in resultados)
            {
                Console.WriteLine(string.Join(" + ", combinacion) + " = " + objetivo);
            }
        }
        else
        {
            Console.WriteLine("No se encontró ninguna combinación de {0} números que sumen {1}.", cantidad, objetivo);
        }
        
        Console.WriteLine("Presione cualquier tecla para salir...");
        Console.ReadKey();
    }
}
