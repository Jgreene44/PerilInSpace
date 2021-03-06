using Microsoft.Xna.Framework;
using PerilInSpace.StateManagement;


namespace PerilInSpace.Screens
{
    // The main menu screen is the first thing displayed when the game starts up.
    public class MainMenuScreen : MenuScreen
    {
        public MainMenuScreen() : base("Peril in Space")
        {
            
            var playGameMenuEntry = new MenuEntry("Play Game");
            var optionsMenuEntry = new MenuEntry("Options");
            var exitMenuEntry = new MenuEntry("Exit");
            
            Settings _settings = new Settings();

            if (!_settings.CheckFileExists())
            {
                _settings.SaveFile();
            }

           

            playGameMenuEntry.Selected += PlayGameMenuEntrySelected;
            optionsMenuEntry.Selected += OptionsMenuEntrySelected;
            exitMenuEntry.Selected += OnCancel;

            MenuEntries.Add(playGameMenuEntry);
            MenuEntries.Add(optionsMenuEntry);
            MenuEntries.Add(exitMenuEntry);
        }

        private void PlayGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            //GameManager.controlsScreen = true;
            //GameScreen[] game = {new ControlsScreen(), new ObjectiveScreen(), new GameplayScreen() };
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex, new GameplayScreen(), new ObjectiveScreen(), new ControlsScreen());
            //ScreenManager.AddScreen(new ControlsScreen(), e.PlayerIndex);
            //ScreenManager.AddScreen(new ObjectiveScreen(), e.PlayerIndex);
            
        }

        private void OptionsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new OptionsMenuScreen(), e.PlayerIndex);
        }

        protected override void OnCancel(PlayerIndex playerIndex)
        {
            const string message = "Are you sure you want to exit this game?";
            var confirmExitMessageBox = new MessageBoxScreen(message);

            confirmExitMessageBox.Accepted += ConfirmExitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmExitMessageBox, playerIndex);
        }

        private void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();
        }
    }
}
