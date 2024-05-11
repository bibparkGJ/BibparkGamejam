using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

public class Zweven : MonoBehaviour
{
    [SerializeField]
    [Range(0.01f, 10)]
    private float _minimumSnelheid = 0;

    [SerializeField]
    [Range(0.01f, 10)]
    private float _maximaleSnelheid = 5;

    [SerializeField]
    [Range(0.01f, 100)]
    private float _maxVersnelling = 0.1f;


    private Speler _speler;
    private Vector2 _inputRichting;

    // Start is called before the first frame update
    void Start()
    {
        _speler = Speler.Instantie;
    }

    // Update is called once per frame
    void Update()
    {
        if (_speler._raaktOndergrond)
        {
            if (_speler._zweeft)
            {
                _speler.StopMetZweven();
                return;
            } 


        }

        else if(!_speler._raaktOndergrond)
        {
            _inputRichting = new Vector2(UnityEngine.Input.GetAxisRaw("Horizontal"), UnityEngine.Input.GetAxisRaw("Vertical"));
            if (!_speler._zweeft && _inputRichting.y > 0)
            {
                _speler.BeginMetZweven(); 
                return;
            }

            else if(_speler._zweeft)  BeweegSpeler();
        } 

        //_inputRichting = UnityEngine.Input.GetAxisRaw("Vertical");
        //BeweegVerticaal();
    }

    void BeweegSpeler()
    {
        Vector2 snelheid = _minimumSnelheid * _inputRichting;
        //Vector2 richting = Vector2.up;
        if (Mathf.Abs(snelheid.magnitude) < _maximaleSnelheid)
        {
            snelheid += _maxVersnelling * Time.deltaTime * _inputRichting;
        }
        else
        {
            snelheid = _maximaleSnelheid * _inputRichting;
        }
        CheckSpelerRichting();
        _speler.PasBewegingToe(snelheid);
    }

    void CheckSpelerRichting()
    {
        if (_inputRichting.x == 1 && _speler._lichaamsRichting == -1)
        {
            _speler.Flip();
        }
        else if (_inputRichting.x == -1 && _speler._lichaamsRichting == 1)
        {
            _speler.Flip();
        }
    }
}
