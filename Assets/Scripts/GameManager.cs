using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instantie;
    public static GameManager Instantie { get { return _instantie; } }

    private Speler _speler;
    // Start is called before the first frame update
    void Start()
    {
        _speler = Speler.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            _speler.Herstart();
        }
    }

    void CheckSpelerReference()
    {
        if (!_speler)
        {
            _speler = Speler.Instance;
            if (!_speler)
            {
                Debug.Log("Speler Niet gevonden");
            }
        }
    }
}
