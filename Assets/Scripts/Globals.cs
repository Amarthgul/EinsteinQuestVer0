using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;

namespace EinsteinQuest
{
    /// <summary>
    /// This class provides numerical/categorical reference for values that may be
    /// used through out the project. 
    /// </summary>
    public class Globals
    {

        /// ================================================================================
        /// ============================ Map Size and Display ==============================
        /// ================================================================================

        public const float MAP_WIDTH = 2f;  // Z axis. Unit in meter 
        public const float MAP_LENGTH = 3f; // X aixs. Unit in meter 

        // Buffer zone since some terrain will occupy a bit of the map border 
        public const float MAP_BUFFER = .1f;


        /// ================================================================================
        /// ================================= Dimensions ===================================
        /// ================================================================================

        public enum Colors { R, G, B, A };

        // For quick iterating access 
        public static List<Colors> ColorIterator = new List<Colors>() { Colors.R, Colors.G, Colors.B };

        /// ================================================================================
        /// ============================== Acorn and levels ================================
        /// ================================================================================

        public const int ACORN_PER_DIM = 6; // Base amount of acorn respawn
        public const int ACORN_FLUC = 2;    // Acorn respawn fluctuation 

        public const float ACORN_SPAWN_Z = .1f;  // Height from ground for spawned acorns 
        public const float ACORN_PICKUP_Z = .2f; // Lift the acorn if it is picked up 

        public const float ACORN_PICKUP_DIST = .2f;

        public static Dictionary<AcornStates, Colors> ColorStates = new Dictionary<AcornStates, Colors>()
        {
            { AcornStates.Red,Colors.R},
            { AcornStates.AntiRed,Colors.R},
            { AcornStates.Blue,Colors.B},
            { AcornStates.AntiBlue,Colors.B},
            { AcornStates.Green,Colors.G},
            { AcornStates.AntiGreen,Colors.G},
            {0, 0 }
        };

        public enum AcornStates { 
            Red = 1, 
            AntiRed = -1, 
            Green = 2, 
            AntiGreen = -2, 
            Blue = 3, 
            AntiBlue = -3 };

        public const int ACORN_CHOICE_PER_DIM = 2; 
        public static List<AcornStates> Reds = new List<AcornStates>() { AcornStates.Red, AcornStates.AntiRed };
        public static List<AcornStates> Greens = new List<AcornStates>() { AcornStates.Green, AcornStates.AntiGreen };
        public static List<AcornStates> Blues = new List<AcornStates>() { AcornStates.Blue, AcornStates.AntiBlue };
        public static List<AcornStates> All = new List<AcornStates>() { AcornStates.Red, AcornStates.AntiRed, AcornStates.Blue, AcornStates.AntiBlue, AcornStates.Green, AcornStates.AntiGreen };
        /// ================================================================================
        /// ==================================== Cutscenes =================================
        /// ================================================================================

        public const float FADE_TIME = 1f;
        public static (string path, float time)[] Slides = new (string path, float time)[]
        {
            //("Cutscene/path/to/img", time),

            /*("Cutscene/TestImages/blue", 2f),
            ("Cutscene/TestImages/green", 3f),
            ("Cutscene/TestImages/red", 4f)*/

            ("Cutscene/Controls", 3f),
            ("Cutscene/Acorn", 3f),
            ("Cutscene/QuantAcorn", 3f),
            ("Cutscene/1", 3f),
            ("Cutscene/2", 3f), //niels
            ("Cutscene/3", 3f), //erwin
            ("Cutscene/4", 1f), //albert
            ("Cutscene/Tree1", 0.1f), //black
            ("Cutscene/Tree2", 10f), //niels
            ("Cutscene/Tree3", 0.1f), //.
            ("Cutscene/Tree4", 0.1f), //..
            ("Cutscene/Tree5", 0.1f), //...
            ("Cutscene/Tree6", 7f), //treant
            ("Cutscene/Tree7", 5f), //treant
            ("Cutscene/Tree8", 10f), //treant
            ("Cutscene/Tree9", 10f),
            ("Cutscene/Tree10", 10f),
            ("Cutscene/Tree11", 4f),
            ("Cutscene/Tree12", 4f),
            ("Cutscene/Tree13", 4f),
            ("Cutscene/Tree14", 4f)
        };

        /// ================================================================================
        /// ==================================== Misc ======================================
        /// ================================================================================

        /// To avoid RNG created during runtime at a very close time interval
        /// generating similar or even same number, this RNG is used as a global
        /// variable to provide a more "random" generation. 
        public static System.Random RNG = new System.Random();

        /// ================================================================================
        /// ============================== Squirrel AI =====================================
        /// ================================================================================
        public static class AISpeed {
            public const float WANDER_MOVEMENT_SPEED = 0.3f;
            public const float WANDER_ROTATION_SPEED = 4f;
            public const float AVOIDANCE_MOVEMENT_SPEED = 0.3f;
            public const float AVOIDANCE_ROTATION_SPEED = 3f;
            public const float ACORN_MOVEMENT_SPEED = 0.3f;
            public const float ACORN_TARGET_ROTATION_SPEED = 5f;

        }
        public static class AIRays {
            public const float AVOIDANCE_RAY_DISTANCE = 0.3f;
            public const float RAY_HEIGHT_FROM_GROUND = 0.1f;
            public const float ACORN_RAY_HEIGHT_FROM_GROUND = 0.08f;
            public const float ACORN_TARGET_RAY_DISTANCE = 0.5f;
            public const int NUMBER_OF_OBSTACLE_RAYS = 27;
            public const int NUMBER_OF_ACORN_RAYS = 36;
        }
        public static class AIFOV {
            public const int OBSTACLE_FIELD_OF_VIEW = 270;
            public const int ACORN_TARGET_FIELD_OF_VIEW = 360;

        }
        public const float RANDOM_WANDER_FACTOR = 1f;
        public const float DISTANCE_TO_OBSERVE_ACORN = 0.15f;

    }
}
