using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour

{
    public GameObject painelMenu, painelCreditos, passPanel,painelIN;
    public AudioSource jukebox;
    public AudioClip musica, efeito;
    [SerializeField] private string nomeDoLevelDeJogo;
    [SerializeField] private GameObject painelMenuInicial;


    void start()
    {
        jukebox.Play();
        painelIN.SetActive(true);
        passPanel.SetActive(false);
    }
    public void Jogar()
    {
        StartCoroutine(passMenu());
        painelIN.SetActive(false);
    }


    public void SairJogo()
    {
        Debug.Log("sair do jogo");
        Application.Quit();
    }

    public void MudarCreditos()
    {
        jukebox.PlayOneShot(efeito);
        painelMenu.SetActive(false);
        painelCreditos.SetActive(true);
    }
    public void MudarMenu()
    {
        jukebox.PlayOneShot(efeito);
        painelMenu.SetActive(true);
        painelCreditos.SetActive(false);
    }

    public void MinhaPagina()
    {
        Application.OpenURL("https://instagram.com/mathreur?igshid=OGQ5ZDc2ODk2ZA==");
    }

    IEnumerator passMenu()
    {
        passPanel.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(nomeDoLevelDeJogo);
    }
}
