using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rigidbody2D;
    [SerializeField] private float movementSpeed;

    private void Start()
    {
        this.rigidbody2D = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MoveHorizontal(InputController.Instance.GetInputHorizontal);
    }

    private void MoveHorizontal(float horizontalDirection)
    {
        this.rigidbody2D.velocity = new Vector2(horizontalDirection * movementSpeed, this.rigidbody2D.velocity.y);
        Debug.Log("moving: " + horizontalDirection);
    } 
}
