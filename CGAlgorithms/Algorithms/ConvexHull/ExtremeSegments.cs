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
            HashSet<Point> extremPoints = new HashSet<Point>();
            List<Point> listExtremPoints = new List<Point>();
            Point last = new Point(0,0);
            int size = points.Count();
            bool outloop = false;
            bool inloop = false;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (points[i] != points[j])
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
                            last = points[i];
                            extremPoints.Add(points[i]);
                            extremPoints.Add(points[j]);
                            inloop = true;
                            break;
                            outloop = true;
                        }
                        right.Clear();
                        left.Clear();
                    }
                    right.Clear();
                    left.Clear();
                    //if (inloop)
                    //{
                    //    break;
                    //}
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
            for (int i = 0; i < size; i++)
            {
                if (last != points[i])
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
                        extremPoints.Add(points[i]);
                        //if(!extremPoints.Contains(points[i]))
                            last = points[i];
                    }
                    right.Clear();
                    left.Clear();
                }
                right.Clear();
                left.Clear();
            }
            outPoints = extremPoints.ToList();
            for (int i = 0; i < listExtremPoints.Count(); i++)
            {
                //Console.WriteLine(last.X+" d "+ last.Y);
                Console.WriteLine(listExtremPoints[i].X + " " + listExtremPoints[i].Y);
            }
        }

        public override string ToString()
        {
            return "Convex Hull - Extreme Segments";
        }
    }
}
