using System;
using System.Collections.Generic;

using SAPTeam.CommonTK.Console.ConsoleForm.Controls;

namespace SAPTeam.CommonTK.Console.ConsoleForm
{
    /// <summary>
    /// Represents a console form or sub form that makes up an application's console user interface.
    /// </summary>
    public partial class Form
    {
        /// <summary>
        /// Gets or Sets the parent <see cref="Interface"/>.
        /// </summary>
        public Interface Platform { get; set; }

        /// <summary>
        /// Gets a dictionary that determines the section, identifier and text of each console elements.
        /// </summary>
        public Dictionary<string, Dictionary<string, string>> ExecutableItems { get; } = new Dictionary<string, Dictionary<string, string>>();

        /// <summary>
        /// Gets a dictionary that determines the section and text of each console elements.
        /// </summary>
        public Dictionary<string, List<string>> Items { get; } = new Dictionary<string, List<string>>();

        /// <summary>
        /// Gets a dictionary that contains all form's console elements.
        /// </summary>
        public Dictionary<int, IControl> Container { get; } = new Dictionary<int, IControl>();

        /// <summary>
        /// Gets the first line of selectable area for this form.
        /// </summary>
        public int First { get; set; }

        /// <summary>
        /// Gets the last line of selectable area for this form.
        /// </summary>
        public int Last { get; set; }

        /// <summary>
        /// Gets the line of the current selected component.
        /// </summary>
        public int Current { get; set; }

        /// <summary>
        /// Gets a value indicating whether when this form is started, the console window must focuses to the stating of this form.
        /// </summary>
        public virtual bool FocusToTop => false;

        /// <summary>
        /// Gets a value indicating whether this form is closable with ESC key.
        /// </summary>
        public virtual bool IsClosable => true;

        /// <summary>
        /// Gets a value indicating whether this form uses the <see cref="Contexts.DisposableWriter"/> for writing data to console or directly writes to the console and in each refreshes calls the <see cref="System.Console.Clear()"/>.
        /// </summary>
        public virtual bool UseDisposableWriter => false;

        /// <summary>
        /// Gets a value indicating whether the sections of this form will be sorted by their names.
        /// </summary>
        public virtual bool SortSectionsByName => false;

        /// <summary>
        /// Gets a value indicating whether the options of this form will be sorted by their names.
        /// </summary>
        public virtual bool SortOptionsByName => false;

        /// <summary>
        /// Initializes a new instance of <see cref="Form"/>.
        /// </summary>
        /// <param name="rootForm">
        /// Determines whether this form is a main form.
        /// </param>
        public Form(bool rootForm = true)
        {
            CreateItems();

            if (rootForm)
            {
                Platform = new Interface(this);
                SetRootProperties();
                SetReceivers();
                CreateObjects();
            }

            SetProperties();
        }

        /// <summary>
        /// Called in the end of the form initialization and reserved for changing form or console ui properties.
        /// </summary>
        protected virtual void SetProperties() { }

        /// <summary>
        /// Called exactly after creating console ui and reserved for changing form or console ui properties.
        /// </summary>
        protected virtual void SetRootProperties() { }

        /// <summary>
        /// Resets the console ui event subscribers. Called when initializing the form and after each active form changing for returning the controls to this form.
        /// </summary>
        public virtual void SetReceivers()
        {
            Platform.ClearEvents();
            Platform.Title += OnTitle;
            Platform.OnClose += OnClose;
            Platform.OnStart += OnStart;
            Platform.KeyPressed += OnKeyPressed;
            Platform.OnEnter += OnEnter;
        }

        /// <summary>
        /// Sets the values of <see cref="Items"/> or <see cref="ExecutableItems"/>. Called when initializing the form.
        /// </summary>
        protected virtual void CreateItems() { }

        /// <summary>
        /// Shows this form components.
        /// </summary>
        public void Start()
        {
            Platform.Start();
        }

        /// <summary>
        /// Called when the console ui creates or refreshes the form elements.
        /// </summary>
        public Action Title => OnTitle;

        /// <summary>
        /// Writes the form title area. Called when the console ui creates or refreshes the form elements.
        /// <para>
        /// All texts must be wrote with the <see cref="Utils.Echo(string, bool)"/> or <see cref="Utils.Echo(Colorize, bool)"/> methods. Otherwise you may face with unexpected behaviors.
        /// </para>
        /// </summary>
        protected virtual void OnTitle() { }

        /// <summary>
        /// Called when the form is closing.
        /// </summary>
        protected virtual void OnClose() { }

        /// <summary>
        /// Called when the root form is about to start.
        /// <para>
        /// This method is not called for sub forms.
        /// </para>
        /// </summary>
        protected virtual void OnStart() { }

        /// <summary>
        /// Called when the user pressed a key.
        /// </summary>
        /// <param name="keyInfo">
        /// The pressed key info.
        /// </param>
        protected virtual void OnKeyPressed(ConsoleKeyInfo keyInfo) { }

        /// <summary>
        /// Sets the functionality of each selectable elements. Called when the user selects a selectable element.
        /// </summary>
        /// <param name="option">
        /// The selected <see cref="ConsoleOption"/>.
        /// </param>
        protected virtual void OnEnter(ConsoleOption option) { }
    }
}