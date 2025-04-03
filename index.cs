using System;
using System.Collections.Generic;

class Program
{
    // Función recursiva que acumula todas las combinaciones de 'count' números cuya suma esté en el rango [objetivo - 0.5, objetivo + 0.5)
    static void EncontrarCombinacionesEnRango(List<double> numeros, int count, double objetivo, int inicio, List<double> combinacionActual, double sumaActual, List<List<double>> resultados)
    {
        if (count == 0)
        {
            // Se verifica que la suma acumulada esté en el rango [objetivo - 0.5, objetivo + 0.5)
            if (sumaActual >= objetivo - 0.5 && sumaActual < objetivo + 0.5)
            {
                resultados.Add(new List<double>(combinacionActual));
            }
            return;
        }
        
        // Se recorren los números a partir del índice 'inicio'
        for (int i = inicio; i < numeros.Count; i++)
        {
            combinacionActual.Add(numeros[i]);
            EncontrarCombinacionesEnRango(numeros, count - 1, objetivo, i + 1, combinacionActual, sumaActual + numeros[i], resultados);
            combinacionActual.RemoveAt(combinacionActual.Count - 1);
        }
    }
    
    static void Main(string[] args)
    {
        string continuar = "si";
        
        while (continuar.ToLower() == "si" || continuar.ToLower() == "s")
        {
            List<double> numeros = new List<double>();
            Console.WriteLine("Ingrese números (enteros o decimales). Escriba 'listo' para finalizar la lista:");
            
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
            
            // Lista para acumular todas las combinaciones válidas
            List<List<double>> resultados = new List<List<double>>();
            EncontrarCombinacionesEnRango(numeros, cantidad, objetivo, 0, new List<double>(), 0, resultados);
            
            // Mostrar resultados
            Console.WriteLine("\nCombinaciones cuya suma redondeada sea {0}:", objetivo);
            if (resultados.Count > 0)
            {
                foreach (var combinacion in resultados)
                {
                    double suma = 0;
                    foreach (double n in combinacion)
                        suma += n;
                    Console.WriteLine(string.Join(" + ", combinacion) + " = " + suma);
                }
            }
            else
            {
                Console.WriteLine("No se encontró ninguna combinación de {0} números cuya suma redondeada sea {1}.", cantidad, objetivo);
            }
            
            Console.WriteLine("\n¿Desea realizar otra búsqueda? (si/no)");
            continuar = Console.ReadLine();
        }
        
        Console.WriteLine("Programa finalizado. Presione cualquier tecla para salir...");
        Console.ReadKey();
    }
}
