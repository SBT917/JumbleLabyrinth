using UnityEngine;

public class FieldOfViewAngle : MonoBehaviour
{
    public float viewAngle; // ‹–ìŠp
    public float viewDistance; // ‹–ì‹——£

    private void OnDrawGizmos()
    {
        // ƒŒƒC‚ÌŠJn’n“_‚ğ©g‚ÌˆÊ’u‚Æ‚·‚é
        Vector3 origin = transform.position;

        // ‹–ìŠp‚Ì”¼•ª‚ğŒvZ
        float halfAngle = viewAngle * 0.5f;

        // ¶‘¤‚Ì‹–ìŠp‚ğŒvZ
        Vector3 leftRayDirection = Quaternion.Euler(0, 0, -halfAngle) * transform.up;
        // ‰E‘¤‚Ì‹–ìŠp‚ğŒvZ
        Vector3 rightRayDirection = Quaternion.Euler(0, 0, halfAngle) * transform.up;

        // ‹–ìŠp‚Ì”ÍˆÍ‚ğGizmo‚Å•`‰æ
        Gizmos.color = Color.red;
        Gizmos.DrawRay(origin, leftRayDirection * viewDistance);
        Gizmos.DrawRay(origin, rightRayDirection * viewDistance);

        // “G‚ÌŒü‚«‚ğ¦‚·Gizmo
        Gizmos.color = Color.green;
        Gizmos.DrawRay(origin, transform.up * viewDistance);
    }
}

