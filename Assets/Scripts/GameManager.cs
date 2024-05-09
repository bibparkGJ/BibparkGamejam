using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instantie;
    public static GameManager Instantie { get { return _instantie; } }

    private Speler _speler;

    [SerializeField]
    private int _punten;

    public int Punten { get { return _punten; } }

    public UnityEvent NaPuntenAanpassing;
    public UnityEvent NaLevelSwitch;

    private void Awake()//Maak singleton instance van Speler component
    {
        if (_instantie != null && _instantie != this) Destroy(this.gameObject);
        else
        {
            _instantie = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        CheckSpelerReference();
        NaPuntenAanpassing.Invoke();
    }

    private void CheckSpelerReference()
    {
        if (!_speler)
        {
            _speler = Speler.Instantie;
            if (!_speler)
            {
                Debug.Log("Speler Niet gevonden");
            }
        }
    }

    public void PuntenAanpassing(int aantal)
    {
        _punten = Mathf.Max(aantal,0);
        NaPuntenAanpassing.Invoke();
    }

    public void LevelSwitch(string levelNaam)
    {
        SceneManager.LoadSceneAsync(levelNaam);
        NaLevelSwitch.Invoke();
    }
}