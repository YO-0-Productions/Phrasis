using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private Queue<string> sentences;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Animator animator;
    private string changeNameFlag = "/changeName";

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        
        animator.SetBool("IsOpen", true);

        DisplayNextSentence();

    }

    private bool IsNameChange(string sentence)
    {
        return sentence.Contains(changeNameFlag) ? true : false;
    }

    private string NameToChange(string sentence)
    {
        // remove changeNameFlag from the string and return the name
        return sentence.Substring(changeNameFlag.Length + 1);
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        if (IsNameChange(sentence))
        {
            nameText.text = NameToChange(sentence);
            sentence = sentences.Dequeue();

        }
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));


    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
    }

}
