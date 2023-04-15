
using UnityEngine;
using EinsteinQuest;
public class SquirrelObstacleAvoidanceState : ICPUState
{
    Vision obstacleVision;
    CPUMovementController squirrelCPU;
    Vector3 locationToMove;
    Transform transform;
    public SquirrelObstacleAvoidanceState(CPUMovementController squirrelCPU) {
        this.transform = squirrelCPU.player.transform;
        this.squirrelCPU = squirrelCPU;
        this.obstacleVision = squirrelCPU.obstacleVision;
    }
    public void Move()
    {
        transform.Translate(transform.forward * Globals.AISpeed.AVOIDANCE_MOVEMENT_SPEED * Time.deltaTime, Space.World);
        Quaternion targetRotation = Quaternion.LookRotation(locationToMove - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Globals.AISpeed.AVOIDANCE_ROTATION_SPEED * Time.deltaTime);
        obstacleVision.FixRotation();
    }

    public void Update()
    {
        obstacleVision.GatherHits();
        // change state to search for acorns if no obstacles
        if(!obstacleVision.ObstaclesFound()) {
            squirrelCPU.state = new SquirrelAcornSearchState(squirrelCPU);
        }
        // set target to safe location to move if there are obstacles
        else {
            locationToMove = obstacleVision.FindLocationToMove();
        }
    }
}
