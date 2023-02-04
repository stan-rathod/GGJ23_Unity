using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        LevelManager.Instance.LoadScene("LV_NarrativeScene");
    }
}
