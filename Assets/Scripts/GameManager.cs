using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private static GameManager _instantie;
    public static GameManager Instantie { get { return _instantie; } }

    private Speler _speler;

    public int Punten;

    public UnityEvent NaPuntenAanpassing;

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
}