﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Dialogue_Manager : MonoBehaviour
{
    public Dialogue dialogue;

    Queue<string> sentences;

    public GameObject dialoguePanel;
    public TextMeshProUGUI displayText;

    string activeSentence;
    public float typingSpeed;

    AudioSource myAudio;
    public AudioClip speedSound;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        myAudio = GetComponent<AudioSource>();
    }

    void StartDialogue()
    {
        sentences.Clear();

        foreach(string sentence in dialogue.sentenceList)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    void DisplayNextSentence()
    {
        if(sentences.Count <= 0)
        {
            displayText.text = activeSentence;
            return;
        }

        activeSentence = sentences.Dequeue();
        displayText.text = activeSentence;

        StopAllCoroutines();
        StartCoroutine(TypeTheSentence(activeSentence));
    }

    IEnumerator TypeTheSentence(string sentence)
    {
        displayText.text = "";

        foreach(char letter in sentence.ToCharArray())
        {
            displayText.text += letter;
            myAudio.PlayOneShot(speedSound);
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            dialoguePanel.SetActive(true);
            StartDialogue();
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && displayText.text == activeSentence)
        {
            DisplayNextSentence();
        }
    }

    void OnTriggerExit2D(Collider2D obj)
    {
        dialoguePanel.SetActive(false);
        StopAllCoroutines();
    }


}
