using System;
using UnityEngine;

namespace Clicker.Tools
{
    public static class GeometricCalculator
    {
        /// <summary>
        /// Calculate area of triangle
        /// </summary>
        /// <param name="point1"> position of first point</param>
        /// <param name="point2"> position of second point</param>
        /// <param name="point3"> position of third point</param>
        /// <returns> area of triangle</returns>
        public static double CalculateAreaOfTriangle(Vector2 point1, Vector2 point2, Vector2 point3)
        {
            double area = Math.Abs(0.5f * (point1.x * point2.y - point2.x * point1.y + point2.x * point3.y -
                point3.x * point2.y + point3.x * point1.y - point1.x * point3.y));
            return area;
        }

        /// <summary>
        /// Calculate area of square
        /// </summary>
        /// <param name="sideLength"> length of square</param>
        /// <returns> area of square with length of side are equal sideLength</returns>
        /// <exception cref="Exception"> if length of square is less or equal 0</exception>
        public static double CalculateAreaOfSquare(float sideLength)
        {
            if (sideLength <= 0)
                throw new ArgumentException("[GeometricCalculator.CalculateAreaOfSquare] sideLength <= 0!");

            return Math.Pow(sideLength, 2);
        }

        /// <summary>
        /// Square contains target point if
        /// sum of area all triangles, between target point and points of all sides
        /// are equal area of all square
        /// </summary>
        /// <param name="coordinatesCenterOfSquare"> coordinate center of square</param>
        /// <param name="coordinatesPoint"> target point</param>
        /// <param name="halfSideLength"> half length of square</param>
        /// <param name="squareArea"> area of square</param>
        /// <returns> true - if square contains target point</returns>
        /// <exception cref="Exception"> if squareArea is less or equal 0</exception>
        public static bool SquareContainsPoint(Vector2 coordinatesCenterOfSquare, Vector2 coordinatesPoint,
            float halfSideLength, double squareArea)
        {
            if (squareArea <= 0)
                throw new Exception("[GeometricCalculator.SquareContainsPoint] square area <= 0!");

            Vector2 a = new Vector2(coordinatesCenterOfSquare.x - halfSideLength,
                coordinatesCenterOfSquare.y + halfSideLength);
            Vector2 b = new Vector2(coordinatesCenterOfSquare.x + halfSideLength,
                coordinatesCenterOfSquare.y + halfSideLength);
            Vector2 c = new Vector2(coordinatesCenterOfSquare.x + halfSideLength,
                coordinatesCenterOfSquare.y - halfSideLength);
            Vector2 d = new Vector2(coordinatesCenterOfSquare.x - halfSideLength,
                coordinatesCenterOfSquare.y - halfSideLength);

            var areaOfTriangle1 = CalculateAreaOfTriangle(a, b, coordinatesPoint);
            var areaOfTriangle2 = CalculateAreaOfTriangle(b, c, coordinatesPoint);
            var areaOfTriangle3 = CalculateAreaOfTriangle(c, d, coordinatesPoint);
            var areaOfTriangle4 = CalculateAreaOfTriangle(a, d, coordinatesPoint);

            return Math.Abs(areaOfTriangle1 + areaOfTriangle2 + areaOfTriangle3 + areaOfTriangle4 - squareArea) < 1e-5;
        }

        /// <summary>
        /// Circle contains point if distance between center of circle and target point are less of radius od circle 
        /// </summary>
        /// <param name="coordinatesCenterOfCircle"> coordinate center of circle</param>
        /// <param name="coordinatesPoint"> target point</param>
        /// <param name="radius"> radius of circle</param>
        /// <returns> true - if circle contain point</returns>
        /// <exception cref="Exception"></exception>
        public static bool CircleContainsPoint(Vector2 coordinatesCenterOfCircle, Vector2 coordinatesPoint,
            float radius)
        {
            if (radius <= 0)
                throw new Exception("[GeometricCalculator.CircleContainsPoint] radius <= 0!");

            return (Vector2.Distance(coordinatesCenterOfCircle, coordinatesPoint) - radius) < 1e-5;
        }
    }
}