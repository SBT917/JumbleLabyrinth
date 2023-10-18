using Aoiti.Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public interface IEnemyState
{
    void EnterState(Enemy enemy);
    void UpdateState();
    void ExitState();

}



#region Enemy States
public class IdleState : IEnemyState
{
    //...state specific methods
    public void EnterState(Enemy enemy)
    {
        Debug.Log("Idle");
    }

    public void ExitState()
    {

    }

    public List<Vector2Int> GetPathToDraw()
    {
        throw new NotImplementedException();
    }

    public void UpdateState()
    {

    }
}
public class ChasingState : IEnemyState
{
    // �o�H�T���̊Ԋu�i�b�j
    public float pathfindingInterval = 1f;

    // ���Ɍo�H�T�����s������
    protected float nextPathfindingTime = 0f;

    protected MonoBehaviour monoBehaviour;

    Coroutine followPathCoroutine;

    protected Tilemap map;

    protected GameObject target;

    protected GameObject enemy;

    public List<Vector2Int> pathToDraw = null;

    public ChasingState(MonoBehaviour monoBehaviour, GameObject target, Tilemap map, GameObject enemy)
    {
        this.monoBehaviour = monoBehaviour;
        this.target = target;
        this.map = map;
        this.enemy = enemy;
    }

    public void EnterState(Enemy enemy)
    {

    }

    public void ExitState()
    {
        if (followPathCoroutine != null)
        {
            monoBehaviour.StopCoroutine(followPathCoroutine);
        }
    }

    public virtual void UpdateState()
    {
        if (Time.time >= nextPathfindingTime)
        {
            Vector2Int goal = Vector2Int.FloorToInt((Vector2)(target.transform.position - map.transform.position));
            Pathfinding(goal);
            nextPathfindingTime = Time.time + pathfindingInterval;
        }


    }


    void Pathfinding(Vector2Int goal)
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
                if (IsWalkable(nextNode))
                {
                    result[nextNode] = 1; // �ړ��R�X�g�͈ꗥ1�Ƃ���
                }
            }

            return result;
        };

        // �p�X�t�@�C���_�[�̃C���X�^���X���쐬
        var pathfinder = new Pathfinder<Vector2Int>(HeuristicFunction, ConnectedNodesFunction);

        // �G�L�����N�^�[�̈ʒu�ƃv���C���[�̈ʒu���Z�����W�ɕϊ�
        Vector2Int start = Vector2Int.FloorToInt((Vector2)(enemy.transform.position - map.transform.position));



        // �o�H�T�������s
        bool pathFound = pathfinder.GenerateAstarPath(start, goal, out List<Vector2Int> path);


        // �o�H�����������ꍇ�A�G�L�����N�^�[�͂��̌o�H�ɉ����Ĉړ�
        if (pathFound)
        {
            pathToDraw = path;

            // �O�̃R���[�`��������΂�����~����
            if (followPathCoroutine != null)
            {
                monoBehaviour.StopCoroutine(followPathCoroutine);
            }
            // �V�����R���[�`�����J�n����
            followPathCoroutine = monoBehaviour.StartCoroutine(FollowPath(path));
        }

        bool IsWalkable(Vector2Int position)
        {
            // Tilemap����w�肳�ꂽ�ʒu�̃^�C�����擾
            TileBase tile = map.GetTile((Vector3Int)position);

            // �^�C�������݂��Ȃ���Έړ��\
            // �^�C�������݂���ꍇ�͈ړ��s�\
            return tile == null;
        }

        IEnumerator FollowPath(List<Vector2Int> path)
        {

            Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
            Animator animator = enemy.GetComponent<Animator>();

            if (path == null)
            {
                yield return new WaitForFixedUpdate();
            }
            foreach (Vector2Int position in path)
            {
                // Calculate a target position that is slightly inside the real target cell
                Vector2 targetPosition = (Vector2)position + new Vector2(0.5f, 0.5f) + (Vector2)map.transform.position; // Adjust this buffer as needed

                while (Vector2.Distance((Vector2)enemy.transform.position, targetPosition) > 0.05f)
                {
                    Vector2 newPosition = Vector2.MoveTowards(enemy.transform.position, targetPosition, enemy.GetComponent<Enemy>().speed * Time.deltaTime);
                    Vector2 direction = newPosition - (Vector2)enemy.transform.position;

                    animator.SetFloat("MoveX", direction.x);
                    animator.SetFloat("MoveY", direction.y);

                    enemy.transform.position = newPosition;
                    yield return new WaitForFixedUpdate();
                }
            }
        }
    }

    public List<Vector2Int> GetPathToDraw()
    {
        return pathToDraw;
    }
}

