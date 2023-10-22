using System.Drawing;
using System.Reflection;
using System.Xml.Linq;
///Задана функция, возвращающая расстояние до точки в 3х мерном пространстве
///Задано максимально допустимое число вызовов такой функции
///Задача: найти точку
///



Console.WriteLine("Исходный алгоритм");
Point3d wantedPoint = new Point3d(5, 5, 5);
FirstSolve.Solve
Console.WriteLine("Погрешность X = " + (x - wantedPoint.X));
Console.WriteLine("Погрешность Y = " + (y - wantedPoint.Y));
Console.WriteLine("Погрешность Z = " + (z - wantedPoint.Z));




Console.WriteLine("");
Console.WriteLine("");
Console.WriteLine("");
Console.WriteLine("Модифицированный алгоритм");
var s = new Solve();
s.Solve1(wantedPoint);

public static class Tests
{
    public static void Go(List<Point3d> Pints3d)
    {



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

public class SecondSolve
{
    public Point3d Solve(Point3d orig)
    {
        var point = new OrigPoint(orig.X, orig.Y, orig.Z);
        var a = point.Distance(0, 0, 0);

        //Задаём сферу
        if (a == 0) return new Point3d { X = 0, Y = 0, Z = 0 };

        //Рисуем на сфере круг
        var b = point.Distance(a, 0, 0);
        if (b == 0) return new Point3d { X = a, Y = 0, Z = 0 };

        var x = (2 * a * a - b * b) / (2 * a);
        //var xOb = a - x;
        //var y = Math.Sqrt(b * b - xOb * xOb);

        //Рисуем на сфере круг
        var c = point.Distance(0, a, 0);
        if (c == 0) return new Point3d { X = 0, Y = c, Z = 0 };
        var y = (2 * a * a - c * c) / (2 * a);

        //var z = Math.Sqrt(b * b - y * y);
        var d = point.Distance(0, 0, a);
        var z = (2 * a * a - d * d) / (2 * a);

        Console.WriteLine("----------------------------- ");
        Console.WriteLine("Погрешность X = " + (x - orig.X));
        Console.WriteLine("Погрешность Y = " + (y - orig.Y));
        Console.WriteLine("Погрешность Z = " + (z - orig.Z));

        if (d == 0)
            return new Point3d { X = x, Y = y, Z = z };
        else
            return new Point3d { X = x, Y = y, Z = -z };




        //Console.WriteLine("Погрешность X = " + (x - point._x));
        //Console.WriteLine("Погрешность Y = " + (y - point._y));
        //Console.WriteLine("Погрешность Z = " + (z - point._z));
        //return null;

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









