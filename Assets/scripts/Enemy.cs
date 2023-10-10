using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int IDinimigo;
    public string nome;
    public Sprite inimigoSprite;
    public int vida, vidaMaxima;
    public int dano;
    public string[] falas;
    public bool vivo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void tomarDano(int dano)
    {
        vida -= dano;
        if(vida<= 0)
        {
            vida = 0;
            vivo= false;
        }
    }
}
