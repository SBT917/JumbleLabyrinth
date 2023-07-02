using Aoiti.Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using static UnityEngine.EventSystems.EventTrigger;


public class EnemyTest : MonoBehaviour
{
    [SerializeField]
    private GameObject player;


    [SerializeField]
    private float moveSpeed;


    // �o�H�T���̊Ԋu�i�b�j
    public float pathfindingInterval = 1f;

    // ���Ɍo�H�T�����s������
    private float nextPathfindingTime = 0f;


    public Tilemap map;  // �����Unity��Inspector����ݒ肵�܂�

    Vector2Int previousPlayerPosition;
    float someThreshold = 1f;  // �v���C���[��1�ȏ㓮������o�H���Čv�Z

    Coroutine followPathCoroutine; // �V���ɃR���[�`����ǉ�

    void Update()
    {
        Vector2Int currentPlayerPosition = Vector2Int.FloorToInt(player.transform.position);
        if (Vector2Int.Distance(previousPlayerPosition, currentPlayerPosition) > someThreshold)
        {
            if (Time.time >= nextPathfindingTime)
            {
                Pathfinding();
                nextPathfindingTime = Time.time + pathfindingInterval;
            }
        }
        previousPlayerPosition = currentPlayerPosition;
    }


    private bool IsWalkable(Vector2Int position)
    {
        // Tilemap����w�肳�ꂽ�ʒu�̃^�C�����擾
        TileBase tile = map.GetTile((Vector3Int)position);

        // �^�C�������݂��Ȃ���Έړ��\�i�܂�true��Ԃ��j
        // �^�C�������݂���ꍇ�͈ړ��s�\�i�܂�false��Ԃ��j
        return tile == null;
    }

    void Pathfinding()
    {
        // ���[�N���b�h�������v�Z����Heuristic�֐�
        Func<Vector2Int, Vector2Int, float> HeuristicFunction = (node1, node2) =>
        {
            return Vector2Int.Distance(node1, node2);
        };

        // �ڑ��m�[�h���擾����ConnectedNodes�֐�
        Func<Vector2Int, Dictionary<Vector2Int, float>> ConnectedNodesFunction = (node) =>
        {
            var result = new Dictionary<Vector2Int, float>();

            // �㉺���E�̃Z���𒲂ׂ�
            var directions = new Vector2Int[]
            {
        new Vector2Int(0, 1),
        new Vector2Int(0, -1),
        new Vector2Int(1, 0),
        new Vector2Int(-1, 0)
            };
            foreach (var direction in directions)
            {
                var nextNode = node + direction;
                if (IsWalkable(nextNode)) // IsWalkable�͂��̃Z�����ړ��\�����`�F�b�N����֐�
                {
                    result[nextNode] = 1; // �ړ��R�X�g�͈ꗥ1�Ƃ���
                }
            }

            return result;
        };

        // �p�X�t�@�C���_�[�̃C���X�^���X���쐬
        var pathfinder = new Pathfinder<Vector2Int>(HeuristicFunction, ConnectedNodesFunction);

        // �G�L�����N�^�[�̈ʒu�ƃv���C���[�̈ʒu���Z�����W�ɕϊ����܂�
        // ����͎��ۂ̃Q�[���̃V�`���G�[�V�����ɂ���ĕς��\��������܂�
        Vector2Int start = Vector2Int.FloorToInt(transform.position);
        Vector2Int goal = Vector2Int.FloorToInt(player.transform.position);

        // �o�H�T�������s���܂�
        bool pathFound = pathfinder.GenerateAstarPath(start, goal, out List<Vector2Int> path);

        // �o�H�����������ꍇ�A�G�L�����N�^�[�͂��̌o�H�ɉ����Ĉړ����܂�
        if (pathFound)
        {
            Debug.Log("Path found!");
            // �O�̃R���[�`��������΂�����~����
            if (followPathCoroutine != null)
            {
                StopCoroutine(followPathCoroutine);
            }
            // �V�����R���[�`�����J�n����
            followPathCoroutine = StartCoroutine(FollowPath(path));
        }

        // �o�H�ɉ����Ĉړ�����R���[�`��
        IEnumerator FollowPath(List<Vector2Int> path)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();  // Rigidbody2D�R���|�[�l���g���擾���܂�

            foreach (Vector2Int position in path)
            {
                Vector2 targetPosition = (Vector2)position;
                while (Vector2.Distance((Vector2)transform.position, targetPosition) > 0.05f)  // 0.05�͋��e����G���[�͈̔͂ł�
                {
                    Vector2 newPosition = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);  // ���݂̈ʒu����ڕW�ʒu�Ɍ������Ĉړ����܂�
                    transform.position = newPosition;  // Transform�̈ʒu���X�V���܂�

                    yield return new WaitForFixedUpdate();  // FixedUpdate�Ԋu�Ŏ��s���܂��i�����X�V�ɍ��킹�āj
                }
            }
        }

    }
}

