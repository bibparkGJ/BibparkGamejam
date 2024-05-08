using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterOfMass : MonoBehaviour
{
    private Speler _speler;

    // Start is called before the first frame update
    void Start()
    {
        _speler = Speler.Instance;

        Invoke("UpdateSpelerCOM",0.1f);
    }

    void UpdateSpelerCOM()
    {
        _speler._rb.centerOfMass = transform.position - _speler.transform.position;
    }
}
