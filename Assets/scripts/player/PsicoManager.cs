using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PsicoManager : MonoBehaviour
{
    public static PsicoManager Instanci;
    public bool psi;
    public GameObject painelPsi, paciente;
    public TMP_Text textoPsi;

    public Sprite[] fotoSprites;
    private void Awake()
    {
        Instanci = this;
    }
    void Start()
    {

    }

    void Update()
    {

    }
    public void chamarPsico()
    {
        psi = true;
        StartCoroutine(Psico());
    }

    IEnumerator Psico()
    {
        Player.Instance.moveSpeed = 0;
        switch (Player.Instance.playerId)
        {
            case 4:
                Player.Instance.myAnim.Play("andre parado");
                break;

            case 5:
                Player.Instance.myAnim.Play("talita parada");
                break;

            case 6:
                Player.Instance.myAnim.Play("tomas parado");
                break;
        }


        transform.position = new Vector3(57.4f, 4.1f, 0);
        yield return new WaitForSeconds(2);
        painelPsi.SetActive(true);
        switch (Player.Instance.playerId)
        {
            case 4:
                textoPsi.text = "Ps�cologo - Ol� Andr� voc� aparenta estar com problemas posso ajuda-lo?";
                break;

            case 5:
                textoPsi.text = "Ps�cologo - Ol� Talita voc� aparenta estar com problemas posso ajuda-la?";
                break;

            case 6:
                textoPsi.text = "Ps�cologo - Ol� Andr� voc� aparenta estar com problemas posso ajuda-lo?";
                break;
        }
        yield return new WaitForSeconds(6);
        switch (Player.Instance.playerId)
        {
            case 4:
                textoPsi.text = "Andr� - Ent�o senhor matheus, eu me encontro um pouco ruim esses dias sinto como se eu n�o fosse eu mesmo para os outros";
                break;

            case 5:
                textoPsi.text = "Talita - Senhor, me sinto observada por todos os lados";
                break;

            case 6:
                textoPsi.text = "Thomas - Ol�, �hh... ent�o, meio que n�o consigo me concentrar direito";
                break;
        }
        yield return new WaitForSeconds(6);
        switch (Player.Instance.playerId)
        {
            case 4:
                textoPsi.text = "Ps�cologo - Entendo, que tal trabalharmos para resolver isso? voc� pode estar sofrendo com s�ndrome do impostor, mas vamos tentar entrar mais nesse assunto";
                break;

            case 5:
                textoPsi.text = "Ps�cologo - Entendo, a senhora pode estar sofrendo com fobia social mas me conte-me mais";
                break;

            case 6:
                textoPsi.text = "Ps�cologo - Entendo, o senhor tem alguns tra�os de TDAH mas iremos trabalhar nisso para termos certezas";
                break;
        }
        yield return new WaitForSeconds(6);
        switch (Player.Instance.playerId)
        {
            case 4:
                textoPsi.text = "Andr� - Eu sinto como eu n�o fosse ningu�m al�m daquilo que as pessoas querem que eu seja, ele fica falando para eu desistir de tudo e eu.. eu n�o sei como... ahh..";
                break;

            case 5:
                textoPsi.text = "Talita - N�o me sinto bem com pessoas me olhando e me sinto muito constrangida quando fa�o alguma atrapalhada na frente dos outros, fico com medo deles rirem de mim..";
                break;

            case 6:
                textoPsi.text = "Thomas - Me desculpa, o que o senhor estava falando mesmo?, estava prestando aten��o no diabinho que esta atras... de voc�..?";
                break;
        }
        yield return new WaitForSeconds(2);
        painelPsi.SetActive(false);
        yield return new WaitForSeconds(2);
        CombatManager.instance.IniciarCombate();
    }

    IEnumerator passarFase()
    {
        yield return new WaitForSeconds(1);
    }
}
