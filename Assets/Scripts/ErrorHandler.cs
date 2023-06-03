using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorHandler : MonoBehaviour
{
    public GameObject ErrorPanel;
    public Text ErrorText;

    public void ErrorMessage(string _text) {
        ErrorPanel.SetActive(true);
        StartCoroutine(Error(_text));
    }

    private IEnumerator Error(string _text)
    {
        ErrorText.text = _text;
        yield return new WaitForSeconds(3f);
        ErrorPanel.SetActive(false);
        ErrorText.text = "...";
    }
}
