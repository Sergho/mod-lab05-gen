using System;
using System.Collections.Generic;

namespace generator
{
    class Program
    {
        static void Main(string[] args)
        {
            var generator = new SteadyCharGenerator();
            Example example = generator.getExample(1000);
            Console.WriteLine(example.String);
            foreach (var entry in example.Stats)
            {
                Console.WriteLine($"{entry.Key} - {entry.Value}");
            }
        }
    }
}

