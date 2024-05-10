using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventBijSnelheidThreshhold : MonoBehaviour
{
    [SerializeField]
    private float _SnelheidThreshold = 1;

    public UnityEvent NaBovenSnelheidThreshold;
    public UnityEvent NaOnderSnelheidThreshold;

    private Rigidbody2D _rb;

    private bool _onderSnelheidThreshold;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        CheckSnelheid();
    }

    // Update is called once per frame
    void Update()
    {
        CheckSnelheid();
    }

    void CheckSnelheid()
    {
        if (_rb.velocity.magnitude > _SnelheidThreshold)
        {
            if (_onderSnelheidThreshold)
            {
                NaBovenSnelheidThreshold.Invoke();
                _onderSnelheidThreshold = false;
            }
        }
        else
        {
            if (!_onderSnelheidThreshold)
            {
                NaOnderSnelheidThreshold.Invoke();
                _onderSnelheidThreshold = true;
            }
        }
    }
}
