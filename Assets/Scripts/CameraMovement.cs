using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float CamDistanceY = 5f;
    public float CamDistanceZ = 8f;
    private Vector3 camPosition;
    private float camPositionY;
    private float camPositionZ;

    void Start()
    {

    }


    void FixedUpdate()
    {
        camPositionY = GameManager.Instance.Player.transform.position.y + CamDistanceY;
        camPositionZ = GameManager.Instance.Player.transform.position.z - CamDistanceZ;
        camPosition = new Vector3(transform.position.x, camPositionY, camPositionZ);

        transform.position = Vector3.Lerp(transform.position, camPosition, 10f * Time.deltaTime);
    }
}
