using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public float MonsterMoveZSpeed;
    private Rigidbody monsterRb;
    private GameObject Vcam;

    private void Start()
    {
        monsterRb = GetComponent<Rigidbody>();
        MonsterMoveZSpeed = GameManager.Instance.MonsterMoveZSpeed;
        Vcam = transform.GetChild(0).gameObject;

        StartCoroutine(WaitStart());
    }

    IEnumerator WaitStart()
    {
        yield return new WaitForSeconds(2f);
        Vcam.SetActive(false);
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
        monsterRb.velocity = new Vector3(monsterRb.velocity.x, monsterRb.velocity.y, MonsterMoveZSpeed);

    }
}
