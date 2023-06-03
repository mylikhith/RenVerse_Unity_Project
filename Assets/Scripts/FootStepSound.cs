using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepSound : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("FootSteps Sounds")]
    public AudioClip[] footstepSound;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private AudioClip GetRandomFootStep() {
        return footstepSound[UnityEngine.Random.Range(0, footstepSound.Length)];
    }

    private void Step() {
        AudioClip clip = GetRandomFootStep();
        audioSource.PlayOneShot(clip);
    }
}
