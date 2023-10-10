using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using Unity.VisualScripting;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;
    public Player player;
    public Enemy enemy;
    public GameObject irLive, youtubePanel, painelWindows, painelCombate, painelAtaque, painelGameOver, transiPanel, falaPanel, painelTwitch, fala1Panel, fala2Panel, fala3Panel, fala4Panel, fala5Panel, fala6Panel, fala7Panel, video0, video1, video2, video3, painelTexto, SPRataque1;
    public TMP_Text combateText, nomeInimigo;
    public string fraseAtaqueInimigo;
    public Image inimigoImage, vidaInimigo, vidaPlayer, playerImage, vidaPanel;

    public Button ataqueButton1, ataqueButton2, ataqueButton3, ataqueButton4;

    public Image spriteSmile;
    //cinemachine camera atual que eu pego no script da room
    public CinemachineVirtualCamera cam;

    public bool onCombate;
    public bool onCombateNoob;
    public bool onYoutube;
    public bool pegouEspecial;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
        StartCoroutine(youtube());
    }

    public void ataque1()
    {
        StartCoroutine(ataque1SPR());
        vidaInimigo.fillAmount = vidaInimigo.fillAmount - 0.15f;
    }

    public void ataqueInimigo()
    {
        int rnd = Random.Range(0, 3);
        switch (rnd)
        {
            case 0:
                Player.Instance.tomarDano(0.8f);
                painelTexto.SetActive(true);
                combateText.text = "nome do ataque";
                break;
            case 1:
                Player.Instance.tomarDano(0.15f);
                painelTexto.SetActive(true);
                combateText.text = "nome do ataque";
                break;
            case 2:
                painelTexto.SetActive(true);
                combateText.text = "Você é uma mentira! Ninguém gostaria de ter alguém como você por perto";
                Player.Instance.tomarDano(0.15f);
                ataqueButton1.interactable = false;
                ataqueButton2.interactable = false;
                ataqueButton4.interactable = false; 
                break;

        }
    }

    IEnumerator ataque1SPR()
    {
        SPRataque1.SetActive(true);
        yield return new WaitForSeconds(1);
        SPRataque1.SetActive(false);
        painelTexto.SetActive(true);
        combateText.text = "Você atirou com sua pistola a lazer!!";
        yield return new WaitForSeconds(2);
        painelTexto.SetActive(false);
        yield return new WaitForSeconds(1);
        ataqueInimigo();
        yield return new WaitForSeconds(3);
        
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
