using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

public class MainMenu : MonoBehaviour
{
    private Dictionary<string, Action> actions;
    private KeywordRecognizer kwRecognizer;
    //private string sceneName = "FirstRoom";
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("MenuMusic");

        actions = new Dictionary<string, Action>();
        actions.Add("play", Play);
        actions.Add("new game", NewGame);
        actions.Add("quit", Quit);

        kwRecognizer = new KeywordRecognizer(actions.Keys.ToArray(), ConfidenceLevel.Low);
        kwRecognizer.OnPhraseRecognized += OnKeywordRecognized;
        kwRecognizer.Start();

        SceneManager.activeSceneChanged += SaveSystem.SaveLocation;
    }

    private void OnKeywordRecognized(PhraseRecognizedEventArgs args)
    {
        string keyword = args.text;
        Debug.Log("recognized: " + keyword);
        actions[keyword].Invoke();
    }

    private void NewGame()
    {
        SaveSystem.PrepareNewGame();
        Play();
    }

    private void Play()
    {
        PlayerData data = SaveSystem.LoadPlayerData();
        Debug.Log("The game is started.");
        SceneManager.LoadScene(data.sceneName);
        FindObjectOfType<AudioManager>().SetBackgroundMusic("MainTheme");
    }

    private void Quit()
    {
        Debug.Log("The game is closed.");
        Application.Quit();
    }
}
