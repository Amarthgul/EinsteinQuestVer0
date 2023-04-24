using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EinsteinQuest
{
    public class Acorn : MonoBehaviour
    {
        /// ===============================================================
        /// ========================== Constants ==========================

        // For testing only, lift the acorn when it is being picked up 
        private const float PICKUP_OFFSET = .3f;

        /// ===============================================================
        /// ======================== Properties =========================== 

        // the geomtry of the acorn
        public GameObject thisAcornModel; 

        // Color state of the acorn, Red, AntiRed, etc. 
        public Globals.AcornStates currentState;

        // When being interacted, the acorn enters pick up protection
        // to avoid the situation when 2 squirrels observe at the same time. 
        public bool pickUpProtection = false;

        // When observed by an squirrel, acorn records the observer ID
        // to record observership and to avoid double observation 
        public int observerID = 0; 

        public float visibility; //not sure if you want this as gradient or flag
        public bool visible;

        public Dictionary<Globals.AcornStates, Shader> colorShaders;


        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            UpdateAcornAnimation();
        }

        /// <summary>
        /// Connect the acorn to a dict of shaders for changing appreance
        /// </summary>
        /// <param name="ShaderList">6 shaders of different states</param>
        public void ConnectShaders(Dictionary<Globals.AcornStates, Shader> ShaderList)
        {
            colorShaders = ShaderList; 
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
            Debug.Log(ObserverDimension.ToString());

            int choice = Globals.RNG.Next() % Globals.ACORN_CHOICE_PER_DIM;
            bool squirrelAcornDontMatch = !(ObserverDimension == Globals.ColorStates[currentState]);

            switch (ObserverDimension)
            {
                case Globals.Colors.R:
                    if (squirrelAcornDontMatch)
                    {
                        currentState = Globals.Reds[choice];
                    }
                    break;
                case Globals.Colors.G:
                    if (squirrelAcornDontMatch)
                    {
                        currentState = Globals.Greens[choice];
                    }
                    break;
                case Globals.Colors.B:
                    if (squirrelAcornDontMatch)
                    {
                        currentState = Globals.Blues[choice];
                    }
                    break;
                case Globals.Colors.A:
                    choice = Globals.RNG.Next() % 6;
                    currentState = Globals.All[choice];
                    break;
                default:
                    break;
            }
            GameObject x = thisAcornModel.transform.GetChild(0).GetChild(0).gameObject;
            switch(currentState) {
                // ensure X is disabled on prefab if this is not an anti acorn
                case Globals.AcornStates.Red:
                case Globals.AcornStates.Blue:
                case Globals.AcornStates.Green:
                    x.SetActive(false);
                    break;
                // this is an anti-acorn, enable the X to show on the prefab.
                default:
                    x.SetActive(true);
                    break;
            }
            UpdateShader();
        }
        public void Consume(Squirrel squirrel) {
            if(currentState < 0) {
                // anti
                if(squirrel.score > 0) {
                    squirrel.score -= 1;
                }
                squirrel.Anti();
                SoundManager.instance.AudioSource.PlayOneShot(SoundManager.instance.anti);
            }
            else {
                //normal acorn
                squirrel.score += 1;
                SoundManager.instance.AudioSource.PlayOneShot(SoundManager.instance.consume);
            }


        }

        /// ===============================================================
        /// ======================== Private Methods ======================
        /// ===============================================================


        /// <summary>
        /// Update the appearance of the acorn according to current state 
        /// </summary>
        private void UpdateShader()
        {
            thisAcornModel.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.shader = colorShaders[currentState];
        }

        /// <summary>
        /// Update the animaiton and movement of the acorn.
        /// </summary>
        private void UpdateAcornAnimation()
        {
            Vector3 pos = thisAcornModel.transform.position;

            if (pickUpProtection)
            {
                thisAcornModel.transform.position = new Vector3(pos.x, Globals.ACORN_PICKUP_Z, pos.z);
            }
            else
            {
                thisAcornModel.transform.position = new Vector3(pos.x, Globals.ACORN_SPAWN_Z, pos.z);
            }

        }
    }
}
