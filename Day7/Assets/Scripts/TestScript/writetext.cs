using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class writetext : MonoBehaviour
{
    //д��Text

    public Text TextW;

    public string content;

    public void WriteText()
    {
        TextW.text = $"{content}";

    }



}
