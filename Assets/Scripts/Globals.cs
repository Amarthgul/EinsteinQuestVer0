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
        /// ============================== Acorn and levels ================================
        /// ================================================================================

        public const int ACORN_PER_DIM = 6; // Base amount of acorn respawn
        public const int ACORN_FLUC = 2;    // Acorn respawn fluctuation 

        public const float ACORN_SPAWN_Z = .1f; // Height from ground for spawned acorns 

        /// ================================================================================
        /// ==================================== Misc ======================================
        /// ================================================================================

        /// To avoid RNG created during runtime at a very close time interval
        /// generating similar or even same number, this RNG is used as a global
        /// variable to provide a more "random" generation. 
        public static System.Random RNG = new System.Random();
    }
}
