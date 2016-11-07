//==============================================================================
//	File:		TableElement.cs
//
//	Namespace:	ModelDesigner
//
//	Classes:	TableElement
//
//	Purpose:	Provides the implementation for TableElement.
//
//==============================================================================
// {project info here}
// {copyright info here}
//==============================================================================

using System.ComponentModel;
using System.Drawing;
using Dundas.Diagramming.Dom;
using Dundas.Diagramming.Dom.Geometry2D;
using Dundas.Diagramming.Dom.Library;
using Dundas.Diagramming.Dom.Measurement;
using Dundas.Diagramming.Serialization;
using Savchin.Data.Schema;

namespace ModelDesigner
{

    #region TableElement

    /// <summary>
    /// The TableElement class provides a new custom element.
    /// </summary>
    [CustomElementType("TableElement", "Dmitry Savchin", "OWL")]
    public class TableElement : ElementRectangle
    {
        #region Fields

        private static readonly MeasurementUnitExtended rounding = new MeasurementUnitExtended(new UnitPercentage(1));
        private static Color backgroundColor = Color.FromArgb(211, 234, 235);

        // MeasurementUnit RowHeight = new MeasurementUnit(0.5, MeasurementUnitType.Inch);
        public MeasurementUnit RowHeight
        {
            get
            {
                Font currentFont = Style.TextProperties.Font;
                return new MeasurementUnit(currentFont.SizeInPoints + 2, MeasurementUnitType.Point);
            }
        }

        private TableSchema schema;

        #endregion // Fields

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public TableElement()
        {
            Style.OutlineProperties.Rounding = rounding;
            Style.FillProperties.Color = backgroundColor;

            PinPoint.PositionType = ElementPointPositionType.TopLeft;
            ConnectionPoints.Add(new ConnectionPoint(ConnectionPointType.InputOutput, ElementPointPositionType.TopRight));
            ConnectionPoints.Add(new ConnectionPoint(ConnectionPointType.InputOutput, ElementPointPositionType.TopCenter));
            ConnectionPoints.Add(new ConnectionPoint(ConnectionPointType.InputOutput, ElementPointPositionType.TopLeft));
            ConnectionPoints.Add(new ConnectionPoint(ConnectionPointType.InputOutput, ElementPointPositionType.BottomCenter));
            ConnectionPoints.Add(new ConnectionPoint(ConnectionPointType.InputOutput, ElementPointPositionType.BottomLeft));
            ConnectionPoints.Add(new ConnectionPoint(ConnectionPointType.InputOutput, ElementPointPositionType.BottomRight));

            //ConnectionPoints.Add(new ConnectionPoint(ConnectionPointType.InputOutput, ElementPointPositionType.Custom));
        }

        public TableElement(TableSchema tableSchema) : this()
        {
            schema = tableSchema;


            //Location= new Point2D(0,0, MeasurementUnitType.Inch);
            //Rendering.MeasureString(engine, this.Text)

            //StringLayoutHelper.MeasureFormattedString(this,
            //                                          new StringLayout(schema.Name, this.Style.TextProperties.Font),
            //                                          1000); 
            //Height = RowHeight.MultiplyValue(schema.Columns.Count + 1);
        }

        #endregion // Constructors

        #region Serialization

        /// <summary>
        /// Deserialization constructor.
        /// </summary>
        /// <param name="archive">
        /// The archive for deserialization.
        /// </param>
        [Description("Constructor.")]
        protected TableElement(StructuredDeserializationArchive archive)
            : base(archive)
        {
        }

        /// <summary>
        /// Serializes the object info part for the element.
        /// </summary>
        /// <param name="archive">
        /// The archive for serialization.
        /// </param>
        [Description("Serializes the object info part for the element.")]
        protected override void SerializeObjectInfo(StructuredSerializationArchive archive)
        {
            // Call the base class.
            base.SerializeObjectInfo(archive);

            // Write version.
            archive.WriteVersion(1, 0, 0, 0);

            // Write data.
            // TODO : write data here using the 'archive' parameter.
            archive.Write(schema.Name);
        }

