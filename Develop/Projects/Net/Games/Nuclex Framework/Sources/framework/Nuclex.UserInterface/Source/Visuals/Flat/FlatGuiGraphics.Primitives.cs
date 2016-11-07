using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Nuclex.UserInterface.Visuals.Flat
{
    public partial class FlatGuiGraphics
    {
        // Fields
        private static readonly Dictionary<string, List<Vector2>> m_circleCache = new Dictionary<string, List<Vector2>>();
        private static Texture2D m_pixel;

        // Methods
        private List<Vector2> CreateArc(float radius, int sides, float startingAngle, float degrees)
        {
            var list = new List<Vector2>();
            list.AddRange(CreateCircle(radius, sides));
            list.RemoveAt(list.Count - 1);
            double num = 0.0;
            double num2 = 360.0 / sides;
            while ((num + (num2 / 2.0)) < startingAngle)
            {
                num += num2;
                list.Add(list[0]);
                list.RemoveAt(0);
            }
            list.Add(list[0]);
            int num3 = (int)((((double)degrees) / num2) + 0.5);
            list.RemoveRange(num3 + 1, (list.Count - num3) - 1);
            return list;
        }

        private List<Vector2> CreateCircle(double radius, int sides)
        {
            string key = radius + "x" + sides;
            if (m_circleCache.ContainsKey(key))
            {
                return m_circleCache[key];
            }
            List<Vector2> list = new List<Vector2>();
            double num = 6.2831853071795862 / ((double)sides);
            for (double i = 0.0; i < 6.2831853071795862; i += num)
            {
                list.Add(new Vector2((float)(radius * Math.Cos(i)), (float)(radius * Math.Sin(i))));
            }
            list.Add(new Vector2((float)(radius * Math.Cos(0.0)), (float)(radius * Math.Sin(0.0))));
            m_circleCache.Add(key, list);
            return list;
        }

        private void CreateThePixel()
        {
            m_pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            Color[] data = new Color[] { Color.White };
            m_pixel.SetData<Color>(data);
        }

        /// <summary>
        /// Draws the arc.
        /// </summary>
        /// <param name="center">The center.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="sides">The sides.</param>
        /// <param name="startingAngle">The starting angle.</param>
        /// <param name="degrees">The degrees.</param>
        /// <param name="color">The color.</param>
        public void DrawArc(Vector2 center, float radius, int sides, float startingAngle, float degrees, Color color)
        {
            DrawArc(center, radius, sides, startingAngle, degrees, color, 1f);
        }

        /// <summary>
        /// Draws the arc.
        /// </summary>
        /// <param name="center">The center.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="sides">The sides.</param>
        /// <param name="startingAngle">The starting angle.</param>
        /// <param name="degrees">The degrees.</param>
        /// <param name="color">The color.</param>
        /// <param name="thickness">The thickness.</param>
        public void DrawArc(Vector2 center, float radius, int sides, float startingAngle, float degrees, Color color, float thickness)
        {
            List<Vector2> points = CreateArc(radius, sides, startingAngle, degrees);
            DrawPoints(center, points, color, thickness);
        }

        /// <summary>
        /// Draws the circle.
        /// </summary>
        /// <param name="center">The center.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="sides">The sides.</param>
        /// <param name="color">The color.</param>
        public void DrawCircle(Vector2 center, float radius, int sides, Color color)
        {
            DrawPoints(center, CreateCircle((double)radius, sides), color, 1f);
        }

        /// <summary>
        /// Draws the circle.
        /// </summary>
        /// <param name="center">The center.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="sides">The sides.</param>
        /// <param name="color">The color.</param>
        /// <param name="thickness">The thickness.</param>
        public void DrawCircle(Vector2 center, float radius, int sides, Color color, float thickness)
        {
            DrawPoints(center, CreateCircle((double)radius, sides), color, thickness);
        }

        /// <summary>
        /// Draws the line.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <param name="color">The color.</param>
        public void DrawLine(Vector2 point1, Vector2 point2, Color color)
        {
            DrawLine(point1, point2, color, 1f);
        }

        /// <summary>
        /// Draws the line.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <param name="color">The color.</param>
        /// <param name="thickness">The thickness.</param>
        public void DrawLine(Vector2 point1, Vector2 point2, Color color, float thickness)
        {
            float length = Vector2.Distance(point1, point2);
            float angle = (float)Math.Atan2((double)(point2.Y - point1.Y), (double)(point2.X - point1.X));
            DrawLine(point1, length, angle, color, thickness);
        }

        /// <summary>
        /// Draws the line.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="length">The length.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="color">The color.</param>
        public void DrawLine(Vector2 point, float length, float angle, Color color)
        {
            DrawLine(point, length, angle, color, 1f);
        }

        /// <summary>
        /// Draws the line.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="length">The length.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="color">The color.</param>
        /// <param name="thickness">The thickness.</param>
        public void DrawLine(Vector2 point, float length, float angle, Color color, float thickness)
        {
            if (m_pixel == null)
            {
                CreateThePixel();
            }
            spriteBatch.Draw(m_pixel, point, null, color, angle, Vector2.Zero, new Vector2(length, thickness), SpriteEffects.None, 0f);
        }

        /// <summary>
        /// Draws the line.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="color">The color.</param>
        public void DrawLine(float x1, float y1, float x2, float y2, Color color)
        {
            DrawLine(new Vector2(x1, y1), new Vector2(x2, y2), color, 1f);
        }

        /// <summary>
        /// Draws the line.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="color">The color.</param>
        /// <param name="thickness">The thickness.</param>
        public void DrawLine(float x1, float y1, float x2, float y2, Color color, float thickness)
        {
            DrawLine(new Vector2(x1, y1), new Vector2(x2, y2), color, thickness);
        }

        private void DrawPoints(Vector2 position, List<Vector2> points, Color color)
        {
            DrawPoints(position, points, color, 1f);
        }

        private void DrawPoints(Vector2 position, List<Vector2> points, Color color, float thickness)
        {
            if (points.Count >= 2)
            {
                for (int i = 1; i < points.Count; i++)
                {
                    DrawLine(points[i - 1] + position, points[i] + position, color, thickness);
                }
            }
        }

        /// <summary>
        /// Draws the rectangle.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="color">The color.</param>
        public void DrawRectangle(Rectangle rect, Color color)
        {
            DrawRectangle(rect, color, 1f, 0f, new Vector2((float)rect.X, (float)rect.Y));
        }

        /// <summary>
        /// Draws the rectangle.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="color">The color.</param>
        /// <param name="thickness">The thickness.</param>
        public void DrawRectangle(Rectangle rect, Color color, float thickness)
        {
            DrawRectangle(rect, color, thickness, 0f, new Vector2((float)rect.X, (float)rect.Y));
        }

        /// <summary>
        /// Draws the rectangle.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="size">The size.</param>
        /// <param name="color">The color.</param>
        public void DrawRectangle(Vector2 location, Vector2 size, Color color)
        {
            DrawRectangle(new Rectangle((int)location.X, (int)location.Y, (int)size.X, (int)size.Y), color, 1f, 0f, location);
        }

        /// <summary>
        /// Draws the rectangle.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="color">The color.</param>
        /// <param name="thickness">The thickness.</param>
        /// <param name="angle">The angle.</param>
        public void DrawRectangle(Rectangle rect, Color color, float thickness, float angle)
        {
            DrawRectangle(rect, color, thickness, angle, new Vector2(rect.X, rect.Y));
        }

        /// <summary>
        /// Draws the rectangle.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="size">The size.</param>
        /// <param name="color">The color.</param>
        /// <param name="thickness">The thickness.</param>
        public void DrawRectangle(Vector2 location, Vector2 size, Color color, float thickness)
        {
            DrawRectangle(new Rectangle((int)location.X, (int)location.Y, (int)size.X, (int)size.Y), color, thickness, 0f, location);
        }

        /// <summary>
        /// Draws the rectangle.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="color">The color.</param>
        /// <param name="thickness">The thickness.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="rotateAround">The rotate around.</param>
        public void DrawRectangle(Rectangle rect, Color color, float thickness, float angle, Vector2 rotateAround)
        {
            DrawLine(new Vector2(rect.X, rect.Y), new Vector2(rect.Right, rect.Y), color, thickness);
            DrawLine(new Vector2(rect.X + 1f, rect.Y), new Vector2(rect.X + 1f, rect.Bottom + 1f), color, thickness);
            DrawLine(new Vector2(rect.X, rect.Bottom), new Vector2(rect.Right, rect.Bottom), color, thickness);
            DrawLine(new Vector2(rect.Right + 1f, rect.Y), new Vector2(rect.Right + 1f, rect.Bottom + 1f), color, thickness);
        }

        /// <summary>
        /// Draws the rectangle.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="size">The size.</param>
        /// <param name="color">The color.</param>
        /// <param name="thickness">The thickness.</param>
        /// <param name="angle">The angle.</param>
        public void DrawRectangle(Vector2 location, Vector2 size, Color color, float thickness, float angle)
        {
            DrawRectangle(new Rectangle((int)location.X, (int)location.Y, (int)size.X, (int)size.Y), color, thickness, angle, location);
        }

        /// <summary>
        /// Draws the rectangle.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="size">The size.</param>
        /// <param name="color">The color.</param>
        /// <param name="thickness">The thickness.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="rotateAround">The rotate around.</param>
        public void DrawRectangle(Vector2 location, Vector2 size, Color color, float thickness, float angle, Vector2 rotateAround)
        {
            DrawRectangle(new Rectangle((int)location.X, (int)location.Y, (int)size.X, (int)size.Y), color, thickness, angle, rotateAround);
        }

        /// <summary>
        /// Fills the rectangle.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="color">The color.</param>
        public void FillRectangle(Rectangle rect, Color color)
        {
            if (m_pixel == null)
            {
                CreateThePixel();
            }
            spriteBatch.Draw(m_pixel, rect, color);
        }

        /// <summary>
        /// Fills the rectangle.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="color">The color.</param>
        /// <param name="angle">The angle.</param>
        public void FillRectangle(Rectangle rect, Color color, float angle)
        {
            if (m_pixel == null)
            {
                CreateThePixel();
            }
            spriteBatch.Draw(m_pixel, rect, null, color, angle, Vector2.Zero, SpriteEffects.None, 0f);
        }

        /// <summary>
        /// Fills the rectangle.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="size">The size.</param>
        /// <param name="color">The color.</param>
        public void FillRectangle(Vector2 location, Vector2 size, Color color)
        {
            FillRectangle(location, size, color, 0f);
        }

        /// <summary>
        /// Fills the rectangle.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="size">The size.</param>
        /// <param name="color">The color.</param>
        /// <param name="angle">The angle.</param>
        public void FillRectangle(Vector2 location, Vector2 size, Color color, float angle)
        {
            if (m_pixel == null)
            {
                CreateThePixel();
            }
            spriteBatch.Draw(m_pixel, location, null, color, angle, Vector2.Zero, size, SpriteEffects.None, 0f);
        }

        /// <summary>
        /// Fills the rectangle.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="color">The color.</param>
        public void FillRectangle(float x1, float y1, float x2, float y2, Color color)
        {
            if (m_pixel == null)
            {
                CreateThePixel();
            }
            FillRectangle(new Vector2(x1, y1), new Vector2(x2, y2), color, 1f);
        }

        /// <summary>
        /// Fills the rectangle.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="color">The color.</param>
        /// <param name="thickness">The thickness.</param>
        public void FillRectangle(float x1, float y1, float x2, float y2, Color color, float thickness)
        {
            if (m_pixel == null)
            {
                CreateThePixel();
            }
            FillRectangle(new Vector2(x1, y1), new Vector2(x2, y2), color, thickness);
        }

        /// <summary>
        /// Puts the pixel.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="color">The color.</param>
        public void PutPixel(Vector2 position, Color color)
        {
            if (m_pixel == null)
            {
                CreateThePixel();
            }
            spriteBatch.Draw(m_pixel, position, color);
        }

        /// <summary>
        /// Puts the pixel.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="color">The color.</param>
        public void PutPixel(float x, float y, Color color)
        {
            PutPixel(new Vector2(x, y), color);
        }

    }
}
