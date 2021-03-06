﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

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

    private Vector3 lastPlatformPos;
    private Vector3 rotateVec;

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
        if (GameManager.Instance.IsStart)
        {
            if (Input.GetMouseButtonDown(0))
            {


                if (isTouching)
                {

                    GameManager.Instance.IsPlatformInvisible?.Invoke(true);
                }

            }
            else if (Input.GetMouseButton(0))
            {



                if (isTouching)
                {

                    GameManager.Instance.IsPlatformInvisible?.Invoke(true);
                }

                PlayerSideMovement();
                PlayerForwardMovement();
            }
            else if (Input.GetMouseButtonUp(0))
            {


                GameManager.Instance.PlayerAnimator.SetBool("isRun", false);

                if (isTouching)
                {
                    GameManager.Instance.IsPlatformInvisible?.Invoke(false);


                }

                PlayerStopMovement();
                PlayerJump();


            }

            CheckFall();

        }

    }


    void CheckFall()
    {

        if (transform.position.y < -3f)
        {
            lastPlatformPos = GameManager.Instance.LastTouchedPlatform.transform.position;
            transform.position = new Vector3(lastPlatformPos.x, lastPlatformPos.y + 5f, lastPlatformPos.z - 1f);
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
        GameManager.Instance.PlayerAnimator.SetBool("isRun", true);
        playerRb.velocity = new Vector3(playerRb.velocity.x, playerRb.velocity.y, playerMoveZSpeed);

        //GetComponent<AudioSource>().clip = SoundManager.Instance.AudioClips[2];
        //GetComponent<AudioSource>().loop = true;
        //GetComponent<AudioSource>().Play();
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
                GameManager.Instance.PlayerAnimator.SetBool("isJump", true);
                playerRb.AddForce(jumpForceVector * 250f);
                GameManager.Instance.IsPlatformInvisible?.Invoke(true);

                GetComponent<AudioSource>().clip = SoundManager.Instance.AudioClips[3];
                GetComponent<AudioSource>().Play();

                isJumping = true;
            }



        }
    }

    IEnumerator WinWait()
    {
        playerRb.isKinematic = true;
        GameManager.Instance.Monster.SetActive(false);
        GameManager.Instance.IsStart = false;

        rotateVec = transform.eulerAngles;
        rotateVec.y -= 180;

        transform.DORotate(rotateVec, 0.2f).OnComplete(() =>
        {
            GameManager.Instance.PlayerAnimator.SetBool("isWin", true);
        });

        yield return new WaitForSeconds(4f);

        GameManager.Instance.WinPanel.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Platform")
        {


            GameManager.Instance.LastTouchedPlatform = collision.gameObject;

            jumpPointPosition = collision.transform.GetChild(0).position;

            GameManager.Instance.PlayerAnimator.SetBool("isJump", false);
            isJumping = false;

            if (!Input.GetMouseButton(0)) GameManager.Instance.IsPlatformInvisible?.Invoke(false);

        }

        if (collision.transform.tag == "LastPlatform")
        {
            Debug.Log("WIN");

            StartCoroutine(WinWait());

        }

    }

    private void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.tag == "Platform")
        {
            //GameManager.Instance.PlayerAnimator.SetBool("isJump", true);


        }
        isTouching = false;


    }

    private void OnCollisionStay(Collision collision)
    {
        isTouching = true;

    }


}
