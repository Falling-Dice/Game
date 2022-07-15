using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class playerMovement : MonoBehaviour
{

    public Rigidbody playerRigibody;
    public Vector2 move;
    public float moveSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        //playerRigibody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame

    public void getMove(InputAction.CallbackContext context)
    {
        Debug.Log("Connard");

        this.move = context.ReadValue<Vector2>();

    }


    void FixedUpdate()
    {
        //Debug.Log("pute");

        if (this.move != Vector2.zero)
        {
            Debug.Log("salope");
            this.playerRigibody.MovePosition(transform.position + new Vector3(this.move.x, 0, this.move.y) * Time.deltaTime * moveSpeed);
            Debug.Log((Vector3)this.move);
        }
    }
}
