using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectivesComplete : MonoBehaviour
{
    [Header("Objectives to Complete")]
    public Text objective1;
    public Text objective2;
    public Text objective3;
    public Text objective4;

    public static ObjectivesComplete occurrence;

    private void Awake()
    {
        occurrence = this;
    }

    public void GetObjectiveDone(bool obj1, bool obj2, bool obj3, bool obj4)
    {
        if (obj1 == true)
        {
            objective1.text = "1. Completed";
            objective1.color = Color.green;
        }
        else
        {
            objective1.text = "1. Find the rifle";
            objective1.color = Color.white;
        }

        if (obj2 == true)
        {
            objective2.text = "2. Completed";
            objective2.color = Color.green;
        }
        else
        {
            objective2.text = "2. Find vehicle";
            objective2.color = Color.white;
        }

        if (obj3 == true)
        {
            objective3.text = "3. Completed";
            objective3.color = Color.green;
        }
        else
        {
            objective3.text = "3.  Find villagers";
            objective3.color = Color.white;
        }

        if (obj4 == true)
        {
            objective4.text = "4. Completed";
            objective4.color = Color.green;
        }
        else
        {
            objective4.text = "4. Take all of villagers into vehicle";
            objective4.color = Color.white;
        }
    }

}
