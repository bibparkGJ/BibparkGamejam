using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuntenBijText : MonoBehaviour
{
    private GameManager _gm;
    private TextMeshProUGUI _textMesh;

    private string _voorText;

    // Start is called before the first frame update
    void Start()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
        _voorText = _textMesh.text;

        _gm = GameManager.Instantie;
        _gm.NaPuntenAanpassing.AddListener(UpdateText);
    }

    void UpdateText()
    {
        _textMesh.text = _voorText + " " + _gm.Punten;
    }    
}
