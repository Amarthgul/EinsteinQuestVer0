using UnityEngine;
public class SquirrelCPU : MonoBehaviour {
    [SerializeField] public float wanderMovementSpeed, wanderRotationSpeed, avoidanceRayDistance, avoidanceMovementSpeed, 
    rayHeightFromGround, randomWanderFactor, obstacleFieldOfView, avoidanceRotationSpeed;
    [SerializeField] public float acornTargetFieldOfView, acornMovementSpeed, acornTargetRayDistance, acornTargetRotationSpeed;
    [SerializeField] public int numberOfObstacleRays, numberOfAcornRays;
    [SerializeField] public GameObject player;
    [SerializeField] public bool enabled;
    public ICPUState state;
    public Vision acornVision, obstacleVision;
    public static string ACORN_TAG = "Acorn", OBSTACLE_TAG = "Obstacle";
    void Start() {
        InitializeVisionModules();
        this.state = new SquirrelAcornSearchState(this);
    }
    void InitializeVisionModules() {
        // acornVision module is for searching for potential acorns only.
        acornVision = new Vision(player, rayHeightFromGround, acornTargetRayDistance, numberOfAcornRays, acornTargetFieldOfView);
        acornVision.AddTag(ACORN_TAG);
        // obstacleVision module is for searching for potential obstacles only.
        obstacleVision = new Vision(player, rayHeightFromGround, avoidanceRayDistance, numberOfObstacleRays, obstacleFieldOfView);
        obstacleVision.AddTag(OBSTACLE_TAG);
    }

    public void Move()
    {
        state.Move();
        
    }
    public void Update()
    {
        if(enabled) {
            state.Update();
            Move();
        }
    }
}
