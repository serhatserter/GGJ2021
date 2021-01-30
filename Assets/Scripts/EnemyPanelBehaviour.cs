using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPanelBehaviour : MonoBehaviour
{
    private GameObject player;
    private GameObject monster;
    private Image redPanel;

    float distanceZ;
    float startDistance;


    void Start()
    {
        player = GameManager.Instance.Player;
        monster = GameManager.Instance.Monster;
        redPanel = GetComponent<Image>();
    }

    void Update()
    {
        if (!GameManager.Instance.IsLose)
        {
            startDistance = player.transform.position.z - (monster.transform.position.z - 30f);

            distanceZ = Mathf.Clamp(100, 0, startDistance);

            redPanel.color = new Color(redPanel.color.r, redPanel.color.g, redPanel.color.b, (0.7f - (distanceZ / 100f)));

        }
    }
}
