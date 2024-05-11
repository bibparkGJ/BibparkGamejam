using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaadSceneViaNaam : MonoBehaviour
{
    public string _levelNaam = "Menu_Levels";

    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instantie;
    }

    public void LaadLevel(float wachttijd = 0.1f)
    {
        Invoke("LaadLevelNaWachttijd", wachttijd);
        //if (SceneManager.GetSceneByName(_levelNaam).IsValid())
        //{
            
        //}
        //else {
        //    print(_levelNaam + " scene niet gevonden!");
        //}
    }

    private void LaadLevelNaWachttijd()
    {
        _gameManager.LevelSwitch(_levelNaam);
    }
}
