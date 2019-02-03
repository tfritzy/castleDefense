using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

[System.Serializable]
public class AudioManager : MonoBehaviour {

    public static AudioManager manager;

    private Dictionary<string, float> lastPlayTimes = new Dictionary<string, float>();
    public Sound[] sounds;
    int numberOfSoundsBeingPlayed = 0;
    float lastAudioSourceCheckTime;


    private void Awake()
    {
        if (AudioManager.manager == null)
        {
            AudioManager.manager = this;
        } else
        {
            Destroy(this);
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        if (s == null)
        {
            throw new Exception(name + " sound name is not in your sound list.");
        }
        s.source = MakeSource(s).source;
        s.source.pitch = UnityEngine.Random.Range(s.source.pitch * .99f, s.source.pitch * 1.01f);
        if (s.randomPitch)
        {
            s.source.pitch = UnityEngine.Random.Range(s.source.pitch * .8f, s.source.pitch * 1.2f);
        }
        if (!lastPlayTimes.ContainsKey(s.name))
        {
            lastPlayTimes.Add(s.name, Time.time);
            s.source.Play();
        } else
        {
            float difference = Time.time - lastPlayTimes[s.name];

            if (difference > .1f)
            {
                lastPlayTimes[s.name] =  Time.time;
                s.source.Play();
            } else
            {
                // Max out the length of the sound effect at .5f
                if (.1f - difference < .5f)
                {
                    s.source.PlayDelayed(.1f - difference);
                    lastPlayTimes[s.name] = lastPlayTimes[s.name] + .1f;
                }
            }
        }
        
    }

    private Sound MakeSource(Sound s)
    {
        s.source = this.gameObject.AddComponent<AudioSource>();
        s.source.clip = s.clip;
        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        Destroy(s.source, s.clip.length);
        return s;
    }

    // Use this for initialization
    void Start () {
		
	}
	
}
