using System;

[System.Serializable]
public struct Point : IEquatable<Point>
{
    public int x;
    public int z;

    public Point(int newX, int newZ)
    {
        x = newX;
        z = newZ;
    }

    public static Point operator +(Point a, Point b)
    {
        return new Point(a.x + b.x, a.z + b.z);
    }

    public static Point operator -(Point a, Point b)
    {
        return new Point(a.x - b.x, a.z - b.z);
    }

    public static bool operator ==(Point a, Point b)
    {
        return a.x == b.x && a.z == b.z;
    }

    public static bool operator !=(Point a, Point b)
    {
        return !(a == b);
    }

    public override bool Equals(object obj)
    {
        if (obj is Point)
        {
            Point p = (Point)obj;
            return x == p.x && z == p.z;
        }

        return false;
    }

    public bool Equals(Point p)
    {
        return x == p.x && z == p.z;
    }

    public override int GetHashCode()
    {
        return x ^ z;
    }

    public override string ToString()
    {
        return string.Format("{0}, {1}", x, z);
    }
}