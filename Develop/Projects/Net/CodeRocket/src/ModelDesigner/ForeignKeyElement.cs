using Dundas.Diagramming.Dom;
using Dundas.Diagramming.Dom.Library;
using Dundas.Diagramming.Dom.Searching;
using Savchin.Data.Schema;

namespace ModelDesigner
{

    /// <summary>
    /// ForeignKeyElement
    /// </summary>
    [CustomElementType("ForeignKeyElement", "Dmitry Savchin", "OWL")]
    public class ForeignKeyElement : RoutingConnector
    {
        private ForeignKeySchema schema;

        /// <summary>
        /// Initializes a new instance of the <see cref="ForeignKeyElement"/> class.
        /// </summary>
        public ForeignKeyElement()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ForeignKeyElement"/> class.
        /// </summary>
        /// <param name="schema">The schema.</param>
        public ForeignKeyElement(ForeignKeySchema schema)
        {
            this.schema = schema;


            // this.ControlPoints.Add(new ControlPoint())
            //this.Con
        }
        protected override void OnAddToDiagram(Document document)
        {
            base.OnAddToDiagram(document);
            SearchForType searchForType = new SearchForType(typeof(TableElement));
            SearchForPropertyValue searchForName = new SearchForPropertyValue("Name",typeof(string),schema.PrimaryTable.Name);
            CompositeSearchExpression expression = new CompositeSearchExpression(searchForType, searchForName, LogicalOperator.And);
            TableElement parentTable = (TableElement)document.FindSingleObject(expression);
            if (parentTable == null) return;
            PlugPoint start = new PlugPoint(PlugPointType.Start, ElementPointPositionType.StartPoint);
            start.ConnectedTo = parentTable.ConnectionPoints[0];
            PlugPoints.Add(start);
        }
    }
}
