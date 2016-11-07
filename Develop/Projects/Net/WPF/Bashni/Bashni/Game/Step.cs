using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Savchin.Core;

namespace Bashni.Game
{
    public enum StepStatus
    {
        New = 0,
        InProgress = 1,
        Stopped = 2,

    }

    public enum StopReason
    {
        None = 0,
        NoMoves = 1,
        FieldFoundEarly = 2,
        ProgressFoundEarly = 3,
        SolutionFoundEarly = 7,
        StepLimit = 4,
        Solved=5,
        Solution=6
    }

    public class Step : IXmlSerializable
    {
        public StepStatus Status { get; set; }
        public StopReason StopReason { get; set; }

        public int Id { get; set; }
        public byte Number { get; set; }
        public byte Progress { get; set; }
        public Step Previous { get; set; }
        public Field Field { get; set; }
        public Movement Move { get; set; }
        public List<Step> Variants { get; set; }

        public IEnumerable<Step> Steps
        {
            get
            {
                return Variants.Concat(Variants.SelectMany(e => e.Steps));
            }

        }
        public Step(Step current, Movement m,int id)
        {
            var after = (Field)current.Field.Clone();
            after.DoMove(m);

            Id = id;
            Field = after;
            Move = m;
            Number = (byte) (current.Number + 1);
            Progress = after.GetProgress();
            Previous = current;
            Variants = new List<Step>();
        }

        public Step()
        {
            Variants = new List<Step>();
        }



        public List<Movement> GetPossibleMoves()
        {
            var result = new List<Movement>();
            for (int columnToMove = 0; columnToMove < Field.Columns; columnToMove++)
            {
                var rowToMove = Field.GetMoveBrick(columnToMove);
                //Empty column
                if (rowToMove == -1) continue;

                var from = new Place(rowToMove, columnToMove);
                var moves=Field.GetPossibleColumns(from);
                result.AddRange(moves);
            }
            return result;
        }

        public override string ToString()
        {
            return string.Format("#{0} St:{1} Pr:{2}  {3}  {4}",Id,Number,  Progress, Status, StopReason );
        }

        #region Implementation of IXmlSerializable

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            Number = Convert.ToByte(reader.GetAttribute("num"));
            Progress = Convert.ToByte(reader.GetAttribute("prog"));
            reader.Read();
            Move = TypeSerializer<Movement>.FromXml(reader);

            var Columns = Convert.ToInt32(reader.GetAttribute("columns"));
            var Rows = Convert.ToInt32(reader.GetAttribute("rows"));

            Field = Field.Create(Rows, Columns);
            Field.ReadXml(reader);

            reader.Read();
            reader.Read();

            reader.ReadToDescendant("Step");
            while (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "Step")
            {
                Variants.Add(TypeSerializer<Step>.FromXml(reader));
                reader.Read();
            }

        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("num", Number.ToString());
            writer.WriteAttributeString("prog", Progress.ToString());
            TypeSerializer<Movement>.ToXml(writer, Move);
            writer.WriteStartElement("Field");
            Field.WriteXml(writer);
            writer.WriteEndElement();

            writer.WriteStartElement("Variants");
            foreach (var step in Variants)
            {
                TypeSerializer<Step>.ToXml(writer, step);
            }

            writer.WriteEndElement();
        }

        #endregion

        public void Stop(StopReason reason)
        {
            StopReason = reason;
            Status = StepStatus.Stopped;
        }
    }
}
