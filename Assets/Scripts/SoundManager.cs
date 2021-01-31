using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region SINGLETON PATTERN
    private static SoundManager _instance;
    public static SoundManager Instance { get { return _instance; } }
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

    public AudioClip[] AudioClips;

    public AudioSource Loop;
    public AudioSource ChancePlatform;


    private void Start()
    {
        Loop.clip = AudioClips[0];
        Loop.Play();
    }
}
