using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Diagnostics.Tracing;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public BoxCollider2D myCollider;
    public Animator myAnim;
    public int playerId;

    public GameObject tali, player;


    public Rigidbody2D rb;
    public float moveSpeed;
    private string currentState;
    Vector2 movimento;
    public Animator animator;

    public Enemy inimigoAtual;

    
    public bool choroTali,cafe;
    public bool animaTalita;
    public bool luta;
    public bool especial;
    public bool desistir;
    public bool dialogo1, dialogo2, dialogo3, dialogo4, dialogo5,dialogo6,dialogo7, dialogo8,dialogo9,dialogo10,dialogo11, dialogoPsi;
    public GameObject entrarPanel,saidaPanel, windowsPanel, youtubePanel, especialSPR, scenePanel, trabalhoPanel,pausePanel, especialPanel;

    public Image sadPanel, vidaPanel;
    public Image spriteSmile;
    public Sprite[] felizSprites;

    private void Awake()
    {
        Instance = this;
        entrarPanel.SetActive(true);

    }

    void Start()
    {
        switch (playerId)
        {
            case 0:
            vidaPanel.fillAmount = 0.7f;
                break;

            case 1:
                vidaPanel.fillAmount = 1;
                break;

            case 2:
                vidaPanel.fillAmount = 1;
                if (cafe == true)
                {
                    StartCoroutine("chorandoTalita");
                }
                break;
        }
        
    }

    void Update()//instancias dos scripts e variaveis da animação
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                pausePanel.SetActive(true);
                Time.timeScale = 0;
                myAnim.Play("idle_idle");
            }
            else
            {
                pausePanel.SetActive(false);
                Time.timeScale = 1;
            }
        }

        if (choroTali == true)
        {
            return;
        }
        if (desistir)
        {
            moveSpeed = 0;
            return;
        }

        if (animaTalita == true)
        {
            return;
        }
        if (DialogueManager.Instance.onDialogue == true)
        {
            moveSpeed = 0;
            myAnim.Play("idle_idle");
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


        if ( DialogueManager.Instance.onDialogue == false)
        {
            moveSpeed = 3;
        }

        if (CombatManager.instance.onCombate)
        {
            return;
        }

        if (CombatManager.instance.onCombateNoob)
        {
            return;
        }

        if (CombatManager.instance.onYoutube)
        {
            return;
        }

        // desistir animação
        if (vidaPanel.fillAmount <= 0)
        {
            CombatManager.instance.PlayerDesistir();
        }

        // cores da barra de vida
        if (vidaPanel.fillAmount >= 0.7f)
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

    public void tomarDano(float dano)
    {
        vidaPanel.fillAmount = vidaPanel.fillAmount - dano;
        CombatManager.instance.vidaPlayer.fillAmount = vidaPanel.fillAmount;
        if(vidaPanel.fillAmount <= 0)
        {
            CombatManager.instance.PlayerDesistir();
        }
    }

    public void sceneTransi()
    {
        SceneManager.LoadScene(3);
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
        if (collision.CompareTag("Cama"))
        {
            if (luta == false)
            {
                trabalhoPanel.gameObject.SetActive(true);
            }
            if (luta == true)
            {
                scenePanel.gameObject.SetActive(true);
            }
        }
        if (collision.CompareTag("Especial"))
        {
            especial = true;
            especialSPR.SetActive(false);
            cameraShake.Instance.CamShake();
            StartCoroutine(falaEspecial());
        }
        if (collision.CompareTag("Inimigo1"))
        { //colisão com o inimigo
            if (luta == false)
            {
                inimigoAtual = collision.gameObject.GetComponent<Enemy>();
                windowsPanel.gameObject.SetActive(true);
            }
        }
        if (collision.CompareTag("Dialogo1"))
        {
            DialogueManager.Instance.playerIsClose = true;
            DialogueManager.Instance.pressPanel1.SetActive(true);
            dialogo1 = true;
            dialogo2 = false;
            dialogo3 = false;
            dialogo4 = false;
            dialogo5 = false;
            dialogo6 = false;
            dialogo7 = false;
            dialogo8 = false;
            dialogo9 = false;
            dialogo10 = false;
            dialogo11 = false;
        }
        if (collision.CompareTag("Dialogo2"))
        {
            DialogueManager.Instance.playerIsClose = true;
            DialogueManager.Instance.pressPanel2.SetActive(true);
            dialogo1 = false;
            dialogo2 = true;
            dialogo3 = false;
            dialogo4 = false;
            dialogo5 = false;
            dialogo6 = false;
            dialogo7 = false;
            dialogo8 = false;
            dialogo9 = false;
            dialogo10 = false;
            dialogo11 = false;
            DialogueManager.Instance.transform.parent = null;
            DialogueManager.Instance.transform.position = collision.gameObject.transform.position;
        }

        if (collision.CompareTag("Dialogo3"))
        {
            DialogueManager.Instance.playerIsClose = true;
            DialogueManager.Instance.pressPanel3.SetActive(true);
            dialogo1 = false;
            dialogo2 = false;
            dialogo3 = true;
            dialogo4 = false;
            dialogo5 = false;
            dialogo6 = false;
            dialogo7 = false;
            dialogo8 = false;
            dialogo9 = false;
            dialogo10 = false;
            dialogo11 = false;
            DialogueManager.Instance.transform.parent = null;
            DialogueManager.Instance.transform.position = collision.gameObject.transform.position;
        }

        if (collision.CompareTag("Dialogo4"))
        {
            DialogueManager.Instance.playerIsClose = true;
            DialogueManager.Instance.pressPanel4.SetActive(true);
            dialogo1 = false;
            dialogo2 = false;
            dialogo3 = false;
            dialogo4 = true;
            dialogo5 = false;
            dialogo6 = false;
            dialogo7 = false;
            dialogo8 = false;
            dialogo9 = false;
            dialogo10 = false;
            dialogo11 = false;
            DialogueManager.Instance.transform.parent = null;
            DialogueManager.Instance.transform.position = collision.gameObject.transform.position;
        }

        if (collision.CompareTag("Dialogo5"))
        {
            DialogueManager.Instance.playerIsClose = true;
            DialogueManager.Instance.pressPanel5.SetActive(true);
            dialogo1 = false;
            dialogo2 = false;
            dialogo3 = false;
            dialogo4 = false;
            dialogo5 = true;
            dialogo6 = false;
            dialogo7 = false;
            dialogo8 = false;
            dialogo9 = false;
            dialogo10 = false;
            dialogo11 = false;
            DialogueManager.Instance.transform.parent = null;
            DialogueManager.Instance.transform.position = collision.gameObject.transform.position;
        }
        if (collision.CompareTag("Dialogo6"))
        {
            DialogueManager.Instance.playerIsClose = true;
            DialogueManager.Instance.pressPanel6.SetActive(true);
            dialogo1 = false;
            dialogo2 = false;
            dialogo3 = false;
            dialogo4 = false;
            dialogo5 = false;
            dialogo6 = true;
            dialogo7 = false;
            dialogo8 = false;
            dialogo9 = false;
            dialogo10 = false;
            dialogo11 = false;
            DialogueManager.Instance.transform.parent = null;
            DialogueManager.Instance.transform.position = collision.gameObject.transform.position;
        }
        if (collision.CompareTag("Dialogo7"))
        {
            DialogueManager.Instance.playerIsClose = true;
            DialogueManager.Instance.pressPanel7.SetActive(true);
            dialogo1 = false;
            dialogo2 = false;
            dialogo3 = false;
            dialogo4 = false;
            dialogo5 = false;
            dialogo6 = false;
            dialogo7 = true;
            dialogo8 = false;
            dialogo9 = false;
            dialogo10 = false;
            dialogo11 = false;
            DialogueManager.Instance.transform.parent = null;
            DialogueManager.Instance.transform.position = collision.gameObject.transform.position;
        }
        if (collision.CompareTag("Dialogo8"))
        {
            DialogueManager.Instance.playerIsClose = true;
            DialogueManager.Instance.pressPanel8.SetActive(true);
            dialogo1 = false;
            dialogo2 = false;
            dialogo3 = false;
            dialogo4 = false;
            dialogo5 = false;
            dialogo6 = false;
            dialogo7 = false;
            dialogo8 = true;
            dialogo9 = false;
            dialogo10 = false;
            dialogo11 = false;

            DialogueManager.Instance.transform.parent = null;
            DialogueManager.Instance.transform.position = collision.gameObject.transform.position;
        }
        if (collision.CompareTag("Dialogo9"))
        {
            DialogueManager.Instance.playerIsClose = true;
            DialogueManager.Instance.pressPanel9.SetActive(true);
            dialogo1 = false;
            dialogo2 = false;
            dialogo3 = false;
            dialogo4 = false;
            dialogo5 = false;
            dialogo6 = false;
            dialogo7 = false;
            dialogo8 = false;
            dialogo9 = true;
            dialogo10 = false;
            dialogo11 = false;
            DialogueManager.Instance.transform.parent = null;
            DialogueManager.Instance.transform.position = collision.gameObject.transform.position;
        }
        if (collision.CompareTag("Dialogo10"))
        {
            DialogueManager.Instance.playerIsClose = true;
            DialogueManager.Instance.pressPanel10.SetActive(true);
            dialogo1 = false;
            dialogo2 = false;
            dialogo3 = false;
            dialogo4 = false;
            dialogo5 = false;
            dialogo6 = false;
            dialogo7 = false;
            dialogo8 = false;
            dialogo9 = false;
            dialogo10 = true;
            dialogo11 = false;
            DialogueManager.Instance.transform.parent = null;
            DialogueManager.Instance.transform.position = collision.gameObject.transform.position;
        }
        if (collision.CompareTag("Dialogo11"))
        {
            DialogueManager.Instance.playerIsClose = true;
            DialogueManager.Instance.pressPanel11.SetActive(true);
            dialogo1 = false;
            dialogo2 = false;
            dialogo3 = false;
            dialogo4 = false;
            dialogo5 = false;
            dialogo6 = false;
            dialogo7 = false;
            dialogo8 = false;
            dialogo9 = false;
            dialogo10 = false;
            dialogo11 = false;
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

        if(collision.CompareTag("irCafe"))
        {
            StartCoroutine(irRua());
        }

        if (collision.CompareTag("animacaoCombate"))
        {
            animaTalita = true;
            if (luta == false)
            {
                inimigoAtual = collision.gameObject.GetComponent<Enemy>();

            }
            StartCoroutine(talitaCombate());
        }

        if(collision.CompareTag("Trabalho"))
        {
            StartCoroutine(trabalhoTomas());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)//saindo da colisão com os objetos de dialogo
    {
        if (collision.CompareTag("Cama"))
        {
            if (luta == false)
            {
                trabalhoPanel.gameObject.SetActive(false);
            }
            if (luta == true)
            {
                scenePanel.gameObject.SetActive(false);
            }
        }
        if (collision.CompareTag("Dialogo1"))
        {
            DialogueManager.Instance.playerIsClose = false;
            DialogueManager.Instance.pressPanel1.SetActive(false);
            dialogo1 = false;
        }
        if (collision.CompareTag("Dialogo2"))
        {
            DialogueManager.Instance.playerIsClose = false;
            DialogueManager.Instance.pressPanel2.SetActive(false);
            dialogo2 = false;
        }
        if (collision.CompareTag("Dialogo3"))
        {
            DialogueManager.Instance.playerIsClose = false;
            DialogueManager.Instance.pressPanel3.SetActive(false);
            dialogo3 = false;
        }
        if (collision.CompareTag("Dialogo4"))
        {
            DialogueManager.Instance.playerIsClose = false;
            DialogueManager.Instance.pressPanel4.SetActive(false);
            dialogo4 = false;
        }
        if (collision.CompareTag("Dialogo5"))
        {
            DialogueManager.Instance.playerIsClose = false;
            DialogueManager.Instance.pressPanel5.SetActive(false);
            dialogo5 = false;
        }

        if (collision.CompareTag("Dialogo6"))
        {
            DialogueManager.Instance.playerIsClose = false;
            DialogueManager.Instance.pressPanel6.SetActive(false);
            dialogo6 = false;
        }
        if (collision.CompareTag("Dialogo7"))
        {
            DialogueManager.Instance.playerIsClose = false;
            DialogueManager.Instance.pressPanel7.SetActive(false);
            dialogo7 = false;
        }
        if (collision.CompareTag("Dialogo8"))
        {
            DialogueManager.Instance.playerIsClose = false;
            DialogueManager.Instance.pressPanel8.SetActive(false);
            dialogo8 = false;
        }
        if (collision.CompareTag("Dialogo9"))
        {
            DialogueManager.Instance.playerIsClose = false;
            DialogueManager.Instance.pressPanel9.SetActive(false);
            dialogo9 = false;
        }
        if (collision.CompareTag("Dialogo10"))
        {
            DialogueManager.Instance.playerIsClose = false;
            DialogueManager.Instance.pressPanel10.SetActive(false);
            dialogo10 = false;
        }
        if (collision.CompareTag("Dialogo11"))
        {
            DialogueManager.Instance.playerIsClose = false;
            DialogueManager.Instance.pressPanel11.SetActive(false);
            dialogo11 = false;
        }
    }

    IEnumerator chorandoTalita()
    {
        yield return new WaitForSeconds(0.2f);
        CombatManager.instance.cam.m_Lens.OrthographicSize = 3;
        CombatManager.instance.cam.Follow = tali.transform;
        yield return new WaitForSeconds(4);
        myAnim.Play("preocupaTomas");
        yield return new WaitForSeconds(6);
        CombatManager.instance.cam.m_Lens.OrthographicSize = 4;
        yield return new WaitForSeconds(0.5f);
        CombatManager.instance.cam.m_Lens.OrthographicSize = 5;
        CombatManager.instance.cam.Follow = player.transform;
        
    }
    IEnumerator falaEspecial()
    {
        especialPanel.SetActive(true);
        yield return new WaitForSeconds(3);
        especialPanel.SetActive(false);
    }
    IEnumerator irRua()
    {
        entrarPanel.SetActive(false);
        saidaPanel.SetActive(true);
        yield return new WaitForSeconds(1);
        transform.position = new Vector3(9, 29, 0);
        yield return new WaitForSeconds(2);
        saidaPanel.SetActive(false);
        entrarPanel.SetActive(true);
    }
    IEnumerator talitaCombate()
    {
        yield return new WaitForSeconds(0.1f);
        saidaPanel.SetActive(false);
        myAnim.Play("derrubarCafe");
        moveSpeed = 0;
        yield return new WaitForSeconds(5);
        saidaPanel.SetActive(true);
        entrarPanel.SetActive(false);
        yield return new WaitForSeconds(1);
        CombatManager.instance.IniciarCombate();
        yield return new WaitForSeconds(1);
        entrarPanel.SetActive(true);
    }

    IEnumerator trabalhoTomas()
    {
        entrarPanel.SetActive(false);
        saidaPanel.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(5);

    }
}

