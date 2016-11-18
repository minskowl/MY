using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using Windows.UI.Core;


namespace CI.UI.Core
{
    public static class WeakEventHandlerManager
    {

        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void CallWeakReferenceHandlers(object sender, List<WeakEventHandler> weakDelegates)
        {
            if (weakDelegates == null)
                return;


            var callees = CleanupOldHandlers(weakDelegates);
            if (callees == null) return;

            var dispatcher = Windows.UI.Core.CoreWindow.GetForCurrentThread().Dispatcher;
            foreach (var weakDelegate in callees)
            {
                if (weakDelegate == null)
                    continue;

                //if (dispatcher != null && !dispatcher.CheckAccess())
                //    dispatcher.RunAsync(CoreDispatcherPriority.High,  new Action<object>(weakDelegate.Invoke), DispatcherPriority.Normal, sender);
                //else
                //    weakDelegate.Invoke(sender);

            }

        }

        private static WeakEventHandler[] CleanupOldHandlers(List<WeakEventHandler> weakDelegates)
        {
            for (var index = weakDelegates.Count - 1; index >= 0; --index)
            {
                var weakDelegate = weakDelegates[index];

                if (weakDelegate == null || weakDelegate.Target == null)
                {
                    weakDelegates.RemoveAt(index);
                    index--;
                }
            }

            return weakDelegates.Count == 0 ? null : weakDelegates.ToArray();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#")]
        public static void AddWeakReferenceHandler(ref List<WeakEventHandler> weakDelegates, EventHandler handler, int defaultListSize)
        {
            if (handler == null)
                return;

            if (weakDelegates == null)
                weakDelegates = defaultListSize > 0 ? new List<WeakEventHandler>(defaultListSize) : new List<WeakEventHandler>();
            weakDelegates.Add(new WeakEventHandler(handler));
        }


        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void RemoveWeakReferenceHandler(List<WeakEventHandler> weakDelegates, EventHandler handler)
        {
            if (weakDelegates == null || handler == null)
                return;

            for (var index = weakDelegates.Count - 1; index >= 0; --index)
            {
                var weakDelegate = weakDelegates[index];
                if (weakDelegate == null || weakDelegate.Target == null || weakDelegate.Target == handler.Target)
                    weakDelegates.RemoveAt(index);
            }
        }
    }
}
