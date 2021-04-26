using System;
using System.Collections.Generic;
using System.Text;

namespace PerilInSpace
{
    public static class Globals
    {
        //ENVIRONMENT MANAGEMENT
        public static string FILE_NAME = "config.json";

        //SCREEN SIZE
        public static int SCREEN_WIDTH = 800;
        public static int SCREEN_HEIGHT = 480;

        //SETTINGS MANAGEMENT
        public static int LOWERBOUND_VOLUME = 0;
        public static int UPPERBOUND_VOLUME = 10;
        public static int LOWERBOUND_NUMBER_OF_LIVES = 1;
        public static int UPPERBOUND_NUMBER_OF_LIVES = 5;
        public static int LOWERBOUND_POINTS_PER_ASTEROID = 100;
        public static int UPPERBOUND_POINTS_PER_ASTEROID = 5000;
        public static int LOWERBOUND_POINTS_PER_ENEMY = 100;
        public static int UPPERBOUND_POINTS_PER_ENEMY = 5000;
        public static int LOWERBOUND_POINT_DEDUCTION_IF_HIT = 100;
        public static int UPPERBOUND_POINT_DEDUCTION_IF_HIT = 5000;
        public static int LOWERBOUND_TIME_LIMIT = 5;
        public static int UPPERBOUND_TIME_LIMIT = 60;

        //SETTINGS MENU INCREMENT
        public static int SETTINGS_INCREMENT = 100;
        public static int TIME_LIMIT_INCREMENT = 5;
    }
}
