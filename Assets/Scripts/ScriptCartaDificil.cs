using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptCartaDificil : MonoBehaviour
{
    [SerializeField]
    public static bool bloqueado;//não deixa virar as cartas
    [SerializeField]
    private bool estado;// true = virado, false = nao virada
    [SerializeField]
    private bool libras;// true = sinal, false = letra
    [SerializeField]
    private bool resolvida;
    [SerializeField]
    private int valorCarta;// valor da carta
    [SerializeField]
    private bool inicializada = false;// controla se a carta foi iniciada
    private Sprite cartaFrente;//sprite da frente da carta
    private Sprite cartaCostas;//sprite das costas da carta
    private GameObject controladorCarta;

    // Use this for initialization
    void Start()
    {
        estado = true;
        resolvida = false;
        bloqueado = false;
        controladorCarta = GameObject.FindGameObjectWithTag("Controlador");
    }


    public void ViraCarta()
    {
        if (!estado && !bloqueado && !resolvida)
        {
            estado = true;
        }
        else if (!bloqueado & !resolvida)
        {
            estado = false;
        }

        cartaFrente = controladorCarta.GetComponent<Controlador_dificil>().PegaFrenteCarta(valorCarta, libras);
        cartaCostas = controladorCarta.GetComponent<Controlador_dificil>().PegaCostasCarta();
        if (!estado && !bloqueado && !resolvida)
        {
            GetComponent<Image>().sprite = cartaCostas;
        }
        else if (estado && !bloqueado && !resolvida)
        {
            GetComponent<Image>().sprite = cartaFrente;
        }
    }
    public int ValorCarta
    {
        get { return valorCarta; }
        set { valorCarta = value; }
    }

    public bool Estado
    {
        get { return estado; }
        set { estado = value; }
    }

    public bool Resolvida
    {
        get { return resolvida; }
        set { resolvida = value; }
    }

    public bool Inicializada
    {
        get { return inicializada; }
        set { inicializada = value; }
    }

    public bool Bloqueada
    {
        get { return bloqueado; }
        set { bloqueado = value; }
    }
    public bool Libras
    {
        get { return libras; }
        set { libras = value; }
    }


    public void CheckFalso()
    {
        StartCoroutine(Pausa());
    }
    IEnumerator Pausa()
    {
        yield return new WaitForSeconds(1);
        if (!estado)
        {
            GetComponent<Image>().sprite = cartaCostas;

        }
        else if (estado)
        {
        }
        bloqueado = false;
    }
}
