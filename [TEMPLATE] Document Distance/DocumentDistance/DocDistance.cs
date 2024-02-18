using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentDistance
{
    class DocDistance
    {
        // *****************************************
        // DON'T CHANGE CLASS OR FUNCTION NAME
        // YOU CAN ADD FUNCTIONS IF YOU NEED TO
        // *****************************************
        /// <summary>
        /// Write an efficient algorithm to calculate the distance between two documents
        /// </summary>
        /// <param name="doc1FilePath">File path of 1st document</param>
        /// <param name="doc2FilePath">File path of 2nd document</param>
        /// <returns>The angle (in degree) between the 2 documents</returns>

        /// ---------- less time = more memorey, No thing is free ---------- ///
        public static Dictionary<string, Dictionary<string, long>> Ds = new Dictionary<string, Dictionary<string, long>>();
        public static Dictionary<string, double> DPs = new Dictionary<string, double>();

        public static double CalculateDistance(string doc1FilePath, string doc2FilePath)
        {
            // TODO comment the following line THEN fill your code here
            // throw new NotImplementedException();
            if (doc2FilePath.Equals(doc1FilePath))
            {
                var x = Ds[doc1FilePath] = F1(doc1FilePath);
                DPs[doc1FilePath] = F2(x, x);
                return 0;
            }

            double D1D2 = 0, D1D1 = 0, D2D2 = 0;
            Dictionary<String, long> D1 = new Dictionary<String, long>();
            Dictionary<String, long> D2 = new Dictionary<String, long>();

            if (DPs.TryGetValue(doc1FilePath, out D1D1) && DPs.TryGetValue(doc2FilePath, out D2D2))
            {
                D1 = Ds[doc1FilePath];
                D2 = Ds[doc2FilePath];
                D1D2 = F2(D1, D2);
                return Math.Acos(D1D2 / Math.Sqrt(D1D1 * D2D2)) * 57.295779513082320;
            }
            else if (DPs.TryGetValue(doc1FilePath, out D1D1) && !DPs.TryGetValue(doc2FilePath, out D2D2))
            {
                D1 = Ds[doc1FilePath];
                D2 = F1(doc2FilePath);
                Ds[doc2FilePath] = D2;

                Thread T1 = new Thread(() => D1D2 = F2(D1, D2));
                Thread T3 = new Thread(() => D2D2 = F2(D2, D2));
                T1.Start();
                T3.Start();
                T1.Join();
                T3.Join();
                DPs[doc2FilePath] = D2D2;

                return Math.Acos(D1D2 / Math.Sqrt(D1D1 * D2D2)) * 57.295779513082320;
            }
            else if (!DPs.TryGetValue(doc1FilePath, out D1D1) && DPs.TryGetValue(doc2FilePath, out D2D2))
            {
                D1 = F1(doc1FilePath);
                Ds[doc1FilePath] = D1;
                D2 = Ds[doc2FilePath];

                Thread T1 = new Thread(() => D1D2 = F2(D1, D2));
                Thread T2 = new Thread(() => D1D1 = F2(D1, D1));

                T1.Start();
                T2.Start();
                T1.Join();
                T2.Join();

                DPs[doc1FilePath] = D1D1;
                return Math.Acos(D1D2 / Math.Sqrt(D1D1 * D2D2)) * 57.295779513082320;
            }
            else
            {
                Thread doc1Pre = new Thread(() => D1 = F1(doc1FilePath));
                Thread doc2Pre = new Thread(() => D2 = F1(doc2FilePath));

                doc1Pre.Start();
                doc2Pre.Start();
                doc1Pre.Join();
                doc2Pre.Join();

                Ds[doc1FilePath] = D1;
                Ds[doc2FilePath] = D2;

                Thread T1 = new Thread(() => D1D2 = F2(D1, D2));
                Thread T2 = new Thread(() => D1D1 = F2(D1, D1));
                Thread T3 = new Thread(() => D2D2 = F2(D2, D2));

                T1.Start();
                T2.Start();
                T3.Start();
                T1.Join();
                T2.Join();
                T3.Join();

                DPs[doc1FilePath] = D1D1;
                DPs[doc2FilePath] = D2D2;
                return Math.Acos(D1D2 / Math.Sqrt(D1D1 * D2D2)) * 57.295779513082320;
            }
        }
        /// count the frequency of each word in the map
        static Dictionary<String, long> F1(String file)
        {
            String all = File.ReadAllText(file);
            StringBuilder s = new StringBuilder();
            Dictionary<String, long> ret = new Dictionary<String, long>();
            foreach (var c in all)
            {
                if (c >= 'A' && c <= 'Z')
                {
                    s.Append((char)(c | 0x20u));
                }
                else if (c >= '0' && c <= '9' || c >= 'a' && c <= 'z')
                {
                    s.Append(c);
                }
                else
                {
                    if (s.Length == 0) continue;
                    string S = s.ToString();
                    s.Clear();
                    ret.TryGetValue(S, out long f);
                    ret[S] = f + 1;
                }

            }
            if (s.Length > 0)
            {
                string S = s.ToString();
                s.Clear();
                ret.TryGetValue(S, out long f);
                ret[S] = f + 1;
            }
            return ret;
        }
        /// calc DotProduct
        static double F2(Dictionary<string, long> D1, Dictionary<string, long> D2)
        {
            double s = 0;
            foreach (var t in D1)
            {
                D2.TryGetValue(t.Key, out long c);
                s += (t.Value * c);
            }
            return s;
        }
    }
}