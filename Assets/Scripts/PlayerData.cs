using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string sceneName;
    public bool[] completions; // 12

    public PlayerData (string sceneName, bool[] completions)
    {
        this.sceneName = sceneName;
        this.completions = completions;
    }
}
