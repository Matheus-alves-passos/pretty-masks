using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.SceneManagement;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;
    public Player player;
    public Enemy enemy;
    public GameObject irLive, youtubePanel, painelWindows, painelCombate, painelAtaque, painelGameOver, transiPanel, falaPanel, painelTwitch, video0, video1, video2, video3, painelTexto, SPRataque1, SPRataque2, SPRataque3, SPRataque4, SPRataqueInimigo1, SPRataqueInimigo2, SPRataqueInimigo3, SPRataqueInimigo4, entradaPanel, saidaPanel;
    public TMP_Text combateText, nomeInimigo;
    public string fraseAtaqueInimigo;
    public Image inimigoImage, vidaInimigo, vidaPlayer, playerImage, vidaPanel;

    public Sprite[] playerDamageSprite, playerNormalSprite, spriteButtonAtaque, noobCutScene;
    public Animator enemyAnimator;
    public Button ataqueButton1, ataqueButton2, ataqueButton3, ataqueButton4;
    public TMP_Text nomeAtaque1, nomeAtaque2, nomeAtaque3, nomeAtaque4;

    public int curas;

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
        painelTwitch.GetComponent<Image>().sprite = noobCutScene[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.Instance.especial == true)
        {
            pegouEspecial = true;
        }
        if (fear == true)
        {
            ataqueButton1.GetComponent<Image>().sprite = spriteButtonAtaque[1];
            ataqueButton2.GetComponent<Image>().sprite = spriteButtonAtaque[1];
            ataqueButton4.GetComponent<Image>().sprite = spriteButtonAtaque[1];
        }

        if (fear == false)
        {
            ataqueButton1.GetComponent<Image>().sprite = spriteButtonAtaque[0];
            ataqueButton2.GetComponent<Image>().sprite = spriteButtonAtaque[0];
            ataqueButton4.GetComponent<Image>().sprite = spriteButtonAtaque[0];
        }

        if (pegouEspecial == false)
        {
            ataqueButton4.GetComponent<Image>().sprite = spriteButtonAtaque[1];
        }

        if (vidaInimigo.fillAmount <= 0)
        {
            Player.Instance.luta = true;
            StartCoroutine(fimCombate());
        }
        if (Player.Instance.especial == true && fear == false)
        {
            ataqueButton4.interactable = true;
            ataqueButton4.GetComponent<Image>().sprite = spriteButtonAtaque[0];
        }
        if(Player.Instance.especial == false)
        {
            ataqueButton4.interactable = false;
        }
        if (fireBall == true && fear == false)
        {
            ataqueButton2.interactable = true;
            ataqueButton2.GetComponent<Image>().sprite = spriteButtonAtaque[0];
        }
        if (fireBall == false)
        {
            ataqueButton2.interactable = false;
            ataqueButton2.GetComponent<Image>().sprite = spriteButtonAtaque[1];
        }
    }

    public void IniciarCombate()
    {
        saidaPanel.SetActive(false);
        entradaPanel.SetActive(true);
        Player.Instance.moveSpeed = 0;
        painelCombate.SetActive(true);
        onCombate = true;
        audioManager.instancia.stopMusicaFundo();
        audioManager.instancia.tocarSomLuta();
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
            case 0 or 4:
                nomeAtaque1.text = "Arma a lazer";
                nomeAtaque2.text = "Bola de fogo";
                nomeAtaque3.text = "Recomponha-se";
                nomeAtaque4.text = "100 mil companheiros!!";
                break;

            case 1 or 5:
                nomeAtaque1.text = "Docinho e quentinho";
                nomeAtaque2.text = "Hora do show";
                nomeAtaque3.text = "Você está sob o holofote";
                nomeAtaque4.text = "Patas da furia!!";
                break;

            case 2 or 6:
                nomeAtaque1.text = "Papelada";
                nomeAtaque2.text = "Inutil sendo util ";
                nomeAtaque3.text = "Lembre-se dela";
                nomeAtaque4.text = "Stimtoy";
                break;
        }
        StartCoroutine(fundoTextoSkip());

    } //começar combate

    public void PlayerDesistir() //botão desistir
    {
        painelCombate.SetActive(false);
        onCombate = false;
        player.Desistiu();
        audioManager.instancia.pararSomLuta();
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
        int rng = 0;
        if (vidaPlayer.fillAmount >= 0.9f)
        {
            StartCoroutine(ataque1SPR());
            vidaInimigo.fillAmount = vidaInimigo.fillAmount - 0.15f;
        }

        if (vidaPlayer.fillAmount >= 0.6f && vidaPlayer.fillAmount < 0.9f)
        {
            rng = Random.Range(0, 10);
            if (rng < 7)
            {
                StartCoroutine(ataque1SPR());
                vidaInimigo.fillAmount = vidaInimigo.fillAmount - 0.15f;
            }
            else
            {
                StartCoroutine(missAtaque(0));
            }
        }

        if (vidaPlayer.fillAmount >= 0.3f && vidaPlayer.fillAmount < 0.6f)
        {
            rng = Random.Range(0, 10);
            if (rng < 4)
            {
                StartCoroutine(ataque1SPR());
                vidaInimigo.fillAmount = vidaInimigo.fillAmount - 0.15f;
            }
            else
            {
                StartCoroutine(missAtaque(1));
            }
        }


    }

    public void ataque2()
    {
        int rng = 0;
        if (vidaPlayer.fillAmount >= 1)
        {
            StartCoroutine(ataque2SPR());
            vidaInimigo.fillAmount = vidaInimigo.fillAmount - 0.15f;
        }

        if (vidaPlayer.fillAmount >= 0.6f && vidaPlayer.fillAmount < 0.9f)
        {
            rng = Random.Range(0, 10);
            if (rng < 5)
            {
                StartCoroutine(ataque2SPR());
                vidaInimigo.fillAmount = vidaInimigo.fillAmount - 0.25f;
            }
            else
            {
                StartCoroutine(missAtaque(0));
            }
        }

        if (vidaPlayer.fillAmount >= 0.3f && vidaPlayer.fillAmount < 0.6f)
        {
            rng = Random.Range(0, 10);
            if (rng < 2)
            {
                StartCoroutine(ataque2SPR());
                vidaInimigo.fillAmount = vidaInimigo.fillAmount - 0.25f;
            }
            else
            {
                StartCoroutine(missAtaque(1));
            }
        }

    }

    public void ataque3()
    {
        StartCoroutine(ataque3SPR());
    }

    public void ataque4()
    {
        int rng = 0;
        if (vidaPlayer.fillAmount >= 0.9f)
        {
            StartCoroutine(ataque4SPR());
            vidaInimigo.fillAmount = vidaInimigo.fillAmount - 0.55f;
        }

        if (vidaPlayer.fillAmount >= 0.6f && vidaPlayer.fillAmount < 0.9f)
        {
            rng = Random.Range(0, 10);
            if (rng < 7)
            {
                StartCoroutine(ataque4SPR());
                vidaInimigo.fillAmount = vidaInimigo.fillAmount - 0.55f;
            }
            else
            {
                StartCoroutine(missAtaque(0));
            }
        }

        if (vidaPlayer.fillAmount >= 0.3f && vidaPlayer.fillAmount < 0.6f)
        {
            rng = Random.Range(0, 10);
            if (rng < 4)
            {
                StartCoroutine(ataque4SPR());
                vidaInimigo.fillAmount = vidaInimigo.fillAmount - 0.55f;
            }
            else
            {
                StartCoroutine(missAtaque(1));
            }
        }

    }

    public void ataqueInimigo1()
    {
        int rnd = Random.Range(0, 4);
        switch (rnd)
        {
            case 0:
                Player.Instance.tomarDano(0.08f);
                painelTexto.SetActive(true);
                combateText.color = Color.red;
                combateText.text = "T-REX te dá um chute";
                StartCoroutine(passarTextoInimigo());

                break;
            case 1:
                Player.Instance.tomarDano(0.15f);
                painelTexto.SetActive(true);
                combateText.color = Color.red;
                combateText.text = "T-REX bate em você usando a cauda";
                StartCoroutine(passarTextoInimigo());
                break;
            case 2:
                painelTexto.SetActive(true);
                combateText.color = Color.red;
                combateText.text = "T-REX diz: você é uma mentira! Ninguém gostaria de ter alguém como você por perto";
                ataqueButton1.interactable = false;
                ataqueButton2.interactable = false;
                ataqueButton4.interactable = false;
                fear = true;
                StartCoroutine(passarTextoInimigo());
                break;
            case 3:
                Player.Instance.tomarDano(0.10f);
                painelTexto.SetActive(true);
                combateText.color = Color.red;
                combateText.text = "T-REX joga um cacto em você";
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
                combateText.color = Color.red;
                painelTexto.SetActive(true);
                combateText.text = "O monstro ataca por meio da menina com uma fumaça que te asfixia";
                StartCoroutine(passarTextoInimigo());
                break;
            case 1:
                Player.Instance.tomarDano(0.15f);
                combateText.color = Color.red;
                painelTexto.SetActive(true);
                combateText.text = "A fumaça se torna mais densa e cerca Talita, apertando seu peito e a fazendo sentir que vai cair e desmaiar";
                StartCoroutine(passarTextoInimigo());
                break;
            case 2:
                combateText.color = Color.red;
                painelTexto.SetActive(true);
                combateText.text = "o monstro se prolifera dizendo que você não faz nada certo ";
                ataqueButton1.interactable = false;
                ataqueButton2.interactable = false;
                ataqueButton4.interactable = false;
                fear = true;
                StartCoroutine(passarTextoInimigo());
                break;
            case 3:
                Player.Instance.tomarDano(0.10f);
                combateText.color = Color.red;
                painelTexto.SetActive(true);
                combateText.text = "O monstro lança uma onda de xingamentos junto a uma grande quantidade de fumaça";
                StartCoroutine(passarTextoInimigo());
                break;
        }
    }

    public void ataqueInimigo3()
    {
        int rnd = Random.Range(0, 4);
        switch (rnd)
        {
            case 0:
                Player.Instance.tomarDano(0.08f);
                combateText.color = Color.red;
                painelTexto.SetActive(true);
                combateText.text = "Celular abre um monte de abas sozinho, te deixando confuso";
                StartCoroutine(passarTextoInimigo());
                break;
            case 1:
                Player.Instance.tomarDano(0.15f);
                combateText.color = Color.red;
                painelTexto.SetActive(true);
                combateText.text = "Aparecem anuncios de jogos deixando voce distraido";
                StartCoroutine(passarTextoInimigo());
                break;
            case 2:
                combateText.color = Color.red;
                painelTexto.SetActive(true);
                combateText.text = "Aparece uma mensagem do seu chefe falando que esta com relatorios atrasados";
                ataqueButton1.interactable = false;
                ataqueButton2.interactable = false;
                ataqueButton4.interactable = false;
                fear = true;
                StartCoroutine(passarTextoInimigo());
                break;
            case 3:
                Player.Instance.tomarDano(0.10f);
                combateText.color = Color.red;
                painelTexto.SetActive(true);
                combateText.text = "Celular esta carregado, tendo muito tempo pára perder";
                StartCoroutine(passarTextoInimigo());
                break;
        }
    }
    IEnumerator missAtaque(int erro)
    {
        switch (erro)
        {
            case 0:
                combateText.text = "Voce esta muito ansioso e errou o ataque";
                break;

            case 1:
                combateText.text = "Voce ficou nervoso e acabou errando";
                break;
        }
        painelTexto.SetActive(true);
        yield return new WaitForSeconds(2);
        painelTexto.SetActive(false);
        switch (Player.Instance.playerId)
        {
            case 0:
                ataqueInimigo1();
                break;
            case 1:
                ataqueInimigo2();
                break;
            case 2:
                ataqueInimigo3();
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
        audioManager.instancia.pararSomLuta();
        audioManager.instancia.musicaFundo();
        combateText.color = Color.cyan;
        switch (Player.Instance.playerId)
        {
            case 0:
                combateText.text = "Você consegiu vencer o desespero!! Agora está muito cansado e precisa dormir";
                break;

            case 1:
                combateText.text = "Você consegiu controlar a crise e se acalmar!!";
                break;
            case 2:
                combateText.text = "Você consegue vencer a distração e volta ao trabalho!!";
                break;
            case 4:
                combateText.text = "Você supera seus medos e se sente melhor consigo mesmo";
                break;

        }

        painelTexto.SetActive(true);
        yield return new WaitForSeconds(5);
        Player.Instance.moveSpeed = 3;
        painelCombate.SetActive(false);
        onCombate = false;
        Player.Instance.animaTalita = false;
        yield return new WaitForSeconds(1);
        switch (Player.Instance.playerId)
        {
            case 0:
                break;

            case 1:
                Player.Instance.myAnim.Play("dead");
                yield return new WaitForSeconds(2);
                saidaPanel.SetActive(true);
                yield return new WaitForSeconds(1);
                SceneManager.LoadScene(4);
                break;

            case 4:
                SceneManager.LoadScene(7);
                break;

            case 5:
                SceneManager.LoadScene(8);
                break;

            case 6:
                SceneManager.LoadScene(1);
                break;
        }
    }
    IEnumerator ataque1SPR()
    {
        switch (Player.Instance.playerId)
        {
            case 0 or 4:
                combateText.color = Color.white;
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
                break;

            case 1 or 5:
                combateText.color = Color.white;
                combateText.text = "Você joga um caneca de café nela";
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
                    ataqueInimigo2();
                }
                yield return new WaitForSeconds(2);
                break;

            case 2 or 6:
                combateText.color = Color.white;
                combateText.text = "Voce lança uma chuva de papel que cortam o inimigo";
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
                    ataqueInimigo3();
                }
                yield return new WaitForSeconds(2);
                break;

        }

    }

    IEnumerator ataque2SPR()
    {
        switch (Player.Instance.playerId)
        {
            case 0 or 4:
                combateText.color = Color.white;
                combateText.text = "Voce lança uma bola de fogo";
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
                break;

            case 1 or 5:
                combateText.color = Color.white;
                combateText.text = "Você saca uma guitarra e ataca com notas musicais";
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
                    ataqueInimigo2();
                }
                yield return new WaitForSeconds(2);
                break;

            case 2 or 6:
                combateText.color = Color.white;
                combateText.text = "Voce lança churikens de papel";
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
                    ataqueInimigo3();
                }
                yield return new WaitForSeconds(2);
                break;


        }
    }

    IEnumerator ataque3SPR()
    {
        switch (Player.Instance.playerId)
        {
            case 0 or 4:
                painelTexto.SetActive(true);
                combateText.color = Color.green;
                combateText.text = "Você respira fundo e afasta esses pensamentos";
                SPRataque3.SetActive(true);
                yield return new WaitForSeconds(1);
                SPRataque3.SetActive(false);

                if (vidaPlayer.fillAmount < 0.5f)
                {
                    if(curas <= 1)
                    {
                        curas++;
                        Player.Instance.tomarDano(-0.3f);
                    }
                    
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
                break;

            case 1 or 5:
                painelTexto.SetActive(true);
                combateText.color = Color.green;
                combateText.text = "Você se recompoe após lembrar que você e a unica que tem que saber de si mesma";
                SPRataque3.SetActive(true);
                yield return new WaitForSeconds(1);
                SPRataque3.SetActive(false);

                if (vidaPlayer.fillAmount < 0.5f)
                {
                    if(curas <= 4)
                    {
                        curas++;
                        Player.Instance.tomarDano(-0.3f);
                    }
                    
                }
                yield return new WaitForSeconds(2);
                ataqueButton1.interactable = true;
                ataqueButton2.interactable = true;
                fireBall = true;
                fear = false;
                painelTexto.SetActive(false);

                if (vidaInimigo.fillAmount > 0)
                {
                    ataqueInimigo2();
                }
                yield return new WaitForSeconds(2);
                break;

            case 2 or 6:
                painelTexto.SetActive(true);
                combateText.color = Color.green;
                combateText.text = "Voce lembra de sua amiga, se sentindo acolhido e amado";
                SPRataque3.SetActive(true);
                yield return new WaitForSeconds(1);
                SPRataque3.SetActive(false);

                if (vidaPlayer.fillAmount < 0.5f)
                {
                    if(curas <= 4)
                    {
                        curas++;
                        Player.Instance.tomarDano(-0.3f);
                    }
                    
                }
                yield return new WaitForSeconds(2);
                ataqueButton1.interactable = true;
                ataqueButton2.interactable = true;
                fireBall = true;
                fear = false;
                painelTexto.SetActive(false);

                if (vidaInimigo.fillAmount > 0)
                {
                    ataqueInimigo3();
                }
                yield return new WaitForSeconds(2);
                break;



        }

    }

    IEnumerator ataque4SPR()
    {
        switch (Player.Instance.playerId)
        {
            case 0 or 4:
                Player.Instance.especial = false;
                combateText.color = Color.white;
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
                pegouEspecial = false;
                fireBall = true;
                if (vidaInimigo.fillAmount > 0)
                {
                    ataqueInimigo1();
                }
                yield return new WaitForSeconds(2);
                break;

            case 1 or 5:
                Player.Instance.especial = false;
                combateText.color = Color.white;
                combateText.text = "Você transforma sua mão em uma para de gato e atira uma bola de ar";
                painelTexto.SetActive(true);
                SPRataque4.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                enemyAnimator.Play("danoInimigo");
                yield return new WaitForSeconds(0.5f);
                enemyAnimator.Play("paradoInimigo");
                SPRataque4.SetActive(false);
                yield return new WaitForSeconds(2);
                painelTexto.SetActive(false);
                pegouEspecial = false;
                fireBall = true;
                if (vidaInimigo.fillAmount > 0)
                {
                    ataqueInimigo2();
                }
                yield return new WaitForSeconds(2);
                break;

            case 2 or 6:
                Player.Instance.especial = false;
                combateText.color = Color.white;
                combateText.text = "Voce desmonta sua caneta";
                painelTexto.SetActive(true);
                SPRataque4.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                enemyAnimator.Play("danoInimigo");
                yield return new WaitForSeconds(0.5f);
                enemyAnimator.Play("paradoInimigo");
                SPRataque4.SetActive(false);
                yield return new WaitForSeconds(2);
                painelTexto.SetActive(false);
                pegouEspecial = false;
                fireBall = true;
                if (vidaInimigo.fillAmount > 0)
                {
                    ataqueInimigo3();
                }
                yield return new WaitForSeconds(2);
                break;
        }

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
        switch (Player.Instance.playerId)
        {
            case 0:
                for (int i = 0; i < 21; i++)
                {
                    painelTwitch.GetComponent<Image>().sprite = noobCutScene[i];
                    yield return new WaitForSeconds(2);

                }
                break;

            case 2:
                for (int i = 0; i < 3; i++)
                {
                    painelTwitch.GetComponent<Image>().sprite = noobCutScene[i];
                    yield return new WaitForSeconds(2);

                }
                break;
        }
        saidaPanel.SetActive(true);
        entradaPanel.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        painelTwitch.SetActive(false);
        yield return new WaitForSeconds(1);
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
