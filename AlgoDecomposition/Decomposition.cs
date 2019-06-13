using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AlgoDecomposition
{
    public class Decomposition
    {
        public ulong Nombre { get; private set; }
        public Dictionary<ulong, int> FacteursPremiers { get; private set; }

        private static List<String> _NombresPremiers = null;
        public static List<String> NombresPremiers
        {
            get
            {
                if (_NombresPremiers == null)
                    _NombresPremiers = Directory.GetFiles("NombresPremiers", "NombresPremiers_*.txt", SearchOption.TopDirectoryOnly).ToList();

                return _NombresPremiers;
            }
        }

        public Decomposition(ulong nb)
        {
            Nombre = nb;
            FacteursPremiers = new Dictionary<ulong, int>();

            getDecomposition();
        }

        private void getDecomposition()
        {
            ulong n = Nombre;
            int i = 999;
            foreach (String file in NombresPremiers)
            {
                String[] info = (Path.GetFileNameWithoutExtension(file)).Split('_');
                ulong first = ulong.Parse(info[2]);
                ulong last = ulong.Parse(info[3]);

                if (first <= n && n <= last)
                {
                    i = int.Parse(info[1]);
                    break;
                }
            }

            ulong nbPremier = ulong.MaxValue;

            while (n != 1 && i >= 0)
            {
                String file = (from x in NombresPremiers where x.Contains("NombresPremiers\\NombresPremiers_" + i + "_") select x).FirstOrDefault();

                foreach (var line in File.ReadLines(file).Reverse())
                {
                    nbPremier = ulong.Parse(line);

                    while (n % nbPremier == 0)
                    {
                        n /= nbPremier;
                        if (!FacteursPremiers.ContainsKey(nbPremier))
                            FacteursPremiers[nbPremier] = 0;
                        FacteursPremiers[nbPremier]++;
                    }
                    
                    TextReader reader = new StreamReader(file);
                    ulong tmp = ulong.Parse(reader.ReadLine());
                    reader.Close();
                    if (n == 1 || tmp > n)
                        break;
                }
                i--;

                if (n < nbPremier && n != 1)
                {
                    while (n < nbPremier)
                    {
                        TextReader reader = new StreamReader(file);
                        nbPremier = ulong.Parse(reader.ReadLine());
                        reader.Close();
                        i--;
                    }
                    i++;
                }
            }

            if (n != 1)
                FacteursPremiers[n] = 1;
        }
    }

    public class DecryptRSA
    {
        public ulong n { get; private set; }
        public ulong e { get; private set; }
        public ulong d { get; private set; }

        public DecryptRSA(ulong n, ulong e)
        {
            this.n = n;
            this.e = e;

            this.d = decrypt();
        }

        private ulong decrypt()
        {
            ulong p, q, phiN;

            Decomposition decompositon = new Decomposition(n);
            p = decompositon.FacteursPremiers.Keys.ToArray()[0];
            q = decompositon.FacteursPremiers.Keys.ToArray()[1];

            phiN = (p - 1) * (q - 1);

            return InverseModuloN(e, phiN);
        }

        public static ulong[] AlgoEuclideEtendu(ulong a, ulong b)
        {
            ulong[] output = new ulong[3];
            ulong x = 1, xx = 0, y = 0, yy = 1;
            
            while (b != 0)
            {
                ulong q = a / b;

                ulong tmp = a;
                a = b;
                b = tmp % b;

                tmp = xx;
                xx = x - q * xx;
                x = tmp;

                tmp = yy;
                yy = y - q * yy;
                y = tmp;
            }

            output[0] = a;
            output[1] = x;
            output[2] = y;

            return output;
        }

        public static ulong InverseModuloN(ulong a, ulong n)
        {
            ulong[] euclide = AlgoEuclideEtendu(a, n);
            if (euclide[0] != 1)
                return 0;
            return euclide[1];
        }
    }
}
