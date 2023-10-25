using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;
    public Player player;
    public Enemy enemy;
    public GameObject irLive, youtubePanel, painelWindows, painelCombate, painelAtaque, painelGameOver, transiPanel, falaPanel, painelTwitch, fala1Panel, fala2Panel, fala3Panel, fala4Panel, fala5Panel, fala6Panel, fala7Panel, video0, video1, video2, video3, painelTexto, SPRataque1, SPRataque2, SPRataque3, SPRataque4, SPRataqueInimigo1, SPRataqueInimigo2, SPRataqueInimigo3, SPRataqueInimigo4;
    public TMP_Text combateText, nomeInimigo;
    public string fraseAtaqueInimigo;
    public Image inimigoImage, vidaInimigo, vidaPlayer, playerImage, vidaPanel;

    public Sprite[] playerDamageSprite, playerNormalSprite; 
    public Animator enemyAnimator;
    public Button ataqueButton1, ataqueButton2, ataqueButton3, ataqueButton4;
    public TMP_Text nomeAtaque1, nomeAtaque2, nomeAtaque3, nomeAtaque4;

    public Image spriteSmile;
    //cinemachine camera atual que eu pego no script da room
    public CinemachineVirtualCamera cam;

    public bool fireBall;
    public bool onCombate;
    public bool onCombateNoob;
    public bool onYoutube;
    public bool pegouEspecial;
    public bool fear;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        fireBall = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (vidaInimigo.fillAmount <= 0)
        {
            Player.Instance.luta = true;
            StartCoroutine(fimCombate());
        }
        if (Player.Instance.especial == true && fear == false)
        {
            ataqueButton4.interactable = true;
        }
        if (fireBall == true && fear == false)
        {
            ataqueButton2.interactable = true;
        }
        if (fireBall == false)
        {
            ataqueButton2.interactable = false;
        }
    }

    public void IniciarCombate()
    {
        Player.Instance.moveSpeed = 0;
        painelCombate.SetActive(true);
        onCombate = true;
        enemy = player.inimigoAtual;
        nomeInimigo.text = enemy.nome;
        inimigoImage.sprite = enemy.inimigoSprite;
        falaPanel.SetActive(true);
        combateText.text = enemy.falas[0];
        spriteSmile.sprite = Player.Instance.spriteSmile.sprite;
        vidaPanel.fillAmount = Player.Instance.vidaPanel.fillAmount;
        ataqueButton4.interactable = false;
        switch (Player.Instance.playerId)
        {
            case 0:
                nomeAtaque1.text = "Arma a lazer";
                nomeAtaque2.text = "Bola de fogo";
                nomeAtaque3.text = "Recomponha-se";
                nomeAtaque4.text = "100 mil companheiros!!";
                break;
            case 1:
                nomeAtaque1.text = "Docinho quentinho";
                nomeAtaque2.text = "Hora do show";
                nomeAtaque3.text = "Você está sob o holofote";
                nomeAtaque4.text = "Patas da furia!!";
                break;
        }
        StartCoroutine(fundoTextoSkip());


    } //começar combate

    public void PlayerDesistir() //botão desistir
    {
        painelCombate.SetActive(false);
        onCombate = false;
        player.Desistiu();
        StartCoroutine(finalizarJogo());
    }

    public void startCombateNoob()
    {
        StartCoroutine(combateNoob());
    }

    public void vendoAnna()
    {
        Player.Instance.vidaPanel.fillAmount = vidaPanel.fillAmount + 0.3f;
        StartCoroutine(youtube());
    }

    public void ataque1()
    {
        StartCoroutine(ataque1SPR());
        vidaInimigo.fillAmount = vidaInimigo.fillAmount - 0.15f;
    }

    public void ataque2()
    {
        StartCoroutine(ataque2SPR());
        vidaInimigo.fillAmount = vidaInimigo.fillAmount - 0.35f;
    }

    public void ataque3()
    {
        StartCoroutine(ataque3SPR());
    }

    public void ataque4()
    {
        StartCoroutine(ataque4SPR());
        vidaInimigo.fillAmount = vidaInimigo.fillAmount - 0.75f;
    }

    public void ataqueInimigo1()
    {
        int rnd = Random.Range(0, 4);
        switch (rnd)
        {
            case 0:
                Player.Instance.tomarDano(0.08f);
                painelTexto.SetActive(true);
                combateText.text = "ele te dá um soco";
                StartCoroutine(passarTextoInimigo());

                break;
            case 1:
                Player.Instance.tomarDano(0.15f);
                painelTexto.SetActive(true);
                combateText.text = "Ele bate em você usando a cauda";
                StartCoroutine(passarTextoInimigo());
                break;
            case 2:
                painelTexto.SetActive(true);
                combateText.text = "Você é uma mentira! Ninguém gostaria de ter alguém como você por perto";
                ataqueButton1.interactable = false;
                ataqueButton2.interactable = false;
                ataqueButton4.interactable = false;
                fear = true;
                StartCoroutine(passarTextoInimigo());
                break;
            case 3:
                Player.Instance.tomarDano(0.10f);
                painelTexto.SetActive(true);
                combateText.text = "Ele joga um cacto em você";
                StartCoroutine(passarTextoInimigo());
                break;
        }
    }

    public void ataqueInimigo2()
    {
        int rnd = Random.Range(0, 4);
        switch (rnd)
        {
            case 0:
                Player.Instance.tomarDano(0.08f);
                painelTexto.SetActive(true);
                combateText.text = "O monstro ataca por meio da menina com uma fumaça que te asfixia";
                StartCoroutine(passarTextoInimigo());
                break;
            case 1:
                Player.Instance.tomarDano(0.15f);
                painelTexto.SetActive(true);
                combateText.text = "A fumaça se torna mais densa e cerca Talita, apertando seu peito e a fazendo sentir que vai cair e desmaiar";
                StartCoroutine(passarTextoInimigo());
                break;
            case 2:
                painelTexto.SetActive(true);
                combateText.text = "o monstro se prolifera e começa a exceder agressividade contra o player, começa a aparecer mais olhos e eles ficam um pouco vermelhos recitando a frase “Todos riem de você, você é uma piada, todos veem uma inútil ocupando espaço, você sabe a verdade. ";
                ataqueButton1.interactable = false;
                ataqueButton2.interactable = false;
                ataqueButton4.interactable = false;
                fear = true;
                StartCoroutine(passarTextoInimigo());
                break;
            case 3:
                Player.Instance.tomarDano(0.10f);
                painelTexto.SetActive(true);
                combateText.text = "O monstro lança uma onda de xingamentos junto a uma grande quantidade de fumaça";
                StartCoroutine(passarTextoInimigo());
                break;
        }
    }
    IEnumerator fundoTextoSkip()
    {
        yield return new WaitForSeconds(2);
        painelTexto.SetActive(false);
    }
    IEnumerator passarTextoInimigo()
    {
        switch (Player.Instance.playerId)
        {
            case 0:
                playerImage.sprite = playerDamageSprite[0];
                yield return new WaitForSeconds(1);
                playerImage.sprite = playerNormalSprite[0];
                break;
            case 1:
                playerImage.sprite = playerDamageSprite[1];
                yield return new WaitForSeconds(1);
                playerImage.sprite = playerNormalSprite[1];
                break;
            case 2:
                playerImage.sprite = playerDamageSprite[2];
                yield return new WaitForSeconds(1);
                playerImage.sprite = playerNormalSprite[2];
                break;
        }
        yield return new WaitForSeconds(1);
        painelTexto.SetActive(false);
    }
    IEnumerator fimCombate()
    {
        combateText.text = "Você consegiu vencer o desespero!! Agora está muito cansado e precisa dormir";
        painelTexto.SetActive(true);
        yield return new WaitForSeconds(4);
        Player.Instance.moveSpeed = 3;
        painelCombate.SetActive(false);
        onCombate = false;

    }
    IEnumerator ataque1SPR()
    {
        combateText.text = "Você atirou com sua pistola a lazer!!";
        painelTexto.SetActive(true);
        SPRataque1.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        enemyAnimator.Play("danoInimigo");
        yield return new WaitForSeconds(0.5f);
        enemyAnimator.Play("paradoInimigo");
        SPRataque1.SetActive(false);
        fireBall = true;
        yield return new WaitForSeconds(2);
        painelTexto.SetActive(false);
        if (vidaInimigo.fillAmount > 0)
        {
            ataqueInimigo1();
        }
        yield return new WaitForSeconds(2);

    }

    IEnumerator ataque2SPR()
    {
        combateText.text = "Você atirou uma bola de fogo!!";
        painelTexto.SetActive(true);
        SPRataque2.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        enemyAnimator.Play("danoInimigo");
        yield return new WaitForSeconds(0.5f);
        enemyAnimator.Play("paradoInimigo");
        SPRataque2.SetActive(false);
        fireBall = false;
        yield return new WaitForSeconds(2);
        painelTexto.SetActive(false);
        if (vidaInimigo.fillAmount > 0)
        {
            ataqueInimigo1();
        }
        yield return new WaitForSeconds(2);
    }

    IEnumerator ataque3SPR()
    {
        painelTexto.SetActive(true);
        combateText.text = "Você respira fundo e afasta esses pensamentos";
        SPRataque3.SetActive(true);
        yield return new WaitForSeconds(1);
        SPRataque3.SetActive(false);
        
        if (vidaPlayer.fillAmount < 0.5f)
        {
            Player.Instance.tomarDano(-0.5f);
        }
        yield return new WaitForSeconds(2);
        ataqueButton1.interactable = true;
        ataqueButton2.interactable = true;
        fireBall = true;
        fear = false;
        painelTexto.SetActive(false);

        if (vidaInimigo.fillAmount > 0)
        {
            ataqueInimigo1();
        }
        yield return new WaitForSeconds(2);

    }

    IEnumerator ataque4SPR()
    {
        Player.Instance.especial = false;
        combateText.text = "Você lança sua placa de 100k de inscritos!!";
        painelTexto.SetActive(true);
        SPRataque4.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        enemyAnimator.Play("danoInimigo");
        yield return new WaitForSeconds(0.5f);
        enemyAnimator.Play("paradoInimigo");
        SPRataque4.SetActive(false);
        yield return new WaitForSeconds(2);
        painelTexto.SetActive(false);
        fireBall = true;
        if (vidaInimigo.fillAmount > 0)
        {
            ataqueInimigo1();
        }
        yield return new WaitForSeconds(2);

    }

    IEnumerator youtube()
    {

        youtubePanel.SetActive(true);
        onYoutube = true;
        yield return new WaitForSeconds(2);
        video0.SetActive(true);
        yield return new WaitForSeconds(2);
        video0.SetActive(false);
        video1.SetActive(true);
        yield return new WaitForSeconds(2);
        video1.SetActive(false);
        video2.SetActive(true);
        yield return new WaitForSeconds(2);
        video2.SetActive(false);
        video3.SetActive(true);
        yield return new WaitForSeconds(2);
        video3.SetActive(false);
        youtubePanel.SetActive(false);
        irLive.SetActive(true);
        onYoutube = false;
    }

    IEnumerator combateNoob()
    {

        onCombateNoob = true;
        painelWindows.SetActive(false);
        painelTwitch.SetActive(true);
        yield return new WaitForSeconds(2);
        fala1Panel.SetActive(true);
        yield return new WaitForSeconds(2);
        fala2Panel.SetActive(true);
        yield return new WaitForSeconds(2);
        fala3Panel.SetActive(true);
        yield return new WaitForSeconds(2);
        fala4Panel.SetActive(true);
        yield return new WaitForSeconds(2);
        fala5Panel.SetActive(true);
        yield return new WaitForSeconds(2);
        fala6Panel.SetActive(true);
        yield return new WaitForSeconds(2);
        fala7Panel.SetActive(true);
        yield return new WaitForSeconds(2);
        yield return new WaitForSeconds(0.1f);
        painelTwitch.SetActive(false);
        IniciarCombate();
        onCombateNoob = false;
    }

    IEnumerator finalizarJogo()
    {
        //desativo o confiner 2D
        cam.GetComponent<CinemachineConfiner2D>().enabled = false;
        //cam é a camera atual da room
        //Pego o tamanho da lente da camera
        //Enquanto o tamanhado da lente for maior que 3, eu diminuio 0.02 a cada 0.1f
        while (cam.m_Lens.OrthographicSize > 3)
        {
            cam.m_Lens.OrthographicSize -= 0.02f;
            yield return new WaitForSeconds(0.05f);
        }
        //espera pelo fim da animação
        transiPanel.SetActive(true);
        yield return new WaitForSeconds(2);
        painelGameOver.SetActive(true);
    }// animação de fim de jogo caso escolha desistir 
}
