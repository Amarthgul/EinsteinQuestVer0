using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EinsteinQuest
{
    public class CutScenePlayer : MonoBehaviour
    {
        [SerializeField] RawImage cutscene;
        private float fadeTime = Globals.FADE_TIME;
        // Start is called before the first frame update
        void Start()
        {
            cutscene.canvasRenderer.SetAlpha(0f);
            StartCoroutine(Play(0));

        }
         
        // Update is called once per frame
        void Update()
        {
        }

        IEnumerator Play(int idx)
        {
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
                cutscene.canvas.gameObject.SetActive(false);
            }
        }
    }
}
