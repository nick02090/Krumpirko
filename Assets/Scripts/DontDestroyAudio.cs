using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyAudio : MonoBehaviour
{

    static DontDestroyAudio instance;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

    }
}
