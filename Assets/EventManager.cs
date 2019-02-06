using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    public delegate void LevelStart();
    public static event LevelStart OnLevelStart;

    public static void StartLevel()
    {
        if (OnLevelStart != null)
            OnLevelStart();
    }

    public delegate void LevelEnd();
    public static event LevelEnd OnLevelEnd;

    public static void EndLevel()
    {
        if (OnLevelEnd != null)
            OnLevelEnd();
    }

    public void StartLevelWrapper()
    {
        StartLevel();
    }

}
