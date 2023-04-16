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
            Debug.Log(squirrel != null);
            squirrel.cpuControl = false;
            /**squirrel.Initialize(
                inputScheme.Player1.JoystickMovement,
                inputScheme.Player1.Observe,
                inputScheme.Player1.JoinLeaveGame);
            **/
        }
        public void JoystickMovement(InputAction.CallbackContext callbackContext) {
            Debug.Log("movement");
            movement = callbackContext.ReadValue<Vector2>();
        }
        public void QuitAction(InputAction.CallbackContext callbackContext) {

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
