using System.Collections.Generic;
using System.ComponentModel;
using Castle.Core;
using ChristianMoser.WpfInspector.Services;


namespace CastleBot.Core
{
    public enum ApplicationsInfoState
    {
        Loading,
        Available,
        Error
    }

    public class ManagedApplicationsInfo : INotifyPropertyChanged
    {
        #region Private Members

        private ApplicationsInfoState _state;
        private readonly List<ManagedApplicationInfo> _managedApplicationInfos;

        #endregion

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagedApplicationsInfo"/> class.
        /// </summary>
        public ManagedApplicationsInfo(List<ManagedApplicationInfo> applicationInfoList)
        {
            _managedApplicationInfos = new List<ManagedApplicationInfo>(applicationInfoList);
        }

        #endregion

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        public ApplicationsInfoState State
        {
            get { return _state; }
            set { PropertyChanged.ChangeAndNotify(ref _state, value, this, "State"); }
        }

        /// <summary>
        /// Gets the managed application infos.
        /// </summary>
        /// <value>The managed application infos.</value>
        public List<ManagedApplicationInfo> ManagedApplicationInfos
        {
            get { return _managedApplicationInfos; }
        }

        #region INotifyPropertyChanged Members

        /// <summary>
        /// See <see cref="INotifyPropertyChanged.PropertyChanged" />
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
