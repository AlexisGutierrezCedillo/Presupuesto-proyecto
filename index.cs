using System;
using System.Collections.Generic;

class Program
{
    // Función recursiva para acumular combinaciones que tengan una suma en el intervalo [limiteInferior, limiteSuperior]
    static void EncontrarCombinacionesEnRango(List<double> numeros, int count, double limiteInferior, double limiteSuperior, int inicio, List<double> combinacionActual, double sumaActual, List<List<double>> resultados)
    {
        if (count == 0)
        {
            // Al haber seleccionado todos los números, se verifica si la suma acumulada se encuentra en el rango deseado.
            if (sumaActual >= limiteInferior && sumaActual <= limiteSuperior)
            {
                resultados.Add(new List<double>(combinacionActual));
            }
            return;
        }
        
        // Se recorren los números a partir del índice 'inicio'
        for (int i = inicio; i < numeros.Count; i++)
        {
            combinacionActual.Add(numeros[i]);
            EncontrarCombinacionesEnRango(numeros, count - 1, limiteInferior, limiteSuperior, i + 1, combinacionActual, sumaActual + numeros[i], resultados);
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
            
            double num;
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
        
        // Listas para acumular las combinaciones en cada rango
        List<List<double>> resultadosInferiores = new List<List<double>>();
        List<List<double>> resultadosSuperiores = new List<List<double>>();
        
        // Buscar combinaciones con suma en el rango [objetivo - 1, objetivo]
        EncontrarCombinacionesEnRango(numeros, cantidad, objetivo - 1, objetivo, 0, new List<double>(), 0, resultadosInferiores);
        // Buscar combinaciones con suma en el rango [objetivo, objetivo + 1]
        EncontrarCombinacionesEnRango(numeros, cantidad, objetivo, objetivo + 1, 0, new List<double>(), 0, resultadosSuperiores);
        
        // Mostrar resultados para el primer rango
        Console.WriteLine("\nCombinaciones con suma entre {0} y {1}:", objetivo - 1, objetivo);
        if (resultadosInferiores.Count > 0)
        {
            foreach (var combinacion in resultadosInferiores)
            {
                double suma = 0;
                foreach (double n in combinacion)
                    suma += n;
                Console.WriteLine(string.Join(" + ", combinacion) + " = " + suma);
            }
        }
        else
        {
            Console.WriteLine("No se encontró ninguna combinación.");
        }
        
        // Mostrar resultados para el segundo rango
        Console.WriteLine("\nCombinaciones con suma entre {0} y {1}:", objetivo, objetivo + 1);
        if (resultadosSuperiores.Count > 0)
        {
            foreach (var combinacion in resultadosSuperiores)
            {
                double suma = 0;
                foreach (double n in combinacion)
                    suma += n;
                Console.WriteLine(string.Join(" + ", combinacion) + " = " + suma);
            }
        }
        else
        {
            Console.WriteLine("No se encontró ninguna combinación.");
        }
        
        Console.WriteLine("\nPresione cualquier tecla para salir...");
        Console.ReadKey();
    }
}
