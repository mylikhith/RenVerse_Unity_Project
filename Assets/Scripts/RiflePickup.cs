using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiflePickup : MonoBehaviour
{
    [Header("Rifle's")]
    public GameObject PlayerRifle;
    public GameObject PlayerRifle1; // short gun
    public GameObject PlayerRifle2; // Aug gun
    public GameObject PickupRifle;
    public GameObject PickupRifle1; // short gun
    public GameObject PickupRifle2; // Aug gun
    public PlayerPunch playerPunch;
    public GameObject rifleUI;

    public ObjectivesComplete objective1;

    [Header("Rifle Assign Things")]
    public PlayerScript player;
    private float radius = 2.5f;
    public Animator animator;
    private float nextTimeToPunch = 0f;
    public float punchCharge = 15f;
    private bool AugGun = AssetsCheck.AugGun;
    private bool ShortGun = AssetsCheck.ShortGun;

    private bool punch;

    private void Awake() {
        PlayerRifle.SetActive(false);
        PlayerRifle1.SetActive(false);
        PlayerRifle2.SetActive(false);
        rifleUI.SetActive(false);
        punch = true;
    }

    private void Update() {

        if(Input.GetButtonDown("Fire1") && Time.time >= nextTimeToPunch && punch) {
            animator.SetBool("Punch", true);
            animator.SetBool("Idle", false);
            nextTimeToPunch = Time.time + 1f/punchCharge;

            playerPunch.Punch();
        } else {
            animator.SetBool("Punch", false);
            animator.SetBool("Idle", true);
        }

        if(Vector3.Distance(transform.position, player.transform.position) < radius) {
            if(Input.GetKeyDown("f")) {
                punch = false;
                PlayerRifle.SetActive(true);
                PlayerRifle1.SetActive(false);
                PlayerRifle2.SetActive(false);
                PickupRifle.SetActive(false);
                PickupRifle1.SetActive(true);
                PickupRifle2.SetActive(true);
                //sound

                //objective completed
                objective1.GetObjectiveDone(true, false, false, false);
            }
        }
    }
}
