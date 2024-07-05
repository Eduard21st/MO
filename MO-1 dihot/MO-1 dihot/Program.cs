namespace MO_1_dihot
{
    internal class Program
    {
        static double e = 0.1;
        static double a, b;
        static double delta = 0.05;
        static void Main(string[] args)
        {
            Console.WriteLine("Введите начальные точки");
            a = Convert.ToDouble(Console.ReadLine());
            b = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine($"Точность: {e}\nНачальные точки: a={a}, b={b}");
            Calc(a, b);
        }


        static void Calc(double a, double b)
        {
            double new_a = a, new_b = b, x1, x2, fx1, fx2;
            int counter = 0;
            do
            {
                x1 = (new_a + new_b - delta) / 2;
                x2 = (new_a + new_b + delta) / 2;
                fx1 = Func(x1);
                fx2 = Func(x2);
                if (fx1 >= fx2) new_a = x1;
                else new_b = x2;
                counter++;
                Console.WriteLine($"{counter}. a = {Math.Round(new_a, 2)} b = {Math.Round(new_b, 2)}");
            } while (e < new_b - new_a);
            Console.WriteLine("\nТочка минимума: {0}\nКоличество интераций: {1}",
                Math.Round(new_a < new_b ? new_a : new_b, 2), counter);
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