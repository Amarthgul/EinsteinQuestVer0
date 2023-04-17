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

        [SerializeField] private GameManager gm;

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
        public int squirrelID = 123456;
        public int score = 0; 

        // CPU
        public bool cpuControl;
        public CPUMovementController cpuMovement;
        /// ===============================================================
        /// =========================== Methods =========================== 
        /// ===============================================================

        // Start is called before the first frame update
        void Start()
        {
            cpuMovement = new CPUMovementController(this, cpuControl);
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
            PickupAttempt();
        }
        public void Consume() {
            ConsumeAttempt();
        }

        // Update is called once per frame
        void Update()
        {
            if(cpuControl) {
                cpuMovement.Update();
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
            thisSquirrel.transform.Translate(new Vector3(X, 0, 0) * Time.deltaTime, Space.World);
            thisSquirrel.transform.Translate(new Vector3(0, 0, Z) * Time.deltaTime, Space.World);
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
            acronHold = gm.SquirrelInteractQuery(this);
        }
        void ConsumeAttempt()
        {
            
        }
        public void TryStartGame() {
            if(!uiManager.started) {
                uiManager.StartUI();
            }
        }
    }
}
