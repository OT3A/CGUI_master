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

            List<Point> tmpPints = new List<Point>();

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

            //Point MinY_Point = points.OrderByDescending(p => p.Y).ToList().Last();

            double minY = 100000000;
            int index = -5;

            //get Min
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].Y < minY)
                {
                    minY = points[i].Y;
                    index = i;
                }
            }

            Point MinY_Point = points[index];
            Point eassumed_pointX = new Point(MinY_Point.X - 20, MinY_Point.Y);
            Point StartedPoint = MinY_Point;

            outPoints.Add(StartedPoint);
            double counter = 0;

            while (counter < points.Count)
            {
                double largest_angle = 0;
                Point next = MinY_Point;
                double Distant = 0;
                double largest_dist = 0;

                for (int i = 0; i < points.Count; i++)
                {
                    Point ab = new Point((MinY_Point.X - eassumed_pointX.X), (MinY_Point.Y - eassumed_pointX.Y));
                    Point ac = new Point((points[i].X - MinY_Point.X), (points[i].Y - MinY_Point.Y));

                    //angle = shift tan ( product / cross  )
                    double angle = Math.Atan2(HelperMethods.CrossProduct(ab, ac), (ab.X * ac.X) + (ab.Y * ac.Y));

                    if (angle < 0)
                        angle += (2 * Math.PI);

                    //get Distant
                    Distant = Math.Sqrt((MinY_Point.X - points[i].X) + (MinY_Point.Y - points[i].Y));

                    //large Angle
                    if (angle > largest_angle)
                    {
                        largest_angle = angle;
                        largest_dist = Distant;
                        next = points[i];
                    }
                    else if (angle == largest_angle)
                    {
                        if (Distant > largest_dist)
                        {
                            largest_dist = Distant;
                            next = points[i];
                        }
                    }

                }

                // Finish Convex
                if (StartedPoint.X == next.X && StartedPoint.Y == next.Y)
                {
                    break;
                }

                outPoints.Add(next);

                eassumed_pointX = MinY_Point;
                MinY_Point = next;

                counter++;

            }

        }

        public override string ToString()
        {
            return "Convex Hull - Jarvis March";
        }
    }
}
