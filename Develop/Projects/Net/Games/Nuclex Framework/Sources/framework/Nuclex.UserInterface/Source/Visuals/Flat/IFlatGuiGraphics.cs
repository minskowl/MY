#region CPL License
/*
Nuclex Framework
Copyright (C) 2002-2010 Nuclex Development Labs

This library is free software; you can redistribute it and/or
modify it under the terms of the IBM Common Public License as
published by the IBM Corporation; either version 1.0 of the
License, or (at your option) any later version.

This library is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
IBM Common Public License for more details.

You should have received a copy of the IBM Common Public
License along with this library
*/
#endregion

using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace Nuclex.UserInterface.Visuals.Flat {

  /// <summary>Provides drawing methods for GUI controls</summary>
  /// <remarks>
  ///   Analogous to System.Drawing.Graphics, but provides specialized methods for
  ///   drawing a GUI with a dynamic, switchable theme.
  /// </remarks>
  public interface IFlatGuiGraphics {

    /// <summary>Sets the clipping region for any future drawing commands</summary>
    /// <param name="clipRegion">Clipping region that will be set</param>
    /// <returns>
    ///   An object that will unset the clipping region upon its destruction.
    /// </returns>
    /// <remarks>
    ///   Clipping regions can be stacked, though this is not very typical for
    ///   a game GUI and also not recommended practice due to performance constraints.
    ///   Unless clipping is implemented in software, setting up a clip region
    ///   on current hardware requires the drawing queue to be flushed, negatively
    ///   impacting rendering performance (in technical terms, a clipping region
    ///   change likely causes 2 more DrawPrimitive() calls from the painter).
    /// </remarks>
    IDisposable SetClipRegion(RectangleF clipRegion);

    /// <summary>Draws a GUI element onto the drawing buffer</summary>
    /// <param name="frameName">Class of the element to draw</param>
    /// <param name="bounds">Region that will be covered by the drawn element</param>
    /// <remarks>
    ///   <para>
    ///     GUI elements are the basic building blocks of a GUI: 
    ///   </para>
    /// </remarks>
    void DrawElement(string frameName, RectangleF bounds);

    /// <summary>Draws text into the drawing buffer for the specified element</summary>
    /// <param name="frameName">Class of the element for which to draw text</param>
    /// <param name="bounds">Region that will be covered by the drawn element</param>
    /// <param name="text">Text that will be drawn</param>
    void DrawString(string frameName, RectangleF bounds, string text);

    /// <summary>Draws a caret for text input at the specified index</summary>
    /// <param name="frameName">Class of the element for which to draw a caret</param>
    /// <param name="bounds">Region that will be covered by the drawn element</param>
    /// <param name="text">Text for which a caret will be drawn</param>
    /// <param name="index">Index the caret will be drawn at</param>
    void DrawCaret(string frameName, RectangleF bounds, string text, int index);

    /// <summary>Measures the extents of a string in the frame's area</summary>
    /// <param name="frameName">Class of the element whose text will be measured</param>
    /// <param name="bounds">Region that will be covered by the drawn element</param>
    /// <param name="text">Text that will be measured</param>
    /// <returns>
    ///   The size and extents of the specified string within the frame
    /// </returns>
    RectangleF MeasureString(string frameName, RectangleF bounds, string text);

    /// <summary>
    ///   Locates the closest gap between two letters to the provided position
    /// </summary>
    /// <param name="frameName">Class of the element in which to find the gap</param>
    /// <param name="bounds">Region that will be covered by the drawn element</param>
    /// <param name="text">Text in which the closest gap will be found</param>
    /// <param name="position">Position of which to determien the closest gap</param>
    /// <returns>The index of the gap the position is closest to</returns>
    int GetClosestOpening(
      string frameName, RectangleF bounds, string text, Vector2 position
    );


    /// <summary>
    /// Draws the arc.
    /// </summary>
    /// <param name="center">The center.</param>
    /// <param name="radius">The radius.</param>
    /// <param name="sides">The sides.</param>
    /// <param name="startingAngle">The starting angle.</param>
    /// <param name="degrees">The degrees.</param>
    /// <param name="color">The color.</param>
    void DrawArc(Vector2 center, float radius, int sides, float startingAngle, float degrees, Color color);
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
    void DrawArc(Vector2 center, float radius, int sides, float startingAngle, float degrees, Color color, float thickness);
    /// <summary>
    /// Draws the circle.
    /// </summary>
    /// <param name="center">The center.</param>
    /// <param name="radius">The radius.</param>
    /// <param name="sides">The sides.</param>
    /// <param name="color">The color.</param>
    void DrawCircle(Vector2 center, float radius, int sides, Color color);
    /// <summary>
    /// Draws the circle.
    /// </summary>
    /// <param name="center">The center.</param>
    /// <param name="radius">The radius.</param>
    /// <param name="sides">The sides.</param>
    /// <param name="color">The color.</param>
    /// <param name="thickness">The thickness.</param>
    void DrawCircle(Vector2 center, float radius, int sides, Color color, float thickness);
    /// <summary>
    /// Draws the line.
    /// </summary>
    /// <param name="point1">The point1.</param>
    /// <param name="point2">The point2.</param>
    /// <param name="color">The color.</param>
    void DrawLine(Vector2 point1, Vector2 point2, Color color);
    /// <summary>
    /// Draws the line.
    /// </summary>
    /// <param name="point1">The point1.</param>
    /// <param name="point2">The point2.</param>
    /// <param name="color">The color.</param>
    /// <param name="thickness">The thickness.</param>
    void DrawLine(Vector2 point1, Vector2 point2, Color color, float thickness);
    /// <summary>
    /// Draws the line.
    /// </summary>
    /// <param name="point">The point.</param>
    /// <param name="length">The length.</param>
    /// <param name="angle">The angle.</param>
    /// <param name="color">The color.</param>
    void DrawLine(Vector2 point, float length, float angle, Color color);
    /// <summary>
    /// Draws the line.
    /// </summary>
    /// <param name="point">The point.</param>
    /// <param name="length">The length.</param>
    /// <param name="angle">The angle.</param>
    /// <param name="color">The color.</param>
    /// <param name="thickness">The thickness.</param>
    void DrawLine(Vector2 point, float length, float angle, Color color, float thickness);
    /// <summary>
    /// Draws the line.
    /// </summary>
    /// <param name="x1">The x1.</param>
    /// <param name="y1">The y1.</param>
    /// <param name="x2">The x2.</param>
    /// <param name="y2">The y2.</param>
    /// <param name="color">The color.</param>
    void DrawLine(float x1, float y1, float x2, float y2, Color color);
    /// <summary>
    /// Draws the line.
    /// </summary>
    /// <param name="x1">The x1.</param>
    /// <param name="y1">The y1.</param>
    /// <param name="x2">The x2.</param>
    /// <param name="y2">The y2.</param>
    /// <param name="color">The color.</param>
    /// <param name="thickness">The thickness.</param>
    void DrawLine(float x1, float y1, float x2, float y2, Color color, float thickness);
    /// <summary>
    /// Draws the rectangle.
    /// </summary>
    /// <param name="rect">The rect.</param>
    /// <param name="color">The color.</param>
    void DrawRectangle(Rectangle rect, Color color);
    /// <summary>
    /// Draws the rectangle.
    /// </summary>
    /// <param name="rect">The rect.</param>
    /// <param name="color">The color.</param>
    /// <param name="thickness">The thickness.</param>
    void DrawRectangle(Rectangle rect, Color color, float thickness);
    /// <summary>
    /// Draws the rectangle.
    /// </summary>
    /// <param name="location">The location.</param>
    /// <param name="size">The size.</param>
    /// <param name="color">The color.</param>
    void DrawRectangle(Vector2 location, Vector2 size, Color color);
    /// <summary>
    /// Draws the rectangle.
    /// </summary>
    /// <param name="rect">The rect.</param>
    /// <param name="color">The color.</param>
    /// <param name="thickness">The thickness.</param>
    /// <param name="angle">The angle.</param>
    void DrawRectangle(Rectangle rect, Color color, float thickness, float angle);
    /// <summary>
    /// Draws the rectangle.
    /// </summary>
    /// <param name="location">The location.</param>
    /// <param name="size">The size.</param>
    /// <param name="color">The color.</param>
    /// <param name="thickness">The thickness.</param>
    void DrawRectangle(Vector2 location, Vector2 size, Color color, float thickness);
    /// <summary>
    /// Draws the rectangle.
    /// </summary>
    /// <param name="rect">The rect.</param>
    /// <param name="color">The color.</param>
    /// <param name="thickness">The thickness.</param>
    /// <param name="angle">The angle.</param>
    /// <param name="rotateAround">The rotate around.</param>
    void DrawRectangle(Rectangle rect, Color color, float thickness, float angle, Vector2 rotateAround);
    /// <summary>
    /// Draws the rectangle.
    /// </summary>
    /// <param name="location">The location.</param>
    /// <param name="size">The size.</param>
    /// <param name="color">The color.</param>
    /// <param name="thickness">The thickness.</param>
    /// <param name="angle">The angle.</param>
    void DrawRectangle(Vector2 location, Vector2 size, Color color, float thickness, float angle);
    /// <summary>
    /// Draws the rectangle.
    /// </summary>
    /// <param name="location">The location.</param>
    /// <param name="size">The size.</param>
    /// <param name="color">The color.</param>
    /// <param name="thickness">The thickness.</param>
    /// <param name="angle">The angle.</param>
    /// <param name="rotateAround">The rotate around.</param>
    void DrawRectangle(Vector2 location, Vector2 size, Color color, float thickness, float angle, Vector2 rotateAround);
    /// <summary>
    /// Fills the rectangle.
    /// </summary>
    /// <param name="rect">The rect.</param>
    /// <param name="color">The color.</param>
    void FillRectangle(Rectangle rect, Color color);
    /// <summary>
    /// Fills the rectangle.
    /// </summary>
    /// <param name="rect">The rect.</param>
    /// <param name="color">The color.</param>
    /// <param name="angle">The angle.</param>
    void FillRectangle(Rectangle rect, Color color, float angle);
    /// <summary>
    /// Fills the rectangle.
    /// </summary>
    /// <param name="location">The location.</param>
    /// <param name="size">The size.</param>
    /// <param name="color">The color.</param>
    void FillRectangle(Vector2 location, Vector2 size, Color color);
    /// <summary>
    /// Fills the rectangle.
    /// </summary>
    /// <param name="location">The location.</param>
    /// <param name="size">The size.</param>
    /// <param name="color">The color.</param>
    /// <param name="angle">The angle.</param>
    void FillRectangle(Vector2 location, Vector2 size, Color color, float angle);
    /// <summary>
    /// Fills the rectangle.
    /// </summary>
    /// <param name="x1">The x1.</param>
    /// <param name="y1">The y1.</param>
    /// <param name="x2">The x2.</param>
    /// <param name="y2">The y2.</param>
    /// <param name="color">The color.</param>
    void FillRectangle(float x1, float y1, float x2, float y2, Color color);
    /// <summary>
    /// Fills the rectangle.
    /// </summary>
    /// <param name="x1">The x1.</param>
    /// <param name="y1">The y1.</param>
    /// <param name="x2">The x2.</param>
    /// <param name="y2">The y2.</param>
    /// <param name="color">The color.</param>
    /// <param name="thickness">The thickness.</param>
    void FillRectangle(float x1, float y1, float x2, float y2, Color color, float thickness);
    /// <summary>
    /// Puts the pixel.
    /// </summary>
    /// <param name="position">The position.</param>
    /// <param name="color">The color.</param>
    void PutPixel(Vector2 position, Color color);
    /// <summary>
    /// Puts the pixel.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <param name="color">The color.</param>
    void PutPixel(float x, float y, Color color);

  }

} // namespace Nuclex.UserInterface.Visuals.Flat
