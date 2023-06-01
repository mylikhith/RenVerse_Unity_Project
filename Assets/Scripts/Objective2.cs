using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective2 : MonoBehaviour
{
    public ObjectivesComplete objective2;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
            //complete objective
            objective2.GetObjectiveDone(true, true, true, false);
            Destroy(gameObject, 2f);
        }
    }
}
