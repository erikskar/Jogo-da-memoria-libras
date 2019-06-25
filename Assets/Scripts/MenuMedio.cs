using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMedio : MonoBehaviour
{

    public void TriggerMenu(int i)
    {
        switch (i)
        {
            default:
            case 0:
                SceneManager.LoadScene("Jogo_Medio");//inicia o jogo
                break;
            case 1:
                Application.Quit();//sai do jogo
                break;
        }
    }
}
