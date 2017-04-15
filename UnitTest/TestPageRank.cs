//This work is contributed by Ji Guan


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuantumToolkit.Type;
using System.Numerics;
using MathNet.Numerics.LinearAlgebra.Complex;
using static QuantumToolkit.QuantumAlgorithm;
using MathNet.Numerics.LinearAlgebra;

namespace UnitTest
{
    public class Rank2
    {
        public int Page;
        public double PageRank;
    }
    public class TestPageRank
    {
        static public void TestMethod()
        {
            Console.WriteLine("The length of an adjacent matrix:");
            int n = int.Parse(Console.ReadLine());
            Console.WriteLine("The adjacent matrix:");
            double[,] M = new double[n, n];
            for (int i = 0; i < n;)
            {
                string X = Console.ReadLine();
                string[] Y = X.Split(' ');
                for (int j = 0; j < n;)
                {
                    M[i, j] = double.Parse(Y[j]);
                    j++;
                }
                i++;
            }
            double[] rowsum = new double[n];
            for (int i = 0; i < n; i++)
            {
                rowsum[i] = 0;
                for (int j = 0; j < n; j++)
                {
                    rowsum[i] = rowsum[i] + M[i, j];
                }
                if (rowsum[i] == 0)
                {
                    M[i, i] = 1;
                    rowsum[i] = 1;
                }
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                   M[i, j]=0.85*M[i,j]/rowsum[i]+0.15/n;
                }
            }
            Ket[] Base = new Ket[n];
            Ket[,] Basetensor = new Ket[n, n];
            Matrix[] Basematrix = new Matrix[n];
            for (int j = 0; j < n; j++)
            {
                Matrix value = (Matrix)Matrix.Build.Dense(n, 1, Complex.Zero);
                value[j, 0] = Complex.One;
                Base[j] = new Ket(value);
                Basematrix[j] = value;
            }
            Matrix[,] Basematrixtensor = new Matrix[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Matrix value = (Matrix)Basematrix[i].KroneckerProduct(Basematrix[j]);
                    Basetensor[i, j] = new Ket(value);
                    Basematrixtensor[i, j] = value;
                }
            }
            Ket[] psi = new Ket[n];
            for (int i = 0; i < n; i++)
            {
                Matrix value = (Matrix)Matrix.Build.Dense(n*n, 1, Complex.Zero);
                Ket temvalue = new Ket(value);
                for (int j = 0;  j < n; j++)
                {
                    temvalue.Value = (Matrix)(temvalue.Value +Math.Sqrt(M[i,j])* Basetensor[i,j].Value);
                }
                psi[i] = new Ket(temvalue.Value);
            }
            Matrix value1 = (Matrix)Matrix.Build.Dense(n * n, 1, Complex.Zero);
            Ket psi0 = new Ket(value1);
            for (int i = 0; i < n; i++)
            {
                psi0.Value = (Matrix)(psi[i].Value + psi0.Value);
            }
            psi0.Value = (Matrix)(psi0.Value.Divide(Math .Sqrt (n)));
            Matrix U = (Matrix)Matrix.Build.Dense(n*n, n*n, Complex.Zero);
            for (int i = 0; i < n; i++)
            {
                var tempsi= (Matrix)psi[i].Value.ConjugateTranspose();
                U =(Matrix)(psi[i].Value*tempsi+U );
            }
            Matrix S = (Matrix)Matrix.Build.Dense(n * n, n * n, Complex.Zero);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    var tembasetensor = Basetensor[i, j].Value.ConjugateTranspose();
                    S = (Matrix)(Basetensor[j, i].Value*tembasetensor+S);
                }
            }
            //Console.WriteLine(U);
            Matrix I = (Matrix)Matrix.Build.DenseIdentity(n * n, n * n);
            //Console.WriteLine(I);
            //Console.WriteLine(S);
            U = (Matrix)(S*(2*U-I));
            //Console.WriteLine(U);
            U=(Matrix)U.Power(2);
            Console.WriteLine("The step of quantum evolution");
            int m = int.Parse(Console.ReadLine());
            var Measurematrix = new Matrix[n];
            Matrix sI = (Matrix)Matrix.Build.DenseIdentity(n , n);
            for (int i = 0; i < n; i++)
            {
                Measurematrix[i] = (Matrix)(sI.KroneckerProduct (Basematrix[i]));
            }
            Matrix [] R=new Matrix[n];
            double[] Rank=new double[n];
            for (int i = 0; i < n; i++)
            {
                Matrix temR = (Matrix)Matrix.Build.Dense(1, n ,Complex.Zero);
                Matrix tem1 = (Matrix)Matrix.Build.Dense(1, n * n, Complex.Zero);
                Matrix tem2 = (Matrix)Matrix.Build.Dense(n * n, n * n, Complex.Zero);
                Matrix tem3 = (Matrix)Matrix.Build.Dense(n , 1, Complex.Zero);
                tem1 = (Matrix)psi0.Value.ConjugateTranspose();
                tem2=(Matrix)U.ConjugateTranspose();
                temR  = (Matrix)(tem1 * tem2.Power(m) * Measurematrix[i]);
                tem3 = (Matrix)temR.ConjugateTranspose();
                R[i] = (Matrix)(temR *tem3 );
                Rank[i] = R[i].Real().At(0, 0);
            }
            Console.WriteLine("The PageRank (from high to low) is (the first column is  pages and the second is the corresponding importantance)");

            Rank2[] Rank1=new Rank2[n];
            for( int i = 0;i<n ;i++)
            {
                Rank1[i] = new Rank2 { Page = i, PageRank = Rank[i] };
            }

            IEnumerable<Rank2> query = Rank1.OrderByDescending(Rank2 =>Rank2.PageRank );

            foreach (Rank2 rank in query)
            {
                Console.WriteLine("{0} - {1}", rank.Page,rank.PageRank);
            }       
        Console.ReadKey();
        }
    }
}
