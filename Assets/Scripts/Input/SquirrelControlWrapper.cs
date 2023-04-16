using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EinsteinQuest
{
    public class SquirrelControlWrapper : MonoBehaviour
    {
        private PlayerInput inputScheme;
        [SerializeField] Squirrel squirrel;
        [SerializeField] int playerNumber;
        void Update() {
            //if(inputScheme.Player1.JoinLeaveGame)
        }
        private void Awake()
        {
            this.inputScheme = new PlayerInput();
            // there's probably a better way to do this.. but it works lol
            switch(playerNumber) {
                case 1:
                    squirrel.Initialize(
                        inputScheme.Player1.JoystickMovement,
                        inputScheme.Player1.Observe,
                        inputScheme.Player1.JoinLeaveGame);
                    break;
                case 2:
                    squirrel.Initialize(
                        inputScheme.Player2.JoystickMovement,
                        inputScheme.Player2.Observe,
                        inputScheme.Player2.JoinLeaveGame);
                    break;
                case 3:
                    squirrel.Initialize(
                        inputScheme.Player3.JoystickMovement,
                        inputScheme.Player3.Observe,
                        inputScheme.Player3.JoinLeaveGame);
                    break;
            }

        }

        private void OnEnable()
        {
            inputScheme.Enable();
            var _q = new QuitHandler(inputScheme.Global.Quit);

        }


    }
}
