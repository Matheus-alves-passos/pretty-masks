using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fadeIN_OUT : MonoBehaviour
{
    public GameObject fadeIN, fadeOUT, aviso1, aviso2;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(fades());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator fades()
    {
        fadeIN.SetActive(true);
        yield return new WaitForSeconds(7);
        fadeIN.SetActive(false);
        fadeOUT.SetActive(true);
        yield return new WaitForSeconds(2);
        aviso1.SetActive(false);
        fadeOUT.SetActive(false);
        fadeIN.SetActive(true);
        aviso2.SetActive(true);
        yield return new WaitForSeconds(7);
        fadeIN.SetActive(false);
        fadeOUT.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }
}
