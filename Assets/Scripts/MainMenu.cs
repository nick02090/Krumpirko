using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

   public int _selectedLevel = 1;
 
   public void PlayLevel1()
    {
        _selectedLevel = 1;
        Play();
    }


    public void PlayLevel2()
    {
        _selectedLevel = 2;
        Play();
    }


    public void PlayLevel3()
    {
        _selectedLevel = 3;
        Play();

    }

    private void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
