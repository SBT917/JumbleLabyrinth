using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTest : MonoBehaviour
{
    [SerializeField]
    private TriggerEvent triggerEvent;

    private Rigidbody2D rb;

    private Transform target;

    [SerializeField]
    float fieldOfView = 60.0f; // ����p��ݒ�

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private Text text;

    // waypoints��ǉ����܂�
    [SerializeField]
    private Transform[] waypoints;
    private int currentWaypointIndex = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        triggerEvent.onTriggerEnter += OnTriggerEnterFunction;
        triggerEvent.onTriggerExit += OnTriggerExitFunction;
        ResetTarget();
    }

    void Update()
    {
        if (target != null)
        {
            Vector2 playerPos = target.position;
            Vector2 enemyPos = this.transform.position;

            Vector2 toPlayer = (playerPos - enemyPos).normalized; // �G����v���C���[�ւ̕����x�N�g��
            float angle = Vector2.Angle(transform.up, toPlayer); // �G�̌����ƃv���C���[�̕����̊p�x���v�Z

            if (angle <= fieldOfView / 2)
            {
                Debug.Log("Player in sight!");

                Vector2 direction = (playerPos - enemyPos).normalized;
                rb.velocity = direction * moveSpeed;
                transform.up = direction;
            }
        }
        else
        {
            // �^�[�Q�b�g�����Ȃ��ꍇ�Awaypoint�����񂷂�
            Patrol();
        }
    }

    private void Patrol()
    {
        if (waypoints.Length > 0)
        {
            Vector2 nextWaypointPos = waypoints[currentWaypointIndex].position;
            Vector2 enemyPos = this.transform.position;

            Vector2 direction = (nextWaypointPos - enemyPos).normalized;

            // Update velocity to move towards waypoint
            rb.velocity = direction * moveSpeed;
            transform.up = direction;

            // If the enemy is close to the waypoint, move to the next waypoint
            if (Vector2.Distance(enemyPos, nextWaypointPos) <= 0.1f)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            }
        }
    }


    private void SetTarget(Transform target)
    {
        this.target = target;
        text.text = "target:" + target.name;
    }

    private void ResetTarget()
    {
        target = null;
        text.text = "target:" + "null";
    }

    private void OnTriggerEnterFunction(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SetTarget(collision.gameObject.transform);
        }
    }

    private void OnTriggerExitFunction(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ResetTarget();
        }
    }
}

