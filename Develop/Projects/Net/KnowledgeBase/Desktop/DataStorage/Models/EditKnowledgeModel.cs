using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using KnowledgeBase.BussinesLayer;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.BussinesLayer.Security;
using KnowledgeBase.Controls;
using KnowledgeBase.Core;
using KnowledgeBase.Core.Collections;
using KnowledgeBase.Desktop.Collections;
using KnowledgeBase.Desktop.Core;
using Savchin.Core;
using Savchin.Validation;
using Savchin.Wpf.Controls.Core;
using Savchin.Wpf.Core;
using Savchin.Wpf.Input;

namespace KnowledgeBase.Desktop.Models
{
    public class EditKnowledgeModel : ModelsBase
    {
        #region Properties
        private readonly ISummaryEditor _summaryEditor;
        private static readonly KnowledgeType[] _types = EnumHelper.GetValuesArray<KnowledgeType>();
        private static readonly KnowledgeStatus[] _statuses = EnumHelper.GetValuesArray<KnowledgeStatus>();

        public KnowledgeType[] Types
        {
            get { return _types; }
        }
        public KnowledgeStatus[] Statuses
        {
            get { return _statuses; }
        }
        public Knowledge Entity
        {
            get;
            private set;
        }
        public ICommand SaveCommand
        {
            get;
            private set;
        }
        public KnowledgeFileCollection Files
        {
            get { return new KnowledgeFileCollection(Entity.KnowledgeID, KbContext.CurrentKb); }
        }
        public ObservableCollection<Keyword> Keywords
        {
            get;
            private set;
        } 
        #endregion

        public event EventHandler Saved;



        public EditKnowledgeModel(int id, Control control,ISummaryEditor editor )
            : base(control)
        {
            SaveCommand = new DelegateCommand(OnSaveCommand);
            Entity = (id > 0 ? KbContext.CurrentKb.ManagerKnowledge.GetByID(id) : new Knowledge()) ?? new Knowledge();
            _summaryEditor = editor;
            _summaryEditor.Value = Entity.Summary;
            Keywords = new ObservableCollection<Keyword>(KbContext.CurrentKb.ManagerKeyword.GetByListID(Entity.KewordsAssociations));
        }

        private void OnSaveCommand()
        {
            try
            {
                var keywords = Keywords.Where(e => e.KeywordID > 0)
                    .Select(e => e.KeywordID).Distinct().ToList();
                var newKeywords = Keywords.Where(e => e.KeywordID < 0)
                    .Select(e => e.Name).Distinct().ToArray();

                Control.UpdateBindings();
                Entity.Summary = _summaryEditor.Value;

                KbContext.CurrentKb.ManagerKnowledge.Save(Entity, keywords, newKeywords);
                if (newKeywords.Length > 0)
                    AppCore.Workspace.Keywords.Refresh();

                OnSaved();
            }
            catch (ValidationException ex)
            {
                ErrorLabel.ShowException(Control, "Error save knowledge", ex);
            }
            catch (PermissionException ex)
            {
                Messages.ShowSecurityAlert(ex.Message, Entity.CategoryID);
            }
            catch (Exception ex)
            {
                ErrorForm.Show("Error save knowledge", ex);
            }
        }

        private void OnSaved()
        {
            var handler = Saved;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
