using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camera : MonoBehaviour
{

    public CinemachineVirtualCamera Cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Cam.gameObject.SetActive(true);
            //o componente da camera atual eu passo para a variavel do combat manager
            CombatManager.instance.cam = Cam;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Cam.gameObject.SetActive(false);
        }
    }

}
