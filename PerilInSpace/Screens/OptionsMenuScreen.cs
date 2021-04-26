using Microsoft.Xna.Framework;
using System;

namespace PerilInSpace.Screens
{
    // The options screen is brought up over the top of the main menu
    // screen, and gives the user a chance to configure the game
    // in various hopefully useful ways.
    public class OptionsMenuScreen : MenuScreen
    {

        //HOW WE READ IN AND OUT SETTINGS FOR PERSISTENT SETTINGS
        public Settings _settings = new Settings();

        private readonly MenuEntry _volumeMenuEntry;
        private readonly MenuEntry _numLivesMenuEntry;
        private readonly MenuEntry _pointsPerAsteroidMenuEntry;
        private readonly MenuEntry _pointsPerEnemyMenuEntry;
        private readonly MenuEntry _pointsDeductionIfHit;
        private readonly MenuEntry _timeLimitMenuEntry;
        private readonly MenuEntry _hitboxesShownMenuEntry;

        public OptionsMenuScreen() : base("Options")
        {
            //CREATE EMPTY MENU ENTRIES
            _volumeMenuEntry = new MenuEntry(string.Empty);
            _numLivesMenuEntry = new MenuEntry(string.Empty);
            _pointsPerAsteroidMenuEntry = new MenuEntry(string.Empty);
            _pointsPerEnemyMenuEntry = new MenuEntry(string.Empty);
            _pointsDeductionIfHit = new MenuEntry(string.Empty);
            _timeLimitMenuEntry = new MenuEntry(string.Empty);
            _hitboxesShownMenuEntry = new MenuEntry(string.Empty);

            _settings.LoadFile();
            SetMenuEntryText();

            var back = new MenuEntry("Back");

            _volumeMenuEntry.Selected += VolumeMenuEntrySelected;
            _numLivesMenuEntry.Selected += NumLivesMenuEntrySelected;
            _pointsPerAsteroidMenuEntry.Selected += PointsPerAsteroidMenuEntrySelected;
            _pointsPerEnemyMenuEntry.Selected += PointsPerEnemyMenuEntrySelected;
            _pointsDeductionIfHit.Selected += PointsDeductionIfHitMenuEntrySelected;
            _timeLimitMenuEntry.Selected += TimeLimitMenuEntrySelected;
            _hitboxesShownMenuEntry.Selected += HitBoxesShownMenuEntrySelected;
            back.Selected += BackMenuEntrySelected;



            //ADD MENU ENTRIES TO THE MENU
            MenuEntries.Add(_volumeMenuEntry);
            MenuEntries.Add(_numLivesMenuEntry);
            MenuEntries.Add(_pointsPerAsteroidMenuEntry);
            MenuEntries.Add(_pointsPerEnemyMenuEntry);
            MenuEntries.Add(_pointsDeductionIfHit);
            MenuEntries.Add(_timeLimitMenuEntry);
            MenuEntries.Add(_hitboxesShownMenuEntry);
            MenuEntries.Add(back);
        }

        private void PointsDeductionIfHitMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            if (_settings.pointDeductionIfHit >= Globals.UPPERBOUND_POINT_DEDUCTION_IF_HIT)
            {
                _settings.pointDeductionIfHit = Globals.LOWERBOUND_POINT_DEDUCTION_IF_HIT;
            }
            else
            {
                _settings.pointDeductionIfHit += Globals.SETTINGS_INCREMENT;
            }

            SetMenuEntryText();
        }

        private void BackMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            _settings.SaveFile();
            ExitScreen();
        }

        // Fills in the latest values for the options screen menu text.
        private void SetMenuEntryText()
        {
            _volumeMenuEntry.Text = $"Volume ({Globals.LOWERBOUND_VOLUME} - {Globals.UPPERBOUND_VOLUME}): {_settings.volume.ToString()}";
            _numLivesMenuEntry.Text = $"Number of Lives ({Globals.LOWERBOUND_NUMBER_OF_LIVES} - {Globals.UPPERBOUND_NUMBER_OF_LIVES}): {_settings.numberOfLives.ToString()}";
            _pointsPerAsteroidMenuEntry.Text = $"Points Per Asteroid ({Globals.LOWERBOUND_POINTS_PER_ASTEROID} - {Globals.UPPERBOUND_POINTS_PER_ASTEROID}): {_settings.pointsPerAsteroid.ToString()}";
            _pointsPerEnemyMenuEntry.Text = $"Points Per Enemy ({Globals.LOWERBOUND_POINTS_PER_ENEMY} - {Globals.UPPERBOUND_POINTS_PER_ENEMY}): {_settings.pointsPerEnemy.ToString()}";
            _pointsDeductionIfHit.Text = $"Points Deducted If Hit ({Globals.LOWERBOUND_POINT_DEDUCTION_IF_HIT} - {Globals.UPPERBOUND_POINT_DEDUCTION_IF_HIT}): {_settings.pointDeductionIfHit.ToString()}";
            _timeLimitMenuEntry.Text = $"Time Limit ({Globals.LOWERBOUND_TIME_LIMIT} - {Globals.UPPERBOUND_TIME_LIMIT}): {_settings.timeLimit.ToString()}";
            _hitboxesShownMenuEntry.Text = $"Show Hitboxes: {(_settings.hitboxesShown == 1 ? "On" : "Off")}";
        }


        private void VolumeMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            if(_settings.volume >= Globals.UPPERBOUND_VOLUME)
            {
                _settings.volume = Globals.LOWERBOUND_VOLUME;
            }
            else
            {
                _settings.volume++;
            }
            
            SetMenuEntryText();
        }

        private void NumLivesMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            if (_settings.numberOfLives >= Globals.UPPERBOUND_NUMBER_OF_LIVES)
            {
                _settings.numberOfLives = Globals.LOWERBOUND_NUMBER_OF_LIVES;
            }
            else
            {
                _settings.numberOfLives++;
            }

            SetMenuEntryText();
        }

        private void PointsPerAsteroidMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            if (_settings.pointsPerAsteroid >= Globals.UPPERBOUND_POINTS_PER_ASTEROID)
            {
                _settings.pointsPerAsteroid = Globals.LOWERBOUND_POINTS_PER_ASTEROID;
            }
            else
            {
                _settings.pointsPerAsteroid += Globals.SETTINGS_INCREMENT;
            }

            SetMenuEntryText();
        }

        private void PointsPerEnemyMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            if (_settings.pointsPerEnemy >= Globals.UPPERBOUND_POINTS_PER_ENEMY)
            {
                _settings.pointsPerEnemy = Globals.LOWERBOUND_POINTS_PER_ENEMY;
            }
            else
            {
                _settings.pointsPerEnemy += Globals.SETTINGS_INCREMENT;
            }

            SetMenuEntryText();
        }

        private void TimeLimitMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            if (_settings.timeLimit >= Globals.UPPERBOUND_TIME_LIMIT)
            {
                _settings.timeLimit = Globals.LOWERBOUND_TIME_LIMIT;
            }
            else
            {
                _settings.timeLimit += Globals.TIME_LIMIT_INCREMENT;
            }
            SetMenuEntryText();
        }
        private void HitBoxesShownMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            if (_settings.hitboxesShown >= 1)
            {
                _settings.hitboxesShown = 0;
            }
            else
            {
                _settings.hitboxesShown = 1;
            }

            SetMenuEntryText();
        }
    }
}
