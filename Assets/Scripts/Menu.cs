using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public void TriggerMenu(int i)
    {
        switch (i)
        {
            default:
            case 0:
                SceneManager.LoadScene("Nivel");//inicia o jogo
                break;
            case 1:
                Application.Quit();//sai do jogo
                break;
        }
    }
}
