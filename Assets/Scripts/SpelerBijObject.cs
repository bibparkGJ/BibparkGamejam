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
        _speler = Speler.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == _speler.gameObject) 
        { 
            OpMomentVanAankomst.Invoke();
        }
    }
}
