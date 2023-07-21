using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FollowCamera : MonoBehaviour
{
    public Transform target; // カメラが追従する対象（プレイヤーキャラクターなど）
    public Vector2 minBounds; // カメラの制限範囲の左下座標
    public Vector2 maxBounds; // カメラの制限範囲の右上座標

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
            Debug.LogWarning("カメラの追従対象が設定されていません。");
            return;
        }

        float clampedX = Mathf.Clamp(target.position.x, (minBounds.x / 2 + 2) + cameraHalfWidth, (maxBounds.x / 2 + 1) - cameraHalfWidth);
        float clampedY = Mathf.Clamp(target.position.y, (minBounds.y / 2 + 2) + cameraHalfHeight, (maxBounds.y / 2 + 1) - cameraHalfHeight);

        // カメラを制限範囲内に移動
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

}
