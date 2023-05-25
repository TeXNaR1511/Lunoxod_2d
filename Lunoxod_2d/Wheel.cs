﻿using Avalonia;
using Lunoxod_2d.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using Avalonia.Media;

namespace Lunoxod_2d
{
    public class Wheel : ViewModelBase
    {
        private double epsilon = 1e-10;

        private double radius = 20.0;
        
        private List<Point> surface = new List<Point> { new Point(0, 0), new Point(10, 0)};

        private List<List<Point>> centerOfWheel = new List<List<Point>>();

        //Constructors

        public Wheel()
        {
            System.Diagnostics.Debug.WriteLine("Empty Constructor");
            centerOfWheel = setCenterOfWheel(surface);
            List<Point> ter = getIntersectionEllipseEllipse(new Point(0, 0), 1, new Point(1, 1), 1);
            for (int i = 0; i < ter.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine(ter[i]);
            }
        }
        public Wheel(List<Point> surface)
        {
            this.surface = surface;
            centerOfWheel = setCenterOfWheel(surface);
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
            centerOfWheel = setCenterOfWheel(surface);
            //System.Diagnostics.Debug.WriteLine(FindRoots.Quadratic(1, 1, 1).Item1.Real);
            //print surface and centerOfWheel
            for (int i = 0; i < surface.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine(surface[i]);
            }
            for (int i = 0; i < centerOfWheel.Count; i++)
            {
                for (int j = 0; j < centerOfWheel[i].Count; j++)
                {
                    System.Diagnostics.Debug.Write("(" + centerOfWheel[i][j] + ") ");
                }
                System.Diagnostics.Debug.WriteLine("");
            }
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
        public List<List<Point>> setCenterOfWheel(List<Point> surface)
        {
            Point temp = getPointAtDistanceFromLineAbove(surface[0], surface[0], surface[1], radius);
            List<List<Point>> answer = new List<List<Point>>();
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
                    answer.Add(new List<Point>() { temp, n });
                    temp = n;
                }
                else
                {
                    Point a = getPointAtDistanceFromLineAbove(surface[i], surface[i - 1], surface[i], radius);
                    Point b = getPointAtDistanceFromLineAbove(surface[i], surface[i], surface[i + 1], radius);
                    answer.Add(new List<Point>() { temp, a });
                    answer.Add(new List<Point>() { a, b, surface[i] });
                    temp = b;
                }
            }
            answer.Add(new List<Point>() { temp, getPointAtDistanceFromLineAbove(surface[^1], surface[^2], surface[^1], radius) });
            return answer;
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

        public List<Point> getIntersectionLineEllipse(Point o, double r, Point a, Point b)
        {
            List<double> coefs = getLineCoefs(a, b);
            double A = coefs[0];
            double B = coefs[1];
            double C = coefs[0] * o.X + coefs[1] * o.Y + coefs[2];

            double x0 = -A * C / (A * A + B * B);
            double y0 = -B * C / (A * A + B * B);

            if (C * C > r * r * (A * A + B * B) + epsilon)
            {
                return new List<Point>();
            }
            else if (Math.Abs(C * C - r * r * (A * A + B * B)) < epsilon)
            {
                return new List<Point> { new Point(x0 + o.X, y0 + o.Y) };
            }
            else
            {
                double d = r * r - C * C / (A * A + B * B);
                double mult = Math.Sqrt(d / (A * A + B * B));
                double ax, ay, bx, by;
                ax = x0 + B * mult;
                bx = x0 - B * mult;
                ay = y0 - A * mult;
                by = y0 + A * mult;
                return new List<Point> { new Point(ax + o.X, ay + o.Y), new Point(bx + o.X, by + o.Y) };
            }
        }

        public List<Point> getIntersectionLineEllipse(Point o, double r, List<double> coefs)
        {
            double A = coefs[0];
            double B = coefs[1];
            double C = coefs[0] * o.X + coefs[1] * o.Y + coefs[2];

            double x0 = -A * C / (A * A + B * B);
            double y0 = -B * C / (A * A + B * B);

            if (C * C > r * r * (A * A + B * B) + epsilon)
            {
                return new List<Point>();
            }
            else if (Math.Abs(C * C - r * r * (A * A + B * B)) < epsilon)
            {
                return new List<Point> { new Point(x0 + o.X, y0 + o.Y) };
            }
            else
            {
                double d = r * r - C * C / (A * A + B * B);
                double mult = Math.Sqrt(d / (A * A + B * B));
                double ax, ay, bx, by;
                ax = x0 + B * mult;
                bx = x0 - B * mult;
                ay = y0 - A * mult;
                by = y0 + A * mult;
                return new List<Point> { new Point(ax + o.X, ay + o.Y), new Point(bx + o.X, by + o.Y) };
            }
        }

