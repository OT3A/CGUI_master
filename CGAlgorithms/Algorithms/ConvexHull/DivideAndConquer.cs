using CGUtilities;
using System.Collections.Generic;
using System.Linq;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class DivideAndConquer : Algorithm
    {

        public List<Point> merge(List<Point> LeftBranch, List<Point> RightBranch)
        {

           
            List<Point> DistinctLeft = new List<Point>();
            List<Point> DistinctRight = new List<Point>();

            // To Make The Right Branch Have the Only Distinct Points
            foreach(Point p in RightBranch)
            {
                if (DistinctRight.Contains(p)==false)
                    DistinctRight.Add(p);
            }
            // To Make The Left Branch Have the Only Distinct Points
            foreach (Point p in LeftBranch)
            {
                if (DistinctLeft.Contains(p)==false)
                    DistinctLeft.Add(p);
                
            }

         
            int LiftBranchLength = DistinctLeft.Count;
            int RightBranchLength = DistinctRight.Count;

            // Find the furthest point in the left branch
            int LeftMaxIdex = GetMaxLeft(DistinctLeft);

            // Find the Nearst point in the Right branch
           int  RightMinIdex = GetMinRight(DistinctRight);

            //  Upper Tangent 
            int UpperLeftIndex = LeftMaxIdex;
            int UpperRightIdex = RightMinIdex;
            GetUpperTangent(DistinctRight, DistinctLeft, ref UpperLeftIndex, ref UpperRightIdex);

            // Lower Tenget
            int LowerLeftIndex = LeftMaxIdex;
            int LowerRightIndex = RightMinIdex;        
            GetLowerTangent(DistinctRight, DistinctLeft,ref LowerLeftIndex,ref LowerRightIndex);

            int Temp = UpperLeftIndex;
            int Temp2 = LowerRightIndex;
            List<Point> New_Convex = new List<Point>();

            if (New_Convex.Contains(DistinctLeft[Temp])==false)
            {

                New_Convex.Add(DistinctLeft[Temp]);
            }

            if(Temp!=LowerLeftIndex)
            {
                while (true)
                {
                    Temp += 1;
                    Temp %= LiftBranchLength;
                    if (New_Convex.Contains(DistinctLeft[Temp])==false)
                    {
                        New_Convex.Add(DistinctLeft[Temp]);

                    }
                    if (Temp == LowerLeftIndex)
                        break;
                }

                    

            }

            if (New_Convex.Contains(DistinctRight[Temp2])==false)
            {
                New_Convex.Add(DistinctRight[Temp2]);
            }

            if(Temp2 != UpperRightIdex)
            {
                while (true)
                {
                    Temp2 += 1;
                    Temp2 %=  RightBranchLength;
                    if (New_Convex.Contains(DistinctRight[Temp2])==false)
                    {
                        New_Convex.Add(DistinctRight[Temp2]);
                    }
                    if (Temp2 == UpperRightIdex)
                        break;
                }

            }
            return New_Convex;
        }

        public int GetMaxLeft(List<Point> DistinctLeft)
        {
            int LeftIndex = 0;
            for (int i = 0; i < DistinctLeft.Count; i++)
            {
                if (DistinctLeft[i].X > DistinctLeft[LeftIndex].X)
                    LeftIndex = i;
                else if (DistinctLeft[i].X == DistinctLeft[LeftIndex].X && DistinctLeft[i].Y > DistinctLeft[LeftIndex].Y)
                    LeftIndex = i;
                

            }

            return LeftIndex;
        }

        public int GetMinRight(List<Point> DistinctRight)
        {
            int RightIndex = 0;
            for (int i = 1; i < DistinctRight.Count; i++)
            {
                if (DistinctRight[i].X < DistinctRight[RightIndex].X)
                    RightIndex = i;
                else if (DistinctRight[i].X == DistinctRight[RightIndex].X && DistinctRight[i].Y < DistinctRight[RightIndex].Y)        
                        RightIndex = i;
                
            }
            return RightIndex;
        }

        public void GetUpperTangent(List<Point> DistinctRight, List<Point> DistinctLeft, ref int UpperLeftIndex,ref int UpperRightIdex)
        {
            Enums.TurnType Direction;
            bool cheack = false;            
            while (true)
            {
                cheack = true;
                //Get the first point in the tanget
                Direction = HelperMethods.CheckTurn(new Line(DistinctRight[UpperRightIdex].X,
                           DistinctRight[UpperRightIdex].Y, DistinctLeft[UpperLeftIndex].X,
                           DistinctLeft[UpperLeftIndex].Y),
                           DistinctLeft[(UpperLeftIndex + 1) % DistinctLeft.Count]);

                while (Direction== Enums.TurnType.Right)
                {
                    UpperLeftIndex = (UpperLeftIndex + 1) % DistinctLeft.Count;
                    cheack = false;
                    Direction = HelperMethods.CheckTurn(new Line(DistinctRight[UpperRightIdex].X,
                    DistinctRight[UpperRightIdex].Y, DistinctLeft[UpperLeftIndex].X,
                    DistinctLeft[UpperLeftIndex].Y),
                    DistinctLeft[(UpperLeftIndex + 1) % DistinctLeft.Count]);
                }
                  
                if (Direction== Enums.TurnType.Colinear)
                {

                    UpperLeftIndex = (UpperLeftIndex + 1) % DistinctLeft.Count;
                }

                Direction = HelperMethods.CheckTurn(new Line(DistinctLeft[UpperLeftIndex].X,
                    DistinctLeft[UpperLeftIndex].Y, DistinctRight[UpperRightIdex].X,
                    DistinctRight[UpperRightIdex].Y),
                    DistinctRight[(DistinctRight.Count + UpperRightIdex - 1) % DistinctRight.Count]);
                //get the second point in the tanget
                while (Direction== Enums.TurnType.Left)
                {
                    UpperRightIdex = (DistinctRight.Count + UpperRightIdex - 1) % DistinctRight.Count;
                    cheack = false;
                    Direction = HelperMethods.CheckTurn(new Line(DistinctLeft[UpperLeftIndex].X,
                    DistinctLeft[UpperLeftIndex].Y, DistinctRight[UpperRightIdex].X,
                    DistinctRight[UpperRightIdex].Y),
                    DistinctRight[(DistinctRight.Count + UpperRightIdex - 1) % DistinctRight.Count]);

                }
                if (Direction== Enums.TurnType.Colinear)
                {

                    UpperRightIdex = (UpperRightIdex + DistinctRight.Count - 1) % DistinctRight.Count;
                }
                if(cheack==true)
                {
                    break;
                }
            }
        }
        public void GetLowerTangent(List<Point> DistinctRight, List<Point> DistinctLeft, ref int LowerLeftIndex, ref int LowerRightIndex)
        {
            bool found = false;
            while (true)
            {
                found = true;
                while (CGUtilities.HelperMethods.CheckTurn(new Line(DistinctRight[LowerRightIndex].X,
                    DistinctRight[LowerRightIndex].Y, DistinctLeft[LowerLeftIndex].X,
                    DistinctLeft[LowerLeftIndex].Y),
                    DistinctLeft[(LowerLeftIndex + DistinctLeft.Count - 1) % DistinctLeft.Count]) 
                    == Enums.TurnType.Left)
                {
                    LowerLeftIndex = (LowerLeftIndex + DistinctLeft.Count - 1) % DistinctLeft.Count;
                    found = false;
                }

                if(HelperMethods.CheckTurn(new Line(DistinctRight[LowerRightIndex].X, 
                    DistinctRight[LowerRightIndex].Y, DistinctLeft[LowerLeftIndex].X,
                    DistinctLeft[LowerLeftIndex].Y),DistinctLeft[(LowerLeftIndex + DistinctLeft.Count - 1)
                    % DistinctLeft.Count]) == Enums.TurnType.Colinear)
                {

                     LowerLeftIndex = (LowerLeftIndex + DistinctLeft.Count - 1) % DistinctLeft.Count;
                }

                while (CGUtilities.HelperMethods.CheckTurn(new Line(DistinctLeft[LowerLeftIndex].X,
                    DistinctLeft[LowerLeftIndex].Y, DistinctRight[LowerRightIndex].X,
                    DistinctRight[LowerRightIndex].Y), DistinctRight[(LowerRightIndex + 1)
                    % DistinctRight.Count]) == Enums.TurnType.Right)
                {
                    LowerRightIndex = (LowerRightIndex + 1) % DistinctRight.Count;
                    found = false;
                }
                if ((CGUtilities.HelperMethods.CheckTurn(new Line(DistinctLeft[LowerLeftIndex].X,
                    DistinctLeft[LowerLeftIndex].Y, DistinctRight[LowerRightIndex].X,
                    DistinctRight[LowerRightIndex].Y), DistinctRight[(LowerRightIndex + 1) 
                    % DistinctRight.Count]) == Enums.TurnType.Colinear))
                    LowerRightIndex = (LowerRightIndex + 1) % DistinctRight.Count;
                if (found == true)
                    break;
            }

        }        
        public List<Point> DivideTwoConvex(List<Point> points)
        {
            List<Point> LeftBranch, RightBranch, NextLeftBranch, NextRightBranch;
             List<Point> DistinctLeft = new List<Point>();
            List<Point> DistinctRight = new List<Point>();
            int Length = points.Count;

            points.Sort((a, b) => {
                if (a.X == b.X) return a.Y.CompareTo(b.Y);
                return a.X.CompareTo(b.X);
            });
          
            if (Length != 1)
            {
                LeftBranch = new List<Point>();
                RightBranch = new List<Point>();                
                for (int i = 0; i < Length / 2; i++)
                     LeftBranch.Add(points[i]);
                for (int i = Length / 2; i < points.Count; i++)
                    RightBranch.Add(points[i]);
                NextLeftBranch = DivideTwoConvex(LeftBranch);
                NextRightBranch = DivideTwoConvex(RightBranch);
                return merge(NextLeftBranch, NextRightBranch);
            }
            else
                return points;
        }
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> LastConvex, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {

            LastConvex = new List<Point>();
            List<Point> ReturnedPoints = DivideTwoConvex(points);
            foreach (Point p in ReturnedPoints)
            {
                if (LastConvex.Contains(p) == false)
                {
                    LastConvex.Add(p);
                }
            }
        }
        public override string ToString()
        {
            return "Convex Hull - DivideTwoConvex & Conquer";
        }

    }
}