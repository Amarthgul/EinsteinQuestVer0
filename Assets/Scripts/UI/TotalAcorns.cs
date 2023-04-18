using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace EinsteinQuest
{
    public class TotalAcorns : MonoBehaviour
    {
        private VisualElement root;
        private Label total;
        private int totalAcorns;
        public bool gameDone = false, firstSwitch = false;
        [SerializeField] private Score scoreScript;
        [SerializeField] public int max;

        // Start is called before the first frame update
        public void FirstSwitch()
        {
            root = GetComponent<UIDocument>().rootVisualElement;
            var con = root.Query<VisualElement>("TotalContainer").First();
            total = con.Query<Label>("TotalAcorns").First();
            firstSwitch = true;
        }

        // Update is called once per frame
        void Update()
        {
            totalAcorns = (scoreScript.sAlbert.score + scoreScript.sErwin.score + scoreScript.sNiels.score);
            totalAcorns.ToString();
            max.ToString();
            if(firstSwitch) {
                total.text = totalAcorns.ToString() + "/"+max.ToString();
            }
            if(totalAcorns == max)
            {
                gameDone = true;
            }
        }
    }
}
