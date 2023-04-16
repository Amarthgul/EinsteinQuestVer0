using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace EinsteinQuest
{
    public class Seconds : MonoBehaviour
    {
        private VisualElement root;
        private Label seconds;

        [SerializeField] float startTime;
        public void OnEnable()
        {
            root = GetComponent<UIDocument>().rootVisualElement;
            var con = root.Query<VisualElement>("TimeContainer").First();
            seconds = con.Query<Label>("Seconds").First();
            StartCoroutine(Timer());
        }

        /*private void Update()
        {
            startTime -= Time.deltaTime;
            if(startTime <= 0)
            {
                startTime= 0;
            }
            seconds.text = startTime.ToString();
        }*/

        private IEnumerator Timer()
        {
            while (startTime > 0)
            {
                startTime -= Time.deltaTime;
                decimal round = System.Math.Round((decimal)startTime, 2);
                var display = round.ToString();
                if (round <= 0)
                {
                    display = "0.00";
                }
                seconds.text = display;
                yield return null;
            }
            
        }
    }
}
