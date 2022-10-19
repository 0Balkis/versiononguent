using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void LoadLevel()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat("SpawnX", -7.6f);
        PlayerPrefs.SetFloat("SpawnY", -2.9f);
        SceneManager.LoadScene("Jeu");
    }

    public void QuitGame(){
        Application.Quit();
    }
}
