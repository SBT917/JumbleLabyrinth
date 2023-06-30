using UnityEngine;

public class FieldOfViewAngle : MonoBehaviour
{
    public float viewAngle; // ����p
    public float viewDistance; // ���싗��

    private void OnDrawGizmos()
    {
        // ���C�̊J�n�n�_�����g�̈ʒu�Ƃ���
        Vector3 origin = transform.position;

        // ����p�̔������v�Z
        float halfAngle = viewAngle * 0.5f;

        // �����̎���p���v�Z
        Vector3 leftRayDirection = Quaternion.Euler(0, 0, -halfAngle) * transform.up;
        // �E���̎���p���v�Z
        Vector3 rightRayDirection = Quaternion.Euler(0, 0, halfAngle) * transform.up;

        // ����p�͈̔͂�Gizmo�ŕ`��
        Gizmos.color = Color.red;
        Gizmos.DrawRay(origin, leftRayDirection * viewDistance);
        Gizmos.DrawRay(origin, rightRayDirection * viewDistance);

        // �G�̌���������Gizmo
        Gizmos.color = Color.green;
        Gizmos.DrawRay(origin, transform.up * viewDistance);
    }
}

