using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class andarPorta : MonoBehaviour
{
    public Rigidbody2D myBody;
    public float velocidade;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        myBody.velocity = new Vector2(0,myBody.velocity.y);
    }
}
