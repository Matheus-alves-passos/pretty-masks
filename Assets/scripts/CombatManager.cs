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
    public GameObject painelCombate, painelAtaque, painelGameOver;
    public TMP_Text combateText, nomeInimigo;
    public Image inimigoImage, vidaInimigo, vidaPlayer, playerImage;

    //cinemachine camera atual que eu pego no script da room
    public CinemachineVirtualCamera cam;

    public bool onCombate;

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
        painelCombate.SetActive(true);
        onCombate = true;
        enemy = player.inimigoAtual;
        nomeInimigo.text = enemy.nome;
        inimigoImage.sprite = enemy.inimigoSprite;
        combateText.text = enemy.falas[0];

    } //começar combate

    public void PlayerDesistir() //botão desistir
    {
        painelCombate.SetActive(false);
        onCombate = false;
        player.Desistiu();
        StartCoroutine(finalizarJogo());
    }

    IEnumerator finalizarJogo()
    {
        //desativo o confiner 2D
        cam.GetComponent<CinemachineConfiner2D>().enabled = false;
        //cam é a camera atual da room
        //Pego o tamanho da lente da camera
        //Enquanto o tamanhado da lente for maior que 3, eu diminuio 0.02 a cada 0.1f
        while (cam.m_Lens.OrthographicSize > 3) {
            cam.m_Lens.OrthographicSize -= 0.02f;
            yield return new WaitForSeconds(0.05f);
        }
        //espera pelo fim da animação
        yield return new WaitForSeconds(3);
        painelGameOver.SetActive(true);
    }// animação de fim de jogo caso escolha desistir 
}
