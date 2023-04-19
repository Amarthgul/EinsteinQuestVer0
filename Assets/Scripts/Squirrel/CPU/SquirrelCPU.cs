using UnityEngine;
using EinsteinQuest;
using System.Collections.Generic;
public class CPUMovementController {
    public GameObject player;
    public bool enabled;
    public Squirrel squirrel;
    private Dictionary<GameObject, float> visitedAcorns;  
    public ICPUState state;
    public Transform transform;
    public Vision acornVision, obstacleVision;
    private GameManager gm;
    public const string ACORN_TAG = "Acorn", OBSTACLE_TAG = "Obstacle";
    public CPUMovementController(GameManager gm, Squirrel squirrel, bool enabled) {
        this.squirrel = squirrel;
        this.player = squirrel.gameObject;
        this.transform = squirrel.transform;
        this.enabled = enabled;
        InitializeVisionModules();
        this.state = new SquirrelAcornSearchState(this);
        this.visitedAcorns = new Dictionary<GameObject, float>();
        this.gm = gm;

    }
    void InitializeVisionModules() {
        // acornVision module is for searching for potential acorns only.
        acornVision = new Vision(player, Globals.AIRays.ACORN_RAY_HEIGHT_FROM_GROUND, Globals.AIRays.ACORN_TARGET_RAY_DISTANCE, 
                                Globals.AIRays.NUMBER_OF_ACORN_RAYS, Globals.AIFOV.ACORN_TARGET_FIELD_OF_VIEW);
        acornVision.AddTag(ACORN_TAG);
        // obstacleVision module is for searching for potential obstacles only.
        obstacleVision = new Vision(player, Globals.AIRays.RAY_HEIGHT_FROM_GROUND, Globals.AIRays.AVOIDANCE_RAY_DISTANCE, 
                                Globals.AIRays.NUMBER_OF_OBSTACLE_RAYS, Globals.AIFOV.OBSTACLE_FIELD_OF_VIEW);
        obstacleVision.AddTag(OBSTACLE_TAG);
    }

    public void Move()
    {
        if(gm.gameState == (int) Globals.GameStates.STARTSCREEN) {
            return;
        }
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
