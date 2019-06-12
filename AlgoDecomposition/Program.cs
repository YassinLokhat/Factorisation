using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoDecomposition
{
    class Program
    {
        static void Main(string[] args)
        {
            /*/Decomposition d = new Decomposition(ulong.MaxValue);
            //Decomposition d = new Decomposition(997773123126183097);
            //Decomposition d = new Decomposition(25);

            Console.Write("" + d.Nombre + " = 1");

            foreach (var facteur in d.FacteursPremiers)
                Console.Write(" * " + facteur.Key + " ^ " + facteur.Value);

            Console.WriteLine();*/

            DecryptRSA decrypt = new DecryptRSA(85, 5);
            Console.WriteLine(decrypt.d);

            //Console.WriteLine(DecryptRSA.PuissanceModulo(5, 11, 14));
            Console.WriteLine("end.");
            Console.ReadKey();
        }
    }
}
