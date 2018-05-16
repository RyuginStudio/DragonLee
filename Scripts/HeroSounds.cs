using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSounds : MonoBehaviour
{
    private float currentTime;
    public float playDialogueUpdate;
    public float dialogueRate;

    AudioSource audioSource;

    private static HeroSounds instance;

    public static HeroSounds getInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;

        currentTime = Time.time;
        playDialogueUpdate = Time.time;

        audioSource = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start()
    {
        playVoiceDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.time;
        playVoiceDialogue();
    }

    public void playVoiceDialogue()
    {
        if ((Character.getInstance().status == Character.CharacterStatus.Idle || Character.getInstance().status == Character.CharacterStatus.Run)
            && currentTime - playDialogueUpdate >= dialogueRate)
        {
            dialogueRate = Random.Range(10, 20);
            playDialogueUpdate = Time.time;

            audioSource.clip = dialogue[Random.Range(0, 16)];

            if (!audioSource.isPlaying)
                audioSource.Play();
        }
    }


    public AudioClip[] dialogue;
    public AudioClip[] laugh;
    public AudioClip[] normalAttackVoice;
    public AudioClip[] die;
    public AudioClip[] skill;
    public AudioClip[] normalAttackSFX;

}