public class RangedEnemyChasingState : ChasingState
{
    public delegate void RaycastHitDelegate();
    public RaycastHitDelegate OnRaycastHit;

    Vector2 lastPosition;
    public Vector2 direction;

    public RangedEnemyChasingState(MonoBehaviour monoBehaviour, GameObject target, Tilemap map, GameObject enemy) : base(monoBehaviour, target, map, enemy)
    {
        this.monoBehaviour = monoBehaviour;
        this.target = target;
        this.map = map;
        this.enemy = enemy;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        Animator animator = enemy.GetComponent<Animator>();
        // �G�������Ă���������擾
        Vector2 direction = ((Vector2)enemy.transform.position - lastPosition).normalized;

        // ���C���[�}�X�N��Enemy���C���[�����O����
        int layerMask = ~LayerMask.GetMask("Enemy");

        RaycastHit2D hit = Physics2D.Raycast(enemy.transform.position, direction, Mathf.Infinity, layerMask);

        // ���C�����o�I�ɕ\������i�F�͐ԁA�\�����Ԃ�1�b�j
        Debug.DrawRay(enemy.transform.position, direction * 10, Color.red, 1.0f);

        // ���C�������ɐڐG�����ꍇ
        if (hit.collider != null)
        {
            // ���C���v���C���[�ɐڐG�����ꍇ
            if (hit.collider.gameObject == target)
            {
                // �f���Q�[�g��null�łȂ���΁A������Ăяo��
                OnRaycastHit?.Invoke();
            }
        }

        // ���݂̈ʒu��ۑ�
        lastPosition = enemy.transform.position;
    }
}
public abstract class AttackingState : IEnemyState
{
    protected GameObject enemy;
    protected IEnemyState previousState;
    protected float attackDuration;  // Example duration for attack animation

    public AttackingState(GameObject enemy, IEnemyState previousState,float attackDuration)
    {
        this.enemy = enemy;
        this.previousState = previousState;
        this.attackDuration = attackDuration;


        // Start returning to previous state after attack
        AfterEffect();
    }

    public virtual void EnterState(Enemy enemy)
    {

    }

    public virtual void ExitState()
    {

    }

    public  virtual void UpdateState()
    {

    }

    public void AfterEffect()
    {
        enemy.GetComponent<Enemy>().StartCoroutine(ReturnToPreviousState());
    }

    private IEnumerator ReturnToPreviousState()
    {
        // Wait for the attack animation to finish
        yield return new WaitForSeconds(attackDuration);

        // After attack animation, return to the previous state
        enemy.GetComponent<Enemy>().ChangeState(previousState);
    }
}

public class MeleeAttackingState : AttackingState
{

    public MeleeAttackingState(GameObject enemy, IEnemyState previousState, float attackDuration) : base(enemy, previousState, attackDuration)
    {
        this.enemy = enemy;
        this.previousState = previousState;
        this.attackDuration = attackDuration;

        AfterEffect();
    }

}

public class RangedAttackingState : AttackingState
{
    GameObject projectilePrefab;
    UnityEngine.Transform target;
    public Vector2 direction;

    string attackAudio;

    public RangedAttackingState(GameObject enemy, IEnemyState previousState, float attackDuration, GameObject projectilePrefab, Vector2 direction,string audioName)
                : base(enemy, previousState, attackDuration)
    {
        this.enemy = enemy;
        this.previousState = previousState;
        this.attackDuration = attackDuration;
        this.projectilePrefab = projectilePrefab;
        this.direction = direction;
        attackAudio = audioName;
        AfterEffect();
    }

    public override void EnterState(Enemy enemy)
    {
        base.EnterState(enemy);
        
        enemy.StartCoroutine(ShootProjectile(enemy));
    }

