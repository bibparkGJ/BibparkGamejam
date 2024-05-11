using UnityEngine;
using UnityEngine.Windows;

[RequireComponent(typeof(GrondDetectie))]
public class HorizontaalBewegen : MonoBehaviour
{


    [SerializeField]
    [Range(0.01f, 10)]
    private float _minimumSnelheid = 0;

    [SerializeField]
    [Range(0.01f, 10)]
    private float _maximaleSnelheid = 5;

    [SerializeField]
    [Range(0.01f,100)]
    private float _maxVersnelling = 0.1f;

    [SerializeField]
    [Range(0, 1)] private float _controleInLucht = 0.05f;


    private Speler _speler;
    private GrondDetectie _gd;
    private float _inputRichting;



    // Start is called before the first frame update
    void Start()
    {
        _speler = Speler.Instantie;
        _gd = GetComponent<GrondDetectie>();
}

    // Update is called once per frame
    void Update()
    {
        _inputRichting = UnityEngine.Input.GetAxisRaw("Horizontal");

        BeweegHorizontaal();
    }

    void BeweegHorizontaal()
    {
        if (!_gd.RaaktOndergrond && _speler._raaktOndergrond || _speler._zweeft) return;

        CheckSpelerRichting();



        if (_inputRichting != 0 )
        {
            float snelheid = _minimumSnelheid * _inputRichting;
            Vector2 richting = Vector2.right;

            if (Mathf.Sign(_speler._nieuweSnelheid.x) == _inputRichting)
            {
                snelheid = _speler._nieuweSnelheid.magnitude * _inputRichting;
            }


            if (_gd.RaaktOndergrond && _speler._kanOpOndergrondBewegen)//Bewegen op de ondergrond
            {
                if (Mathf.Abs(snelheid) <= _maximaleSnelheid)
                {
                    snelheid = snelheid + _maxVersnelling * Time.deltaTime * _inputRichting;
                }
                else
                {
                    snelheid = _maximaleSnelheid * _inputRichting;
                }
            }
            else if (!_gd.RaaktOndergrond && !_speler._raaktOndergrond)//Bewegen in de lucht
            {
                snelheid = _maximaleSnelheid * _inputRichting * _controleInLucht;
            }




            
            _speler.PasBewegingToe(snelheid * richting);
        }
    }

    void CheckSpelerRichting()
    {
        if (_inputRichting == 1 && _speler._lichaamsRichting == -1)
        {
            _speler.Flip();
        }
        else if (_inputRichting == -1 && _speler._lichaamsRichting == 1)
        {
            _speler.Flip();
        }
    }

}
