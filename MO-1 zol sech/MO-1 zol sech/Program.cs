namespace MO_1_zol_sech
{
    internal class Program
    {
        private const double e = 0.01;
        static void Main(string[] args)
        {
            double a, b;
            Console.WriteLine("Введите начальные точки");
            a = Convert.ToDouble(Console.ReadLine());
            b = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine($"Точность: {e}\nНачальные точки: a={a}, b={b}");
            Calc(a, b);
        }


        static void Calc(double a, double b)
        {
            double new_a = a, new_b = b, xb, xa, fxb, fxa;
            double alpha = 0.618, beta = 0.382, delta;
            int iterations = 0;
            do
            {
                delta = Math.Abs(new_b - new_a);
                xb = new_a + beta * delta;
                xa = new_a + alpha * delta;
                fxb = Func(xb);
                fxa = Func(xa);
                if (fxb >= fxa) new_a = xb;
                else new_b = xa;
                iterations++;
                Console.WriteLine($"{iterations}. a = {Math.Round(new_a, 2)} b = {Math.Round(new_b, 2)}");
            } while (e < new_b - new_a);
            Console.WriteLine("\nТочка минимума: {0}\nКоличество интераций: {1}", 
                Math.Round(new_a < new_b ? new_a : new_b, 2), iterations);
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