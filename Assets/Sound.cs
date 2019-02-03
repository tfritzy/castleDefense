using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound {

    [HideInInspector]
    public AudioSource source;
    public bool randomPitch;
    public string name;
    public AudioClip clip;
    [Range(0f,1f)]
    public float volume;
    [Range(.5f, 1.5f)]
    public float pitch;


}
