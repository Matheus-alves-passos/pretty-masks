using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eGil : MonoBehaviour
{
    public Rigidbody2D rb;
    
    // Update is called once per frame
    void Update()
    {
        if( Player.Instance.caiu == true)
        {
            rb.velocity = new Vector2(0, -1);
        }
    }
}
