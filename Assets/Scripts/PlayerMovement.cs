using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D TankRigidBody_P1, TankRigidBody_P2;
    public float HorizontalInput_P1 = 0, HorizontalInput_P2 = 0;
    public float VerticalInput_P1 = 0, VerticalInput_P2 = 0;
    public float MovementSpeed = 0.0f;
    public float RotationSpeed = 0.0f;

    void Start()
    {
        TankRigidBody_P1 = GetComponent<Rigidbody2D>();
        if (!MainMenu.IsGameOnePlayer)
            TankRigidBody_P2 = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        getPlayerInput();
    }

    void Update()
    {
        movePlayer("TankPlayer1", VerticalInput_P1, TankRigidBody_P1);
        rotatePlayer("TankPlayer1", HorizontalInput_P1, TankRigidBody_P1);
        TankRigidBody_P1.freezeRotation = true;
        if(!MainMenu.IsGameOnePlayer)
        {
            movePlayer("TankPlayer2", VerticalInput_P2, TankRigidBody_P2);
            rotatePlayer("TankPlayer2", HorizontalInput_P2, TankRigidBody_P2);
            TankRigidBody_P2.freezeRotation = true;
        }
    }

    private void getPlayerInput()
    {
        HorizontalInput_P1 = Input.GetAxisRaw("Horizontal_P1");
        VerticalInput_P1 = Input.GetAxisRaw("Vertical_P1");
        if (!MainMenu.IsGameOnePlayer)
        {
            HorizontalInput_P2 = Input.GetAxisRaw("Horizontal_P2");
            VerticalInput_P2 = Input.GetAxisRaw("Vertical_P2");
        }
    }

    private void movePlayer(string tankTag, float verticalInput, Rigidbody2D tankRigidBody)
    {
        if (gameObject.tag == tankTag && verticalInput == 1)
            tankRigidBody.velocity = transform.up * Mathf.Clamp01(verticalInput) * MovementSpeed;
        if (gameObject.tag == tankTag && verticalInput == -1)
            tankRigidBody.velocity = transform.up * Mathf.Clamp01(-verticalInput) * -MovementSpeed;
        if (gameObject.tag == tankTag && verticalInput == 0)
            tankRigidBody.velocity = new Vector2(0, 0);
    }

    private void rotatePlayer(string tankTag, float horizontalInput, Rigidbody2D tankRigidBody)
    {
        float rotation = -horizontalInput * RotationSpeed;
        if (gameObject.tag == tankTag)
            tankRigidBody.transform.Rotate(Vector3.forward * rotation);
    }
}
