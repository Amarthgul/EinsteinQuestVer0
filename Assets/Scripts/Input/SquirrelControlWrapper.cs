using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace EinsteinQuest
{
    public class SquirrelControlWrapper : MonoBehaviour
    {
        private PlayerInput inputScheme;
        private SquirrelManager squirrelManager;
        private Squirrel squirrel;
        private Vector2 movement = Vector2.zero;
        private void Awake()
        {
            squirrelManager = GameObject.Find("SquirrelManager").GetComponent<SquirrelManager>();
            this.squirrel = squirrelManager.GetAvailableSquirrel();
            // tries to start the game. if the game has already started, this does nothing
            squirrel.TryStartGame();
            squirrel.cpuControl = false;
        }
        public void JoystickMovement(InputAction.CallbackContext callbackContext) {
            Debug.Log("movement");
            movement = callbackContext.ReadValue<Vector2>();
        }
        public void QuitAction(InputAction.CallbackContext callbackContext) {
            //var _q = new QuitHandler(callbackContext);
        }
        public void ObserveAction(InputAction.CallbackContext callbackContext) {
            if(callbackContext.action.triggered) {
                squirrel.Observe();
            }
        }
        void Update() {
            squirrel.MoveSquirrel(movement.x, movement.y);
        }
        private void OnEnable()
        {
            //inputScheme.Enable();
            //var _q = new QuitHandler(inputScheme.Global.Quit);

        }


    }
}
