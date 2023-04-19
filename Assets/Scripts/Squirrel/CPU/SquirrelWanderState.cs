using UnityEngine;
using EinsteinQuest;
public class SquirrelAcornSearchState : ICPUState
{
    Vision acornVision, obstacleVision;
    CPUMovementController squirrelCPU;
    Transform transform;
    private Quaternion targetRotation;
    private float time = 0f;
    private Squirrel squirrel;
    public SquirrelAcornSearchState(CPUMovementController squirrelCPU) {
        this.squirrelCPU = squirrelCPU;
        this.transform = squirrelCPU.player.transform;
        this.acornVision = squirrelCPU.acornVision;
        this.obstacleVision = squirrelCPU.obstacleVision;
        this.targetRotation = Quaternion.LookRotation(transform.forward, Vector3.up);
        this.time = 1f;
        this.squirrel = squirrelCPU.squirrel;
    }
    public void Move()
    {
        float modifier = 1f;
        if(squirrel.state == (int) Globals.SquirrelStates.ANTI) {
            modifier = Globals.ANTI_PENALTY_SPEED_AI;
        }
        transform.Translate(transform.forward * modifier * Globals.AISpeed.WANDER_MOVEMENT_SPEED * Time.deltaTime, Space.World);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Globals.AISpeed.WANDER_ROTATION_SPEED * Time.deltaTime);
        acornVision.FixRotation();
    }

    public void Update()
    {
        obstacleVision.GatherHits();
        acornVision.GatherHits();
        if(acornVision.ObstaclesFound()) {
            Debug.Log("saw acorn");
        }
        if(obstacleVision.ObstaclesFound()) {
            // we need to avoid an obstacle
            squirrelCPU.state = new SquirrelObstacleAvoidanceState(squirrelCPU);
        }
        // only possible obstacle is an acorn
        else if (acornVision.ObstaclesFound() && squirrelCPU.VisitableAcorn(acornVision.FindClosestHitObject(CPUMovementController.ACORN_TAG))) {
            // set target to prey
            squirrelCPU.state = new SquirrelAcornTargetState(squirrelCPU, acornVision.FindClosestHitObject(CPUMovementController.ACORN_TAG));

        } 
        else if(time <= 0f) {
            time = 3f;
            targetRotation = Quaternion.LookRotation(transform.forward + Vision.VectorFromAngle(Random.Range(-70, 70)), transform.up);
        }
        time -= Time.deltaTime;
    }
}