using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace EinsteinQuest
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] UIDocument active;
        private Score score;
        private Seconds seconds;

        [SerializeField] UIDocument menu;
        [SerializeField] UIDocument endgame;
        [SerializeField] GameManager gm;

        private PlayerInput inputScheme;
        private InputAction startAction;
        private bool startGame;
        void Start()
        {
            inputScheme= new PlayerInput();
            active.enabled= false;
            score = active.GetComponent<Score>();
            seconds = active.GetComponent<Seconds>();

            menu.enabled = true;
            endgame.enabled = false;
            startAction = inputScheme.Player1.Observe;
            startAction.Enable();
            Debug.Log(startAction);
        }

        void Update()
        {
            startGame = startAction.WasPressedThisFrame();
            Debug.Log(startGame);
            if(startGame)
            {
                menu.enabled = false;
                active.enabled = true;
                
                score.FirstSwitch();
                
                seconds.FirstSwitch();
                //Debug.Log("score is enabled and active: " + score.isActiveAndEnabled);
            }

            if (seconds.gameDone)
            {
                endgame.enabled = true;
                active.enabled = false;
            }
        }
    }
}
