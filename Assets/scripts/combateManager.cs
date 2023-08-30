using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combateManager : MonoBehaviour
{
    private fighter[] fighters;
    private int fighterIndex;
    void Start()
    {
        logPanel.write("Battle initiated.");

        this.fighterIndex = 0;
    }
}
