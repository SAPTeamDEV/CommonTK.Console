namespace SAPTeam.CommonTK.Console.ConsoleForm
{
    public partial class Interface
    {
        /// <summary>
        /// Shows a toast message with default colors and position..
        /// </summary>
        /// <param name="message">
        /// The toast message text.
        /// </param>
        /// <param name="msec">
        /// The toast message display time.
        /// </param>
        public void ScreenMessage(string message, int msec = 3000)
        {
            ScreenMessage(message, msec, ConsoleCoords.ScreenMessage.ResolveX() == Index ? ColorSet.InvertedScreenMesage : ColorSet.ScreenMesage, ConsoleCoords.ScreenMessage);
        }

        /// <summary>
        /// Show a toast message.
        /// </summary>
        /// <param name="message">
        /// The toast message text.
        /// </param>
        /// <param name="msec">
        /// The toast message display time.
        /// </param>
        /// <param name="colors">
        /// The <see cref="ColorSet"/> of toast message.
        /// </param>
        /// <param name="coordinates">
        /// The coordinates of toast message.
        /// </param>
        public void ScreenMessage(string message, int msec, ColorSet colors, ConsoleCoords coordinates)
        {
            if (coordinates.Center)
            {
                coordinates.Y = Utils.GetCenterPosition(message.Length);
            }
            coordinates.Focus();
            colors.ChangeColor();
            Utils.Echo(message, false);
            Utils.ResetColor();
            Timer.Set(msec, onEnd);

            void onEnd()
            {
                coordinates.Focus();
                Utils.ResetColor();
                Utils.ClearLine(true);
                int actX = coordinates.ResolveX();
                if (actX == Index && activeForm.Container[Index] is ISelectableControl opClass)
                {
                    opClass.Select();
                }
                else if (activeForm.Container.ContainsKey(actX))
                {
                    activeForm.Container[actX].Write();
                }
            }
        }
    }
}