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
        public bool secondStarted = false;
        [SerializeField] RawImage cutscene;
        [SerializeField] Image backdrop;
        private float fadeTime = Globals.FADE_TIME;
        // Start is called before the first frame update
        public void Begin()
        {
            Debug.Log("In begin");
            cutscene.canvasRenderer.SetAlpha(0f);
            StartCoroutine(Play(0, 2));

        }

        public void End()
        {
            Debug.Log("end");
            backdrop.gameObject.SetActive(true);
            cutscene.gameObject.SetActive(true);
            StartCoroutine(Play(0, Globals.Slides.Length - 1));
        }
         
        // Update is called once per frame
        void Update()
        {
        }

        IEnumerator Play(int idx, int idxEnd)
        {
            Debug.Log("Playing cutscene");
            var current = Globals.Slides[idx];
            cutscene.texture = (Texture2D)Resources.Load(current.path);
            cutscene.CrossFadeAlpha(1, fadeTime, false);
            yield return new WaitForSeconds(current.time);
            cutscene.CrossFadeAlpha(0, fadeTime, false);
            yield return new WaitForSeconds(fadeTime);
            if (idx < idxEnd)
            {
                StartCoroutine(Play(idx+1, idxEnd));
            } else
            {
                if (firstFinished)
                {
                    secondFinished= true;
                }
                firstFinished= true;
                //cutscene.canvasRenderer.SetAlpha(0f);
                cutscene.gameObject.SetActive(false);
                backdrop.gameObject.SetActive(false);

            }
        }
    }
}
