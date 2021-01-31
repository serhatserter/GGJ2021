using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehaviour : MonoBehaviour
{
    private Material startMat;
    private Material transparentMat;

    private bool isPlayerTouch;
    private bool isVisible;
    private void Start()
    {
        startMat = GetComponent<MeshRenderer>().sharedMaterial;
        transparentMat = GameManager.Instance.TransparentMat;

        GameManager.Instance.IsPlatformInvisible += OnVisibleControl;

        int random = UnityEngine.Random.Range(0, 100);
        if (random > 70) isVisible = true;
    }
    private void OnDestroy()
    {
        GameManager.Instance.IsPlatformInvisible -= OnVisibleControl;

    }

    private void OnVisibleControl(bool isInvisible)
    {
        if (!isVisible)
        {
            if (!isPlayerTouch)
            {
                if (isInvisible) GetComponent<MeshRenderer>().sharedMaterial = transparentMat;
                else GetComponent<MeshRenderer>().sharedMaterial = startMat;
            }
        }


    }


    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.name == "Player")
        {
            GetComponent<MeshRenderer>().sharedMaterial = startMat;
            isPlayerTouch = true;
        }
    }
}
