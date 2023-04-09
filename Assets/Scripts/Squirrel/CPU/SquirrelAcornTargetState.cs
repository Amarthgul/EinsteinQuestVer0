using UnityEngine;
public class SquirrelAcornTargetState : ICPUState {
    Vision obstacleVision, acornVision;
    SquirrelCPU squirrelCPU;
    Transform transform;
    GameObject targetAcorn;
    public SquirrelAcornTargetState(SquirrelCPU squirrelCPU, GameObject acorn) {
        this.squirrelCPU = squirrelCPU;
        this.transform = squirrelCPU.player.transform;
        this.obstacleVision = squirrelCPU.obstacleVision;
        this.acornVision = squirrelCPU.acornVision;
        this.targetAcorn = acorn;
    }

    public void Move() {
        transform.Translate(transform.forward * squirrelCPU.acornMovementSpeed * Time.deltaTime, Space.World);
        Quaternion targetRotation = Quaternion.LookRotation(targetAcorn.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, squirrelCPU.acornTargetRotationSpeed * Time.deltaTime);
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
            GameObject newAcorn = acornVision.FindClosestHitObject(SquirrelCPU.ACORN_TAG);
            if(newAcorn != null) {
                // change to new acorn
                targetAcorn = newAcorn;
            }
            // switch to search state, we lost track of an acorn :(
            else {
                squirrelCPU.state = new SquirrelAcornSearchState(squirrelCPU);
            }
        }
    }
}
