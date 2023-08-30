using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using Cinemachine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public Rigidbody2D myBody;
    public BoxCollider2D myCollider;
    public Animator myAnim;
    public Rigidbody2D rb;
    public GameObject painelDialogo,painelCombate;    

   

    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PC.Instance.onDialogue)
        {
            return;
        }
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("PC"))
        {
            painelDialogo.SetActive(true);
        }

    }
}
