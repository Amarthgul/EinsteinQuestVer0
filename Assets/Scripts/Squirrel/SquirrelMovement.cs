using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EinsteinQuest
{
    public class SquirrelMovement : MonoBehaviour
    {
        /// <summary>
        /// ======================= Input actions =========================
        /// </summary>
        private InputAction joystickMovement;


        /// <summary>
        /// ========================== Constants ==========================
        /// </summary>
        
        private const float PLACEHOLDER = 0f;

        /// <summary>
        /// ==================== Serialized variables ===================== 
        /// </summary>
        
        // Squirrel facing (0, 0, 1)
        [SerializeField] private GameObject thisSquirrel;

        [Space(20)]
        [Header("General")]
        [Space(5)]

        [Tooltip("Damping the movement speed of the squirrel. This can be fine tunned to fit your device.")]
        [Range(0f, 10f)]
        [SerializeField] private float speedNormalizer = 1;


        /// ===============================================================
        /// =========================== Methods =========================== 
        /// ===============================================================

        public void Initialize(InputAction Joystick)
        {
            joystickMovement = Joystick;
            joystickMovement.Enable();
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            
            float movementX = joystickMovement.ReadValue<Vector2>().x / speedNormalizer;
            float movementZ = joystickMovement.ReadValue<Vector2>().y / speedNormalizer;

            UpdateDirection(movementX, movementZ);
            UpdatePosition(movementX, movementZ);

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
    }
}
