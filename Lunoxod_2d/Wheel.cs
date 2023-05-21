using Avalonia;
using Lunoxod_2d.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace Lunoxod_2d
{
    public class Wheel : ViewModelBase
    {
        private double epsilon = 1e-10;

        private double radius = 20.0;
        
        private List<Point> surface = new List<Point> { new Point(0, 0), new Point(10, 0)};

        private List<List<Point>> centerOfWheel = new List<List<Point>>();

        //Constructor

        public Wheel()
        {
            System.Diagnostics.Debug.WriteLine("Empty Constructor");
        }
        public Wheel(List<Point> surface)
        {
            this.surface = surface;
        }

        public Wheel(List<Point> surface, double radius)
        {
            this.surface = surface;
            this.radius = radius;
            //System.Diagnostics.Debug.WriteLine(checkIfPointLowerThanLine(new Point(2, 2), new Point(1, 2), new Point(3, 5)));
            //System.Diagnostics.Debug.WriteLine(checkIfPointLowerThanLine(new Point(2, 4), new Point(1, 2), new Point(3, 5)));
            //System.Diagnostics.Debug.WriteLine(getPointAtDistanceFromLineAbove(new Point(0, 0.5), new Point(1, 2), new Point(3, 5), 3));
            //System.Diagnostics.Debug.WriteLine(getPointAtDistanceFromLineAbove(new Point(0, -0.5), new Point(-1, 1), new Point(1, -2), 3));
            //var A = Matrix<double>.Build.DenseOfArray(new double[,] {
            //    { 3, -2 },
            //    { 3, -1 }
            //    });
            //var b = Vector<double>.Build.Dense(new double[] { -1, -1 });
            //var x = A.Solve(b);
            //System.Diagnostics.Debug.WriteLine(x);
            //System.Diagnostics.Debug.WriteLine(HasValue(x[0]));
            //System.Diagnostics.Debug.WriteLine(HasValue(x[1]));
            //System.Diagnostics.Debug.WriteLine(getIntersectionOfTwoLines(new List<double>() { 3, -2, 1 }, new List<double>() { 3, -2, 1 }));
            fillCenterOfWheel(surface);
            //print surface and centerOfWheel
            //for (int i = 0; i < surface.Count; i++)
            //{
            //    System.Diagnostics.Debug.WriteLine(surface[i]);
            //}
            //for (int i = 0; i < centerOfWheel.Count; i++)
            //{
            //    for (int j = 0; j < centerOfWheel[i].Count; j++)
            //    {
            //        System.Diagnostics.Debug.Write("(" + centerOfWheel[i][j] + ") ");
            //    }
            //    System.Diagnostics.Debug.WriteLine("");
            //}
        }

        /// <summary>
        /// Checks if value not NaN and not Infinity 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool HasValue(double value)
        {
            return !Double.IsNaN(value) && !Double.IsInfinity(value);
        }

        /// <summary>
        /// Get coefficients of line, formed by Point a and Point b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public List<double> getLineCoefs(Point a, Point b) 
        {
            double c1 = b.Y - a.Y;
            double c2 = a.X - b.X;
            double c3 = a.Y * (b.X - a.X) - a.X * (b.Y - a.Y);
            return new List<double> { c1, c2, c3 };
        }

        /// <summary>
        /// Сhecks whether point a is lower than line formed by points b and c.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns>True if point is lower, else false</returns>
        public bool checkIfPointLowerThanLine(Point a, Point b, Point c)
        {
            List<double> coefs = getLineCoefs(b, c);
            if ((coefs[0] * a.X + coefs[1] * a.Y + coefs[2]) * (coefs[1]) < 0)
            {
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Get point at distance d from line formed by points b and c and above line, where base of perpendicular from point to line is a
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public Point getPointAtDistanceFromLineAbove(Point a, Point b, Point c, double d)
        {
            Point answer;
            List<double> coefs = getLineCoefs(b, c);
            double n = Math.Sqrt(Math.Pow(coefs[0], 2) + Math.Pow(coefs[1], 2));
            if (coefs[1] > 0)
            {
                answer = new Point(a.X + coefs[0] * d / n, a.Y + coefs[1] * d / n);
            }
            else
            {
                answer = new Point(a.X - coefs[0] * d / n, a.Y - coefs[1] * d / n);
            }
            return answer;
        }
        
        /// <summary>
        /// Get Point that represents intersection of two lines, formed by Point a, Point b and Point c, Point d, respectively
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>

        public Point getIntersectionOfTwoLines(Point a, Point b, Point c, Point d)
        {
            List<double> coefs1 = getLineCoefs(a, b);
            List<double> coefs2 = getLineCoefs(c, d);
            var A = Matrix<double>.Build.DenseOfArray(new double[,] {
                { coefs1[0], coefs1[1] },
                { coefs2[0], coefs2[1] }
                });
            var B = Vector<double>.Build.Dense(new double[] { -coefs1[2], -coefs2[2] });
            var x = A.Solve(B);
            return new Point(x[0], x[1]);
        }

        /// <summary>
        /// Get Point that represents intersection of two lines, formed by its coefficients coefs1 and coefs2, respectively
        /// </summary>
        /// <param name="coefs1"></param>
        /// <param name="coefs2"></param>
        /// <returns></returns>
        public Point getIntersectionOfTwoLines(List<double> coefs1, List<double> coefs2)
        {
            var A = Matrix<double>.Build.DenseOfArray(new double[,] {
                { coefs1[0], coefs1[1] },
                { coefs2[0], coefs2[1] }
                });
            var B = Vector<double>.Build.Dense(new double[] { -coefs1[2], -coefs2[2] });
            var x = A.Solve(B);
            return new Point(x[0], x[1]);
        }

        /// <summary>
        /// Get coefficients of line passing through Point a, parallel to line, formed by Point b and Point c 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public List<double> getParallelLineCoefs(Point a, Point b, Point c)
        {
            List<double> coefs = getLineCoefs(b, c);
            return new List<double> { coefs[0], coefs[1], -coefs[0] * a.X - coefs[1] * a.Y };
        }

        /// <summary>
        /// Fill up List centerOfWheel
        /// </summary>
        /// <param name="surface"></param>
        public void fillCenterOfWheel(List<Point> surface)
        {
            Point temp = getPointAtDistanceFromLineAbove(surface[0], surface[0], surface[1], radius);
            //centerOfWheel.Add(new object[] {  });
            for (int i = 1; i < surface.Count - 1; i++)
            {
                if (checkIfPointLowerThanLine(surface[i], surface[i - 1], surface[i + 1]))
                {
                    Point a = getPointAtDistanceFromLineAbove(surface[i - 1], surface[i - 1], surface[i], radius);
                    Point b = getPointAtDistanceFromLineAbove(surface[i + 1], surface[i], surface[i + 1], radius);
                    List<double> coefs1 = getParallelLineCoefs(a, surface[i - 1], surface[i]);
                    List<double> coefs2 = getParallelLineCoefs(b, surface[i], surface[i + 1]);
                    Point n = getIntersectionOfTwoLines(coefs1, coefs2);
                    centerOfWheel.Add(new List<Point>() { temp, n });
                    temp = n;
                }
                else
                {
                    Point a = getPointAtDistanceFromLineAbove(surface[i], surface[i - 1], surface[i], radius);
                    Point b = getPointAtDistanceFromLineAbove(surface[i], surface[i], surface[i + 1], radius);
                    centerOfWheel.Add(new List<Point>() { temp, a });
                    centerOfWheel.Add(new List<Point>() { a, b, surface[i] });
                    temp = b;
                }
            }
            centerOfWheel.Add(new List<Point>() { temp, getPointAtDistanceFromLineAbove(surface[^1], surface[^2], surface[^1], radius) });
        }

        /// <summary>
        /// Get Y coordinate by X coordinate of point on center of wheel
        /// </summary>
        /// <param name="X"></param>
        /// <param name="center"></param>
        /// <returns></returns>
        public double getYOfCenterByX(double X, List<List<Point>> center)
        {
            double Y = 0.0;
            for (int i = 0; i < center.Count; i++)
            {
                if (center[i][0].X <= X && X <= center[i][1].X )
                {
                    if (center[i].Count == 2)
                    {
                        List<double> coefs = getLineCoefs(center[i][0], center[i][1]);
                        Y = (-coefs[0] * X - coefs[2]) / coefs[1];
                    }
                    else
                    {
                        Y = center[i][2].Y + Math.Sqrt(Math.Pow(radius, 2) - Math.Pow(X - center[i][2].X, 2));
                    }
                    break;
                }
            }
            return Y;
        }

        /// <summary>
        /// Return CenterOfWheel
        /// </summary>
        /// <returns></returns>
        public List<List<Point>> getCenterOfWheel()
        {
            return centerOfWheel;
        }

        public double distance(Point a, Point b)
        {
            return Math.Sqrt(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.Y, 2));
        }

        /// <summary>
        /// Check if sections, formed by Point a, Point b and Point c, Point d respectively, intersect 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public bool checkIntersectionOfSections(Point a, Point b, Point c, Point d)
        {
            Point intersect = getIntersectionOfTwoLines(a, b, c, d);
            if (HasValue(intersect.X) && HasValue(intersect.Y))
            {
                if (Math.Abs(distance(a, b) - distance(a, intersect) - distance(intersect, b)) < epsilon && Math.Abs(distance(c, d) - distance(c, intersect) - distance(intersect, d)) < epsilon)
                {
                    return true;
                }
                else return false;
            }
            else return false;
        }
    }
}
