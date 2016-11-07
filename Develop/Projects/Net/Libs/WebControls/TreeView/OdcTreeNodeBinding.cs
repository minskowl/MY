using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.ComponentModel;

namespace Savchin.Web.UI.TreeView
{
    /// <summary>
    /// Specifies Bindings and/or default values for OdcTreeNodes, optionally with a specific Level.
    /// </summary>
    public sealed class OdcTreeNodeBinding :Object, IStateManager, IDataSourceViewSchemaAccessor
    {
        private StateBag viewState;

        /// <summary>
        /// Gets the state of the view.
        /// </summary>
        /// <value>The state of the view.</value>
        public StateBag ViewState
        {
            get
            {
                if (this.viewState == null)
                {
                    this.viewState = new StateBag();
                    if (this.isTrackingViewState)
                    {
                        ((IStateManager)this.viewState).TrackViewState();
                    }
                }
                return this.viewState;
            }
        }

        #region IStateManager Members

        bool isTrackingViewState = false;
        bool IStateManager.IsTrackingViewState
        {
            get { return isTrackingViewState; }
        }

        void IStateManager.LoadViewState(object state)
        {
            if (state != null)
            {
                ((IStateManager)ViewState).LoadViewState(state);
            }
        }

        object IStateManager.SaveViewState()
        {
            if (viewState != null)
            {
                return ((IStateManager)viewState).SaveViewState();
            }
            return null;
        }

        void IStateManager.TrackViewState()
        {
            isTrackingViewState = true;
            if (viewState != null)
            {
                ((IStateManager)viewState).TrackViewState();
            }
        }

        #endregion

        #region IDataSourceViewSchemaAccessor Members

        object IDataSourceViewSchemaAccessor.DataSourceViewSchema
        {
            get
            {
                return ViewState["DataSourceViewSchema"];
            }
            set
            {
                ViewState["DataSourceViewSchema"] = value;
            }
        }

        #endregion

        /// <summary>
        /// Gets or sets the property name for the Text property binding of the OdcTreeNode.
        /// </summary>
        public string TextField
        {
            get
            {
                object o = ViewState["TextField"];
                return o != null ? (string)o : null;
            }
            set
            {
                ViewState["TextField"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the property name for the Value property binding of the OdcTreeNode.
        /// </summary>
        public string ValueField
        {
            get
            {
                object o = ViewState["ValueField"];
                return o != null ? (string)o : null;
            }
            set
            {
                ViewState["ValueField"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the level for which this TreeNodeBinding is specified. If left empty, the bindings are level independend.
        /// Unless there is another TreeNodeBinding that has the exact level specified, the first TreeNodeBinding without a Level is used as default.
        /// </summary>
        public int? Level
        {
            get
            {
                object o = ViewState["Level"];
                return o != null ? (int?)o : null;
            }
            set
            {
                ViewState["Level"] = value;
            }
        }


        /// <summary>
        /// Gets or sets the property name for the Checked property binding of the OdcTreeNode.
        /// </summary>
        public string CheckedField
        {
            get
            {
                object o = ViewState["CheckedName"];
                return o != null ? (string)o : null;
            }
            set
            {
                ViewState["CheckedName"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the default value for the IsChecked property of the OdcTreeNode with the specified Level.
        /// </summary>
        public bool? IsChecked
        {
            get
            {
                object o = ViewState["Checked"];
                return o != null ? (bool?)o : null;
            }
            set
            {
                ViewState["Checked"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the default value for the ShowCheckBox property of the OdcTreeNode with the specified Level.
        /// </summary>
        public bool? ShowCheckBox
        {
            get
            {
                object o = ViewState["ShowCheckBox"];
                return o != null ? (bool?)o : null;
            }
            set
            {
                ViewState["ShowCheckBox"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the property name for the ShowCheckBox property binding of the OdcTreeNode.
        /// </summary>
        public string ShowCheckBoxField
        {
            get
            {
                object o = ViewState["ShowCheckBoxField"];
                return o != null ? (string)o : null;
            }
            set
            {
                ViewState["ShowCheckBoxField"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the property name for the ImageUrl property binding of the OdcTreeNode.
        /// </summary>
        public string ImageUrlField
        {
            get
            {
                object o = ViewState["ImageUrlField"];
                return o != null ? (string)o : null;
            }
            set
            {
                ViewState["ImageUrlField"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the default value for the ImageUrl property of the OdcTreeNode with the specified Level.
        /// </summary>
        public string ImageUrl
        {
            get
            {
                object o = ViewState["ImageUrl"];
                return o != null ? (string)o : null;
            }
            set
            {
                ViewState["ImageUrl"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the name for this binding. This can be used in conjunction with GetNamedBinding().
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the NodeTemplate for OdcTreeNode with the specified Level.
        /// </summary>
        [TemplateContainer(typeof(OdcTreeNodeContainer), BindingDirection.TwoWay)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public IBindableTemplate NodeTemplate { get; set; }
      
      
    }
}
