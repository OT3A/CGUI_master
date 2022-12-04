using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class JarvisMarch : Algorithm
    {
        public double Theta(Point v1,Point v2,double m1,double m2)
        {
            double dotProduct = v1.X * v2.X + v1.Y * v2.Y;
            double tmp = dotProduct / (m1 * m2);
            double theta = Math.Acos(tmp);
            return theta;

        }
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            double dotProduct;
            Point start, end;
            Point V1, V2;
            double magV1, magV2;
            List<Point> right = new List<Point>();
            List<Point> left = new List<Point>();
            List<Point> extremPoints = new List<Point>();
            Point last;
            int size = points.Count();
            points.Sort((p1, p2) => p1.X.CompareTo(p2.X));
            last = points[0];
            extremPoints.Add(last);
            bool second = false;
            for (int i = 0; i < size; i++)
            {
                if (last != points[i] && !(extremPoints.Contains(points[i])))
                {
                    Line s = new Line(last, points[i]);
                    for (int j = 0; j < size; j++)
                    {
                        if (left.Count() != 0 && right.Count() != 0)
                        {
                            break;
                        }
                        if (points[j] != points[i] && points[j] != last)
                        {
                            if (CGUtilities.HelperMethods.CheckTurn(s, points[j]) == CGUtilities.Enums.TurnType.Left)
                            {
                                left.Add(points[j]);
                            }
                            else if (CGUtilities.HelperMethods.CheckTurn(s, points[j]) == CGUtilities.Enums.TurnType.Right)
                            {
                                right.Add(points[j]);
                            }
                        }
                    }
                    if (left.Count() == 0 || right.Count() == 0)
                    {
                        if (!extremPoints.Contains(points[i]))
                        {
                            extremPoints.Add(points[i]);
                            last = points[i];
                            second = true;
                            break;
                        }
                    }
                    right.Clear();
                    left.Clear();
                }
                right.Clear();
                left.Clear();
                if (second)
                {
                    break;
                }
            }
            end = extremPoints[0];
            start = extremPoints[1];
            for (int i = 0; i < size; i++)
            {
                double maxTheata = 0;
                if (last != points[i] && !(extremPoints.Contains(points[i])))
                {

                    Line s = new Line(last, points[i]);
                    V1 = CGUtilities.HelperMethods.GetVector(s);
                    double tmp = (V1.X * V1.X) + (V1.Y + V1.Y);
                    magV1 = Math.Sqrt(tmp);
                    for (int j = 0; j < size; j++)
                    {
                        Line l = new Line( points[j], points[i]);
                        V2 = CGUtilities.HelperMethods.GetVector(l);
                        double tmp2 = (V2.X * V2.X) + (V2.Y + V2.Y);
                        magV2 = Math.Sqrt(tmp2);

                        if (points[j] != points[i] && points[j] != last)
                        {
                            
                        }
                    }
                    if (left.Count() == 0 || right.Count() == 0)
                    {
                        if (!extremPoints.Contains(points[i]))
                        {
                            extremPoints.Add(points[i]);
                            last = points[i];
                            i = -1;
                        }
                    }
                    right.Clear();
                    left.Clear();
                }
                right.Clear();
                left.Clear();
            }
            outPoints = extremPoints.ToList();
        }

        public override string ToString()
        {
            return "Convex Hull - Jarvis March";
        }
    }
}
