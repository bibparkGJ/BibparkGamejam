using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(GrondDetectie))]
public class Springen : MonoBehaviour
{

    [SerializeField]
    private float _springHoogte = 5;
    [SerializeField]
    private int _maximumSprongenInLucht = 2;
    [SerializeField]

    private int _sprongenInLuchtTeller = 0;
    private Speler _speler;
    private GrondDetectie _gd;


    // Start is called before the first frame update
    void Start()
    {
        _speler = Speler.Instance;
        _gd = GetComponent<GrondDetectie>();
    }

    // Update is called once per frame
    void Update()
    {

        if (_gd.RaaktOndergrond && _sprongenInLuchtTeller != 0)
        {
            _sprongenInLuchtTeller = 0;
        }

        if (_sprongenInLuchtTeller < _maximumSprongenInLucht || _gd.RaaktOndergrond) 
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (_speler._kanSpringen)
                {
                    _sprongenInLuchtTeller++;
                    SpringOmhoog();

                }
            }
        }
    }

    private void SpringOmhoog()
    {
        _speler.LanceerNaarLocatie( new Vector2(0, _springHoogte), true);        
    }
}