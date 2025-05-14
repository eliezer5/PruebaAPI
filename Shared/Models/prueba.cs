using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    internal class prueba
    {
        public void ProcessData(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("Input cannot be null or empty");
            }

            var result = input.ToUpper();
            Console.WriteLine($"Processed: {result}");
            SaveToDatabase(result);
        }

        // Método duplicado 2 (copia del primero con cambios mínimos)
        public void HandleData(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                throw new ArgumentException("Input cannot be null or empty");
            }

            var output = data.ToUpper();
            Console.WriteLine($"Processed: {output}");
            SaveToDatabase(output);
        }

        private void SaveToDatabase(string value)
        {
            // Simulación de guardado en DB
            Console.WriteLine($"Saving: {value}");
        }
    }
}
