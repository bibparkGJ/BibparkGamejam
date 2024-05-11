using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ConstanteSnelheid : MonoBehaviour
{
    [SerializeField]
    private float _startSnelheid = 1;

    private Rigidbody2D _rb;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        StelSnelheidIn();
    }


    public void StelSnelheidIn()
    {

        _rb.velocity = _startSnelheid * transform.right;
    }
    // Update is called once per frame

}
