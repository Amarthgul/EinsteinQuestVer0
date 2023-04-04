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
         

        /// <summary>
        /// ==================== Serialized variables ===================== 
        /// </summary>

        [SerializeField] List<GameObject> acornModels = new List<GameObject>();


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

                newAcorn.AddComponent<Acorn>();
                // TODO: attach more script or set the gameobject attributes 

            }
        }
    }
}
