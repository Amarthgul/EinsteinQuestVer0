using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EinsteinQuest
{
    public class SquirrelControlWrapper : MonoBehaviour
    {
        [SerializeField] public PlayerInput inputScheme;
        /// <summary>
        /// ======================= Input actions =========================
        /// </summary>
        //

        private Squirrel squirrel;

        private void Awake()
        {
            inputScheme = new PlayerInput();
            this.squirrel = SquirrelManager.Instance.GetAvailableSquirrel();
            squirrel.Initialize(
                inputScheme.Player1.JoystickMovement,
                inputScheme.Player1.Observe);


        }

        private void OnEnable()
        {
            inputScheme.Enable();
            squirrel.cpuControl = false;
            var _q = new QuitHandler(inputScheme.Global.Quit);

        }


    }
}
