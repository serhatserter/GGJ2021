using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCreator : MonoBehaviour
{
    public int PlatformCount;
    public Transform StartPoint;
    public GameObject[] Platforms;

    private void Start()
    {
        CreatePlatforms();
    }

    void CreatePlatforms()
    {
        Vector3 prePlatformPos = StartPoint.position;
        for (int i = 0; i < PlatformCount; i++)
        {
            GameObject newPlatform = Instantiate(Platforms[Random.Range(0, Platforms.Length)]);
            newPlatform.transform.position = new Vector3(prePlatformPos.x, prePlatformPos.y, prePlatformPos.z + 25f);

            prePlatformPos = newPlatform.transform.position;
        }
    }

}
