using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class SpelerBijObject : MonoBehaviour
{
    private Speler _speler;

    public UnityEvent OpMomentVanAankomst;


    // Start is called before the first frame update
    void Start()
    {
        _speler = Speler.Instantie;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == _speler.gameObject) 
        { 
            OpMomentVanAankomst.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == _speler.gameObject)
        {
            OpMomentVanAankomst.Invoke();
        }
    }
}
