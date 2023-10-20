using System.Drawing;
using System.Reflection;
using System.Xml.Linq;
///Задана функция, возвращающая расстояние до точки в 3х мерном пространстве
///Задано максимально допустимое число вызовов такой функции
///Задача: найти точку
///




var orig = new Point3d { X = 11, Y = 45252285285285, Z = -333 };

var point = new OrigPoint(orig);

var a = point.Distance(0, 0, 0);
var b = point.Distance(0, 1, 0);
var c = point.Distance(0, 0, 1);

var y = (a * a - b * b + 1) / 2;
var z = (a * a - c * c + 1) / 2;
var x = Math.Sqrt(a * a - y * y - z * z);

var test = point.Distance(x, y, z);
if (test < 0.0000000001)
{
    Console.WriteLine(new { x, y, z });
}
else
{
    Console.WriteLine(new { x = -x, y, z });
}

Console.WriteLine("Погрешность X = " + (x - point.X));
Console.WriteLine("Погрешность Y = " + (y - point.Y));
Console.WriteLine("Погрешность Z = " + (z - point.Z));

var s = new Solve();
s.Solve1(orig);



public class Point3d
{
    public double X;
    public double Y;
    public double Z;
    public Point3d(double x, double y, double z)
    {
        
    }

}

public class Solve
{
    public Point3d Solve1(Point3d orig)
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

        Console.WriteLine("----------------------------- " );
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


public class OrigPoint
{
    public double X { get; }
    public  double Y { get; }
    public  double Z { get; }

    public OrigPoint(OrigPoint point)
    {
        X = point.X;
        Y = point.Y;
        Z = point.Z;
    }

    public double Distance(double x, double y, double z)
    {
        return Math.Sqrt(Math.Pow(X - x, 2) + Math.Pow(Y - y, 2) + Math.Pow(Z - z, 2));
    }



}









