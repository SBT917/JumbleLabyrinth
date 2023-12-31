using UnityEngine;

public class FieldOfViewAngle : MonoBehaviour
{
    public float viewAngle; // ìp
    public float viewDistance; // ì£

    private void OnDrawGizmos()
    {
        // CÌJnn_ð©gÌÊuÆ·é
        Vector3 origin = transform.position;

        // ìpÌ¼ªðvZ
        float halfAngle = viewAngle * 0.5f;

        // ¶¤ÌìpðvZ
        Vector3 leftRayDirection = Quaternion.Euler(0, 0, -halfAngle) * transform.up;
        // E¤ÌìpðvZ
        Vector3 rightRayDirection = Quaternion.Euler(0, 0, halfAngle) * transform.up;

        // ìpÌÍÍðGizmoÅ`æ
        Gizmos.color = Color.red;
        Gizmos.DrawRay(origin, leftRayDirection * viewDistance);
        Gizmos.DrawRay(origin, rightRayDirection * viewDistance);

        // GÌü«ð¦·Gizmo
        Gizmos.color = Color.green;
        Gizmos.DrawRay(origin, transform.up * viewDistance);
    }
}

