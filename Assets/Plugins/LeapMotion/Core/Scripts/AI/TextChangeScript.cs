using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextChangeScript : MonoBehaviour
{
    public TextMeshPro predicted_word;
    public string temp_text;

    // Start is called before the first frame update
    void Start()
    {
        predicted_word = GetComponent<TextMeshPro> ();
        temp_text = "Left";
        predicted_word.text = temp_text;
        Debug.Log(Application.dataPath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
