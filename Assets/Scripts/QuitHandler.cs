using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace EinsteinQuest
{
    /// <summary>
    /// This class handles when Escape key is pressed on keyboard. 
    /// Note that since players do not have access to keyboard (they use gamepad),
    /// this should be inaccessible to the players. 
    /// </summary>
    /// 
    public class QuitHandler : MonoBehaviour
    {
        public QuitHandler(InputAction quitAction)
        {
            quitAction.performed += QuitAction_performed;
            quitAction.Enable();
        }


        private void QuitAction_performed(InputAction.CallbackContext obj)
        {

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif

            Application.Quit();
        }
    }
}
