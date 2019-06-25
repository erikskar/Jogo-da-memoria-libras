using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class Controlador_dificil : MonoBehaviour
{
    public Sprite[] cartaLibras;
    public Sprite[] cartaLibrasErro;
    public Sprite[] cartaLibrasAcerto;
    public Sprite[] cartaPT;
    public Sprite[] cartaPTErro;
    public Sprite[] cartaPTAcerto;
    public Sprite cartaCostas;
    public GameObject[] cartas;
    public Text textoTentativas;
    public Text textoAcertos;
    private int acertos = 0;
    private int tentativas = 0;
    private bool inicializado = false;


    // Update is called once per frame
    void Update()
    {
        if (!inicializado)
        {
            SetarCartas();
        }
        if (Input.GetMouseButtonUp(0))
        {
            VerificaCartas();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }

    void SetarCartas()
    {
        var valores = new HashSet<int>() { };
        int val = 0;
        for (int i = 0; i < 16; i++)
        {
            bool y = true;
            while (y)
            {
                val = Random.Range(0, cartaLibras.Length);
                y = valores.Contains(val);
            }
            valores.Add(val);
        }

        for (int r = 0; r < 2; r++)
        {

            foreach (int valor in valores)
            {
                bool x = false;
                int id = 0;
                while (!x)
                {
                    id = Random.Range(0, cartas.Length);
                    x = !cartas[id].GetComponent<ScriptCartaDificil>().Inicializada;
                }

                cartas[id].GetComponent<ScriptCartaDificil>().ValorCarta = valor;
                cartas[id].GetComponent<ScriptCartaDificil>().Inicializada = true;
                if (r==0)
                    cartas[id].GetComponent<ScriptCartaDificil>().Libras = true;
                else
                    cartas[id].GetComponent<ScriptCartaDificil>().Libras = false;
            }
        }

        foreach (GameObject c in cartas)
        {
            c.GetComponent<ScriptCartaDificil>().ViraCarta();
        }
        if (!inicializado)
        {
            inicializado = true;
        }
    }

    void VerificaCartas()
    {
        List<int> c = new List<int>();
        for (int i = 0; i < cartas.Length; i++)
        {
            if (cartas[i].GetComponent<ScriptCartaDificil>().Estado == true && cartas[i].GetComponent<ScriptCartaDificil>().Resolvida == false)
            {
                c.Add(i);
            }
        }
        if (c.Count == 2)
        {
            Comparar(c);
            tentativas++;
            textoTentativas.text = "Numero de Tentativas: " + tentativas;
        }
    }

    void Comparar(List<int> c)
    {
        ScriptCartaDificil.bloqueado = true;
        bool estado = false;



        if (cartas[c[0]].GetComponent<ScriptCartaDificil>().ValorCarta == cartas[c[1]].GetComponent<ScriptCartaDificil>().ValorCarta)
        {
            estado = true;
            acertos++;
            textoAcertos.text = "Número de Acertos: " + acertos;
            if (acertos == 16)
            {
                SceneManager.LoadScene("Menu");
            }

            cartas[c[0]].GetComponent<Image>().sprite = PegaFrenteCartaAcerto(cartas[c[0]].GetComponent<ScriptCartaDificil>().ValorCarta, cartas[c[0]].GetComponent<ScriptCartaDificil>().Libras);
            cartas[c[1]].GetComponent<Image>().sprite = PegaFrenteCartaAcerto(cartas[c[1]].GetComponent<ScriptCartaDificil>().ValorCarta, cartas[c[1]].GetComponent<ScriptCartaDificil>().Libras);
        }
        else
        {
            cartas[c[0]].GetComponent<Image>().sprite = PegaFrenteCartaErro(cartas[c[0]].GetComponent<ScriptCartaDificil>().ValorCarta, cartas[c[0]].GetComponent<ScriptCartaDificil>().Libras);
            cartas[c[1]].GetComponent<Image>().sprite = PegaFrenteCartaErro(cartas[c[1]].GetComponent<ScriptCartaDificil>().ValorCarta, cartas[c[1]].GetComponent<ScriptCartaDificil>().Libras);
        }
        for (int i = 0; i < c.Count; i++)
        {
            cartas[c[i]].GetComponent<ScriptCartaDificil>().Estado = estado;
            cartas[c[i]].GetComponent<ScriptCartaDificil>().Resolvida = estado;
            cartas[c[i]].GetComponent<ScriptCartaDificil>().CheckFalso();
        }
    }



    public Sprite PegaCostasCarta()
    {
        return cartaCostas;
    }
    public Sprite PegaFrenteCarta(int i, bool libras)
    {
        if (libras)
        {

            return cartaLibras[i];
        }
        else
        {
            return cartaPT[i];
        }
        
    }

    public Sprite PegaFrenteCartaAcerto(int i, bool libras)
    {
        if (libras)
        {

            return cartaLibrasAcerto[i];
        }
        else
        {
            return cartaPTAcerto[i];
        }

    }
    public Sprite PegaFrenteCartaErro(int i, bool libras)
    {
        if (libras)
        {

            return cartaLibrasErro[i];
        }
        else
        {
            return cartaPTErro[i];
        }

    }

}
