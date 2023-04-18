using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EinsteinQuest
{
    public class SquirrelObserveState : ICPUState
    {
        float timer;
        string state;
        CPUMovementController squirrelCPU;
        Quaternion targetRotation;
        Quaternion previousRotation;
        Transform transform;
        GameObject acorn;
        // Start is called before the first frame update
        public SquirrelObserveState(CPUMovementController squirrelCPU, GameObject acorn) {
            this.squirrelCPU = squirrelCPU;
            timer = 5f;
            state = "observeState";
            squirrelCPU.squirrel.acronHold = squirrelCPU.squirrel.gm.SquirrelInteractQuery(squirrelCPU.squirrel,false);
            this.previousRotation = squirrelCPU.transform.rotation;
            this.transform = squirrelCPU.transform;
            targetRotation = Quaternion.LookRotation(-transform.forward, Vector3.up);
            transform.LookAt(acorn.transform);
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
                if(Random.Range(0,2) == 0) {
                    squirrelCPU.squirrel.acronHold = squirrelCPU.squirrel.gm.SquirrelInteractQuery(squirrelCPU.squirrel,false);
                } 
                else {
                    squirrelCPU.squirrel.gm.SquirrelInteractQuery(squirrelCPU.squirrel,true);
                }
            } else if(timer < 2f && state.Equals("dropState")) {
                state = "turnaroundState";
                transform.rotation = previousRotation;
            }
            if(state.Equals("turnaroundState")) {
                if(timer > 1.25f) {
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
                } else {
                    squirrelCPU.AddVisitedAcorn(acorn);
                    //this.acorn.tag = CPUMovementController.ACORN_TAG;
                    squirrelCPU.state = new SquirrelAcornSearchState(squirrelCPU);
                    return;
                }
            }
            timer -= Time.deltaTime;
        }
    }
}
