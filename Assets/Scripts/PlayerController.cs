using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Windows.Speech;
using System;

public class PlayerController : MonoBehaviour

{

    public float movementUnit = 10.0f;

    private Dictionary<string, Action> keywordActions = new Dictionary<string, Action>();
    private KeywordRecognizer keywordRecognizer;
    private Rigidbody playerRb;
    private Vector3 destination;
    public float speed = 0.01f;


    // Start is called before the first frame update
    void Start()
    {
        keywordActions.Add("front", GoForward);
        keywordActions.Add("left", TurnLeft);
        keywordActions.Add("right", TurnRight);
        keywordActions.Add("back", TurnBack);

        keywordRecognizer = new KeywordRecognizer(keywordActions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += OnKeywordsRecognised;
        keywordRecognizer.Start();
        playerRb = GetComponent<Rigidbody>();
        playerRb.centerOfMass = new Vector3(0, -1, 0);
        destination = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, destination, speed);
    }

    private void OnKeywordsRecognised(PhraseRecognizedEventArgs args)
    {
        Debug.Log("Keyword: " + args.text);
        keywordActions[args.text].Invoke();
    }

    private void GoForward()
    {
        destination = gameObject.transform.position + Vector3.forward * 10;
    }

    private void TurnLeft()
    {
        destination = gameObject.transform.position + Vector3.left * 13;

    }

    private void TurnRight()
    {
        destination = gameObject.transform.position + Vector3.right * 22;
    }


    private void TurnBack()
    {
        destination = gameObject.transform.position + Vector3.back * 10;
    }

}
