using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOverManager : MonoBehaviour
{
    [SerializeField] private string nomeDoLevelDeJogo;
    public void Nao()
    {
        Debug.Log("sair do jogo");
        Application.Quit();
    }

    public void yes()
    {
        SceneManager.LoadScene(nomeDoLevelDeJogo);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
