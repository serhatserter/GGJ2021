using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerRb;
    private float playerMoveZSpeed;

    private Vector3 screenPosition;
    private Vector2 worldPosition;
    private Vector3 playerSidePosition;

    private Vector3 jumpForceVector;
    private Vector3 jumpPointPosition;

    private bool isJumping;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerMoveZSpeed = GameManager.Instance.PlayerMoveZSpeed;
        jumpForceVector = new Vector3(0, 1, 2);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

        }
        else if (Input.GetMouseButton(0))
        {
            PlayerForwardMovement();
            PlayerSideMovement();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            PlayerStopMovement();
            PlayerJump();
        }
    }

    void PlayerSideMovement()
    {
        screenPosition = Input.mousePosition;
        screenPosition.z = 20f;
        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        playerSidePosition = new Vector3(worldPosition.x, transform.position.y, transform.position.z);

        if (!isJumping)
        {
            transform.position = Vector3.Lerp(transform.position, playerSidePosition, 10f * Time.deltaTime);

        }
    }

    void PlayerForwardMovement()
    {
        playerRb.velocity = new Vector3(playerRb.velocity.x, playerRb.velocity.y, playerMoveZSpeed);
    }

    void PlayerStopMovement()
    {
        if (!isJumping)
        {
            playerRb.velocity = new Vector3(playerRb.velocity.x, playerRb.velocity.y, 0);

        }
    }

    void PlayerJump()
    {
        if (!isJumping)
        {
            if (transform.position.z > jumpPointPosition.z)
            {

                playerRb.AddForce(jumpForceVector * 350f);

            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Platform")
        {
            jumpPointPosition = collision.transform.GetChild(0).position;
            isJumping = false;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        isJumping = true;
    }


}
