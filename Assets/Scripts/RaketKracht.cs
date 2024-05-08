using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaketKracht : MonoBehaviour
{
    private Speler _speler;

    [SerializeField]
    private float _kracht;
    
    // Start is called before the first frame update
    void Start()
    {
        _speler = Speler.Instantie;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Jump"))
        {
            if (_speler._kanSpringen)
            {
                VliegOmhoog();

            }
        }
    }

    void VliegOmhoog()
    {
        _speler.PasKrachtToe(transform.up * _kracht);
    }
}
