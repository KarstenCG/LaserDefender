using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] [Range(0f, 1f)] float masterVol = 0.35f;

    private void Awake()
    {
        SetUpSingeleton();
    }

    private void SetUpSingeleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        AudioListener.volume = masterVol;
    }

}