        /// <summary>
        /// Deserializes the object info part for the element.
        /// </summary>
        /// <param name="archive">
        /// The archive for deserialization.
        /// </param>
        [Description("Deserializes the object info part for the element.")]
        protected override void DeserializeObjectInfo(StructuredDeserializationArchive archive)
        {
            // Call the base class.
            base.DeserializeObjectInfo(archive);

            // Read and validate version.
            int version = archive.ReadVersion(1, 0, 0, 0);

            // Read data.
            // TODO : read data here using the 'archive' parameter.
            string tableName = archive.ReadString();
        }

        #endregion // Serialization

        #region Properties
 
        /// <summary>
        /// Gets the element's capabilities.
        /// </summary>
        /// <value>
        /// An <see cref="T:Dundas.Diagramming.Dom.ElementCapabilities"/> value that represents the
        /// capabilities of this element.
        /// </value>
        /// <remarks>
        /// The purpose of this property is to allow an Element-derived
        /// type to limit access to some standard operations on it.
        /// </remarks>
        public override ElementCapabilities Capabilities
        {
            get
            {
                // Allow CanDoAll but do not allow rotation, flipping in
                // XY direction, editing of text or changing the pin point offset.
                return ElementCapabilities.CanDoAll ^
                       ElementCapabilities.CanFlipXY ^
                       ElementCapabilities.CanEditText ^
                       ElementCapabilities.CanChangePinPointOffset ;
            }
        }

        #endregion // Properties

        #region Indexers

        #endregion // Indexers

        #region Methods

        /// <summary>
        /// Draws the element's shape using specified <b>IRenderEngine</b>
        /// object.
        /// <seealso cref="Draw"/>
        /// </summary>
        /// <param name="engine">
        /// The <see cref="IRenderEngine"/> object that provides the rendering 
        /// infrastructure.
        /// </param>
        protected override void DrawShape(IRenderEngine engine)
        {
            base.DrawShape(engine);

            MeasurementUnit rowHeight = RowHeight;
            Size2D rowSize = new Size2D(Width, RowHeight);
            MeasurementUnit bottom = Location.Y.AddValue(Height);
            MeasurementUnit midPoint = Location.Y.AddValue(rowHeight);
            Rendering.DrawLine(engine,
                               new Point2D(base.Location.X, midPoint),
                               new Point2D(base.Location.X.AddValue(Width), midPoint));

            Point2D lefTop = Location;
            engine.Graphics.TextProperties.HorizontalAlignment = StringAlignment.Center;

            Rendering.DrawString(engine, schema.Name, new Rectangle2D(lefTop, rowSize));
            lefTop.Y = lefTop.Y.AddValue(rowHeight);

            foreach (ColumnSchema column in schema.Columns)
            {
                engine.Graphics.TextProperties.HorizontalAlignment = StringAlignment.Near;
                Rendering.DrawString(engine, column.Name, new Rectangle2D(lefTop, rowSize));
                lefTop.Y = lefTop.Y.AddValue(rowHeight);
                if (lefTop.Y > bottom)
                    break;
            }
           
            //ElementLine line = new ElementLine();
            //line.Size = Width;
        }

        /// <summary>
        /// Copies the internal properties from this object to the specified 
        /// shallow copy of this object.  
        /// </summary>
        /// <param name="shallowCopy">
        /// Shallow copy of this object that has only the data necessary 
        /// for object instantiation.
        /// </param>
        /// <param name="resetNontransferableData">
        /// Boolean <b>true</b> to signal that all internal data that cannot be 
        /// copied safely between two objects has to be reset; otherwise, 
        /// <b>false</b> to keep such internal data intact.
        /// </param>
        protected override void CloneInternals(BaseObject shallowCopy, bool resetNontransferableData)
        {
            // call base type implementation first
            base.CloneInternals(shallowCopy, resetNontransferableData);

            if (!(shallowCopy is TableElement))
            {
                return;
            }

            // TODO : copy custom data
            TableElement tableElement = (TableElement) shallowCopy;
            tableElement.schema = schema;
        }

        #endregion // Methods

        #region Events

        #endregion // Events
    }

    #endregion // Table
}