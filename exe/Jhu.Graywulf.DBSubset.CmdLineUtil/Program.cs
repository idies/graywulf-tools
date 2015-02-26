using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using Jhu.Graywulf.DBSubset.Lib;

namespace Jhu.Graywulf.DBSubset.CommandLine
{
    class Program
    {
        static void Main(string[] args)
        {
            SubsetDefinition sdef = LoadSubsetDefinition(args[0]);

            SubsetEngine e = new SubsetEngine(sdef);

            e.Execute();

            Console.WriteLine("Done.");
        }

        static void SaveSubsetDefinition(SubsetDefinition subsetDefinition, string filename)
        {
            using (StreamWriter outfile = new StreamWriter(filename))
            {
                subsetDefinition.Save(outfile);
            }
        }

        static SubsetDefinition LoadSubsetDefinition(string filename)
        {
            SubsetDefinition res;

            using (StreamReader infile = new StreamReader(filename))
            {
                res = SubsetDefinition.Load(infile);
            }

            return res;
        }
    }
}
