using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    private Dictionary<string, Action> actions;
    private KeywordRecognizer kwRecognizer;

    private void Start()
    {
        actions = new Dictionary<string, Action>();
        actions.Add("speak", TriggerDialogue);
        actions.Add("next", FindObjectOfType<DialogueManager>().DisplayNextSentence);
        actions.Add("continue", FindObjectOfType<DialogueManager>().DisplayNextSentence);
        actions.Add("end", FindObjectOfType<DialogueManager>().EndDialogue);
        kwRecognizer = new KeywordRecognizer(actions.Keys.ToArray(), ConfidenceLevel.Low);
        kwRecognizer.OnPhraseRecognized += OnKeywordsRecognised;
        kwRecognizer.Start();
    }
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    private void OnKeywordsRecognised(PhraseRecognizedEventArgs args)
    {
        Debug.Log("Keyword: " + args.text);
        actions[args.text].Invoke();
    }
}
