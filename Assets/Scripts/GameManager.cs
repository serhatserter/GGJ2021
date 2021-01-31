using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region SINGLETON PATTERN
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    public bool IsStart;
    public bool IsLose;

    public GameObject Player;
    public GameObject Monster;

    public float PlayerMoveZSpeed;
    public float MonsterMoveZSpeed;

    public Material TransparentMat;

    public GameObject WinPanel;
    public GameObject FailPanel;

    public Animator PlayerAnimator;
    public Animator MonsterAnimator;

    public GameObject LastTouchedPlatform;
    public Action<bool> IsPlatformInvisible;

    
}
