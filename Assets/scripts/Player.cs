using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using Cinemachine;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public Rigidbody2D myBody;
    public BoxCollider2D myCollider;
    public Animator myAnim;
    public Rigidbody2D rb;
    public float horizontalInput, verticalInput;
    private string currentState;

    public Enemy inimigoAtual;

    public bool desistir;
    public bool dialogo1, dialogo2, dialogo3;



    public float moveSpeed;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {

    }

    void Update()//instancias dos scripts e variaveis da animação
    {
        if (desistir)
        {
            return;
        }
        if (DialogueManager.Instance.onDialogue)
        {
            return;
        }

        if (CombatManager.instance.onCombate)
        {
            return;
        }

        Animations();
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    public void Animations() //animações do personagem player
    {
        if (horizontalInput == 0 && verticalInput == 0)
            ChangeAnimationState("Idle");

        if (verticalInput > 0)
            ChangeAnimationState("WalkCima");

        if (verticalInput < 0)
            ChangeAnimationState("WalkBaixo");

        if (horizontalInput > 0)
            ChangeAnimationState("WalkDireita");

        if (horizontalInput < 0)
            ChangeAnimationState("WalkEsquerda");
    }
    void ChangeAnimationState(string newState)//mudar animação
    {
        if (currentState == newState)
            return;

        myAnim.Play(newState);
    }

    public void Desistiu()//animação de derrota do player caso escolha a opção desistir
    {
        myAnim.Play("dead");
        desistir = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)//colisão com os objetos
    {
        if (collision.CompareTag("Inimigo1")) //colisão com o inimigo
        {
            inimigoAtual = collision.gameObject.GetComponent<Enemy>();
            CombatManager.instance.IniciarCombate();
        }
        if (collision.CompareTag("Dialogo1"))
        {
            DialogueManager.Instance.playerIsClose = true;
            dialogo1 = true;
        }
        if (collision.CompareTag("Dialogo2"))
        {
            DialogueManager.Instance.playerIsClose = true;
            dialogo2 = true;
            DialogueManager.Instance.transform.parent = null;
            DialogueManager.Instance.transform.position = collision.gameObject.transform.position;
        }

        if (collision.CompareTag("Dialogo3"))
        {
            DialogueManager.Instance.playerIsClose = true;
            dialogo3 = true;
            DialogueManager.Instance.transform.parent = null;
            DialogueManager.Instance.transform.position = collision.gameObject.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)//saindo da colisão com os objetos de dialogo
    {
        if (collision.CompareTag("Dialogo1"))
        {
            DialogueManager.Instance.playerIsClose = false;
            dialogo1 = false;
        }
        if (collision.CompareTag("Dialogo2"))
        {
            DialogueManager.Instance.playerIsClose = false;
            dialogo2 = false;
        }
        if (collision.CompareTag("Dialogo3"))
        {
            DialogueManager.Instance.playerIsClose = false;
            dialogo3 = false;
        }
    }

}