    IEnumerator ShootProjectile(Enemy enemy)
    {
        yield return new WaitForSeconds(0.5f);

        // ���˕��̏����ʒu������o��
        Vector3 spawnPosition = enemy.transform.position;

        // �A�j���[�^�[����������擾
        Animator animator = enemy.GetComponent<Animator>();
        Vector2 direction = new Vector2(animator.GetFloat("MoveX"), animator.GetFloat("MoveY"));

        // ����o�����ʒu�ɔ��˕��𐶐�����
        GameObject projectile = UnityEngine.Object.Instantiate(projectilePrefab, spawnPosition, Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));

        AudioManager.instance.PlaySE(attackAudio);
        // ���˕��̉�]�����킹��
        EnemyProjectile projectileScript = projectile.GetComponent<EnemyProjectile>();
        if (projectileScript != null)
        {
            projectileScript.SetDirection(direction);
        }
    }
}

public class MazeWalkState : IEnemyState
{
    private GameObject enemy;
    private Tilemap map;
    private Vector2Int currentDirection;
    private Vector3 targetPosition;
    private Animator animator;

    public MazeWalkState(GameObject enemy, Tilemap map)
    {
        this.enemy = enemy;
        this.map = map;
        targetPosition = enemy.transform.position;
        animator = enemy.GetComponent<Animator>();
        // Choose a random initial direction
        List<Vector2Int> directions = new List<Vector2Int> { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
        currentDirection = directions[UnityEngine.Random.Range(0, directions.Count)];
    }

    public void EnterState(Enemy enemy)
    {

    }

    public void ExitState()
    {

    }


    public void UpdateState()
    {
        if ((Vector3)enemy.transform.position == targetPosition)
        {
            Vector3Int cellPosition = map.WorldToCell(enemy.transform.position);
            Vector2Int position = new Vector2Int(cellPosition.x, cellPosition.y);

            Vector2Int left = TurnLeft(currentDirection);

            if (!IsWall(position + left))  // If there's no wall to the left
            {
                currentDirection = left;  // Turn left
            }
            else if (IsWall(position + currentDirection))  // If there's a wall straight ahead
            {
                currentDirection = !IsWall(position + TurnRight(currentDirection)) ? TurnRight(currentDirection) : TurnAround(currentDirection);
            }

            targetPosition = map.GetCellCenterWorld(cellPosition + (Vector3Int)currentDirection);
        }

        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, targetPosition, enemy.GetComponent<Enemy>().speed * Time.deltaTime);
        animator.SetFloat("MoveX", currentDirection.x);
        animator.SetFloat("MoveY", currentDirection.y);

    }

    private Vector2Int TurnLeft(Vector2Int direction)
    {
        return new Vector2Int(-direction.y, direction.x);
    }

    private Vector2Int TurnRight(Vector2Int direction)
    {
        return new Vector2Int(direction.y, -direction.x);
    }

    private Vector2Int TurnAround(Vector2Int direction)
    {
        return new Vector2Int(-direction.x, -direction.y);
    }

    private bool IsWall(Vector2Int position)
    {
        return map.HasTile((Vector3Int)position);
    }
}

public class KnockbackState : IEnemyState
{
    private GameObject enemy;
    protected IEnemyState previousState;
    private Vector3 direction;
    private float speed;
    private float knockbackTime;
    private float timer;

    public KnockbackState(GameObject enemy,IEnemyState enemyState, Vector3 direction, float speed, float knockbackTime)
    {
        this.enemy = enemy;
        this.previousState = enemyState;
        this.direction = direction;
        this.speed = speed;
        this.knockbackTime = knockbackTime;

        AfterEffect();
    }

    public void EnterState()
    {
        timer = 0;
    }

    public void UpdateState()
    {
        // Move enemy in the knockback direction
        enemy.transform.position += direction * speed * Time.deltaTime;

        // Increment timer
        timer += Time.deltaTime;

        // Check if knockback time has ended
        if (timer >= knockbackTime)
        {
            enemy.GetComponent<Enemy>().ChangeState(new IdleState());
        }
    }

    public void ExitState()
    {

    }

    public void EnterState(Enemy enemy)
    {
        
    }

    public void AfterEffect()
    {
        enemy.GetComponent<Enemy>().StartCoroutine(ReturnToPreviousState());
    }

    private IEnumerator ReturnToPreviousState()
    {
        // Wait for the attack animation to finish
        yield return new WaitForSeconds(knockbackTime);

        // After attack animation, return to the previous state
        enemy.GetComponent<Enemy>().ChangeState(previousState);
    }

}


#endregion
