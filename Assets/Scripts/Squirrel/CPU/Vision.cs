using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Vision
{
    Transform transform;
    // owner of vision
    GameObject owner;
    float rayDistance, sideFieldOfView, rayHeightFromGround, angleBetweenRays;
    int numberOfRays;
    bool obstaclesFound;
    Vector3 rayPosition;
    List<(bool hit, RaycastHit hitInfo)> rays;
    List<string> tagsToInclude;
    public Vision(GameObject gameObject, float rayHeightFromGround, float rayDistance, int numberOfRays, float fieldOfView) {
        this.owner = gameObject;
        this.transform = gameObject.transform;
        this.rayDistance = rayDistance;
        this.sideFieldOfView = fieldOfView / 2;
        this.angleBetweenRays = fieldOfView / numberOfRays;
        this.rayHeightFromGround = rayHeightFromGround;
        this.numberOfRays = numberOfRays;
        this.rayPosition = new Vector3(transform.position.x, transform.position.y + rayHeightFromGround, transform.position.z);
        this.tagsToInclude = new List<string>();
    }
    public void GatherHits() {
        Vector3 rayPosition = transform.position;
        rayPosition.y+=rayHeightFromGround;

        // hit?, distance away (null if no hit)
        rays = new List<(bool hit, RaycastHit hitInfo)>();

        // Only used so the compiler doesn't complain. Null isn't allowed as an argument in C#
        RaycastHit notHit = new RaycastHit();

        obstaclesFound = false;

        for(float i=-sideFieldOfView; i<=sideFieldOfView; i+=angleBetweenRays) {
            Vector3 angle = VectorFromAngle(i);
            if(Physics.Raycast(rayPosition, transform.TransformDirection(angle) * rayDistance, out RaycastHit hitInfo, rayDistance)) {
                if(tagsToInclude.Contains(hitInfo.collider.gameObject.tag)) {
                    rays.Add((true, hitInfo));
                    Debug.DrawRay(rayPosition, transform.TransformDirection(angle) * hitInfo.distance, Color.green);
                    obstaclesFound = true;
                } else {
                    rays.Add((false, notHit));
                    Debug.DrawRay(rayPosition, transform.TransformDirection(angle) * rayDistance, Color.red);
                }
            } else {
                rays.Add((false, notHit));
                Debug.DrawRay(rayPosition, transform.TransformDirection(angle) * rayDistance, Color.red);
            }
        }
    }
    public bool ObstaclesFound() {
        return obstaclesFound;
    }
    public void AddTag(string tag) {
        tagsToInclude.Add(tag);
    }
    public bool ObstaclesFoundExcludingTag(string tag) {
        foreach((bool hit, RaycastHit hitInfo) in rays) {
            if(hit && hitInfo.collider.gameObject.tag != tag) {
                return true;
            }
        }
        return false;
    }
    public Vector3 FindLocationToMove() {
        int targetRay = FindRayAtLargestGapCenter(rays);
        Vector3 targetVector = VectorFromAngle(-sideFieldOfView + (targetRay * angleBetweenRays));
        targetVector = transform.TransformDirection(targetVector) * rayDistance;
        // Draw debug ray as magenta to show obstacle corrected direction
        Debug.DrawRay(rayPosition, targetVector, Color.magenta);
        // this should not be zero
        return transform.position + targetVector;
    }
    public GameObject FindClosestHitObject(string tag) {
        Dictionary<GameObject, float> preyAndDistanceAway = new Dictionary<GameObject, float>();
        // find object and associate different distances away
        foreach((bool hit, RaycastHit hitInfo) in rays) {
            if(hitInfo.collider != null) {
                GameObject gameObject = hitInfo.collider.gameObject;
                if(gameObject != null) {
                        if(!preyAndDistanceAway.ContainsKey(gameObject)) {
                            preyAndDistanceAway.Add(gameObject, Mathf.Abs(Vector3.Distance(gameObject.transform.position, transform.position)));
                        }
                }
            }
        }
        // choose closest object
        GameObject closestPrey = null;
        float shortestDistance = float.MaxValue;
        foreach(var prey in preyAndDistanceAway) {
            if(prey.Value < shortestDistance) {
                closestPrey = prey.Key;
                shortestDistance = prey.Value;
            }
        }
        // return location
        return closestPrey;
    }
    public bool InVision(GameObject gameObject) {
        foreach((bool hit, RaycastHit hitInfo) in rays) {
            if(hitInfo.collider != null) {
                GameObject potentialGameObject = hitInfo.collider.gameObject;
                if(gameObject.Equals(gameObject)) {
                    return true;
                }
            }
        }
        return false;
    }
    public void FixRotation() {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
    public static Vector3 VectorFromAngle(float angle) {
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }
    private int FindRayAtLargestGapCenter(List<(bool hit, RaycastHit hitInfo)> rays) {
            // largest gap (size)
            int largestGap = -1;
            // index of first ray in largest gap
            int largestGapIndex = 0;
            // index of first ray in current gap
            int currentGapIndex = 0;
            for(int i = 0; i < rays.Count; i++) {
                if(rays[i].hit) {
                    if(i - currentGapIndex > largestGap) {
                        largestGap =  i - currentGapIndex;
                        largestGapIndex = currentGapIndex;
                    }
                    currentGapIndex = i;
                }
                // deals with a special case where the max is at the end 
                else if (i == rays.Count - 1 && i - currentGapIndex > largestGap){
                    largestGap =  i - currentGapIndex;
                    largestGapIndex = currentGapIndex;
                }
            }
            if(largestGap == -1) {
                // if every ray is colliding, just return the median ray (approx) so it continues straight
                return Mathf.FloorToInt((rays.Count - 1) / 2);
            }
            // this will be the correct ray
            int index = Mathf.FloorToInt(largestGapIndex + ((largestGap - 1) / 2));
            return index;
    }
}