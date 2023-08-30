using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class logPanel : MonoBehaviour
{
    protected static logPanel current;

    public Text logLabel;

    void Awake()
    {
        current = this;
    }

    public static void write(string message)
    {
        current.logLabel.text = message;
    }
}
