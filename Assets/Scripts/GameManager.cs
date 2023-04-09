using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EinsteinQuest
{
    public class GameManager : MonoBehaviour
    {

        /// ===============================================================
        /// ========================== Constants ==========================

        /// ===============================================================
        /// ========================== Settings =====-=====================
        public Globals.Colors currentDimension { get; set; }

        /// ===============================================================
        /// ==================== Serialized variables ===================== 

        // Provides randomization of acorns 
        [SerializeField] List<GameObject> acornModels = new List<GameObject>();

        // List of squirrel instances 
        [SerializeField] List<Squirrel> squirrels = new List<Squirrel>();

        private List<Acorn> acorns = new List<Acorn>();

        /// ===============================================================
        /// ========================== Methods ============================

        // Start is called before the first frame update
        void Start()
        {
            ShuffleColor();
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

                GameObject acornModelToSpawn = acornModels[Globals.RNG.Next() % acornModels.Count];

                // Create an acorn model
                GameObject newAcornModel = Instantiate(
                    acornModelToSpawn, 
                    new Vector3(posX, Globals.ACORN_SPAWN_Z, posZ), 
                    Quaternion.identity);

                // Create an acorn class and collapse it
                Acorn newAcorn = new Acorn();

                // Attach the class onto the model 
                newAcornModel.AddComponent<Acorn>();
                newAcornModel.GetComponent<Acorn>().Collapse(currentDimension);
                newAcornModel.GetComponent<Acorn>().thisAcornModel = newAcornModel;

                // Add acorn class into the list 
                acorns.Add(newAcornModel.GetComponent<Acorn>());
                
            }
        }

        /// <summary>
        /// Shuffle a new dimension color.
        /// Pesudo random is used to avoid having the same dimension. 
        /// </summary>
        private void ShuffleColor()
        {
            List<Globals.Colors> pool = new List<Globals.Colors>();

            foreach (Globals.Colors c in Globals.ColorIterator)
                if (c != currentDimension)
                    pool.Add(c);

            currentDimension = pool[Globals.RNG.Next() % pool.Count];
        }

        /// ===============================================================
        /// ======================== Public Methods =======================
        /// ===============================================================


        /// <summary>
        /// Invoked when a squirrel tries to interact with an acorn. 
        /// </summary>
        /// <returns>True if there is an acorn to interact with.</returns>
        public bool SquirrelInteractQuery()
        {

            var squirrel = squirrels[0]; //for sake of testing, CONSIDER ASYNC FOR FINAL
            foreach (Acorn a in acorns)
            {
                
                Debug.Log(a.thisAcornModel.transform.position);
                float distance = Vector3.Distance(a.thisAcornModel.transform.position, 
                    squirrel.thisSquirrel.transform.position);
                //Debug.Log(distance);
                if (distance <= Globals.ACORN_PICKUP_DIST) {
                    var closestacorn = a.GetComponent<Acorn>();
                    closestacorn.Collapse(squirrel.squirrelColor);

                    // Successfully picked up an acorn  
                    return true; 
                }
            }

            // No acorn is avilable to be interacted with 
            return false;
        }



    }
}
