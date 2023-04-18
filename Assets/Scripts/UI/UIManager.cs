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
        private TotalAcorns total;
        //private Seconds seconds;

        [SerializeField] UIDocument menu;
        [SerializeField] UIDocument endgame;
        [SerializeField] GameManager gm;
        [SerializeField] CutScenePlayer csp;

        public bool started;
        private bool startGame;
        void Start()
        {
            started = false;
            active.enabled= false;
            score = active.GetComponent<Score>();
            total = active.GetComponent<TotalAcorns>();
            //seconds = active.GetComponent<Seconds>();

            menu.enabled = true;
            endgame.enabled = false;
        }
        public void StartCutscene()
        {
            menu.rootVisualElement.style.display = DisplayStyle.None;
            csp.Begin();
        }
        public void StartUI() {
            Debug.Log("accessing here!!!!!!");
            started = true;
            active.enabled = true;
            
            score.FirstSwitch();

            total.FirstSwitch();
            
            //seconds.FirstSwitch();
            //Debug.Log("score is enabled and active: " + score.isActiveAndEnabled);
        }
        void Update()
        {
            if (csp.firstFinished)
            {
                StartUI();
            }
            if (total.gameDone)
            {
                active.rootVisualElement.style.display = DisplayStyle.None;
                if (csp.secondFinished)
                {
                    endgame.enabled = true;
                    active.enabled = false;
                } else if (!csp.secondStarted)
                {
                    csp.End();
                    csp.secondStarted = true;
                }
            }
        }
    }
}
