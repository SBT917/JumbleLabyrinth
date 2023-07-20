using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FollowCamera : MonoBehaviour
{
    public Transform target; // �J�������Ǐ]����Ώہi�v���C���[�L�����N�^�[�Ȃǁj
    public Vector2 minBounds; // �J�����̐����͈͂̍������W
    public Vector2 maxBounds; // �J�����̐����͈͂̉E����W

    private Camera mainCamera;
    private float cameraHalfWidth;
    private float cameraHalfHeight;

    private void Start()
    {
        mainCamera = GetComponent<Camera>();
        cameraHalfHeight = mainCamera.orthographicSize;
        cameraHalfWidth = cameraHalfHeight * mainCamera.aspect;
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("�J�����̒Ǐ]�Ώۂ��ݒ肳��Ă��܂���B");
            return;
        }

        float clampedX = Mathf.Clamp(target.position.x, (minBounds.x / 2 + 2) + cameraHalfWidth, (maxBounds.x / 2 + 1) - cameraHalfWidth);
        float clampedY = Mathf.Clamp(target.position.y, (minBounds.y / 2 + 2) + cameraHalfHeight, (maxBounds.y / 2 + 1) - cameraHalfHeight);

        // �J�����𐧌��͈͓��Ɉړ�
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

}
