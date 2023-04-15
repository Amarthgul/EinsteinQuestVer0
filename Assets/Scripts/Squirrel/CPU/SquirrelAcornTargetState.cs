using UnityEngine;
using EinsteinQuest;
public class SquirrelAcornTargetState : ICPUState {
    Vision obstacleVision, acornVision;
    CPUMovementController squirrelCPU;
    Transform transform;
    GameObject targetAcorn;
    public SquirrelAcornTargetState(CPUMovementController squirrelCPU, GameObject acorn) {
        Debug.Log("Acorn target state");
        this.squirrelCPU = squirrelCPU;
        this.transform = squirrelCPU.player.transform;
        this.obstacleVision = squirrelCPU.obstacleVision;
        this.acornVision = squirrelCPU.acornVision;
        this.targetAcorn = acorn;
    }

    public void Move() {
        transform.Translate(transform.forward * Globals.AISpeed.ACORN_MOVEMENT_SPEED * Time.deltaTime, Space.World);
        Quaternion targetRotation = Quaternion.LookRotation(targetAcorn.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Globals.AISpeed.ACORN_TARGET_ROTATION_SPEED * Time.deltaTime);
        obstacleVision.FixRotation();
    }
    // Update is called once per frame
    public void Update()
    {
        obstacleVision.GatherHits();
        // check if there is an obstacle to avoid
        if(obstacleVision.ObstaclesFound()) {
            squirrelCPU.state = new SquirrelObstacleAvoidanceState(squirrelCPU);
            return;
        }
        acornVision.GatherHits();
        // check if the original acorn is NOT still in vision
        if (!acornVision.InVision(targetAcorn)) {
            // check if a different acorn is in vision
            GameObject newAcorn = acornVision.FindClosestHitObject(CPUMovementController.ACORN_TAG);
            if(newAcorn != null && squirrelCPU.VisitableAcorn(newAcorn)) {
                // change to new acorn
                targetAcorn = newAcorn;
            }
            // switch to search state, we lost track of an acorn :(
            else {
                squirrelCPU.state = new SquirrelAcornSearchState(squirrelCPU);
            }
        }
        else if(Mathf.Abs(Vector3.Distance(targetAcorn.transform.position, transform.position)) <= Globals.DISTANCE_TO_OBSERVE_ACORN){
            squirrelCPU.state = new SquirrelObserveState(squirrelCPU, targetAcorn);
        }
    }
}
