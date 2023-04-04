using SAPTeam.CommonTK.Console;
using SAPTeam.CommonTK.Console.ConsoleForm;
using SAPTeam.CommonTK.Console.ConsoleForm.Controls;

// This template created with SAPTeam.CommonTK.Console version 1.4.3.
// Some APIs may be changed in future, Please keep this template updated alongside the CommonTK.Console.

namespace SampleConsoleForm
{
    public class ConsoleForm1 : Form // Console forms must implement Form base class.
    {
        // Default value is true, set it to false for preventing form closing by ESC key.
        public override bool IsClosable => true;

        // Default value is false, suitable for pages that have items higher tha the console space.
        public override bool FocusToTop => true;

        // Default value is false, set it to true to track all wrote data and clear it after closing the form.
        public override bool UseDisposableWriter => false;

        // Set this values to true to sort your form elements by their names.
        public override bool SortSectionsByName => false;
        public override bool SortOptionsByName => false;

        // Initializes a new console form.
        public ConsoleForm1(bool rootForm = true) : base(rootForm)
        {
            
        }

        // Create your form elements here.
        protected override void CreateItems()
        {
            // First create a section.
            // If you don't want to categorize your options, create a blank section. Like:
            // Items[""] = new List<string>();
            Items["section 1"] = new List<string>();

            // After creating at least one section you can create your form options.
            // You can create options in various ways that you want.
            Items["section 1"].AddRange(new string[]
            {
                "Option 1"
            });
        }

        // Define your form header here.
        protected override void OnTitle()
        {
            // NOTE: You must use the Utils.Echo for writing texts to console.
            // This method handles bunch of contexts that used in the console ui creation.
            // If you don't use this method you may face with unexpected behaviors.
            Utils.Echo($"Sample Console Form");

            // Write a free line for better look.
            Utils.Echo();
        }

        // Define your form initialization scripts. This method called when the console ui is in initialization state.
        // This method only called if this form is the root form. 
        protected override void SetRootProperties()
        {
            // set some variables or something else...
        }

        // Define your form initialization scripts.
        protected override void SetProperties()
        {
            // set some variables or something else...
        }

        // Define your form startup scripts. this method called before starting the console ui.
        // This method only called if this form is the root form.
        protected override void OnStart()
        {
            // Check some conditions...
        }

        // You can set custom actions when the user presses a key.
        // The behavior of ESC key is to close the sub form or the console ui and this is handled by the console ui.
        // To prevent the form from closing, you must define a logic to set the IsClosable property to false.
        // Also behavior of the Up and Down arrow keys handled by console ui, but after changing the selected entry, this method is called as well as other keys.
        protected override void OnKeyPressed(ConsoleKeyInfo keyInfo)
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.F5:
                case ConsoleKey.UpArrow:
                case ConsoleKey.DownArrow:
                case ConsoleKey.Enter:
                case ConsoleKey.Escape:
                    // Exclude the handled keys.
                    break;
                default:
                    // Warn the user with a temporary toast message.
                    Platform.ScreenMessage($"The pressed key: {keyInfo.Key} is invalid.");
                    break;
            }
        }

        // You can set the behavior of each option here.
        protected override void OnEnter(ConsoleOption option)
        {
            // Write the logic of your form here...
        }

        // Define your form closing scripts.
        protected override void OnClose()
        {
            // Clear some variables...
        }
    }
}
