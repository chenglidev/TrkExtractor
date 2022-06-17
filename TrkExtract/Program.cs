using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrkExtract
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Argument missing. Please input xml file path.");
                return;
            }
            try {
                Extractor extractor = new Extractor();
                extractor.Extract(args[0], "Output.csv");
            } catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }
    }
}
