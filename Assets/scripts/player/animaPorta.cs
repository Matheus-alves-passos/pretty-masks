using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class animaPorta : MonoBehaviour
{
    public Animator portAnim;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tali"))
        {
            Debug.Log("aaaaaaa");
            portAnim.Play("fechar_porta");
        }
    }
}
