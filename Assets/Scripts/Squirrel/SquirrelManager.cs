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
        private int numberOfHumanPlayers = 0;
        private List<Squirrel> squirrels = new List<Squirrel>();
        void Start() {
            squirrels.Add(albertRedSquirrel);
            squirrels.Add(nielsBlueSquirrel);
            squirrels.Add(erwinGreenSquirrel);
        }
        public Squirrel GetAvailableSquirrel() {
            ++numberOfHumanPlayers;
            switch(numberOfHumanPlayers) {
                case 1:
                    Debug.Log("returning niels");
                    return nielsBlueSquirrel;
                case 2:
                    Debug.Log("returning albert");
                    return albertRedSquirrel;
                case 3:
                    Debug.Log("returning erwin");
                    return erwinGreenSquirrel;
            }
            Debug.Log("returning null");
            return null;
        }

    }
}
