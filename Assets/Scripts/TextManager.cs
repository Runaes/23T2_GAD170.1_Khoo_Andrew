using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Text myText;

    static string text;

    void Update()
    {
        myText.text = text?.ToString();
    }

    public static void NewLine(string line)
    {
        if (text?.Count(c => c == '\r') > 10)
        {
            ResetLines();
        }
        text = text + "\r\n" + line;
    }

    public static void ResetLines()
    {
        text = string.Empty;
    }
}
