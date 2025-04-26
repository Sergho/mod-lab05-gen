using System.Collections.Generic;
using System.IO;

namespace generator
{
    class Program
    {
        static readonly int exampleSize = 1000;
        static readonly string[] outputs = { "Results/gen-0", "Results/gen-1", "Results/gen-2" };
        static void Main(string[] args)
        {
            Generator[] generators = { new SteadyGenerator(), new BigramGenerator(), new WordGenerator() };
            List<Example> examples = new();
            foreach (var generator in generators)
            {
                examples.Add(generator.getExample(exampleSize));
            }

            Directory.CreateDirectory("Results");

            Logger logger = new Logger(outputs);
            logger.LogTxt(examples.ToArray());
            logger.LogPlot(examples.ToArray());
        }
    }
}

