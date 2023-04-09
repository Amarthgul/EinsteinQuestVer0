using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EinsteinQuest
{
    public class Acorn : MonoBehaviour
    {
        public GameObject thisAcornModel; 

        public Globals.AcornStates currentState;

        public float visibility; //not sure if you want this as gradient or flag
        public bool visibile;

        public Acorn()
        {

        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }


        /// <summary>
        /// Depending on the color/dimension of the observer, 
        /// collapse the acorn into corresponding dimension. 
        /// </summary>
        /// <param name="ObserverDimension">Color/dimension of 
        /// the observer in R, G, or B</param>
        public void Collapse(Globals.Colors ObserverDimension)
        {
            Debug.Log("Try to collapse");

            int choice = Globals.RNG.Next() % Globals.ACORN_CHOICE_PER_DIM; 

            switch (ObserverDimension)
            {
                case Globals.Colors.R:
                    currentState = Globals.Reds[choice];
                    break;
                case Globals.Colors.G:
                    currentState = Globals.Greens[choice];
                    break;
                case Globals.Colors.B:
                    currentState = Globals.Blues[choice];
                    break;
                default:
                    break;
            }
        }



    }
}
