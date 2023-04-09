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
        [SerializeField] private Squirrel squirrel;

        

        private void Awake()
        {
            inputScheme = new PlayerInput();

            squirrel.Initialize(
                inputScheme.Player1.JoystickMovement,
                inputScheme.Player1.Observe);


        }

        private void OnEnable()
        {
            inputScheme.Enable();

            var _q = new QuitHandler(inputScheme.Global.Quit);

        }


    }
}
