using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EinsteinQuest
{
    public class SquirrelManager : MonoBehaviour
    {
        [SerializeField] Squirrel albertRedSquirrel;
        [SerializeField] Squirrel nielsBlueSquirrel;
        [SerializeField] Squirrel erwinGreenSquirrel;
        private List<Squirrel> squirrels = new List<Squirrel>();

        private static SquirrelManager instance = null;

        private SquirrelManager()
        {
        }

        public static SquirrelManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SquirrelManager();
                }
                return instance;
            }
        }
        void Start() {
            squirrels.Add(albertRedSquirrel);
            squirrels.Add(nielsBlueSquirrel);
            squirrels.Add(erwinGreenSquirrel);
        }
        // Start is called before the first frame update
        public bool HasAvailableSquirrel()
        {
            foreach(Squirrel squirrel in squirrels) {
                if(squirrel.cpuControl) {
                    return true;
                }
            }
            return false;
        }
        public Squirrel GetAvailableSquirrel() {
            foreach(Squirrel squirrel in squirrels) {
                if(squirrel.cpuControl) {
                    return squirrel;
                }
            }
            return null;
        }

    }
}
