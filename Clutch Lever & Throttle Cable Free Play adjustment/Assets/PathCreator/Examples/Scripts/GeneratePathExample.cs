using UnityEngine;

namespace PathCreation.Examples {
    // Example of creating a path at runtime from a set of points.
    
    [ExecuteInEditMode]
    [RequireComponent(typeof(PathCreator))]
    public class GeneratePathExample : MonoBehaviour {

        public bool closedLoop = true;
        public float controlSpacing = 0.01f;
        public Transform[] waypoints;

        void Update () {
            if (waypoints.Length > 0) {
                // Create a new bezier path from the waypoints.
                BezierPath bezierPath = new BezierPath (waypoints, closedLoop, PathSpace.xyz);
                bezierPath.AutoControlLength = controlSpacing;
                GetComponent<PathCreator> ().bezierPath = bezierPath;
            }
        }
    }
}