using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntenAanpassen : MonoBehaviour
{
    private GameManager _gm;
    // Start is called before the first frame update
    void Start()
    {
        _gm = GameManager.Instantie;
        _gm.NaPuntenAanpassing.Invoke();
    }

    public void VoegPuntenToe(int aantal)
    {
        _gm.Punten += aantal;
        _gm.NaPuntenAanpassing.Invoke();
    }

    public void TrekPuntenAf(int aantal)
    {
        _gm.Punten -= aantal;
        _gm.NaPuntenAanpassing.Invoke();
    }

    public void OverschrijfAantalPunten(int aantal)
    {
        _gm.Punten = aantal;
        _gm.NaPuntenAanpassing.Invoke();
    }
}
