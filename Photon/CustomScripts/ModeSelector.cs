using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameMode
    {
        FFA = 0,
        TDM = 1
    }
public class ModeSelector : MonoBehaviour 
{

    public static GameMode gMode = GameMode.FFA;
}
