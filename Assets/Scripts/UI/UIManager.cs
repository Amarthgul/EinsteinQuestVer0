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

        public bool started;
        private bool startGame;
        void Start()
        {
            started = false;
            active.enabled= false;
            score = active.GetComponent<Score>();
            seconds = active.GetComponent<Seconds>();

            menu.enabled = true;
            endgame.enabled = false;
        }
        public void StartUI() {
            Debug.Log("accessing here!!!!!!");
            started = true;
            menu.rootVisualElement.style.display = DisplayStyle.None;
            active.enabled = true;
            
            score.FirstSwitch();
            
            seconds.FirstSwitch();
            //Debug.Log("score is enabled and active: " + score.isActiveAndEnabled);
        }
        void Update()
        {
            if (seconds.gameDone)
            {
                endgame.enabled = true;
                active.enabled = false;
            }
        }
    }
}
