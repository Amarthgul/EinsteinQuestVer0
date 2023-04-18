using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EinsteinQuest
{
    public class CutScenePlayer : MonoBehaviour
    {
        public bool firstFinished = false;
        public bool secondFinished = false;
        [SerializeField] RawImage cutscene;
        private float fadeTime = Globals.FADE_TIME;
        // Start is called before the first frame update
        public void Begin()
        {
            Debug.Log("In begin");
            cutscene.canvasRenderer.SetAlpha(0f);
            StartCoroutine(Play(0));

        }
         
        // Update is called once per frame
        void Update()
        {
        }

        IEnumerator Play(int idx)
        {
            Debug.Log("Playing cutscene");
            var current = Globals.Slides[idx];
            cutscene.texture = (Texture2D)Resources.Load(current.path);
            cutscene.CrossFadeAlpha(1, fadeTime, false);
            yield return new WaitForSeconds(current.time);
            cutscene.CrossFadeAlpha(0, fadeTime, false);
            yield return new WaitForSeconds(fadeTime);
            if (idx < Globals.Slides.Length - 1)
            {
                StartCoroutine(Play(idx+1));
            } else
            {
                firstFinished= true;
                cutscene.canvas.gameObject.SetActive(false);
            }
        }
    }
}
