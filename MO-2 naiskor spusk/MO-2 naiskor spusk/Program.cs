namespace MO_2_naiskor_spusk
{
    internal class Program
    {
        private const double Epsilon = 0.01;
        private const double EpsilonT = 0.01;

        static double FuncT(double t, double[] x, double[] grad)
        {
            //return 9.3 * Math.Pow((x[0] - grad[0] * t) - 9, 2) + 9.5 * Math.Pow((x[1] - grad[1] * t) - 3, 2);
            return Math.Pow((x[0] - grad[0] * t) - 1, 2) + Math.Pow((x[1] - grad[1] * t) - 6, 2) +
                50 * Math.Pow((x[1] - grad[1] * t) + 2 * (x[0] - grad[0] * t) - 6, 2) + 9.7;
        }

        static double[] Grad(double[] x)
        {
            double[] grad = new double[2];
            //grad[0] = 18.6 * (x[0] - 9);
            //grad[1] = 19 * (x[1] - 3);
            grad[0] = 402 * x[0] - 1202 + 200 * x[1];
            grad[1] = 102 * x[1] - 612 + 200 * x[0];
            return grad;
        }

        static double Zolsech(double[] x, double[] grad)
        {
            double new_a = 0, new_b = 1, tb, ta, ftb, fta;
            double alpha = 0.618, beta = 0.382, delta;
            do
            {
                delta = Math.Abs(new_b - new_a);
                tb = new_a + beta * delta;
                ta = new_a + alpha * delta;
                ftb = FuncT(tb, x, grad);
                fta = FuncT(ta, x, grad);
                if (ftb >= fta) new_a = tb;
                else new_b = ta;
            } while (EpsilonT < new_b - new_a);
            return Math.Round(new_a < new_b ? new_a : new_b, 2);
        }

        static double[] Optimize()
        {
            double[] x0 = new double[2];
            double[] xk = new double[2];
            double[] xk1 = new double[2];
            double t;
            double[] grad = new double[2];
            Console.WriteLine("Введите начальные точки");
            x0[0] = Convert.ToDouble(Console.ReadLine());
            x0[1] = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Точноть: {0}", Epsilon);
            int iterations = 0;
            xk = x0;
            while (true)
            {
                if (iterations > 100)
                    return xk1;
                grad = Grad(xk);
                t = Zolsech(xk, grad);
                xk1[0] = xk[0] - t * grad[0];
                xk1[1] = xk[1] - t * grad[1];
                Console.WriteLine("{0}. x = ({1}, {2})", iterations, xk1[0], xk1[1]);
                xk = xk1;
                iterations++;
            }
        }

        static void Main(string[] args)
        {
            double[] result = Optimize();
            Console.WriteLine("Оптимальная точка: ({0}, {1})", Math.Round(result[0], 2), Math.Round(result[1], 2));
        }
    }
}