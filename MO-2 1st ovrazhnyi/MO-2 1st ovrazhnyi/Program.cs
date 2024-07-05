namespace MO_2_1st_ovrazhnyi
{
    internal class Program
    {
        private const double Epsilon = 0.01;
        private const double t1 = 0.001;
        private const double t2 = 0.005;
        private const double e1 = 1500;
        private const double e2 = 1100;
        private const double N = 15;
        private const double N1 = 10;
        private const double N2 = 5;
        static int iteration_count = 0;

        static double Func(double x1, double x2)
        {
            //return 9.3 * Math.Pow(x1 - 9, 2) + 9.5 * Math.Pow(x2 - 3, 2);
            //return 0.5 * x1 * x1 + 50 * x2 * x2;
            return Math.Pow(x1 - 1, 2) + Math.Pow(x2 - 6, 2) +
               50 * Math.Pow(x2 + 2 * x1 - 6, 2) + 9.7;
        }

        static double[] Grad(double[] x)
        {
            double[] grad = new double[2];
            //grad[0] = 18.6 * (x[0] - 9);
            //grad[1] = 19 * (x[1] - 3);
            //grad[0] = x[0];
            //grad[1] = 100 * x[1];

            grad[0] = 402 * x[0] - 1202 + 200 * x[1];
            grad[1] = 102 * x[1] - 612 + 200 * x[0];
            return grad;
        }

        static double[] Calc_g_stage_1(double[] x)
        {
            double[] G = new double[2];
            double[] derivative = Grad(x);
            if (Math.Abs(derivative[0]) < e1)
                G[0] = 0;
            else G[0] = derivative[0];

            if (Math.Abs(derivative[1]) < e1)
                G[1] = 0;
            else G[1] = derivative[1];

            return G;
        }

        static double[] Calc_g_stage_2(double[] x)
        {
            double[] G = new double[2];
            double[] derivative = Grad(x);
            if (Math.Abs(derivative[0]) > e2)
                G[0] = 0;
            else G[0] = derivative[0];

            if (Math.Abs(derivative[1]) > e2)
                G[1] = 0;
            else G[1] = derivative[1];

            return G;
        }

        static double[] Optimize()
        {
            double[] x0 = new double[2];
            double[] xk = new double[2];
            double[] xk1 = new double[2];
            double[] G = new double[2];
            Console.WriteLine("Введите начальные точки");
            x0[0] = Convert.ToDouble(Console.ReadLine());
            x0[1] = Convert.ToDouble(Console.ReadLine());
            
            int iterations = 0;
            int cycle = 1;
            xk = x0;
            while (true)
            {
                if (cycle == N)
                    return xk;
                Console.WriteLine("\n------{0} ЦИКЛ------", cycle);
                Console.WriteLine("\n1 Этап:");
                iterations = 0;
                while (true) //1 ЭТАП
                {
                    G = Calc_g_stage_1(xk);
                    if (G[0] == 0 && G[1] == 0 || iterations == N1)
                        break;
                    xk1[0] = xk[0] - t1 * G[0];
                    xk1[1] = xk[1] - t1 * G[1];
                    iterations++;
                    iteration_count++;
                    Console.WriteLine("{0}. G: ({1}, {2});   x: ({3}, {4})", iterations, G[0], G[1], xk1[0], xk1[1]);
                    Console.WriteLine("   F=({0})", Func(xk1[0], xk1[1]));
                    xk = xk1;
                    if ((Math.Abs(Grad(xk)[0]) < Epsilon && Math.Abs(Grad(xk)[1]) < Epsilon))
                        return xk;
                }
                Console.WriteLine("\n2 Этап:");
                iterations = 0;
                while (true) // 2 ЭТАП
                {
                    G = Calc_g_stage_2(xk);
                    if ((G[0] == 0 && G[1] == 0) || iterations == N2)
                        break;
                    xk1[0] = xk[0] - t2 * G[0];
                    xk1[1] = xk[1] - t2 * G[1];
                    iterations++;
                    iteration_count++;
                    Console.WriteLine("{0}. G: ({1}, {2});   x: ({3}, {4})", iterations, G[0], G[1], xk1[0], xk1[1]);
                    Console.WriteLine("   F=({0})", Func(xk1[0], xk1[1]));
                    xk = xk1;
                    if ((Math.Abs(Grad(xk)[0]) < Epsilon && Math.Abs(Grad(xk)[1]) < Epsilon))
                        return xk;
                }
                cycle++;
            }
        }

        static void Main(string[] args)
        {
            double[] result = Optimize();
            Console.WriteLine("\nОптимальная точка: ({0}, {1})", Math.Round(result[0], 2), Math.Round(result[1], 2));
            Console.WriteLine("\nЗначение функции в данной точке: ({0})", Func(result[0], result[1]));
            Console.WriteLine("Количество итераций: {0}", iteration_count);
        }
    }
}