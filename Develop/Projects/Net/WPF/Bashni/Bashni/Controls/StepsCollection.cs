using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Bashni.Game;

namespace Bashni.Controls
{
    public class StepsCollection : INotifyPropertyChanged
    {
        private ObservableCollection<StepView> allKeys;
        private int dataItemsCount;

        public ObservableCollection<StepView> AllKeys
        {
            get { return allKeys; }
        }

        public int DataItemsCount
        {
            get
            {
                return dataItemsCount;
            }
            set
            {
                dataItemsCount = value;
                OnPropertyChanged("DataItemsCount");
            }
        }

        public StepsCollection()
        {
            this.allKeys = new ObservableCollection<StepView>();

        }
        /// <summary>
        /// Inits the specified step.
        /// </summary>
        /// <param name="step">The step.</param>
        public void Init(Step  step)
        {
            AllKeys.Clear();
            AddNew(step);
            DataItemsCount = 1;
        }

        public void AddNew(Step s)
        {
            StepView newKeyHolder = new StepView(s);
            newKeyHolder.PropertyChanged += new PropertyChangedEventHandler(KeyHolder_PropertyChanged);
            this.allKeys.Add(newKeyHolder);
        }

        public void PopulateSubKeys(StepView parent)
        {
            int indexParentKey = this.allKeys.IndexOf(parent);
            if (indexParentKey == this.allKeys.Count - 1 || this.allKeys[indexParentKey + 1].Level <= parent.Level)
            {

                for (int i = 0; i < parent.Key.Variants.Count; i++)
                {
                    StepView childKeyHolder = new StepView(parent.Key.Variants[i]);
                    childKeyHolder.PropertyChanged += KeyHolder_PropertyChanged;
                    allKeys.Insert(indexParentKey + i + 1, childKeyHolder);
                    this.DataItemsCount++;
                }
            }
        }
        public StepView Expand(Step node)
        {
            if (node.Previous != null)
                Expand(node.Previous);

            var view = FindInTree(node);
            if (view != null)
                view.IsExpanded = true;

            return view;
        }
        void KeyHolder_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsExpanded")
            {
                StepView keyHolder = (StepView)sender;
                if (keyHolder.IsExpanded)
                {
                    this.PopulateSubKeys(keyHolder);
                }
                else
                {
                    this.ClearSubKeys(keyHolder);
                }
            }
        }

        public void ClearSubKeys(StepView parentKeyHolder)
        {
            int indexToRemove = this.allKeys.IndexOf(parentKeyHolder) + 1;
            while ((indexToRemove < this.allKeys.Count) && (this.allKeys[indexToRemove].Level > parentKeyHolder.Level))
            {
                this.allKeys.RemoveAt(indexToRemove);
                this.DataItemsCount--;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }




        private StepView FindInTree(Step step)
        {
            return AllKeys.FirstOrDefault(i => i.Key.Id == step.Id);
        }

    }
}