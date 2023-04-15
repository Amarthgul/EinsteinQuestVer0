using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EinsteinQuest
{
    public class SquirrelControlWrapper : MonoBehaviour
    {
        private PlayerInput inputScheme;
        [SerializeField] Squirrel squirrel;
        void Update() {
            //if(inputScheme.Player1.JoinLeaveGame)
        }
        private void Awake()
        {
            this.inputScheme = new PlayerInput();
            squirrel.Initialize(
                inputScheme.Player1.JoystickMovement,
                inputScheme.Player1.Observe,
                inputScheme.Player1.JoinLeaveGame);
        }

        private void OnEnable()
        {
            inputScheme.Enable();
            var _q = new QuitHandler(inputScheme.Global.Quit);

        }


    }
}
