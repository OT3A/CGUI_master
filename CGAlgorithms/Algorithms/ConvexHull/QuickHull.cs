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
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            int size = points.Count();
            List<Point> copePoints = new List<Point>();
            Point minX;
            Point miny;
            Point maxX;
            Point maxy;
            for(int i = 0; i < size; i++)
            {
                Point p = new Point(0, 0);
                p.X = points[i].X;
                p.Y = points[i].Y;
                copePoints.Add(p);
            }
            copePoints.Sort((p1, p2) => p1.X.CompareTo(p2.X));
            minX = copePoints[0];
            maxX = copePoints[size - 1];
            copePoints.Sort((p1, p2) => p1.Y.CompareTo(p2.Y));
            miny = copePoints[0];
            maxy = copePoints[size - 1];

        }

        public override string ToString()
        {
            return "Convex Hull - Quick Hull";
        }
    }
}
