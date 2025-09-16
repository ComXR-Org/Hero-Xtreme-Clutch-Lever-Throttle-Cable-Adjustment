using UnityEngine;

public class CurvedLinePoint : MonoBehaviour {

    public float gizmoSize = 0.05f;
    public Color gizmoColor = new Color(1, 0, 0, 0.5f);

    void OnDrawGizmos() {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(this.transform.position, gizmoSize);
    }
}
