using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.UIElements;

namespace EinsteinQuest
{
    public class Score : MonoBehaviour
    {
        private VisualElement root;
        private Label nielsScore;
        private Label erwinScore;
        private Label albertScore;

        [SerializeField] public Squirrel sNiels;
        [SerializeField] public Squirrel sErwin;
        [SerializeField] public Squirrel sAlbert;
        public void FirstSwitch()
        {
            Debug.Log("I am enabled in Score!");
            root = GetComponent<UIDocument>().rootVisualElement;

            var niels = root.Query<VisualElement>("Niels").First();
            var erwin = root.Query<VisualElement>("Erwin").First();
            var albert = root.Query<VisualElement>("Albert").First();

            nielsScore = niels.Query<Label>("num");
            erwinScore = erwin.Query<Label>("num");
            albertScore = albert.Query<Label>("num");

        }

        private void Update()
        {
            int nis = sNiels.score;
            int ers = sErwin.score;
            int als = sAlbert.score;
            if(nielsScore != null && erwinScore.text != null && albertScore != null) {
                nielsScore.text = nis.ToString();
                erwinScore.text = ers.ToString();
                albertScore.text = als.ToString();
            }
        }
    }
}
