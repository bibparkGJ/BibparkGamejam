using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyroscoop : MonoBehaviour
{
    private Speler _speler;

    [SerializeField]
    private float _draaiHoek = 1;

    private float _richting;
    
    // Start is called before the first frame update
    void Start()
    {
        _speler = Speler.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            _richting = -Input.GetAxis("Horizontal");
            RoteerSpeler();
        }
    }

    void RoteerSpeler()
    {
        _speler.PasRotatieToe(_richting * _draaiHoek, false);
    }
}
