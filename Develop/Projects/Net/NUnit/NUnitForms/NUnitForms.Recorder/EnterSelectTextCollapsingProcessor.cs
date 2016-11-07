
namespace NUnit.Extensions.Forms.Recorder
{
    ///<summary>
    /// A <see cref="CollapsingProcessor"/> that combines entering and selecting text.
    ///</summary>
    public class EnterSelectTextCollapsingProcessor : CollapsingProcessor
    {
        private const string Enter = "Enter";
        private const string Select = "Select";

        /// <summary>
        /// Returns true if the given actions can be collapsed.
        /// </summary>
        /// <param name="action1">The earlier event to test.</param>
        /// <param name="action2">The latter event to test.</param>
        /// <returns>True if these events can be collapsed; else false.</returns>
        public override bool CanCollapse(EventAction action1, EventAction action2)
        {
            return action1.Control == action2.Control &&
                   action1.MethodName == Enter &&
                   action2.MethodName == Select;
        }
    }
}