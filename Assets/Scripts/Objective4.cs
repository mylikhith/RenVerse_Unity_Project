using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Objective4 : MonoBehaviour
{
    public ObjectivesComplete objective4;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Vehicle") {
            //complete objective
            objective4.GetObjectiveDone(true, true, true, true);
            SceneManager.LoadScene("MainMenu");
        }
    }
}
