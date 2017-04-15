using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;
using QuantumToolkit.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace UnitTest
{
    public class Rank2
    {
        public int Page;
        public double PageRank;
    }
    public class TestPageRank2
    {
        static public void TestMethod()
        {
            var useDefault = false;

            Console.WriteLine("Please enter the length of an adjacency matrix (Enter key to use Default config):\n");
            if (!int.TryParse(Console.ReadLine(), out int n))
            {
                Console.WriteLine("Currently, we are using the default configuration.\n");
                useDefault = true;
                n = 7;
            }

            Console.WriteLine("The length of the adjacency matrix is {0}.\n", n);

            var M = new double[n, n];
            if (useDefault == false)
            {
                Console.WriteLine("Please enter the adjacency matrix:");

                for (var i = 0; i < n;)
                {
                    var X = Console.ReadLine();
                    var Y = X.Split(' ');
                    for (var j = 0; j < n;)
                    {
                        if (!double.TryParse(Y[j], out M[i, j]))
                        {
                            useDefault = true;
                            break;
                        }
                        j++;
                    }
                    if (useDefault)
                    {
                        break;
                    }
                    i++;
                }
            }
            else
            {
                M = new double[7, 7]
                    {
                    { 0, 0, 0, 0, 0, 0, 0 },
                    { 1, 0, 0, 0, 0 ,0 ,0 },
                    { 1, 0, 0, 0 ,0 ,0, 0 },
                    { 0, 1, 0, 0, 0, 0, 0 },
                    { 0, 1 ,0, 0, 0 ,0 ,0 },
                    { 0, 0 ,1 ,0 ,0 ,0 ,0 },
                    { 0 ,0, 1 ,0 ,0 ,0 ,0 }
                     };

            }


            var outputMatrix = MathNet.Numerics.LinearAlgebra.Double.Matrix.Build.DenseOfArray(M);
            Console.WriteLine("The adjacency matrix is:\n");
            Console.WriteLine(outputMatrix);

            var rowsum = new double[n];
            for (var i = 0; i < n; i++)
            {
                rowsum[i] = 0;
                for (var j = 0; j < n; j++)
                {
                    rowsum[i] = rowsum[i] + M[i, j];
                }
                if (rowsum[i] == 0)
                {
                    M[i, i] = 1;
                    rowsum[i] = 1;
                }
            }
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    M[i, j] = 0.85 * M[i, j] / rowsum[i] + 0.15 / n;
                }
            }
            var Base = new Ket[n];
            var Basetensor = new Ket[n, n];
            var Basematrix = new Matrix[n];
            for (var j = 0; j < n; j++)
            {
                var value = (Matrix)Matrix.Build.Dense(n, 1, Complex.Zero);
                value[j, 0] = Complex.One;
                Base[j] = new Ket(value);
                Basematrix[j] = value;
            }
            var Basematrixtensor = new Matrix[n, n];
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    var value = (Matrix)Basematrix[i].KroneckerProduct(Basematrix[j]);
                    Basetensor[i, j] = new Ket(value);
                    Basematrixtensor[i, j] = value;
                }
            }
            var psi = new Ket[n];
            for (var i = 0; i < n; i++)
            {
                var value = (Matrix)Matrix.Build.Dense(n * n, 1, Complex.Zero);
                var temvalue = new Ket(value);
                for (var j = 0; j < n; j++)
                {
                    temvalue.Value = (Matrix)(temvalue.Value + Math.Sqrt(M[i, j]) * Basetensor[i, j].Value);
                }
                psi[i] = new Ket(temvalue.Value);
            }
            var value1 = (Matrix)Matrix.Build.Dense(n * n, 1, Complex.Zero);
            var psi0 = new Ket(value1);
            for (var i = 0; i < n; i++)
            {
                psi0.Value = (Matrix)(psi[i].Value + psi0.Value);
            }
            psi0.Value = (Matrix)(psi0.Value.Divide(Math.Sqrt(n)));
            var U = (Matrix)Matrix.Build.Dense(n * n, n * n, Complex.Zero);
            for (var i = 0; i < n; i++)
            {
                var tempsi = (Matrix)psi[i].Value.ConjugateTranspose();
                U = (Matrix)(psi[i].Value * tempsi + U);
            }
            var S = (Matrix)Matrix.Build.Dense(n * n, n * n, Complex.Zero);
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    var tembasetensor = Basetensor[i, j].Value.ConjugateTranspose();
                    S = (Matrix)(Basetensor[j, i].Value * tembasetensor + S);
                }
            }
            //Console.WriteLine(U.ToComplexString());
            var I = (Matrix)Matrix.Build.DenseIdentity(n * n, n * n);
            //Console.WriteLine(I.ToComplexString());
            //Console.WriteLine(S.ToComplexString());
            U = (Matrix)(S * (2 * U - I));
            //Console.WriteLine(U.ToComplexString());
            U = (Matrix)U.Power(2);



            int m = 0;
            if (useDefault == false)
            {
                Console.WriteLine("Please enter the step of quantum evolution");
                m = int.Parse(Console.ReadLine());
            }
            else
            {
                m = 5;
            }
            Console.WriteLine("The Step of quantum evoloution is {0}.\n ", m);



            var Measurematrix = new Matrix[n];
            var sI = (Matrix)Matrix.Build.DenseIdentity(n, n);
            for (var i = 0; i < n; i++)
            {
                Measurematrix[i] = (Matrix)(sI.KroneckerProduct(Basematrix[i]));
            }
            var R = new Matrix[n];
            var Rank = new double[n];
            for (var i = 0; i < n; i++)
            {
                var temR = (Matrix)Matrix.Build.Dense(1, n, Complex.Zero);
                var tem1 = (Matrix)Matrix.Build.Dense(1, n * n, Complex.Zero);
                var tem2 = (Matrix)Matrix.Build.Dense(n * n, n * n, Complex.Zero);
                var tem3 = (Matrix)Matrix.Build.Dense(n, 1, Complex.Zero);
                tem1 = (Matrix)psi0.Value.ConjugateTranspose();
                tem2 = (Matrix)U.ConjugateTranspose();
                temR = (Matrix)(tem1 * tem2.Power(m) * Measurematrix[i]);
                tem3 = (Matrix)temR.ConjugateTranspose();
                R[i] = (Matrix)(temR * tem3);
                Rank[i] = R[i].Real().At(0, 0);
            }
            Console.WriteLine("The {0}-step quantum PageRank (from high to low) is ", m);
            Console.WriteLine("(the first column is page and the second is the corresponding importantance)");
            var Rank1 = new Rank2[n];
            for (var i = 0; i < n; i++)
            {
                Rank1[i] = new Rank2 { Page = i, PageRank = Rank[i] };
            }

            var query = Rank1.OrderByDescending(Rank2 => Rank2.PageRank);

            foreach (var rank in query)
            {
                Console.WriteLine("{0} - {1}", rank.Page + 1, rank.PageRank);
            }
            for (var i = 0; i < n; i++)
            {
                Rank[i] = 0;
                for (var j = 0; j < 300; j++)
                {
                    var temR = (Matrix)Matrix.Build.Dense(1, n, Complex.Zero);
                    var tem1 = (Matrix)Matrix.Build.Dense(1, n * n, Complex.Zero);
                    var tem2 = (Matrix)Matrix.Build.Dense(n * n, n * n, Complex.Zero);
                    var tem3 = (Matrix)Matrix.Build.Dense(n, 1, Complex.Zero);
                    tem1 = (Matrix)psi0.Value.ConjugateTranspose();
                    tem2 = (Matrix)U.ConjugateTranspose();
                    temR = (Matrix)(tem1 * tem2.Power(j) * Measurematrix[i]);
                    tem3 = (Matrix)temR.ConjugateTranspose();
                    R[i] = (Matrix)(temR * tem3);
                    Rank[i] = R[i].Real().At(0, 0) + Rank[i];
                }
                Rank[i] = Rank[i] / 300;
            }
            Console.WriteLine("\nThe averaged quantum PageRank (from high to low) is ");
            var Rank3 = new Rank2[n];
            for (var i = 0; i < n; i++)
            {
                Rank3[i] = new Rank2 { Page = i, PageRank = Rank[i] };
            }

            var query1 = Rank3.OrderByDescending(Rank2 => Rank2.PageRank);

            foreach (var rank in query1)
            {
                Console.WriteLine("{0} - {1}", rank.Page + 1, rank.PageRank);
            }
            Console.ReadKey();
        }
    }
}
