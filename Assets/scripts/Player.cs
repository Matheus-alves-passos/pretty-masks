using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public Rigidbody2D myBody;
    public BoxCollider2D myCollider;
    public Animator myAnim;

    public Rigidbody2D rb;
    public float moveSpeed;
    private string currentState;
    Vector2 movimento;
    public Animator animator;

    public Enemy inimigoAtual;

    public bool desistir;
    public bool dialogo1, dialogo2, dialogo3, dialogo4, dialogo5;

    public Image sadPanel, vidaPanel;
    public Image spriteSmile;
    public Sprite[] felizSprites;

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
            moveSpeed = 0;
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
        // movimentação
        movimento.x = Input.GetAxisRaw("Horizontal");
        movimento.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("horizontal", movimento.x);
        animator.SetFloat("vertical", movimento.y);
        animator.SetFloat("velocidade", movimento.sqrMagnitude);

        if (movimento != Vector2.zero)
        {
            animator.SetFloat("horizontalidle", movimento.x);
            animator.SetFloat("verticalidle", movimento.y);
        }

        // desistir animação
        if (vidaPanel.fillAmount <= 0)
        {
            CombatManager.instance.PlayerDesistir();
        }

        // cores da barra de vida
        if (vidaPanel.fillAmount > 0.7f)
        {
            vidaPanel.color = Color.green;
            spriteSmile.sprite = felizSprites[0];
        }
        if (vidaPanel.fillAmount < 0.7f && vidaPanel.fillAmount > 0.5f)
        {
            vidaPanel.color = Color.yellow;
            spriteSmile.sprite = felizSprites[1];
        }
        if (vidaPanel.fillAmount < 0.5f && vidaPanel.fillAmount > 0.3f)
        {
            vidaPanel.color = new Color(255, 95, 0, 255);
            spriteSmile.sprite = felizSprites[2];
        }
        if (vidaPanel.fillAmount < 0.3f)
        {
            vidaPanel.color = Color.red;
            spriteSmile.sprite = felizSprites[3];
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movimento * moveSpeed * Time.fixedDeltaTime);
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
            dialogo2 = false;
            dialogo3 = false;
            dialogo4 = false;
            dialogo5 = false;
        }
        if (collision.CompareTag("Dialogo2"))
        {
            DialogueManager.Instance.playerIsClose = true;
            dialogo1 = false;
            dialogo2 = true;
            dialogo3 = false;
            dialogo4 = false;
            dialogo5 = false;
            DialogueManager.Instance.transform.parent = null;
            DialogueManager.Instance.transform.position = collision.gameObject.transform.position;
        }

        if (collision.CompareTag("Dialogo3"))
        {
            DialogueManager.Instance.playerIsClose = true;
            dialogo1 = false;
            dialogo2 = false;
            dialogo3 = true;
            dialogo4 = false;
            dialogo5 = false;

            DialogueManager.Instance.transform.parent = null;
            DialogueManager.Instance.transform.position = collision.gameObject.transform.position;
        }

        if (collision.CompareTag("Dialogo4"))
        {
            DialogueManager.Instance.playerIsClose = true;
            dialogo1 = false;
            dialogo2 = false;
            dialogo3 = false;
            dialogo4 = true;
            dialogo5 = false;
            DialogueManager.Instance.transform.parent = null;
            DialogueManager.Instance.transform.position = collision.gameObject.transform.position;
        }

        if (collision.CompareTag("Dialogo5"))
        {
            DialogueManager.Instance.playerIsClose = true;
            dialogo1 = false;
            dialogo2 = false;
            dialogo3 = false;
            dialogo4 = false;
            dialogo5 = true;
            DialogueManager.Instance.transform.parent = null;
            DialogueManager.Instance.transform.position = collision.gameObject.transform.position;
        }

        if (collision.CompareTag("sadPanel"))
        {
            vidaPanel.fillAmount = vidaPanel.fillAmount - 0.15f;

            if (sadPanel.color.a < 0.25f)
            {
                sadPanel.color = new Color(0, 0, 255, sadPanel.color.a + 0.02f);
            }
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
        if (collision.CompareTag("Dialogo4"))
        {
            DialogueManager.Instance.playerIsClose = false;
            dialogo4 = false;
        }
        if (collision.CompareTag("Dialogo5"))
        {
            DialogueManager.Instance.playerIsClose = false;
            dialogo5 = false;
        }
    }

}

