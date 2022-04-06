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
    private string sceneName = "FirstRoom";
    // Start is called before the first frame update
    void Start()
    {
        actions = new Dictionary<string, Action>();
        actions.Add("play", StartGame);
        actions.Add("quit", Quit);

        kwRecognizer = new KeywordRecognizer(actions.Keys.ToArray(), ConfidenceLevel.Low);
        kwRecognizer.OnPhraseRecognized += OnKeywordRecognized;
        kwRecognizer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnKeywordRecognized(PhraseRecognizedEventArgs args)
    {
        string keyword = args.text;
        Debug.Log("recognized: " + keyword);
        actions[keyword].Invoke();
    }

    private void StartGame()
    {
        Debug.Log("The game is started.");
        SceneManager.LoadScene(sceneName);
    }

    private void Quit()
    {
        Debug.Log("The game is closed.");
        Application.Quit();
    }
}
