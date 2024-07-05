using System.Diagnostics.Metrics;

namespace MO_1_comb
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double e = 0.01;
            double[] x = new double[4];
            double dx, fx0, fx1, fx2, fx3, a, b, c;
            Console.WriteLine("Введите начальную точку и начальное изменение dx");
            x[0] = Convert.ToDouble(Console.ReadLine());
            dx = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine($"Точность: {e}\nНачальная точка: x0={x[0]},  дельта x={dx}");
            int counter = 1;
            fx0 = Func(x[0]);
            x[1] = x[0] + dx; //x[k]
            fx1 = Func(x[1]);
            if (fx0 < fx1) dx = -dx;
            else dx = 2 * dx;

            x[2] = x[1] + dx; //x[k+1]
            fx2 = Func(x[2]);
            while(fx2 < fx1)
            {
                dx = 2 * dx;
                x[0] = x[1];
                x[1] = x[2];        //x[k]
                fx1 = Func(x[1]);
                x[2] = x[1] + dx;   //x[k+1]
                fx2 = Func(x[2]);
            }
            x[3] = x[2] - dx / 2;
            Array.Sort(x);   //x[k+2]
            fx1 = Func(x[1]);
            fx2 = Func(x[2]);
            if (ZolSechIskl(fx1,fx2))
            {
                a = x[0]; b = x[1]; c = x[2];
            }
            else
            {
                a = x[1]; b = x[2]; c = x[3];
            }
            Console.WriteLine($"{counter}. a = {Math.Round(a, 2)} b = {Math.Round(b, 2)} c = {Math.Round(c, 2)}");
            double fa, fb, fc, fx_2;
            double x_1, x_2 = b; //икс со звездой
            do
            {
                x_1 = x_2;
                fa = Func(a); fb = Func(b); fc = Func(c);
                x_2 = -0.5 *
                    ((b * b - a * a) * (fc - fb) - (c * c - b * b) * (fb - fa))
                    / ((c - b) * (fb - fa) - (b - a) * (fc - fb));
                fx_2 = Func(x_2);
                if (x_2 > a && x_2 < b)
                {
                    if (ZolSechIskl(fx_2, fb))
                    {
                        a = a; c = b; b = x_2;
                    }
                    else
                    {
                        a = x_2; b = b; c = c;
                    } 
                }
                else if (x_2 > b && x_2 < c)
                {
                    if (ZolSechIskl(fb, fx_2))
                    {
                        a = a; b = b; c = x_2;
                    }
                    else
                    {
                        a = b; b = x_2; c = c;
                    }
                }
                counter++;
                Console.WriteLine($"{counter}. a = {Math.Round(a, 2)} b = {Math.Round(b, 2)} c = {Math.Round(c, 2)}");
            } while (e < Math.Abs(x_2 - x_1));
            Console.WriteLine("\nТочка минимума: {0}\nКоличество интераций: {1}",
                Math.Round(x_2 < x_1 ? x_2 : x_1, 2), counter);

        }


        static bool ZolSechIskl(double f_left, double f_right)
        {
            if (f_left < f_right)
                return true;
            else return false;
        }


        static double Func(double x)
        {
            //double f = (x - 6) * (x - 3) * (x - 1);
            //double f = x + 6 * Math.Sin(4.7 * Math.PI * x + 1);
            double f = 1 - 6 * Math.Exp(-Math.Pow(x - 9, 2));
            return f;
        }
    }
}