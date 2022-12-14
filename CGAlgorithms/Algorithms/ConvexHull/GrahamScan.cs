using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class GrahamScan : Algorithm
    {

        public static int getBy_Y(List<Point> points) 
        {
            double Min = 10000000000;
            int index = 0;
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].Y < Min)
                {
                    Min = points[i].Y;
                    index = i;
                }
            }
            return index;
        }

        public static List<KeyValuePair<Point, double>> SortAngle(List<Point> points ,Line LineHorizontal, Point p)
        {
            List<KeyValuePair<Point, double>> SortPointsAngle = new List<KeyValuePair<Point, double>>();

            for (int i = 0; i < points.Count; i++)
            {
                Point tmp = new Point((points[i].X - LineHorizontal.Start.X), (points[i].Y - LineHorizontal.Start.Y));
                double dotProduct = (p.X * tmp.X) + (p.Y * tmp.Y);
                double radAngel = Math.Atan2(dotProduct, HelperMethods.CrossProduct(p, tmp));
                double degAngel = (180 / Math.PI) * (radAngel);
                SortPointsAngle.Add(new KeyValuePair<Point, double>(points[i], degAngel));
            }

            SortPointsAngle.Sort((x, y) => x.Value.CompareTo(y.Value));
            return SortPointsAngle;
        }

        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            List<Point> tmpPints = new List<Point>();
            List<KeyValuePair<Point, double>> Sorted_Points = new List<KeyValuePair<Point, double>>();

            //remove Dublicated
            for (int i = 0; i < points.Count; i++)
            {
                if (!tmpPints.Contains(points[i]))
                    tmpPints.Add(points[i]);
            }

            points = tmpPints;

            //if No Convex
            if (points.Count < 3)
            {
                outPoints = points;
                return;
            }

            Point MinY = points[getBy_Y(points)];
            Point FirstPoint = new Point(MinY.X + 1, MinY.Y);
            Line FirstLineHorizontal = new Line(MinY, FirstPoint); //min y line const 

            points.Remove(MinY);

            Point p = new Point((FirstLineHorizontal.End.X - FirstLineHorizontal.Start.X), (FirstLineHorizontal.End.Y - FirstLineHorizontal.Start.Y));
            Point top, preTop;
            //sorted point by angle
            Sorted_Points = SortAngle(points, FirstLineHorizontal, p);

            Sorted_Points.Add(new KeyValuePair<Point, double>(MinY, 0));

            Stack<Point> ConvexHull = new Stack<Point>();
            ConvexHull.Push(MinY);
            ConvexHull.Push(Sorted_Points[0].Key);
            

            for (int i = 1; i < Sorted_Points.Count; i++)
            {
                top = ConvexHull.Pop();
                preTop = ConvexHull.Pop();
                ConvexHull.Push(preTop);
                ConvexHull.Push(top);
                Line segment = new Line(top, preTop);
                if (ConvexHull.Count > 2)//exist convex
                {
                    while (HelperMethods.CheckTurn(segment, Sorted_Points[i].Key) != Enums.TurnType.Left) //if not extreme 
                    {
                        ConvexHull.Pop();
                        top = ConvexHull.Pop();
                        preTop = ConvexHull.Pop();
                        ConvexHull.Push(preTop);
                        ConvexHull.Push(top);
                        segment = new Line(top, preTop);
                    }
                }
                ConvexHull.Push(Sorted_Points[i].Key);
            }
          
            //output
            for(int i = ConvexHull.Count; i > 1; i--)
            {
                outPoints.Add(ConvexHull.Pop());
            }

            //outPoints.RemoveAt(outPoints.Count - 1);
        }
        public override string ToString()
        {
            return "Convex Hull - Graham Scan";
        }

    }
}