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

        [SerializeField] private Squirrel sNiels;
        [SerializeField] private Squirrel sErwin;
        [SerializeField] private Squirrel sAlbert;
        public void OnEnable()
        {
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
            nielsScore.text = nis.ToString();
            erwinScore.text = ers.ToString();
            albertScore.text = als.ToString();
        }
    }
}
