using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class QuickHull : Algorithm
    {
        public double distance(Line l,Point p)
        {
            double d = 0;
            double d1 = Math.Sqrt(Math.Pow(p.Y - l.Start.Y, 2) + Math.Pow(p.X - l.Start.X, 2));
            double d2 = Math.Sqrt(Math.Pow(p.Y - l.End.Y, 2) + Math.Pow(p.X - l.End.X, 2));
            d = d1 + d2;
            return d;
        } 
        public void Recursion(Line l1,Line l2 ,List<Point> input, List<Point> output)
        {
            Point p1 =null;
            Point p2 =null;
            double maxD1 = 0;
            double maxD2 = 0;
            int size = input.Count();
            for(int i = 0; i < size; i++)
            {
                if(CGUtilities.HelperMethods.CheckTurn(l1, input[i]) == CGUtilities.Enums.TurnType.Right)
                {
                    double d = distance(l1, input[i]);
                    if (d > maxD1)
                    {
                        p1 = input[i];
                        maxD1 = d;
                    }
                }
            }
            if (p1 != null)
            {
                output.Add(p1);
                Line ll1 = new Line(l1.Start, p1);
                Line ll2 = new Line(p1, l1.End);
                Recursion(ll1, ll2, input, output);
            }
            
            
            for (int i = 0; i < size; i++)
            {

                if (CGUtilities.HelperMethods.CheckTurn(l2, input[i]) == CGUtilities.Enums.TurnType.Right)
                {
                    double d = distance(l2, input[i]);
                    if (d > maxD2)
                    {
                        p2 = input[i];
                        maxD2 = d;
                    }
                }
            }
            if (p2 != null)
            {
                output.Add(p2);
                Line ll1 = new Line(l2.Start, p2);
                Line ll2 = new Line(p2, l2.End);
                Recursion(ll1, ll2, input, output);
            }
        }
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            int size = points.Count();
            Point minX = null;
            Point miny = null;
            Point maxX = null;
            Point maxy = null;

            for (int i = 0; i < size; i++)
            {
                if(i != 0)
                {
                    if (points[i].X < minX.X)
                    {
                        minX = points[i];
                    }
                    else if (points[i].X > maxX.X)
                    {
                        maxX = points[i];
                    }
                    else if (points[i].Y < miny.Y)
                    {
                        miny = points[i];
                    }
                    else if (points[i].Y > maxy.Y)
                    {
                        maxy = points[i];
                    }
                }
                else
                {
                    minX = points[i];
                    miny = points[i];
                    maxX = points[i];
                    maxy = points[i];
                }
            }

            if(!outPoints.Contains(miny))
                outPoints.Add(miny);
            if (!outPoints.Contains(minX) )
                outPoints.Add(minX);
            if (!outPoints.Contains(maxy))
                outPoints.Add(maxy);
            if (!outPoints.Contains(maxX))
                outPoints.Add(maxX);
            Line l1 = new Line(miny, maxX);
            Line l2 = new Line(maxX, maxy);
            Line l3 = new Line(maxy, minX);
            Line l4 = new Line(minX, miny);
            Recursion(l1, l2, points, outPoints);
            Recursion(l3, l4, points, outPoints);
        }

        public override string ToString()
        {
            return "Convex Hull - Quick Hull";
        }
    }
}
