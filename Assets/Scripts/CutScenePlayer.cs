using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.ReorderableList.Internal;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

namespace EinsteinQuest
{
    public class CutScenePlayer : MonoBehaviour
    {
        [SerializeField] RawImage cutscene;
        [SerializeField] float freezeTime = 4f;
        [SerializeField] float fadeTime = 2f;
        private Object[] scenes;
        // Start is called before the first frame update
        void Start()
        {
            scenes = Resources.LoadAll("Cutscene/TestImages", typeof(Texture2D));
            cutscene.canvasRenderer.SetAlpha(0f);
            StartCoroutine(Play(0));

        }
         
        // Update is called once per frame
        void Update()
        {
        }

        IEnumerator Play(int slidenum)
        {
            cutscene.texture = (Texture2D)scenes[slidenum];
            cutscene.CrossFadeAlpha(1, fadeTime, false);
            yield return new WaitForSeconds(freezeTime);
            cutscene.CrossFadeAlpha(0, fadeTime, false);
            yield return new WaitForSeconds(fadeTime);
            if (slidenum < scenes.Length - 1)
            {
                StartCoroutine(Play(slidenum + 1));
            } else
            {
                cutscene.canvas.gameObject.SetActive(false);
            }
        }
    }
}
