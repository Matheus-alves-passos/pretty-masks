using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseManger : MonoBehaviour
{
    [SerializeField] private string nomeDoLevelDeJogo;
    // Start is called before the first frame update
    public void voltarMenu()
    {
        //SceneManager.LoadScene(1);
        Debug.Log("vasco");
        SceneManager.LoadScene(nomeDoLevelDeJogo);
        Time.timeScale = 1;
    }
}
