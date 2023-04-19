using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EinsteinQuest
{
    public class Squirrel : MonoBehaviour
    {
        /// ===============================================================
        /// ======================= Input actions =========================
        private InputAction joystickMovement, observePress, joinLeavePress;

        /// ===============================================================
        /// ========================== Constants ==========================

        private const float PLACEHOLDER = 0f;

        /// ===============================================================
        /// ==================== Serialized variables ===================== 

        // Squirrel model facing (0, 0, 1)
        [SerializeField] public GameObject thisSquirrel;
        [SerializeField] UIManager uiManager;

        [SerializeField] public GameManager gm;

        [Space(20)]
        [Header("General")]
        [Space(5)]

        [Tooltip("Damping the movement speed of the squirrel. This can be fine tunned to fit your device.")]
        [Range(0f, 10f)]
        [SerializeField] private float speedNormalizer = 1;

        /// ===============================================================
        /// ======================= Squirrel Stats ========================

        //public Globals.Colors squirrelColor { get; set; }
        [SerializeField] public Globals.Colors squirrelColor;

        public bool acronHold = false;
        public int squirrelID = 123456, score = 0, state;
        private float antiTimeRemaining;
        private AntiX antiX;
        // CPU
        public bool cpuControl;
        public CPUMovementController cpuMovement;
        /// ===============================================================
        /// =========================== Methods =========================== 
        /// ===============================================================

        // Start is called before the first frame update
        void Start()
        {
            cpuMovement = new CPUMovementController(gm, this, cpuControl);
            state = (int)Globals.SquirrelStates.NORMAL;
            antiTimeRemaining = 3f;
        }
        public void MoveSquirrel(float x, float y) {
            if(!acronHold) {
                x /= speedNormalizer;
                y /= speedNormalizer;
                UpdateDirection(x, y);
                UpdatePosition(x, y);
            }
        }
        public void Observe() {
            acronHold = gm.SquirrelInteractQuery(this, false);
        }
        public void Consume() {
            if(acronHold) {
                gm.SquirrelInteractQuery(this, true);
            }
        }
        // penalize this squirrel and make them slow for eating an anti acorn
        public void Anti() {
            antiTimeRemaining = 3f;
            state = (int) Globals.SquirrelStates.ANTI;
            Debug.Log("anti");
            antiX = gm.CreateAntiXToFollowSquirrel(this);
        }

        // Update is called once per frame
        void Update()
        {
            if(cpuControl) {
                cpuMovement.Update();
            }
            if(state == (int) Globals.SquirrelStates.ANTI) {
                if(antiTimeRemaining > 0f) {
                    antiTimeRemaining -= Time.deltaTime;
                }
                else {
                    state = (int) Globals.SquirrelStates.NORMAL;
                    antiX.Destroy();
                    Destroy(antiX);
                }
            }
        }

        /// <summary>
        /// Turn the squirrel to face the moving direction 
        /// </summary>
        /// <param name="X">X axis input from joystick</param>
        /// <param name="Z">Z axis input from joystick</param>
        private void UpdateDirection(float X, float Z)
        {
            if(X != 0)
            {
                thisSquirrel.transform.rotation =
                    Quaternion.Euler(0, Mathf.Atan2(X, Z) * Mathf.Rad2Deg, 0);
            }
            
        }

        /// <summary>
        /// Move the squirrel according to player input  
        /// </summary>
        /// <param name="X">X axis input from joystick</param>
        /// <param name="Z">Z axis input from joystick</param>
        private void UpdatePosition(float X, float Z)
        {
            float modifier = 1f;
            if(state == (int) Globals.SquirrelStates.ANTI) {
                modifier = Globals.ANTI_PENALTY_SPEED_PLAYER;
            }
            thisSquirrel.transform.Translate(new Vector3(X*modifier, 0, 0) * Time.deltaTime, Space.World);
            thisSquirrel.transform.Translate(new Vector3(0, 0, Z*modifier) * Time.deltaTime, Space.World);
        }

        /// <summary>
        /// This method is for testing purpose only. 
        /// Actual players should be using a gamepad/controller rather than keyboard. 
        /// </summary>
        private void UpdateKeyboard()
        {
            
        }

        /// <summary>
        /// Attempt to pick up an acorn.
        /// If there is currently no acorn pciked up by this squirrel, then
        /// this sends an query to GM asking for the nearest acorn. 
        /// </summary>
        public void PickupAttempt()
        {
            acronHold = gm.SquirrelInteractQuery(this, false);
        }
        void ConsumeAttempt()
        {
            gm.SquirrelInteractQuery(this, true);
        }
        public void TryStartGame() {
            if(!uiManager.started) {
                uiManager.StartCutscene();
                gm.gameState = (int) Globals.GameStates.QUANTUMFOREST;
            }
            if(acronHold) {
                acronHold = gm.SquirrelInteractQuery(this, false);
            }
        }
    }
}
