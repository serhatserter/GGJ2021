using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerRb;
    private float playerMoveZSpeed;

    private Vector3 screenPosition;
    private Vector2 worldPosition;
    private Vector3 PlayerSidePosition;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerMoveZSpeed = GameManager.Instance.PlayerMoveZSpeed;
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
        }
    }

    void PlayerSideMovement()
    {
        screenPosition = Input.mousePosition;
        screenPosition.z = 20f;
        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        PlayerSidePosition = new Vector3(worldPosition.x, transform.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, PlayerSidePosition, 10f * Time.deltaTime);
    }

    void PlayerForwardMovement()
    {
        playerRb.velocity = new Vector3(playerRb.velocity.x, playerRb.velocity.y, playerMoveZSpeed);
    }

    void PlayerStopMovement()
    {
        playerRb.velocity = new Vector3(playerRb.velocity.x, playerRb.velocity.y, 0);
    }


}
