

using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Savchin.Forms.ListView
{

    
    partial class ObjectListView
    {
        //-----------------------------------------------------------------------------------
        #region Events

        /// <summary>
        /// Triggered after a ObjectListView has been searched by the user typing into the list
        /// </summary>
        [Category("Behavior - ObjectListView")]
        public event EventHandler<AfterSearchingEventArgs> AfterSearching;

        /// <summary>
        /// Triggered after a ObjectListView has been sorted
        /// </summary>
        [Category("Behavior - ObjectListView")]
        public event EventHandler<AfterSortingEventArgs> AfterSorting;

        /// <summary>
        /// Triggered before a ObjectListView is searched by the user typing into the list
        /// </summary>
        /// <remarks>
        /// Set Cancelled to true to prevent the searching from taking place.
        /// Changing StringToFind or StartSearchFrom will change the subsequent search.
        /// </remarks>
        [Category("Behavior - ObjectListView")]
        public event EventHandler<BeforeSearchingEventArgs> BeforeSearching;

        /// <summary>
        /// Triggered before a ObjectListView is sorted
        /// </summary>
        /// <remarks>
        /// Set Cancelled to true to prevent the sort from taking place.
        /// Changing ColumnToSort or SortOrder will change the subsequent sort.
        /// </remarks>
        [Category("Behavior - ObjectListView")]
        public event EventHandler<BeforeSortingEventArgs> BeforeSorting;
        /// <summary>
        /// Occurs when [cell click].
        /// </summary>
        [Category("Behavior - ObjectListView")]
        public event CellMouseEventHandler ItemClick;
        /// <summary>
        /// Occurs when [item double click].
        /// </summary>
        [Category("Behavior - ObjectListView")]
        public event CellMouseEventHandler ItemDoubleClick;
        /// <summary>
        /// Triggered when a cell is about to finish being edited.
        /// </summary>
        /// <remarks>If Cancel is already true, the user is cancelling the edit operation.
        /// Set Cancel to true to prevent the value from the cell being written into the model.
        /// You cannot prevent the editing from finishing.</remarks>
        [Category("Behavior - ObjectListView")]
        public event CellEditEventHandler CellEditFinishing;

        /// <summary>
        /// Triggered when a cell is about to be edited.
        /// </summary>
        /// <remarks>Set Cancel to true to prevent the cell being edited.
        /// You can change the the Control to be something completely different.</remarks>
        [Category("Behavior - ObjectListView")]
        public event CellEditEventHandler CellEditStarting;

        /// <summary>
        /// Triggered when a cell editor needs to be validated
        /// </summary>
        /// <remarks>
        /// If this event is cancelled, focus will remain on the cell editor.
        /// </remarks>
        [Category("Behavior - ObjectListView")]
        public event CellEditEventHandler CellEditValidating;

        /// <summary>
        /// Triggered when a column header is right clicked.
        /// </summary>
        [Category("Behavior - ObjectListView")]
        public event ColumnRightClickEventHandler ColumnRightClick;

        /// <summary>
        /// Some new objects are about to be added to an ObjectListView.
        /// </summary>
        [Category("Behavior - ObjectListView")]
        public event EventHandler<ItemsAddingEventArgs> ItemsAdding;

        /// <summary>
        /// The contents of the ObjectListView has changed.
        /// </summary>
        [Category("Behavior - ObjectListView")]
        public event EventHandler<ItemsChangedEventArgs> ItemsChanged;

        /// <summary>
        /// The contents of the ObjectListView is about to change via a SetObjects call
        /// </summary>
        /// <remarks>
        /// <para>Set Cancelled to true to prevent the contents of the list changing. This does not work with virtual lists.</para>
        /// </remarks>
        [Category("Behavior - ObjectListView")]
        public event EventHandler<ItemsChangingEventArgs> ItemsChanging;

        /// <summary>
        /// Some objects are about to be removed from an ObjectListView.
        /// </summary>
        [Category("Behavior - ObjectListView")]
        public event EventHandler<ItemsRemovingEventArgs> ItemsRemoving;

        #endregion

        //-----------------------------------------------------------------------------------
        #region OnEvents
        /// <summary>
        /// Raises the <see cref="E:CellClick"/> event.
        /// </summary>
        /// <param name="e">The <see cref="AfterSearchingEventArgs"/> instance containing the event data.</param>
        protected virtual void OnItemClick(ItemEventArgs e)
        {
            if (this.ItemClick != null)
                this.ItemClick(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:ItemDoubleClick"/> event.
        /// </summary>
        /// <param name="e">The <see cref="ItemEventArgs"/> instance containing the event data.</param>
        protected virtual void OnItemDoubleClick(ItemEventArgs e)
        {
            if (this.ItemDoubleClick != null)
                this.ItemDoubleClick(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:AfterSearching"/> event.
        /// </summary>
        /// <param name="e">The <see cref="AfterSearchingEventArgs"/> instance containing the event data.</param>
        protected virtual void OnAfterSearching(AfterSearchingEventArgs e)
        {
            if (this.AfterSearching != null)
                this.AfterSearching(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:AfterSorting"/> event.
        /// </summary>
        /// <param name="e">The <see cref="AfterSortingEventArgs"/> instance containing the event data.</param>
        protected virtual void OnAfterSorting(AfterSortingEventArgs e)
        {
            if (this.AfterSorting != null)
                this.AfterSorting(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:BeforeSearching"/> event.
        /// </summary>
        /// <param name="e">The <see cref="BeforeSearchingEventArgs"/> instance containing the event data.</param>
        protected virtual void OnBeforeSearching(BeforeSearchingEventArgs e)
        {
            if (this.BeforeSearching != null)
                this.BeforeSearching(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:BeforeSorting"/> event.
        /// </summary>
        /// <param name="e">The <see cref="BeforeSortingEventArgs"/> instance containing the event data.</param>
        protected virtual void OnBeforeSorting(BeforeSortingEventArgs e)
        {
            if (this.BeforeSorting != null)
                this.BeforeSorting(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:ColumnRightClick"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.ColumnClickEventArgs"/> instance containing the event data.</param>
        protected virtual void OnColumnRightClick(ColumnClickEventArgs e)
        {
            if (this.ColumnRightClick != null)
                this.ColumnRightClick(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:ItemsAdding"/> event.
        /// </summary>
        /// <param name="e">The <see cref="ItemsAddingEventArgs"/> instance containing the event data.</param>
        protected virtual void OnItemsAdding(ItemsAddingEventArgs e)
        {
            if (this.ItemsAdding != null)
                this.ItemsAdding(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:ItemsChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="ItemsChangedEventArgs"/> instance containing the event data.</param>
        protected virtual void OnItemsChanged(ItemsChangedEventArgs e)
        {
            if (this.ItemsChanged != null)
                this.ItemsChanged(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:ItemsChanging"/> event.
        /// </summary>
        /// <param name="e">The <see cref="ItemsChangingEventArgs"/> instance containing the event data.</param>
        protected virtual void OnItemsChanging(ItemsChangingEventArgs e)
        {
            if (this.ItemsChanging != null)
                this.ItemsChanging(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:ItemsRemoving"/> event.
        /// </summary>
        /// <param name="e">The <see cref="ItemsRemovingEventArgs"/> instance containing the event data.</param>
        protected virtual void OnItemsRemoving(ItemsRemovingEventArgs e)
        {
            if (this.ItemsRemoving != null)
                this.ItemsRemoving(this, e);
        }

        /// <summary>
        /// Tell the world when a cell is about to be edited.
        /// </summary>
        protected virtual void OnCellEditStarting(CellEditEventArgs e)
        {
            if (this.CellEditStarting != null)
                this.CellEditStarting(this, e);
        }

        /// <summary>
        /// Tell the world when a cell is about to finish being edited.
        /// </summary>
        protected virtual void OnCellEditorValidating(CellEditEventArgs e)
        {
            // Hack. ListView is an imperfect control container. It does not manage validation
            // perfectly. If the ListView is part of a TabControl, and the cell editor loses
            // focus by the user clicking on another tab, the TabControl processes the click
            // and switches tabs, even if this Validating event cancels. This results in the
            // strange situation where the cell editor is active, but isn't visible. When the
            // user switches back to the tab with the ListView, composite controls like spin
            // controls, DateTimePicker and ComboBoxes do not work properly. Specifically,
            // keyboard input still works fine, but the controls do not respond to mouse
            // input. SO, if the validation fails, we have to specifically give focus back to
            // the cell editor. (this is the Select() call in the code below). 
            // But (there is always a 'but'), doing that changes the focus so the cell editor
            // triggers another Validating event -- which fails again. From the user's point
            // of view, they click away from the cell editor, and the validating code
            // complains twice. So we only trigger a Validating event if more than 0.1 seconds
            // has elapsed since the last validate event.
            // I know it's a hack. I'm very open to hear a neater solution.

            // Also, this timed response stops us from sending a series of validation events
            // if the user clicks and holds on the OLV scroll bar.
            if ((Environment.TickCount - lastValidatingEvent) < 500) {
                e.Cancel = true;
            } else {
                lastValidatingEvent = Environment.TickCount;
                if (this.CellEditValidating != null)
                    this.CellEditValidating(this, e);
            }
            lastValidatingEvent = Environment.TickCount;
        }
        private int lastValidatingEvent = 0;

        /// <summary>
        /// Tell the world when a cell is about to finish being edited.
        /// </summary>
        protected virtual void OnCellEditFinishing(CellEditEventArgs e)
        {
            if (this.CellEditFinishing != null)
                this.CellEditFinishing(this, e);
        }

        #endregion
    }


}
