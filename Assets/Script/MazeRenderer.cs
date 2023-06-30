using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRenderer : MonoBehaviour
{
    [SerializeField]
    [Range(1, 50)]
    private int width = 10;

    [SerializeField]
    private float size = 1f;

    [SerializeField]
    private Transform wallPrefab = null;

    [SerializeField]
    private Transform floorPrefab = null;

    // Start is called before the first frame update
    void Start()
    {
        var maze = MazeGenerator.Generate(width, width); // Generate a square maze
        Draw(maze);
    }

    private void Draw(WallState[,] maze)
    {
        var floor = Instantiate(floorPrefab, transform);
        floor.localScale = new Vector3(width, 1, width);

        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < width; ++j)
            {
                var cell = maze[i, j];
                var position = new Vector3(i * size, 0, j * size);

                if (cell.HasFlag(WallState.UP))
                {
                    var topWall = Instantiate(wallPrefab, transform) as Transform;
                    topWall.position = position + new Vector3(size / 2, 0, size);
                    topWall.localScale = new Vector3(size, topWall.localScale.y, topWall.localScale.z);
                }

                if (cell.HasFlag(WallState.LEFT))
                {
                    var leftWall = Instantiate(wallPrefab, transform) as Transform;
                    leftWall.position = position + new Vector3(0, 0, size / 2);
                    leftWall.localScale = new Vector3(size, leftWall.localScale.y, leftWall.localScale.z);
                    leftWall.eulerAngles = new Vector3(0, 90, 0);
                }

                if (cell.HasFlag(WallState.RIGHT))
                {
                    var rightWall = Instantiate(wallPrefab, transform) as Transform;
                    rightWall.position = position + new Vector3(size, 0, size / 2);
                    rightWall.localScale = new Vector3(size, rightWall.localScale.y, rightWall.localScale.z);
                    rightWall.eulerAngles = new Vector3(0, 90, 0);
                }

                if (cell.HasFlag(WallState.DOWN))
                {
                    var bottomWall = Instantiate(wallPrefab, transform) as Transform;
                    bottomWall.position = position + new Vector3(size / 2, 0, 0);
                    bottomWall.localScale = new Vector3(size, bottomWall.localScale.y, bottomWall.localScale.z);
                }
            }
        }
    }
}