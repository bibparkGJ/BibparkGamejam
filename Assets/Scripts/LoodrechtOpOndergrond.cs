using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoodrechtOpOndergrond : MonoBehaviour
{
    private Speler _speler;

    private bool _prevRotationLock;

    // Start is called before the first frame update
    void Start()
    {
        _speler = Speler.Instantie;
        _prevRotationLock = _speler._rb.freezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponentInParent<Speler>())
        {
            _speler._rb.freezeRotation = false;
            _speler.PasRotatieToe(_speler._neerwaardseHellingsgraad, true);
        }
    }
}
