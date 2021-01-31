using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public float MonsterMoveZSpeed;
    private Rigidbody monsterRb;

    public GameObject StartCam;
    public GameObject LoseCam;

    bool isFalling;
    private void Start()
    {
        monsterRb = GetComponent<Rigidbody>();
        MonsterMoveZSpeed = GameManager.Instance.MonsterMoveZSpeed;


        StartCoroutine(WaitStart());
    }

    IEnumerator WaitStart()
    {
        yield return new WaitForSeconds(2f);
        StartCam.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.IsStart = true;

    }

    private void Update()
    {
        MonsterForwardMovement();
        MonsterSideMovement();
    }

    void MonsterSideMovement()
    {
        transform.position = new Vector3(GameManager.Instance.Player.transform.position.x, transform.position.y, transform.position.z);
    }

    void MonsterForwardMovement()
    {
        if (transform.position.z < GameManager.Instance.Player.transform.position.z)
        {
            monsterRb.velocity = new Vector3(monsterRb.velocity.x, monsterRb.velocity.y, MonsterMoveZSpeed);

        }
        else
        {
            isFalling = true;
            monsterRb.velocity = Vector3.zero;

            GameManager.Instance.FailPanel.SetActive(true);

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "Player" && !isFalling)
        {
            GetComponent<AudioSource>().Play();
            collision.gameObject.GetComponent<AudioSource>().clip = SoundManager.Instance.AudioClips[1];
            collision.gameObject.GetComponent<AudioSource>().Play();

            GameManager.Instance.MonsterAnimator.SetBool("isFail", true);
            GameManager.Instance.PlayerAnimator.SetBool("isFall", true);

            StartCoroutine(LoseCamWait());
        }
    }

    IEnumerator LoseCamWait()
    {
        GameManager.Instance.Player.GetComponent<Rigidbody>().isKinematic = true;
        GameManager.Instance.IsStart = false;
        StartCam.transform.position = Camera.main.transform.position;
        StartCam.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        LoseCam.SetActive(true);
        yield return new WaitForSeconds(6f);
        GameManager.Instance.FailPanel.SetActive(true);

    }
}
