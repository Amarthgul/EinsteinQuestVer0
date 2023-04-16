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

        [Header("Acorn Models")]
        [Space(5)]

        [Tooltip("Provides randomization of acorn shapes")]
        [SerializeField] List<GameObject> acornModels = new List<GameObject>();

        [Header("Acorn Shaders")]
        [Space(5)]
        [SerializeField] Shader AcornRedShader;
        [SerializeField] Shader AcornAntiRedShader;
        [SerializeField] Shader AcornGreenShader;
        [SerializeField] Shader AcornAntiGreenShader;
        [SerializeField] Shader AcornBlueShader;
        [SerializeField] Shader AcornAntiBlueShader;

        [Space(15)]
        [Header("Squirrels")]
        [Space(5)]

        [Tooltip("List of squirrel instances")]
        [SerializeField] List<Squirrel> squirrels = new List<Squirrel>();

        /// ===============================================================
        /// ======================== Properties =========================== 

        private List<Acorn> acorns = new List<Acorn>();

        private Dictionary<Globals.AcornStates, Shader> acornShaders = new Dictionary<Globals.AcornStates, Shader>();

        /// ===============================================================
        /// ========================== Methods ============================

        // Start is called before the first frame update
        void Start()
        {
            CreateShaderList();

            //ShuffleColor();
            currentDimension = Globals.Colors.A;
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
            // TODO: add pesudo random to ensure the distance between acorns
            // are above squirrel's pickup distance 

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

                // Attach the class onto the model and set properties 
                newAcornModel.AddComponent<Acorn>();
                newAcornModel.GetComponent<Acorn>().ConnectShaders(acornShaders);
                newAcornModel.GetComponent<Acorn>().thisAcornModel = newAcornModel;
                Debug.Log(currentDimension);
                newAcornModel.GetComponent<Acorn>().Collapse(currentDimension);

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

        /// <summary>
        /// Create list of shaders accroding to what was given. 
        /// </summary>
        private void CreateShaderList()
        {
            acornShaders.Add(Globals.AcornStates.Red, AcornRedShader);
            acornShaders.Add(Globals.AcornStates.AntiRed, AcornAntiRedShader);
            acornShaders.Add(Globals.AcornStates.Green, AcornGreenShader);
            acornShaders.Add(Globals.AcornStates.AntiGreen, AcornAntiGreenShader);
            acornShaders.Add(Globals.AcornStates.Blue, AcornBlueShader);
            acornShaders.Add(Globals.AcornStates.AntiBlue, AcornAntiBlueShader);
        }



        /// ===============================================================
        /// ======================== Public Methods =======================
        /// ===============================================================


        /// <summary>
        /// Invoked when a squirrel tries to interact with an acorn. 
        /// </summary>
        /// <returns>True if there is an acorn to interact with.</returns>
        public bool SquirrelInteractQuery(Squirrel squirrel)
        {

            foreach (Acorn a in acorns)
            {

                Vector3 acornPlanar = new Vector3(
                    a.thisAcornModel.transform.position.x, 
                    0, 
                    a.thisAcornModel.transform.position.z);
                Vector3 squirrelPlanar = new Vector3(
                    squirrel.thisSquirrel.transform.position.x, 
                    0, 
                    squirrel.thisSquirrel.transform.position.z);
                float distance = Vector3.Distance(acornPlanar, squirrelPlanar);

                // The acorn spawn should be set so that no 2 acorn can be within the
                // pickup range of the squirrel at the same time. 
                if (distance <= Globals.ACORN_PICKUP_DIST) {
                    if (!a.pickUpProtection)
                    {
                        // If this acorn is free to be picked up / interacted with
                        a.tag = CPUMovementController.OBSTACLE_TAG;
                        a.Collapse(squirrel.squirrelColor);
                        a.observerID = squirrel.squirrelID;
                        a.pickUpProtection = true;

                        return true;
                    }
                    else if (a.pickUpProtection && a.observerID == squirrel.squirrelID)
                    {
                        // If this is the same acorn the squirrel have been holding 
                        a.tag = CPUMovementController.ACORN_TAG;
                        a.observerID = 0;
                        a.pickUpProtection = false;

                        return false;
                    }
                }
            }

            // No acorn is avilable to be interacted with 
            return false;
        }



    }
}
