using System.Collections.Generic;

namespace Bashni.Game
{
    internal class SimpleSolutionBuilder : SolutionBuilderBase<Step>
    {
        protected override void AddInProgress(Step step)
        {
            StepInProgress.Add(step);
            FindSolution(step);
            StepInProgress.Remove(step);
        }

        protected override int GetId()
        {
            return LastId++;
        }

        protected override bool GetLevelForField(Field field, out byte existsStepNumber)
        {
            return FieldLevels.TryGetValue(field, out existsStepNumber);
        }

        protected override bool GetLevelForProgress(byte progress, out byte num)
        {
            return ProgressLevels.TryGetValue(progress, out num);
        }

        protected override void SetLevelField(Step step)
        {
            FieldLevels[step.Field] = step.Number;
        }

        protected override void SetLevelForProgress(Step step)
        {
            byte oldValue;
            if (ProgressLevels.TryGetValue(step.Progress, out oldValue))
            {
                if (oldValue > step.Number)
                    ProgressLevels[step.Progress] = step.Number;
            }
            else
            {
                ProgressLevels.Add(step.Progress, step.Number);
            }
        }
    }
}
