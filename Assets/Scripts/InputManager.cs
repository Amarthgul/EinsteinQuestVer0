using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EinsteinQuest
{
    public class InputManager : MonoBehaviour
    {
        /// <summary>
        /// ======================= Input actions =========================
        /// </summary>
        private PlayerInput inputScheme;


        /// <summary>
        /// ==================== Serialized variables ===================== 
        /// </summary>
        [SerializeField] private SquirrelMovement squirrelMovement;

        

        private void Awake()
        {
            inputScheme = new PlayerInput();

            squirrelMovement.Initialize(inputScheme.Player1.JoystickMovement);


        }

        private void OnEnable()
        {
            inputScheme.Enable();

            var _q = new QuitHandler(inputScheme.Global.Quit);

        }


    }
}
