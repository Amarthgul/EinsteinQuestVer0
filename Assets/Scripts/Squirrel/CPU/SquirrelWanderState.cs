using UnityEngine;
public class SquirrelAcornSearchState : ICPUState
{
    Vision acornVision, obstacleVision;
    SquirrelCPU squirrelCPU;
    Transform transform;
    private Quaternion targetRotation;
    private float time = 0f;
    public SquirrelAcornSearchState(SquirrelCPU squirrelCPU) {
        this.squirrelCPU = squirrelCPU;
        this.transform = squirrelCPU.player.transform;
        this.acornVision = squirrelCPU.acornVision;
        this.obstacleVision = squirrelCPU.obstacleVision;
        this.targetRotation = Quaternion.LookRotation(transform.forward, Vector3.up);
        this.time = 1f;
    }
    public void Move()
    {
        transform.Translate(transform.forward * squirrelCPU.wanderMovementSpeed * Time.deltaTime, Space.World);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, squirrelCPU.wanderRotationSpeed * Time.deltaTime);
        acornVision.FixRotation();
    }

    public void Update()
    {
        obstacleVision.GatherHits();
        acornVision.GatherHits();
        if(obstacleVision.ObstaclesFound()) {
            // we need to avoid an obstacle
            squirrelCPU.state = new SquirrelObstacleAvoidanceState(squirrelCPU);
        }
        // only possible obstacle is an acorn
        else if (acornVision.ObstaclesFound() && squirrelCPU.VisitableAcorn(acornVision.FindClosestHitObject(SquirrelCPU.ACORN_TAG))) {
            // set target to prey
            squirrelCPU.state = new SquirrelAcornTargetState(squirrelCPU, acornVision.FindClosestHitObject(SquirrelCPU.ACORN_TAG));

        } 
        else if(time <= 0f) {
            time = 3f;
            targetRotation = Quaternion.LookRotation(transform.forward + Vision.VectorFromAngle(Random.Range(-70, 70)), transform.up);
        }
        time -= Time.deltaTime;
    }
}