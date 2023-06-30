using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//敵AIをテストするための仮のプレイヤー移動
public class PlayerMovement : MonoBehaviour
{

    private Vector2 input;

    private Rigidbody2D rb;

    [SerializeField]
    private float moveSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = input.normalized * moveSpeed;
    }


    public void OnMove(InputAction.CallbackContext _context)
    {
        input = _context.ReadValue<Vector2>();
    }
}
