using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaadSceneViaNaam : MonoBehaviour
{
    public string _levelNaam = "Menu_Levels";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LaadLevel(float wachttijd = 0.1f)
    {
        if (SceneManager.GetSceneByName(_levelNaam).IsValid())
        {
            Invoke("LaadLevelNaWachttijd", wachttijd);
        }
        else {
            print(_levelNaam + " scene niet gevonden!");
        }
    }

    private void LaadLevelNaWachttijd()
    {
        SceneManager.LoadSceneAsync(_levelNaam);
    }
}
