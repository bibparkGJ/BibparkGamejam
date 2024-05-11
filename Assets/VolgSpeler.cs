using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VolgSpeler : MonoBehaviour
{
    [SerializeField]
    private Vector2 _spelerOffset = Vector2.zero;

    [SerializeField]
    private float _maxSnelheid = 5;


    private Speler _speler;


    // Start is called before the first frame update
    void Start()
    {
        _speler = Speler.Instantie;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        BeweegNaarSpeler();
    }


    void BeweegNaarSpeler()
    {
        Vector2 snelheid = Vector2.zero;
        Vector2 richting =  (Vector2)(_speler.transform.position - transform.position) + _spelerOffset;

        snelheid = Time.deltaTime * richting;
        
        snelheid = Vector2.ClampMagnitude(snelheid, _maxSnelheid);


        transform.position += (Vector3)snelheid;
    }
}
