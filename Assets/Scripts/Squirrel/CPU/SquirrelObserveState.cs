using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EinsteinQuest
{
    public class SquirrelObserveState : ICPUState
    {
        float timer;
        string state;
        SquirrelCPU squirrelCPU;
        Quaternion targetRotation;
        Quaternion previousRotation;
        Transform transform;
        GameObject acorn;
        // Start is called before the first frame update
        public SquirrelObserveState(SquirrelCPU squirrelCPU, GameObject acorn) {
            this.squirrelCPU = squirrelCPU;
            timer = 5f;
            state = "observeState";
            squirrelCPU.squirrel.PickupAttempt();
            this.previousRotation = squirrelCPU.transform.rotation;
            this.transform = squirrelCPU.transform;
            transform.LookAt(acorn.transform);
            targetRotation = Quaternion.LookRotation(-transform.forward, Vector3.up);
            this.acorn = acorn;
        }
        public void Move() {
            // empty
        }

        // Update is called once per frame
        public void Update()
        {
            if(timer < 3f && state.Equals("observeState")) {
                state = "dropState";
                squirrelCPU.squirrel.PickupAttempt();
            } else if(timer < 2f && state.Equals("dropState")) {
                state = "turnaroundState";
                transform.rotation = previousRotation;
            }
            if(state.Equals("turnaroundState")) {
                if(timer > 0f) {
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
                } else {
                    squirrelCPU.AddVisitedAcorn(acorn);
                    squirrelCPU.state = new SquirrelAcornSearchState(squirrelCPU);
                    return;
                }
            }
            timer -= Time.deltaTime;
        }
    }
}
