using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScrollingText : MonoBehaviour
{
    // TODO change the name of this function to ReturnToStartMenu
    private void CloseTheGame()
    {
        Debug.Log("The CloseTheGame() function is called.");
        SceneManager.LoadScene("StartMenu");
    }
}
