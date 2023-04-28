using System.Collections.Generic;
using System.Linq;

namespace SAPTeam.CommonTK.Console.ConsoleForm
{
    public partial class Interface
    {
        private readonly List<Form> subForms = new List<Form>();

        /// <summary>
        /// Registers a new sub form and activates it.
        /// </summary>
        /// <param name="subForm">
        /// The new sub form.
        /// </param>
        public void AddSubForm(Form subForm)
        {
            subForms.Add(subForm);
            System.Console.Clear();
            subForm.Platform = this;
            subForm.CreateObjects();
            SetActiveForm(subForm);
        }

        /// <summary>
        /// Changes the <see cref="activeForm"/> and sets it's receivers.
        /// </summary>
        /// <param name="form"></param>
        public void SetActiveForm(Form form)
        {
            form.SetReceivers();
            activeForm = form;
            Refresh();
        }

        /// <summary>
        /// Closes the active sub form.
        /// </summary>
        public void CloseSubForm()
        {
            if (activeForm == subForms.Last())
            {
                RaiseClose();
                subForms.RemoveAt(subForms.Count - 1);
                SetActiveForm(subForms.Count > 0 ? subForms.Last() : rootForm);
            }
        }
    }
}