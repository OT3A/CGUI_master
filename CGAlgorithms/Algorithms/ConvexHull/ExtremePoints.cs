using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class ExtremePoints : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            List<CGUtilities.Point> ps = new List<CGUtilities.Point>();

            int size = points.Count();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    for (int k = 0; k < size; k++)
                    {
                        if (points[i] != points[j] && points[j] != points[k] && points[i] != points[k])
                        {
                            for (int p = 0; p < size; p++)
                            {
                                if (i != p && j != p && k != p)
                                {
                                    if (CGUtilities.HelperMethods.PointInTriangle(points[p], points[i], points[j], points[k]) == CGUtilities.Enums.PointInPolygon.Inside)
                                    {
                                        ps.Add(points[p]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            
            for (int i = 0; i < size; i++)
            {
                if (!ps.Contains(points[i]))
                    outPoints.Add(points[i]);
            }
            for(int i = 0; i < outPoints.Count(); i++)
            {
                Console.WriteLine(outPoints[i].X + " " + outPoints[i].Y);
            }
        }

        public override string ToString()
        {
            return "Convex Hull - Extreme Points";
        }
    }
}
