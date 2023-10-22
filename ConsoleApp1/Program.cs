using System.Drawing;
using System.Reflection;
using System.Xml.Linq;
///Задана функция, возвращающая расстояние до точки в 3х мерном пространстве
///Задано максимально допустимое число вызовов такой функции
///Задача: найти точку
///



Tests tests = new Tests(0.01);
tests.PrepareTest(new Point3d(5, 5, 5));
tests.PrepareTest(new Point3d(-5, 5, 5));
tests.PrepareTest(new Point3d(5, -5, 5));
tests.PrepareTest(new Point3d(5, 5, -5));
tests.PrepareTest(new Point3d(-5, -5, 5));
tests.PrepareTest(new Point3d(5, -5, -5));
tests.PrepareTest(new Point3d(-5, -5, 5));
tests.PrepareTest(new Point3d(-5, -5, -5));
tests.PrepareTest(new Point3d(57777, 57777, 57777));
tests.PrepareTest(new Point3d(0, 57777, 57777));
tests.PrepareTest(new Point3d(9999999, 57777, 57777));
tests.PrepareTest(new Point3d(57777, 0, -57777));
tests.PrepareTest(new Point3d(-57777, -656747454, 57777));
tests.PrepareTest(new Point3d(57777, 656747454, 57777));
tests.PrepareTest(new Point3d(-57777, 656744, -5779877));
tests.PrepareTest(new Point3d(-57867777, -6567474, 57777));



tests.Go();


public class Tests
{
    public class TestPoint3d
    {
        public Point3d Point3dWanted;
        public Point3d Point3dSolve1;
        public Point3d Point3dSolve2;
    }

    private double Tolerance;

    public Tests(double tolerance)
    {
        Tolerance = tolerance;
    }


    private List<TestPoint3d> testPoints3d = new List<TestPoint3d>();

    public void PrepareTest(Point3d wantedPoint)
    {
        TestPoint3d testPoint3d = new TestPoint3d();
        testPoint3d.Point3dWanted = wantedPoint;
        testPoint3d.Point3dSolve1 = FirstSolve.Solve(wantedPoint);
        testPoint3d.Point3dSolve2 = SecondSolve.Solve(wantedPoint);
        testPoints3d.Add(testPoint3d);
    }

    public void Go()
    {
        Console.BackgroundColor = ConsoleColor.White;
        void ColorWrite(double pointWanted, double pointSolve, String axis)
        {
            double value = pointWanted - pointSolve;
            if (Math.Abs(value) > Math.Abs(Tolerance))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" (pointWanted = " + pointWanted + "; ");
                Console.Write("pointSolve = " + pointSolve + ";) ");
                Console.Write("Погрешн по модулю = " + (Math.Abs(pointWanted) - Math.Abs(pointSolve)) + ";) ");
                if (axis == "Z")
                    Console.WriteLine("  " + axis + "=" + value);
                else
                    Console.Write("  " + axis + "=" + value);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Black;
                if (axis == "Z")
                    Console.WriteLine("  " + axis + "=" + value);
                else
                    Console.Write("  " + axis + "=" + value);
            }
        }
        void CheckToleranceAndWrite(Point3d point3dWanted, Point3d point3dSolve)
        {
            ColorWrite(point3dWanted.X, point3dSolve.X, "X");

            ColorWrite(point3dWanted.Y, point3dSolve.Y, "Y");

            ColorWrite(point3dWanted.Z, point3dSolve.Z, "Z");
            Console.ForegroundColor = ConsoleColor.Black;
        }

        foreach (var item in testPoints3d)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Искомая точка.  X=" + item.Point3dWanted.X + "  Y=" + item.Point3dWanted.Y + "  Z=" + item.Point3dWanted.Z);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("Исходный алгоритм. Погрешн.");
            CheckToleranceAndWrite(item.Point3dWanted, item.Point3dSolve1);

            Console.Write("Модифиц. алгоритм. Погрешн.");
            CheckToleranceAndWrite(item.Point3dWanted, item.Point3dSolve2);
        }
    }
}



public class Point3d
{
    public readonly double X;
    public readonly double Y;
    public readonly double Z;
    public Point3d(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

}

/// <summary>
/// Исходный алгоритм
/// </summary>
public static class FirstSolve
{
    public static Point3d Solve(Point3d wantedPoint)
    {
        var a = Distance.Get(wantedPoint, new Point3d(0, 0, 0));
        var b = Distance.Get(wantedPoint, new Point3d(0, 1, 0));
        var c = Distance.Get(wantedPoint, new Point3d(0, 0, 1));

        var y = (a * a - b * b + 1) / 2;
        var z = (a * a - c * c + 1) / 2;
        var x = Math.Sqrt(a * a - y * y - z * z);

        var expectedPoint = new Point3d(x, y, z);
        var test = Distance.Get(wantedPoint, expectedPoint);
        if (test < 0.0000000001)
        {
            return expectedPoint;
        }
        else
        {
            return new Point3d(-expectedPoint.X, expectedPoint.Y, expectedPoint.Z);
        }
    }
}

public static class SecondSolve
{
    public static Point3d Solve(Point3d wantedPoint)
    {
        //Задаём сферу
        var a = Distance.Get(wantedPoint, new Point3d(0, 0, 0));
        //Если попали в точку - выдаём координаты
        if (a == 0) return new Point3d(0, 0, 0);

        //Рисуем на сфере круг
        var b = Distance.Get(wantedPoint, new Point3d(a, 0, 0));
        //Если попали в точку - выдаём координаты
        if (b == 0) return new Point3d(a, 0, 0);
        var x = (2 * a * a - b * b) / (2 * a);

        //Рисуем на сфере круг
        var c = Distance.Get(wantedPoint, new Point3d(0, a, 0));
        //Если попали в точку - выдаём координаты
        if (c == 0) return new Point3d(0, a, 0);
        var y = (2 * a * a - c * c) / (2 * a);

        var d = Distance.Get(wantedPoint, new Point3d(0, 0, a));
        //Если попали в точку - выдаём координаты
        if (d == 0) return new Point3d(0, 0, a);
        var z = (2 * a * a - d * d) / (2 * a);

        return new Point3d(x, y, z);

    }



}


public static class Distance
{
    //функция, возвращающая расстояние до точки в 3х мерном пространстве
    public static double Get(Point3d wanted, Point3d testPoint)
    {
        return Math.Sqrt(Math.Pow(wanted.X - testPoint.X, 2) + Math.Pow(wanted.Y - testPoint.Y, 2) + Math.Pow(wanted.Z - testPoint.Z, 2));
    }
}









