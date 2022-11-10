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
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            List<Point> right = new List<Point>();
            List<Point> left = new List<Point>();
            List<Point> extremPoints = new List<Point>();
            Point last;
            int size = points.Count();
            points.Sort((p1, p2) => p1.X.CompareTo(p2.X));
            last = points[size - 1];
            extremPoints.Add(last);
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
