using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

//[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Speler : MonoBehaviour
{
    private static Speler _instantie;
    public static Speler Instantie { get { return _instantie; } }


    //[Range(0, 1)]
    //public float GrondCheckAfstand;
    [SerializeField]
    private float _slopeCheckDistance;
    [SerializeField]
    private float _maximumHellingsgraad;
    [SerializeField]
    private Transform _grondCheckLocatie;
    [SerializeField]
    public LayerMask GrondLaag;


    [SerializeField]
    private PhysicsMaterial2D _spelerZonderGrip;
    [SerializeField]
    private PhysicsMaterial2D _spelerMetGrip;


    [SerializeField]
    [Range(0, 1)] private float _luchtWeerstand = 0;

    [SerializeField]
    private bool _richtSpelerVolgensBeweging = false;
    [SerializeField]
    private bool _debug = false;

    [ShowOnly] public float _neerwaardseHellingsgraad;

    [ShowOnly] public float _zijwaardseHellingsgraad;
    [ShowOnly] public float _laatsteHellingsgraad;

    [ShowOnly] public int _lichaamsRichting = 1;

    [ShowOnly] public bool _raaktOndergrond;
    [ShowOnly] public bool _isOpHelling;
    [ShowOnly] public bool _isInLancering;
    [ShowOnly] public bool _doetBeweging;
    
    [ShowOnly] public bool _kanOpOndergrondBewegen;
    [ShowOnly] public bool _kanSpringen;
    [ShowOnly] public bool _zweeft = false;

    [ShowOnly] public bool _gepauzeerd = false;

    

    public Vector2 _nieuweSnelheid;
    public Vector2 _nieuweKracht { get; private set; }
    //public Vector2 _capsuleColliderSize { get; private set; }

    public Vector2 _hellingLoodrechteVector { get; private set; }
    public Rigidbody2D _rb { get; private set; }
    public Collider2D _cc { get; private set; }


    private float _grondCheckRadius;

    private Vector2 _startPos;
    private float _startRot;
    private Vector2 _startScale;

    private Vector2 _totaleKrachtVector;

    public List<GrondDetectie>  _grondDetectieSensors;
    public List<GrondDetectie> _grondContacten;

    private float _startZwaartekrachtSchaal;


    private void Awake()//Maak singleton instance van Speler component
    {
        if (_instantie != null && _instantie != this) Destroy(this.gameObject);
        else
        {
            _instantie = this;
        } 
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _startPos = transform.position;
        _startRot = transform.rotation.z;
        _startScale = transform.localScale;


        _startZwaartekrachtSchaal = _rb.gravityScale;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Herstart();
        }
    }

    private void FixedUpdate()
    {
        GrondCheck();
        HellingCheck();
        VoerBewegingUit();
        SpringCheck();
    }
    private void GrondCheck()
    {
        _raaktOndergrond = false;
        _grondContacten.Clear();

        foreach (GrondDetectie _sensor  in _grondDetectieSensors)
        {
            if (_sensor.GrondCheck() == true)
            {
                _grondContacten.Add(_sensor);
                _raaktOndergrond = true;
            }
        }
    }

    private void SpringCheck()
    {
        if (_raaktOndergrond && _isInLancering && _rb.velocity.y <= 0)
        {
            _isInLancering = false;
        }

        if (!_raaktOndergrond || _raaktOndergrond && _neerwaardseHellingsgraad <= _maximumHellingsgraad)
        {
            _kanSpringen = true;
        }
        else _kanSpringen = false;
    }

    private void HellingCheck()
    {
        _totaleKrachtVector = Physics2D.gravity + _rb.totalForce;
        foreach (GrondDetectie sensor in _grondContacten)
        {
            //Vector2 checkPos = transform.position - transform.up * _capsuleColliderSize.y / 2;
            Vector2 checkPos = (Vector2) sensor.transform.position + sensor.Offset + _totaleKrachtVector.normalized * sensor.Radius;
            //Debug.DrawLine(checkPos, transform.position, Color.red);

            HorizontaleHellingCheck(checkPos);
            VerticaleHellingCheck(checkPos);
        }

    }

    private void HorizontaleHellingCheck(Vector2 checkPos)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, -Vector2.Perpendicular(_totaleKrachtVector.normalized), _slopeCheckDistance, GrondLaag);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, Vector2.Perpendicular(_totaleKrachtVector.normalized), _slopeCheckDistance, GrondLaag);

        if (_debug)//toon raycasts voor slope detectie
        {
            if(slopeHitFront) Debug.DrawRay(checkPos, -Vector2.Perpendicular(_totaleKrachtVector.normalized) * slopeHitFront.distance, Color.white);
            else Debug.DrawRay(checkPos, -Vector2.Perpendicular(_totaleKrachtVector.normalized) * _slopeCheckDistance, Color.red);

            if(slopeHitBack) Debug.DrawRay(checkPos, Vector2.Perpendicular(_totaleKrachtVector.normalized) * slopeHitBack.distance, Color.white);
            else Debug.DrawRay(checkPos, Vector2.Perpendicular(_totaleKrachtVector.normalized) * _slopeCheckDistance, Color.green);

        }


        if (slopeHitFront)
        {
            _isOpHelling = true;

            _zijwaardseHellingsgraad = Vector2.Angle(slopeHitFront.normal, -_totaleKrachtVector.normalized);

        }
        else if (slopeHitBack)
        {
            _isOpHelling = true;

            _zijwaardseHellingsgraad = Vector2.Angle(slopeHitBack.normal, -_totaleKrachtVector.normalized) ;
        }
        else
        {
            _zijwaardseHellingsgraad = 0.0f;
            _isOpHelling = false;
        }

    }

    private void VerticaleHellingCheck(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, _totaleKrachtVector.normalized, _slopeCheckDistance, GrondLaag);

        if (hit)
        {

            _hellingLoodrechteVector = Vector2.Perpendicular(hit.normal).normalized;

            _neerwaardseHellingsgraad = Vector2.Angle(hit.normal, _totaleKrachtVector.normalized * Vector2.down);

            if (_neerwaardseHellingsgraad != _laatsteHellingsgraad)
            {
                _isOpHelling = true;
            }

            _laatsteHellingsgraad = _neerwaardseHellingsgraad;

            //Debug.DrawRay(hit.point, _hellingLoodrechteVector, Color.blue);
            //Debug.DrawRay(hit.point, hit.normal, Color.green);

        }

        if (_neerwaardseHellingsgraad > _maximumHellingsgraad || _zijwaardseHellingsgraad > _maximumHellingsgraad)
        {
            _kanOpOndergrondBewegen = false;
        }
        else
        {
            _kanOpOndergrondBewegen = true;
        }

        if (_isOpHelling && _kanOpOndergrondBewegen)
        {
            _rb.sharedMaterial = _spelerMetGrip;
        }
        else if(_isOpHelling && !_kanOpOndergrondBewegen)
        {
            _rb.sharedMaterial = _spelerZonderGrip;
        }
        else if (!_isOpHelling && _kanOpOndergrondBewegen)
        {
            _rb.sharedMaterial = _spelerMetGrip;
        }

    }

    public void LanceerNaarLocatie(Vector2 doelwit, bool relatief)
    {
        _doetBeweging = true;

        if (!relatief)
        {
            doelwit = transform.InverseTransformPoint(doelwit);
        }

        Vector2 kracht = LanceerKrachtBerekening(doelwit);
        //_nieuweKracht = kracht;
        _nieuweSnelheid = new Vector2(_rb.velocity.x + kracht.x, kracht.y) ;
        VoerBewegingUit();

        //Debug.DrawLine(transform.position + Vector3.down * _capsuleColliderSize.y/2, transform.position + Vector3.down * _capsuleColliderSize.y/2 + new Vector3(doelwit.x, doelwit.y), Color.red, 1);
        _isInLancering = true;
        _kanSpringen = false;
    }

    private Vector2 LanceerKrachtBerekening(Vector2 doelwit)
    {
        Vector2 zwaartekracht = Physics2D.gravity * _rb.gravityScale;
        Vector2 initieeleSnelheid = BerekenLanceerSnelheid(doelwit, zwaartekracht);

        return initieeleSnelheid;
    }

    private Vector2 BerekenLanceerSnelheid(Vector2 doelwit, Vector2 zwaartekracht)
    {
        return new Vector2(doelwit.x * 4, Mathf.Sqrt(doelwit.y * -2 * zwaartekracht.y));
    }

    public void PasRotatieToe(float hoek, bool oververschrijft)
    {
        if(_gepauzeerd) return;
        if (oververschrijft)
        {
            _rb.rotation = hoek;
        }
        else
        {
            _rb.rotation += hoek;
        }
    }

    public void PasBewegingToe(Vector2 bewegingSnelheid)
    {
        if (_gepauzeerd) return;
        _nieuweSnelheid = bewegingSnelheid;

        //print(bewegingSnelheid + " m/s and can move : " + _kanOpOndergrondBewegen);
        if (_raaktOndergrond && !_isInLancering && _kanOpOndergrondBewegen) //If on ground
        {
            if (_isOpHelling)
            {
                _nieuweSnelheid = new Vector2(bewegingSnelheid.x * -_hellingLoodrechteVector.x, bewegingSnelheid.x * -_hellingLoodrechteVector.y);
            }
            else
            {
                _nieuweSnelheid = new Vector2(bewegingSnelheid.x, 0);
            }
            _doetBeweging = true;
        }
        else if (!_raaktOndergrond) //If in air
        {

            if (_zweeft)
            {
                _nieuweSnelheid = _rb.velocity * (1 - _luchtWeerstand) + bewegingSnelheid;
            }
            else
            {
                _nieuweSnelheid = _rb.velocity + bewegingSnelheid;
            }
            _doetBeweging = true;
        }
    }

    public void PasKrachtToe(Vector2 kracht)
    {
        if (_gepauzeerd) return;
        _rb.AddForce(kracht, ForceMode2D.Impulse);

    }

    private void VoerBewegingUit()
    {
        if (_gepauzeerd) return;//sla over als de game gepauzeert is
        if (_doetBeweging)
        {
            _rb.velocity = _nieuweSnelheid ;
            _doetBeweging = false;
        }
        else
        {
            _nieuweSnelheid = _rb.velocity;
            _nieuweKracht = Vector2.zero;
        }
    }

    public void BeginMetZweven()
    {
        ZwaartekrachtAanpassing(0);
        _zweeft = true;
    }
    public void StopMetZweven()
    {
        ZwaartekrachtAanpassing(_startZwaartekrachtSchaal);
        _zweeft = false;
    }
    private void ZwaartekrachtAanpassing(float schaal)
    {
        _rb.gravityScale = schaal;
    }

    public void Flip()
    {
        if (_gepauzeerd || !_richtSpelerVolgensBeweging) return;
        _lichaamsRichting *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public void Herstart()
    {
        //Pauzeer game
        _gepauzeerd = true;
        
        _rb.velocity = Vector2.zero;
        _rb.totalForce = Vector2.zero;
        _rb.simulated = false;
        //_rb.MovePosition(_startPos);
        gameObject.transform.SetPositionAndRotation(_startPos, Quaternion.AngleAxis(_startRot, Vector3.forward));
        gameObject.transform.localScale = _startScale;


        _rb.simulated = true;
        _rb.totalForce = Vector2.zero;
        _gepauzeerd = false;
        _lichaamsRichting = 1;

        StopMetZweven();
    }
}
