using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Bashni.Controls;
using Bashni.Core;
using Bashni.Game;
using Savchin.Wpf.Input;

namespace Bashni.Views
{
    public class Navigator : ObjectBase
    {
        public ICommand GoBackCommand { get; private set; }
        public ICommand GoForwardCommand { get; private set; }
        public ICommand CodeCommand { get; private set; }

        public ObservableCollection<int> Variants { get; private set; }
        public StepsCollection Steps { get; private set; }

        private Step _currentStep;

        public Step Root { get; set; }

        public Step CurrentStep
        {
            get { return _currentStep; }
            set
            {
                if (_currentStep == value) return;
                _currentStep = value;
                if (_currentStep != null)
                    SetVariants();
                OnPropertyChanged("CurrentStep");
            }
        }

        private int _selectedVariant;
        public int SelectedVariant
        {
            get { return _selectedVariant; }
            set
            {
                if (_selectedVariant == value) return;
                _selectedVariant = value;
                OnPropertyChanged("SelectedVariant");
            }
        }

        public Navigator()
        {
            Variants = new ObservableCollection<int>();
            Steps = new StepsCollection();

            GoBackCommand = new DelegateCommand(OnGoBack);
            GoForwardCommand = new DelegateCommand(OnForward);
            CodeCommand = new DelegateCommand<int>(OnCode);
        }



        private void OnCode(int id)
        {
            var step = Root.Steps.FirstOrDefault(s => s.Id == id);
            if (step != null)
            {
                CurrentStep = step;
            }
        }

        private void OnForward()
        {
            int i = SelectedVariant;
            if (CurrentStep == null || CurrentStep.Variants == null || i >= CurrentStep.Variants.Count) return;
            CurrentStep = CurrentStep.Variants[i];
        }

        private void OnGoBack()
        {
            if (CurrentStep == null || CurrentStep.Previous == null) return;
            CurrentStep = CurrentStep.Previous;
        }

        private void SetVariants()
        {
            Variants.Clear();
            foreach (var n in Enumerable.Range(0, CurrentStep.Variants.Count))
            {
                Variants.Add(n);
            }

        }
    }
}
