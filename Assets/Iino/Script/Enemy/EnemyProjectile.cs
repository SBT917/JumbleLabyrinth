using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyProjectile : MonoBehaviour
{
    private Rigidbody2D rb;
    
    public float speed = 10f;

    private Vector2 direction;

    [SerializeField]
    private GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            if (collision.transform.TryGetComponent(out IKnockBackable knockBackable))
            {
                Vector2 dir = collision.transform.position - transform.position;
                knockBackable.StartKnockBack(dir, 10f, 0.1f);
            }

            if (collision.transform.TryGetComponent(out IStanable stanable))
            {
                stanable.StartStan(1f);
            }
        }

        if (explosion != null)
        {
            //îöî≠Çê∂ê¨Ç∑ÇÈ
            GameObject Explosion = Instantiate(explosion, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        rb.velocity = direction.normalized * speed;
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
    }
}
