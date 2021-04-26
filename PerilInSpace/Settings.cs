using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace PerilInSpace
{
    public class Settings
    {
        public enum DefaultSettings
        {
            Volume = 10,
            NumberOfLives = 3,
            PointsPerAsteroid = 500,
            PointsPerEnemy = 1000,
            TimeLimit = 30,
            //0 = False, 1 = True
            HitboxesShown = 0
        }

        public int volume { get; set; }
        public int numberOfLives { get; set; }
        public int pointsPerAsteroid { get; set; }
        public int pointsPerEnemy { get; set; }
        public int timeLimit { get; set; }
        public int hitboxesShown { get; set; }


        public Settings()
        {
            volume = (int)DefaultSettings.Volume;
            numberOfLives = (int)DefaultSettings.NumberOfLives;
            pointsPerAsteroid = (int)DefaultSettings.PointsPerAsteroid;
            pointsPerEnemy = (int)DefaultSettings.PointsPerEnemy;
            timeLimit = (int)DefaultSettings.TimeLimit;
            hitboxesShown = (int)DefaultSettings.HitboxesShown;
        }

        public void CreateFile()
        {

            File.WriteAllText(Globals.FILE_NAME, JsonConvert.SerializeObject(this, Formatting.Indented));

        }

        public void LoadFile()
        {
            Settings temp = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(Globals.FILE_NAME));
            volume = temp.volume;
            numberOfLives = temp.numberOfLives;
            pointsPerAsteroid = temp.pointsPerAsteroid;
            pointsPerEnemy = temp.pointsPerEnemy;
            timeLimit = temp.timeLimit;
            hitboxesShown = temp.hitboxesShown;

        }

        public void SaveFile()
        {
            File.WriteAllText(Globals.FILE_NAME, JsonConvert.SerializeObject(this, Formatting.Indented));
        }

        public bool CheckFileExists()
        {
            return File.Exists(Globals.FILE_NAME);
        }
    }
}