        public bool checkIfPointOnUpperArc(Point p, Point o, double r)
        {
            return Math.Abs(p.Y - o.Y - Math.Sqrt(r * r - Math.Pow(p.X - o.X, 2))) < epsilon;
        }

        public List<Point> getIntersectionEllipseEllipse(Point o1, double r1, Point o2, double r2)
        {
            if (o1.X == o2.X && o1.Y == o2.Y)
            {
                if (Math.Abs(r1 - r2) < epsilon)
                {
                    return new List<Point> { new Point(Double.PositiveInfinity, Double.PositiveInfinity) };
                }
                else
                {
                    return new List<Point>();
                }
            }
            else
            {
                List<double> coefs = new List<double> { 2 * (o1.X - o2.X), 2 * (o1.Y - o2.Y), Math.Pow(o1.X - o2.X, 2) + Math.Pow(o1.Y - o2.Y, 2) - r2 * r2 + r1 * r1 };
                List<Point> answer = getIntersectionLineEllipse(new Point(0, 0), r1, coefs);
                for (int i = 0; i < answer.Count; i++)
                {
                    answer[i] = new Point(answer[i].X + o1.X, answer[i].Y + o1.Y);
                }
                return answer;
            }
        }

        /// <summary>
        /// Returns point, located at distance d from point P, both points are on center. If point behind P, then bool behind true, else ahead
        /// </summary>
        /// <param name="P"></param>
        /// <param name="d"></param>
        /// <param name="center"></param>
        /// <param name="behind"></param>
        /// <returns></returns>
        public Point getXYAtDistanceFromPoint(Point P, double d, List<List<Point>> center, bool behind)
        {
            Point answer = new Point(0, 0);
            List<Point> temp = new List<Point>();
            if (behind)
            {
                for (int i = 0; i < center.Count; i++)
                {
                    if (center[i][0].X <= P.X && center[i][1].X <= P.X && distance(center[i][1], P) <= d && d <= distance(center[i][0], P)
                        || center[i][0].X <= P.X && P.X <= center[i][1].X && d <= distance(center[i][0], P))
                    {
                        temp = center[i];
                        break;
                    }
                }
                if (temp.Count == 0)
                {
                    answer = new Point(P.X - d, P.Y);
                }
                else
                {
                    if (temp.Count == 2)
                    {
                        List<Point> intersec = getIntersectionLineEllipse(P, d, temp[0], temp[1]);
                        answer = intersec[0].X <= intersec[1].X ? intersec[0] : intersec[1];
                    }
                    else
                    {
                        List<Point> intersec = getIntersectionEllipseEllipse(temp[2], radius, P, d);
                        answer = checkIfPointOnUpperArc(intersec[0], temp[2], radius) ? intersec[0] : intersec[1];
                    }
                }
            }
            else
            {
                for (int i = 0; i < center.Count; i++)
                {
                    if (center[i][0].X >= P.X && center[i][1].X >= P.X && distance(center[i][1], P) >= d && d >= distance(center[i][0], P)
                        || center[i][0].X <= P.X && P.X <= center[i][1].X && d <= distance(center[i][1], P))
                    {
                        temp = center[i];
                        break;
                    }
                }
                if (temp.Count == 0)
                {
                    answer = new Point(P.X + d, P.Y);
                }
                else
                {
                    if (temp.Count == 2)
                    {
                        List<Point> intersec = getIntersectionLineEllipse(P, d, temp[0], temp[1]);
                        answer = intersec[0].X >= intersec[1].X ? intersec[0] : intersec[1];
                    }
                    else
                    {
                        List<Point> intersec = getIntersectionEllipseEllipse(temp[2], radius, P, d);
                        answer = checkIfPointOnUpperArc(intersec[0], temp[2], radius) ? intersec[0] : intersec[1];
                    }
                }
            }
            return answer;
        }
    }
}
