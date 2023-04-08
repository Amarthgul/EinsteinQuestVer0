using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EinsteinQuest
{
    public class GameManager : MonoBehaviour
    {

        /// <summary>
        /// ========================== Constants ==========================
        /// </summary>
        private int currentDimension; //0 red, 1 green, 2 blue 

        /// <summary>
        /// ==================== Serialized variables ===================== 
        /// </summary>

        [SerializeField] List<GameObject> acornModels = new List<GameObject>();
        [SerializeField] List<GameObject> squirrels = new List<GameObject>();
        private List<GameObject> acorns= new List<GameObject>();


        // Start is called before the first frame update
        void Start()
        {
            RespawnAcorn();

        }

        // Update is called once per frame
        void Update()
        {
            
        }

        /// <summary>
        /// Adds a handful of acorns onto the map. 
        /// </summary>
        private void RespawnAcorn()
        {
            int amountToRespawn = Globals.ACORN_PER_DIM +
                (int)(Globals.ACORN_FLUC * (Globals.RNG.NextDouble() - 0.5) * 2);

            for(int i = 0; i < amountToRespawn; i++)
            {
                float posX = (float)(Globals.RNG.NextDouble() - 0.5) * Globals.MAP_LENGTH;
                float posZ = (float)(Globals.RNG.NextDouble() - 0.5) * Globals.MAP_WIDTH;

                GameObject acornToSpawn = acornModels[Globals.RNG.Next() % acornModels.Count];

                // TODO: some form of spawn animation
                
                GameObject newAcorn = Instantiate(
                    acornToSpawn, 
                    new Vector3(posX, Globals.ACORN_SPAWN_Z, posZ), 
                    Quaternion.identity);
                acorns.Add(newAcorn);

                var a = newAcorn.AddComponent<Acorn>();
                // TODO: attach more script or set the gameobject attributes 
                var initialState = Globals.RNG.Next(3) + 1; //1, 2, 3
                if (Globals.RNG.Next(2) == 0)
                {
                    initialState *= -1;
                }
                a.currentState = (Acorn.states)initialState;
            }
        }

        public bool SquirrelAcorn()
        {
            var pickable = false;

            var squirrel = squirrels[0]; //for sake of testing, CONSIDER ASYNC FOR FINAL
            foreach (GameObject a in acorns)
            {
                float distance = Vector3.Distance(a.transform.position, squirrel.transform.position);
                //Debug.Log(distance);
                if (distance <= 0.2) {
                    pickable = true;
                    var closestacorn = a.GetComponent<Acorn>();
                    closestacorn.Observe();
                }
            }
            return pickable;
        }
    }
}
