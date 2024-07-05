namespace MO_2_Nelder_Mead
{
    internal class Program
    {
        private const double Epsilon = 0.01;
        private const double ReflectionCoeff = 1.0; 
        private const double ExpansionCoeff = 2.0; 
        private const double ContractionCoeff = 0.5; 
        private const double ReductionCoeff = 0.5; 

        static double Func(double x1, double x2)
        {
            //return 9.3 * Math.Pow(x1 - 9, 2) + 9.5 * Math.Pow(x2 - 3, 2);
            return Math.Pow(x1 - 1, 2) + Math.Pow(x2 - 6, 2) + 50 * Math.Pow(x2 + 2 * x1 - 6, 2) + 9.7;
        }

        static double[] Optimize()
        {
            double[][] simplex = new double[3][]; // Начальный симплекс из 3 точек
            simplex[0] = new double[] { -1, -1 }; // Точка A
            simplex[1] = new double[] { 2, 2 }; // Точка B
            simplex[2] = new double[] { -1, 2 }; // Точка C
            int iterations = 0;
            while (true)
            {
                // Сортировка точек симплекса
                Array.Sort(simplex, (a, b) => Func(a[0], a[1]).CompareTo(Func(b[0], b[1])));

                // Вычисление центра тяжести
                double[] x_center = new double[] { (simplex[0][0] + simplex[1][0]) / 2, (simplex[0][1] + simplex[1][1]) / 2 };
                double Fx_center = Func(x_center[0], x_center[1]);

                // Проверка критерия остановки
                double bestValue = Math.Pow(Func(simplex[0][0], simplex[0][1]) - Fx_center, 2) + Math.Pow(Func(simplex[1][0], simplex[1][1])
                    - Fx_center, 2) + Math.Pow(Func(simplex[2][0], simplex[2][1]) - Fx_center, 2);
                if (bestValue < Epsilon)
                    return simplex[0]; // Возвращаем наилучшую точку

                // Отражение
                double[] x_reflected = new double[] { x_center[0] + ReflectionCoeff * (x_center[0] - simplex[2][0]), x_center[1]
                    + ReflectionCoeff * (x_center[1] - simplex[2][1]) };
                double Fx_reflected = Func(x_reflected[0], x_reflected[1]);

                if (Fx_reflected < Func(simplex[0][0], simplex[0][1]))
                {
                    // Отраженная точка легче, чем легкая точка
                    double[] x_expanded = new double[] { x_center[0] + ExpansionCoeff * (x_reflected[0] - x_center[0]), x_center[1]
                        + ExpansionCoeff * (x_reflected[1] - x_center[1]) };
                    double Fx_expanded = Func(x_expanded[0], x_expanded[1]);

                    if (Fx_expanded < Fx_reflected)
                    {
                        // Растяжение точки
                        simplex[2] = x_expanded;
                    }
                    else
                    {
                        // Отражение точки
                        simplex[2] = x_reflected;
                    }
                }
                else if (Fx_reflected >= Func(simplex[1][0], simplex[1][1]))
                {
                    // Отраженная точка тяжелее, чем средняя точка
                    if (Fx_reflected < Func(simplex[2][0], simplex[2][1]))
                    {
                        // Отраженная точка легче, чем тяжелая точка
                        simplex[2] = x_reflected;
                    }

                    //Сжатие
                    double[] x_contracted = new double[] { x_center[0] + ContractionCoeff * (simplex[2][0] - x_center[0]), x_center[1]
                        + ContractionCoeff * (simplex[2][1] - x_center[1]) };
                    double Fx_contracted = Func(x_contracted[0], x_contracted[1]);

                    if (Fx_contracted < Func(simplex[2][0], simplex[2][1]))
                    {
                        // Сжатая точка легче, чем тяжелая точка
                        simplex[2] = x_contracted;
                    }
                    else
                    {
                        // Точка сжатия тяжелее тяжелой точки 
                        // Редукция средней и тяжелой точек
                        simplex[1] = new double[] { simplex[0][0] + ReductionCoeff * (simplex[1][0] - simplex[0][0]), simplex[0][1]
                            + ReductionCoeff * (simplex[1][1] - simplex[0][1]) };
                        simplex[2] = new double[] { simplex[0][0] + ReductionCoeff * (simplex[2][0] - simplex[0][0]), simplex[0][1]
                            + ReductionCoeff * (simplex[2][1] - simplex[0][1]) };
                    }
                }
                else
                {
                    // Отраженная точка
                    simplex[2] = x_reflected;
                }

                iterations++;
                if (iterations > 1000) // Защита от бесконечного цикла
                    throw new Exception("Превышено максимальное количество итераций.");
                Console.WriteLine("{0}. Симплекс:  ({1}, {2}); ({3}, {4}); ({5}, {6})", iterations, Math.Round(simplex[0][0],2),
                    Math.Round(simplex[0][1], 2), Math.Round(simplex[1][0], 2), Math.Round(simplex[1][1], 2), Math.Round(simplex[2][0], 2),
                    Math.Round(simplex[2][1],2));
            }
        }

        static void Main(string[] args)
        {
            double[] result = Optimize();
            Console.WriteLine("Оптимальная точка: ({0}, {1})", Math.Round(result[0], 2), Math.Round(result[1], 2));
            Console.WriteLine("Значение функции в оптимальной точке: {0}", Math.Round(Func(result[0], result[1]), 2));
        }
    }
}