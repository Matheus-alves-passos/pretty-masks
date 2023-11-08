using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public static audioManager instancia;
    public AudioSource audioCombate,audioFundo;
    // Start is called before the first frame update

    private void Awake()
    {
        instancia = this;
    }

    public void musicaFundo()
    {
        audioFundo.Play();
    }
    public void stopMusicaFundo()
    {
        audioFundo.Stop();
    }

    public void tocarSomLuta()
    {
        audioCombate.Play();
    }
    public void pararSomLuta()
    {
        audioCombate.Stop();
    }

}
