using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class Incremental : Algorithm
    {

        public int CheckPoint(Point P1, Point P2)
        {
            if (P1.X < P2.X)
            {
                return -1;
            }

            if (P1.X == P2.X)
            {
                if (P1.Y < P2.Y)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
            return 1;
        }

        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {

            List<Point> tmpPints = new List<Point>();

            //remove Dublicated
            for (int i = 0; i < points.Count; i++)
                if (!tmpPints.Contains(points[i]))
                    tmpPints.Add(points[i]);
            
            points = tmpPints;

            //if No Convex
            if (points.Count < 3)
            {
                outPoints = points;
                return;
            }

            //special sort
            points.Sort(CheckPoint);

            Line FirstLine = new Line(points[0], points[1]);

            int[] prevIdx = new int[points.Count];
            int[] nextIdx = new int[points.Count];
            
            if (HelperMethods.CheckTurn(FirstLine, points[2]) == Enums.TurnType.Right)//anti clock wise
            {
                nextIdx[0] = 2; prevIdx[0] = 1;
                nextIdx[2] = 1; prevIdx[2] = 0;
                nextIdx[1] = 0; prevIdx[1] = 2;
            }
            
            if (HelperMethods.CheckTurn(FirstLine, points[2]) == Enums.TurnType.Left)
            { 
                nextIdx[0] = 1; prevIdx[0] = 2;
                nextIdx[1] = 2; prevIdx[1] = 0;
                nextIdx[2] = 0; prevIdx[2] = 1;
               
            }
            int StartFromPoint = 2;
            //we will start from poit 4
            for (int i = 3; i < points.Count; i++)
            {
                Line line;

                int upper, lower;
                while (true)
                {
                    //line between new point and last point added in convex
                    line = new Line(points[i], points[StartFromPoint]);
                    if (HelperMethods.CheckTurn(line, points[nextIdx[StartFromPoint]]) == Enums.TurnType.Left)
                    {
                        upper = StartFromPoint;
                        break;
                    }
                    else
                    {
                        StartFromPoint = nextIdx[StartFromPoint];
                    }
                }
                
                StartFromPoint = i - 1;
                while (true)
                {
                    line = new Line(points[i], points[StartFromPoint]);
                    if (HelperMethods.CheckTurn(line, points[prevIdx[StartFromPoint]]) == Enums.TurnType.Right)
                    {
                        lower = StartFromPoint;
                        break;
                    }
                    else
                    {
                        StartFromPoint = prevIdx[StartFromPoint];
                    }
                }

                prevIdx[upper] = i;
                nextIdx[lower] = i;
                StartFromPoint = i;
                nextIdx[i] = upper;
                prevIdx[i] = lower;
            }

            outPoints.Add(points[StartFromPoint]);
            StartFromPoint = nextIdx[StartFromPoint];

            while (points[StartFromPoint] != outPoints[0])
            {
                outPoints.Add(points[StartFromPoint]);
                StartFromPoint = nextIdx[StartFromPoint];
            } 
        }


        public override string ToString()
        {
            return "Convex Hull - Incremental";
        }
    }
}
