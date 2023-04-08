using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EinsteinQuest
{
    public class Acorn : MonoBehaviour
    {
        public enum states
        {
            Red = 1,
            aRed = -1,
            Blue = 2,
            aBlue = -2,
            Green = 3,
            aGreen = -3
        }
        public states currentState;
        public float visibility; //not sure if you want this as gradient or flag
        public bool visibile;
        /// <summary>
        /// ==================== Serialized variables ===================== 
        /// </summary>

        [Tooltip("This is only a test variable to see if the script is attched at runtime")]
        [Range(0f, 10f)]
        [SerializeField] private float placeHolder = 1;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        public void Observe()
        {
            var random = Globals.RNG.Next(2) + 1; // either 1 or 2
            // get integral value of state
            // abs(int) gives the general color of state
            // then we add random and mod by 3 to get one of the other two colors
            currentState = (states)((random + Mathf.Abs((int)currentState)) % 3);
            random = Globals.RNG.Next(2);
            if(random == 0)
            {
                currentState = (states)((int)currentState * -1);
            }
        }
    }
}
