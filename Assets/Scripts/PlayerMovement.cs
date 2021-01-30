using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Transform StartJumpPos;
    private Rigidbody playerRb;
    private float playerMoveZSpeed;

    private Vector3 screenPosition;
    private Vector2 worldPosition;
    private Vector3 playerSidePosition;

    private Vector3 jumpForceVector;
    private Vector3 jumpPointPosition;

    private bool isJumping;
    private bool isTouching;

    void Start()
    {
        jumpPointPosition = StartJumpPos.position;
        playerRb = GetComponent<Rigidbody>();
        playerMoveZSpeed = GameManager.Instance.PlayerMoveZSpeed;
        jumpForceVector = new Vector3(0, 1, 2);
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (isTouching) GameManager.Instance.IsPlatformInvisible?.Invoke(true);

        }
        else if (Input.GetMouseButton(0))
        {
            if (isTouching) GameManager.Instance.IsPlatformInvisible?.Invoke(true);

            PlayerSideMovement();
            PlayerForwardMovement();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (isTouching) GameManager.Instance.IsPlatformInvisible?.Invoke(false);

            PlayerStopMovement();
            PlayerJump();


        }

        CheckFall();

    }


    void CheckFall()
    {
        if (transform.position.y < -2f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void PlayerSideMovement()
    {

        screenPosition = Input.mousePosition;
        screenPosition.z = 20f;
        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        playerSidePosition = new Vector3(worldPosition.x, transform.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, playerSidePosition, 10f * Time.deltaTime);



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

                playerRb.AddForce(jumpForceVector * 250f);
                GameManager.Instance.IsPlatformInvisible?.Invoke(true);
                isJumping = true;
            }



        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Platform")
        {
            Debug.Log("Platform Exit added");
            jumpPointPosition = collision.transform.GetChild(0).position;
            isJumping = false;

            if (!Input.GetMouseButton(0)) GameManager.Instance.IsPlatformInvisible?.Invoke(false);

        }

    }

    private void OnCollisionExit(Collision collision)
    {

        
        isTouching = false;


    }

    private void OnCollisionStay(Collision collision)
    {
        isTouching = true;

    }


}
