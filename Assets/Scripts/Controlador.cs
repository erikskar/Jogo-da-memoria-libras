using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class Controlador : MonoBehaviour
{
    public Sprite[] cartaFrente;
    public Sprite[] cartaFrenteComLetra;
    public Sprite[] cartaFrenteComLetraErro;
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
                val = Random.Range(0, cartaFrente.Length);
                y = valores.Contains(val);
            }
            valores.Add(val);
        }


        List<int> lst = valores.ToList();
        for (int r = 0; r < 2; r++)
        {

            foreach (int valor in valores)
            {
                bool x = false;
                int id = 0;
                while (!x)
                {
                    id = Random.Range(0, cartas.Length);
                    x = !cartas[id].GetComponent<ScriptCarta>().Inicializada;
                }

                cartas[id].GetComponent<ScriptCarta>().ValorCarta = valor;
                cartas[id].GetComponent<ScriptCarta>().Inicializada = true;
            }
        }

        foreach (GameObject c in cartas)
        {
            c.GetComponent<ScriptCarta>().ViraCarta();
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
            if (cartas[i].GetComponent<ScriptCarta>().Estado == true && cartas[i].GetComponent<ScriptCarta>().Resolvida == false)
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
        ScriptCarta.bloqueado = true;
        bool estado = false;



        if (cartas[c[0]].GetComponent<ScriptCarta>().ValorCarta == cartas[c[1]].GetComponent<ScriptCarta>().ValorCarta)
        {
            estado = true;
            acertos++;
            textoAcertos.text = "Número de Acertos: " + acertos;
            if (acertos == 16)
            {
                SceneManager.LoadScene("Menu");
            }
            cartas[c[0]].GetComponent<Image>().sprite = PegaFrenteCarta(cartas[c[0]].GetComponent<ScriptCarta>().ValorCarta, true);
            cartas[c[1]].GetComponent<Image>().sprite = PegaFrenteCarta(cartas[c[1]].GetComponent<ScriptCarta>().ValorCarta, true);


        }
        else
        {
            cartas[c[0]].GetComponent<Image>().sprite = PegaFrenteCartaErro(cartas[c[0]].GetComponent<ScriptCarta>().ValorCarta);
            cartas[c[1]].GetComponent<Image>().sprite = PegaFrenteCartaErro(cartas[c[1]].GetComponent<ScriptCarta>().ValorCarta);
        }
        for (int i = 0; i < c.Count; i++)
        {
            cartas[c[i]].GetComponent<ScriptCarta>().Estado = estado;
            cartas[c[i]].GetComponent<ScriptCarta>().Resolvida = estado;
            cartas[c[i]].GetComponent<ScriptCarta>().CheckFalso();
        }
    }



    public Sprite PegaCostasCarta()
    {
        return cartaCostas;
    }
    public Sprite PegaFrenteCarta(int i, bool estado)
    {
        if (estado)
        {
            return cartaFrenteComLetra[i];
        }
        else
        {
            return cartaFrente[i];
        }
    }

    public Sprite PegaFrenteCartaErro(int i)
    {
        return cartaFrenteComLetraErro[i];

    }
}
