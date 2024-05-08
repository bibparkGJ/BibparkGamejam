using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpelerResetten : MonoBehaviour
{
    private Speler _speler;


    // Start is called before the first frame update
    void Start()
    {
        _speler = Speler.Instantie;
    }

    // Update is called once per frame
    public void ResetSpeler()
    {
        _speler.Herstart();
    }
}
