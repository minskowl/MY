using System;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;


namespace Savchin.Forms.Wizard
{
    /// <summary>
    /// Summary description for WizardPageDesigner.
    /// </summary>
    public class WizardPageDesigner : ParentControlDesigner
    {
        /// <summary>
        /// Gets the design-time verbs supported by the component that is associated with the designer.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// A <see cref="T:System.ComponentModel.Design.DesignerVerbCollection"/> of <see cref="T:System.ComponentModel.Design.DesignerVerb"/> objects, or null if no designer verbs are available. This default implementation always returns null.
        /// </returns>
        public override DesignerVerbCollection Verbs
        {
            get
            {
                var verbs = new DesignerVerbCollection
				                {
				                    new DesignerVerb("Remove Page", handleRemovePage)
				                };

                return verbs;
            }
        }

        private void handleRemovePage(object sender, EventArgs e)
        {
            var page = this.Control as WizardPage;

            var h = (IDesignerHost)GetService(typeof(IDesignerHost));
            var c = (IComponentChangeService)GetService(typeof(IComponentChangeService));

            var dt = h.CreateTransaction("Remove Page");

            if (page.Parent is Wizard)
            {
                Wizard wiz = page.Parent as Wizard;

                c.OnComponentChanging(wiz, null);
                //Drop from wizard
                wiz.Pages.Remove(page);
                wiz.Controls.Remove(page);
                c.OnComponentChanged(wiz, null, null, null);
                h.DestroyComponent(page);
            }
            else
            {
                c.OnComponentChanging(page, null);
                //Mark for destruction
                page.Dispose();
                c.OnComponentChanged(page, null, null, null);
            }
            dt.Commit();
        }

    }
}
