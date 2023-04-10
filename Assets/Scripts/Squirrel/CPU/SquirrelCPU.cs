using UnityEngine;
using EinsteinQuest;
using System.Collections.Generic;
public class SquirrelCPU : MonoBehaviour {
    [SerializeField] public float wanderMovementSpeed, wanderRotationSpeed, avoidanceRayDistance, avoidanceMovementSpeed, 
    rayHeightFromGround, acornRayHeightFromGround, randomWanderFactor, obstacleFieldOfView, avoidanceRotationSpeed, distanceToAcornToObserve;
    [SerializeField] public float acornTargetFieldOfView, acornMovementSpeed, acornTargetRayDistance, acornTargetRotationSpeed;
    [SerializeField] public int numberOfObstacleRays, numberOfAcornRays;
    [SerializeField] public GameObject player;
    [SerializeField] public bool enabled;
    [SerializeField] public Squirrel squirrel;
    private Dictionary<GameObject, float> visitedAcorns;  
    public ICPUState state;
    public Vision acornVision, obstacleVision;
    public static string ACORN_TAG = "Acorn", OBSTACLE_TAG = "Obstacle";
    void Start() {
        InitializeVisionModules();
        this.state = new SquirrelAcornSearchState(this);
        this.visitedAcorns = new Dictionary<GameObject, float>();
    }
    void InitializeVisionModules() {
        // acornVision module is for searching for potential acorns only.
        acornVision = new Vision(player, acornRayHeightFromGround, acornTargetRayDistance, numberOfAcornRays, acornTargetFieldOfView);
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
        List<GameObject> acornKeys = new List<GameObject>(visitedAcorns.Keys);
        foreach(GameObject acorn in acornKeys) {
            if(visitedAcorns[acorn] <= 0) {
                visitedAcorns.Remove(acorn);
            } else {
                visitedAcorns[acorn] -= Time.deltaTime;
            }
        }
    }
    public void AddVisitedAcorn(GameObject acorn) {
        visitedAcorns.Add(acorn, 15f);
    }
    public bool VisitableAcorn(GameObject acorn) {
        if(acorn == null) {
            return false;
        }
        return !visitedAcorns.ContainsKey(acorn);
    }
}
