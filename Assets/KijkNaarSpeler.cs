using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]

public class KijkNaarSpeler : MonoBehaviour
{
    private Speler _speler;
    private CircleCollider2D _circleCollider;
    private bool _kanSpelerZien = false;

    public UnityEvent NaSpelerGespot;
    public UnityEvent NaSpelerVerloren;

    // Start is called before the first frame update
    void Start()
    {
        _speler = Speler.Instantie;
        _circleCollider = GetComponent<CircleCollider2D>();
        _circleCollider.isTrigger = true;
        _circleCollider.enabled = false;
    }


    private void Update()
    {
        float afstand = Vector2.Distance(_speler.transform.position, transform.position);
        if (afstand < _circleCollider.radius)
        {
            if (_kanSpelerZien != true)
            {
                NaSpelerGespot.Invoke();
                _kanSpelerZien = true;
            }
            Vector2 richting = _speler.transform.position - transform.position;
            float hoek = Mathf.Atan2(richting.y, richting.x) * Mathf.Rad2Deg; ;
            
            transform.eulerAngles = new Vector3(0, 0, hoek);
        }
        else 
        {
            if (_kanSpelerZien == true)
            {
                NaSpelerVerloren.Invoke();
                _kanSpelerZien = false;
            }
        }
    }
}
