using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EinsteinQuest
{
    public class AntiX : MonoBehaviour
    {
        public Squirrel squirrel;
        public void SetSquirrel(Squirrel squirrel) {
            this.squirrel = squirrel;
        }
        // Update is called once per frame
        void Update()
        {
            if(squirrel == null) {
                return;
            }
            transform.position = new Vector3(squirrel.transform.position.x, transform.position.y, squirrel.transform.position.z);
        }
        public void Destroy() {
            Destroy(gameObject);
            Destroy(this);
        }
    }
}
