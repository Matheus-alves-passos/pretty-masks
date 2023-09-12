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

    public Enemy inimigoAtual;

    public bool desistir;



    public float moveSpeed;
    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (desistir)
        {
            return;
        }
        if (PC.Instance.onDialogue)
        {
            return;
        }

        if (CombatManager.instance.onCombate)
        {
            return;
        }
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;
    }

    public void Desistiu()
    {
        myAnim.Play("dead");
        desistir = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pia"))
        {
            inimigoAtual = collision.gameObject.GetComponent<Enemy>();
            CombatManager.instance.IniciarCombate();
        }

    }


}
