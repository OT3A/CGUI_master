using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class ExtremeSegments : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            List<Point> right = new List<Point>();
            List<Point> left = new List<Point>();
            List<Point> extremPoints = new List<Point>();
            Point last ;
            int size = points.Count();
            bool outloop = false;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (points[i] != points[j] && points[i].X != points[j].X && points[i].Y!=points[j].Y)
                    {
                        Line segment = new Line(points[i], points[j]);
                        for (int k = 0; k < size; k++)
                        {
                            if (left.Count() != 0 && right.Count() != 0)
                            { 
                                break;
                            }
                            if (points[k] != points[i] && points[k] != points[j])
                            {
                                if (CGUtilities.HelperMethods.CheckTurn(segment, points[k]) == CGUtilities.Enums.TurnType.Left)
                                {
                                    left.Add(points[k]);
                                }
                                else if (CGUtilities.HelperMethods.CheckTurn(segment, points[k]) == CGUtilities.Enums.TurnType.Right)
                                {
                                    right.Add(points[k]);
                                }
                            }
                        }
                        if (right.Count() == 0 || left.Count() == 0)
                        {
                            if(!extremPoints.Contains(points[i]))
                                extremPoints.Add(points[i]);
                            if(!extremPoints.Contains(points[j]))
                                extremPoints.Add(points[j]);
                            outloop = true;
                            break;
                        }
                        right.Clear();
                        left.Clear();
                    }
                    right.Clear();
                    left.Clear();
                }
                right.Clear();
                left.Clear();
                if (outloop)
                {
                    break;
                }
            }
            right.Clear();
            left.Clear();

            if (extremPoints.Count() != 0)
                last = extremPoints[0];
            else
                last = new Point(0, 0);

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
            return "Convex Hull - Extreme Segments";
        }
    }
}
