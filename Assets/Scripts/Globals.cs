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

        public enum Colors { R, G, B };

        // For quick iterating access 
        public static List<Colors> ColorIterator = new List<Colors>() { Colors.R, Colors.G, Colors.B};

        /// ================================================================================
        /// ============================== Acorn and levels ================================
        /// ================================================================================

        public const int ACORN_PER_DIM = 6; // Base amount of acorn respawn
        public const int ACORN_FLUC = 2;    // Acorn respawn fluctuation 

        public const float ACORN_SPAWN_Z = .1f; // Height from ground for spawned acorns 

        public const float ACORN_PICKUP_DIST = .2f; 

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

        /// ================================================================================
        /// ==================================== Misc ======================================
        /// ================================================================================

        /// To avoid RNG created during runtime at a very close time interval
        /// generating similar or even same number, this RNG is used as a global
        /// variable to provide a more "random" generation. 
        public static System.Random RNG = new System.Random();
    }
}
