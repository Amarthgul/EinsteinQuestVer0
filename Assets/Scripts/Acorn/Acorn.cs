using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EinsteinQuest
{
    public class Acorn : MonoBehaviour
    {
        /// <summary>
        /// ==================== Serialized variables ===================== 
        /// </summary>

        [Tooltip("This is only a test variable to see if the script is attched at runtime")]
        [Range(0f, 10f)]
        [SerializeField] private float placeHolder = 1;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
