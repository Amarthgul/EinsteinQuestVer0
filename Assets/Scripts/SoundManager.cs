using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EinsteinQuest
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] public AudioClip consume,anti,ui_click,acorn_pickup,acorn_drop;
        public AudioSource AudioSource;
        public static SoundManager instance;
        void Awake() {
            if(instance != null && instance != this) {
                Destroy(this.gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
}