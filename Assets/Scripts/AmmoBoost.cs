using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoost : MonoBehaviour
{
    [Header("AmmoBoost")]
    public Rifle rifle;
    public Rifle rifle1;
    public Rifle rifle2;
    private int magToGive = 15;
    private float radius = 2.2f;

    [Header("Sounds")]
    public AudioClip AmmoBoostSound;
    public AudioSource audioSource;

    [Header("AmmoBox Animator")]
    public Animator animator;

    private void Update()
    {
        if (Vector3.Distance(transform.position, rifle.transform.position) < radius)
        {
            if (Input.GetKeyDown("f"))
            {
                animator.SetBool("Open", true);
                rifle.mag = magToGive;
                rifle1.mag = 8;
                rifle2.mag = 10;

                //sound effect
                audioSource.PlayOneShot(AmmoBoostSound);

                Object.Destroy(gameObject, 1.5f);
            }
        }
    }
}
