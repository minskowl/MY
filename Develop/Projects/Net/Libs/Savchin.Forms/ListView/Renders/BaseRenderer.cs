using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Savchin.Forms.ListView
{
    /// <summary>
    /// Renderers are responsible for drawing a single cell within an owner drawn ObjectListView.
    /// </summary>
    /// <remarks>
    /// <para>Methods on this class are called during the DrawItem or DrawSubItemEvent.
    /// Subclasses can tell which type of event they are handling by examining DrawItemEvent: if this
    /// is not null, it is a DrawItem event.</para>
    /// <para>Subclasses will normally override the RenderWithDefault or Render method, and use the other
    /// methods as helper functions.</para>
    /// <para>If a renderer is installed on the primary column (column 0), it will be given a chance
    /// to draw the whole item in all views (Details, Tile, etc.). If the renderer returns true,
    /// default processing will continue. If it returns false, no other rendering will happen.</para>
    /// <para>This means that when an ObjectListView is in Details view, the renderer on column 0
    /// will be called twice: once to handle the DrawItem event, and then again to draw only the
    /// first cell. Subclasses must distinguish between these two very different events (using
    /// the "this.DrawItemEvent == null" test).</para>
    /// </remarks>
    [Browsable(false)]
    public class BaseRenderer
    {
        #region Properties

        private Object aspect;
        private OLVColumn column;
        private Font font;
        private bool isDrawBackground = true;
        private ListViewItem.ListViewSubItem listSubItem;
        private Object rowObject;
        private int spacing = 1;
        private Brush textBrush;

        /// <summary>
        /// Get/set the event that caused this renderer to be called
        /// </summary>
        public DrawListViewSubItemEventArgs Event { get; set; }

        /// <summary>
        /// Get/set the event that caused this renderer to be called
        /// </summary>
        public DrawListViewItemEventArgs DrawItemEvent { get; set; }

        /// <summary>
        /// Get/set the listview for which the drawing is to be done
        /// </summary>
        public ObjectListView ListView { get; set; }

        /// <summary>
        /// Get or set the OLVColumn that this renderer will draw
        /// </summary>
        public OLVColumn Column
        {
            get { return column; }
            set { column = value; }
        }

        /// <summary>
        /// Get or set the model object that this renderer should draw
        /// </summary>
        public Object RowObject
        {
            get { return rowObject; }
            set { rowObject = value; }
        }

        /// <summary>
        /// Get or set the aspect of the model object that this renderer should draw
        /// </summary>
        public Object Aspect
        {
            get
            {
                if (aspect == null)
                    aspect = column.GetValue(rowObject);
                return aspect;
            }
            set { aspect = value; }
        }

        /// <summary>
        /// Get or set the listitem that this renderer will be drawing
        /// </summary>
        public OLVListItem ListItem { get; set; }

        /// <summary>
        /// Get or set the list subitem that this renderer will be drawing
        /// </summary>
        public ListViewItem.ListViewSubItem SubItem
        {
            get { return listSubItem; }
            set { listSubItem = value; }
        }

        /// <summary>
        /// Get the specialized OLVSubItem that this renderer is drawing
        /// </summary>
        /// <remarks>This returns null for column 0.</remarks>
        public OLVListSubItem OLVSubItem
        {
            get { return listSubItem as OLVListSubItem; }
        }

        /// <summary>
        /// Cache whether or not our item is selected
        /// </summary>
        public bool IsItemSelected { get; set; }


        /// <summary>
        /// Return the font to be used for text in this cell
        /// </summary>
        /// <returns>The font of the subitem</returns>
        public Font Font
        {
            get
            {
                if (font == null)
                {
                    if (SubItem == null || ListItem.UseItemStyleForSubItems)
                        return ListItem.Font;
                    else
                        return SubItem.Font;
                }
                else
                    return font;
            }
            set { font = value; }
        }

        /// <summary>
        /// The brush that will be used to paint the text
        /// </summary>
        public Brush TextBrush
        {
            get
            {
                if (textBrush == null)
                    return new SolidBrush(GetForegroundColor());
                else
                    return textBrush;
            }
            set { textBrush = value; }
        }

        /// <summary>
        /// Should this renderer fill in the background before drawing?
        /// </summary>
        public bool IsDrawBackground
        {
            get { return isDrawBackground; }
            set { isDrawBackground = value; }
        }

        /// <summary>
        /// Can the renderer wrap lines that do not fit completely within the cell?
        /// </summary>
        public bool CanWrap { get; set; }

        /// <summary>
        /// When rendering multiple images, how many pixels should be between each image?
        /// </summary>
        public int Spacing
        {
            get { return spacing; }
            set { spacing = value; }
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Return the string that should be drawn within this
        /// </summary>
        /// <returns></returns>
        public string GetText()
        {
            if (SubItem == null)
                return ListItem.Text;
            else
                return SubItem.Text;
        }

        /// <summary>
        /// Return the image that should be drawn against this subitem
        /// </summary>
        /// <returns>An Image or null if no image should be drawn.</returns>
        public Image GetImage()
        {
            if (Column.Index == 0)
                return GetImage(ListItem.ImageSelector);
            else
                return GetImage(OLVSubItem.ImageSelector);
        }

        /// <summary>
        /// Return the actual image that should be drawn when keyed by the given image selector.
        /// An image selector can be: <list>
        /// <item>an int, giving the index into the image list</item>
        /// <item>a string, giving the image key into the image list</item>
        /// <item>an Image, being the image itself</item>
        /// </list>
        /// </summary>
        /// <param name="imageSelector">The value that indicates the image to be used</param>
        /// <returns>An Image or null</returns>
        public Image GetImage(Object imageSelector)
        {
            if (imageSelector == null || imageSelector == DBNull.Value)
                return null;

            ImageList il = ListView.BaseSmallImageList;
            if (il != null)
            {
                if (imageSelector is Int32)
                {
                    var index = (Int32) imageSelector;
                    if (index < 0 || index >= il.Images.Count)
                        return null;
                    else
                        return il.Images[index];
                }

                var str = imageSelector as String;
                if (str != null)
                {
                    if (il.Images.ContainsKey(str))
                        return il.Images[str];
                    else
                        return null;
                }
            }

            return imageSelector as Image;
        }

        /// <summary>
        /// Return the Color that is the background color for this item's cell
        /// </summary>
        /// <returns>The background color of the subitem</returns>
        public Color GetBackgroundColor()
        {
            if (IsItemSelected && ListView.FullRowSelect)
            {
                if (ListView.Focused)
                    return ListView.HighlightBackgroundColorOrDefault;
                else if (!ListView.HideSelection)
                    return SystemColors.Control; //TODO: What color should this be?
            }
            if (SubItem == null || ListItem.UseItemStyleForSubItems)
                return ListItem.BackColor;
            else
                return SubItem.BackColor;
        }

        /// <summary>
        /// Return the Color that is the background color for this item's text
        /// </summary>
        /// <returns>The background color of the subitem's text</returns>
        protected Color GetTextBackgroundColor()
        {
            if (IsItemSelected && (Column.Index == 0 || ListView.FullRowSelect))
                return ListView.HighlightBackgroundColorOrDefault;
            else if (SubItem == null || ListItem.UseItemStyleForSubItems)
                return ListItem.BackColor;
            else
                return SubItem.BackColor;
        }

        /// <summary>
        /// Return the color to be used for text in this cell
        /// </summary>
        /// <returns>The text color of the subitem</returns>
        protected Color GetForegroundColor()
        {
            if (IsItemSelected && (Column.Index == 0 || ListView.FullRowSelect))
            {
                if (ListView.Focused)
                    return ListView.HighlightForegroundColorOrDefault;
                else if (!ListView.HideSelection)
                    return SystemColors.ControlText; //TODO: What color should this be?
            }
            if (SubItem == null || ListItem.UseItemStyleForSubItems)
                return ListItem.ForeColor;
            else
                return SubItem.ForeColor;
        }


        /// <summary>
        /// Align the second rectangle with the first rectangle,
        /// according to the alignment of the column
        /// </summary>
        /// <param name="outer">The cell's bounds</param>
        /// <param name="inner">The rectangle to be aligned within the bounds</param>
        /// <returns>An aligned rectangle</returns>
        protected Rectangle AlignRectangle(Rectangle outer, Rectangle inner)
        {
            var r = new Rectangle(outer.Location, inner.Size);

            // Centre horizontally depending on the column alignment
            if (inner.Width < outer.Width)
            {
                switch (Column.TextAlign)
                {
                    case HorizontalAlignment.Left:
                        r.X = outer.Left;
                        break;
                    case HorizontalAlignment.Center:
                        r.X = outer.Left + ((outer.Width - inner.Width)/2);
                        break;
                    case HorizontalAlignment.Right:
                        r.X = outer.Right - inner.Width - 1;
                        break;
                }
            }
            // Centre vertically too
            if (inner.Height < outer.Height)
                r.Y = outer.Top + ((outer.Height - inner.Height)/2);

            return r;
        }

        /// <summary>
        /// Draw the given image aligned horizontally within the column.
        /// </summary>
        /// <remarks>
        /// Over tall images are scaled to fit. Over-wide images are
        /// truncated. This is by design!
        /// </remarks>
        /// <param name="g">Graphics context to use for drawing</param>
        /// <param name="r">Bounds of the cell</param>
        /// <param name="image">The image to be drawn</param>
        protected void DrawAlignedImage(Graphics g, Rectangle r, Image image)
        {
            if (image == null)
                return;

            // By default, the image goes in the top left of the rectangle
            var imageBounds = new Rectangle(r.Location, image.Size);

            // If the image is too tall to be drawn in the space provided, proportionally scale it down.
            // Too wide images are not scaled.
            if (image.Height > r.Height)
            {
                float scaleRatio = r.Height/(float) image.Height;
                imageBounds.Width = (int) (image.Width*scaleRatio);
                imageBounds.Height = r.Height - 1;
            }

            // Align and draw our (possibly scaled) image
            g.DrawImage(image, AlignRectangle(r, imageBounds));
        }

        /// <summary>
        /// Fill in the background of this cell
        /// </summary>
        /// <param name="g">Graphics context to use for drawing</param>
        /// <param name="r">Bounds of the cell</param>
        protected void DrawBackground(Graphics g, Rectangle r)
        {
            if (IsDrawBackground)
            {
                using (Brush brush = new SolidBrush(GetBackgroundColor()))
                {
                    g.FillRectangle(brush, r);
                }
            }
        }

        #endregion

        /// <summary>
        /// The delegate that is called from the list view. This is the main entry point, but
        /// subclasses should override Render instead of this method.
        /// </summary>
        /// <param name="e">The event that caused this redraw</param>
        /// <param name="g">The context that our drawing should be done using</param>
        /// <param name="r">The bounds of the cell within which the renderer can draw</param>
        /// <param name="rowObject">The model object for this row</param>
        /// <returns>A boolean indicating whether the default process should occur</returns>
        public bool HandleRendering(EventArgs e, Graphics g, Rectangle r, Object rowObject)
        {
            ListView = (ObjectListView) Column.ListView;
            if (e is DrawListViewSubItemEventArgs)
            {
                Event = (DrawListViewSubItemEventArgs) e;
                ListItem = Event.Item as OLVListItem;
                SubItem = Event.SubItem;
                Column = ListView.GetColumn(Event.ColumnIndex);
            }
            else
            {
                DrawItemEvent = (DrawListViewItemEventArgs) e;
                ListItem = DrawItemEvent.Item as OLVListItem;
                SubItem = null;
                Column = ListView.GetColumn(0);
            }
            RowObject = rowObject;
            Aspect = null; // uncache previous result
            IsItemSelected = ListItem.Selected;
            // ((e.ItemState & ListViewItemStates.Selected) == ListViewItemStates.Selected);
            IsDrawBackground = true;
            Font = null;
            TextBrush = null;
            return OptionalRender(g, r);
        }

        /// <summary>
        /// Draw our data into the given rectangle using the given graphics context.
        /// </summary>
        /// <remarks>
        /// <para>Subclasses should override this method.</para></remarks>
        /// <param name="g">The graphics context that should be used for drawing</param>
        /// <param name="r">The bounds of the subitem cell</param>
        /// <returns>Returns whether the renderering has already taken place.
        /// If this returns false, the default processing will take over.
        /// </returns>
        public virtual bool OptionalRender(Graphics g, Rectangle r)
        {
            Render(g, r);
            return true;
        }

        /// <summary>
        /// Draw our data into the given rectangle using the given graphics context.
        /// </summary>
        /// <remarks>
        /// <para>Subclasses should override this method if they never want
        /// to fall back on the default processing</para></remarks>
        /// <param name="g">The graphics context that should be used for drawing</param>
        /// <param name="r">The bounds of the subitem cell</param>
        public virtual void Render(Graphics g, Rectangle r)
        {
            DrawBackground(g, r);

            // Adjust the rectangle to match the padding used by the native mode of the ListView
            Rectangle r2 = r;
            r2.X += 4;
            r2.Width -= 4;
            DrawImageAndText(g, r2);
        }

        /// <summary>
        /// Draw our subitems image and text
        /// </summary>
        /// <param name="g">Graphics context to use for drawing</param>
        /// <param name="r">Bounds of the cell</param>
        protected void DrawImageAndText(Graphics g, Rectangle r)
        {
            if (ListView.CheckBoxes && Column.Index == 0)
            {
                int spaceUsed = DrawCheckBox(g, r);
                r.X += spaceUsed;
                r.Width -= spaceUsed;
            }

            DrawImageAndText(g, r, GetText(), GetImage());
        }

        /// <summary>
        /// Draw the check box of this row
        /// </summary>
        /// <param name="g">Graphics context to use for drawing</param>
        /// <param name="r">Bounds of the cell</param>
        protected int DrawCheckBox(Graphics g, Rectangle r)
        {
            ImageList il = ListView.StateImageList;
            if (il == null || ListItem.StateImageIndex < 0)
                return 0;
            try
            {
                Image image = il.Images[ListItem.StateImageIndex];
                Point pt = r.Location;
                pt.Offset(2, (r.Height - il.ImageSize.Height)/2);
                g.DrawImage(image, pt);
            }
            catch (ArgumentOutOfRangeException)
            {
                return 0;
            }
            return il.ImageSize.Width + 4;
        }

        /// <summary>
        /// Draw the given text and optional image in the "normal" fashion
        /// </summary>
        /// <param name="g">Graphics context to use for drawing</param>
        /// <param name="r">Bounds of the cell</param>
        /// <param name="txt">The string to be drawn</param>
        /// <param name="image">The optional image to be drawn</param>
        protected void DrawImageAndText(Graphics g, Rectangle r, String txt, Image image)
        {
            // Draw the image
            if (image != null)
            {
                int top = r.Y;
                if (image.Size.Height < r.Height)
                    top += ((r.Height - image.Size.Height)/2);

                g.DrawImageUnscaled(image, r.X, top);
                r.X += image.Width + 2;
                r.Width -= image.Width + 2;
            }

            var fmt = new StringFormat();
            fmt.LineAlignment = StringAlignment.Center;
            fmt.Trimming = StringTrimming.EllipsisCharacter;
            if (!CanWrap)
                fmt.FormatFlags = StringFormatFlags.NoWrap;
            switch (Column.TextAlign)
            {
                case HorizontalAlignment.Center:
                    fmt.Alignment = StringAlignment.Center;
                    break;
                case HorizontalAlignment.Left:
                    fmt.Alignment = StringAlignment.Near;
                    break;
                case HorizontalAlignment.Right:
                    fmt.Alignment = StringAlignment.Far;
                    break;
            }

            // Draw the background of the text as selected, if it's the primary column
            // and it's selected and it's not in FullRowSelect mode.
            if (IsDrawBackground && IsItemSelected && Column.Index == 0 && !ListView.FullRowSelect)
            {
                SizeF size = g.MeasureString(txt, Font, r.Width, fmt);
                // This is a tighter selection box
                //Rectangle r2 = this.AlignRectangle(r, new Rectangle(0, 0, (int)(size.Width + 1), (int)(size.Height + 1)));
                Rectangle r2 = r;
                r2.Width = (int) size.Width + 1;
                using (Brush brush = new SolidBrush(ListView.HighlightBackgroundColorOrDefault))
                    g.FillRectangle(brush, r2);
            }

            RectangleF rf = r;
            g.DrawString(txt, Font, TextBrush, rf, fmt);

            // We should put a focus rectange around the column 0 text if it's selected --
            // but we don't because:
            // - I really dislike this UI convention
            // - we are using buffered graphics, so the DrawFocusRecatangle method of the event doesn't work

            //if (this.Column.Index == 0) {
            //    Size size = TextRenderer.MeasureText(this.SubItem.Text, this.ListView.ListFont);
            //    if (r.Width > size.Width)
            //        r.Width = size.Width;
            //    this.Event.DrawFocusRectangle(r);
            //}
        }
    }
}