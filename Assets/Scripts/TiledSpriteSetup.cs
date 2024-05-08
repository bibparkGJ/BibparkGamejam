using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[ExecuteInEditMode]
public class TiledSpriteSetup : MonoBehaviour
{
    private SpriteRenderer _sr;
    private BoxCollider2D _collider;

    private bool _hasBoxCollider = true;
    private Vector3 _prevScale;

    // Start is called before the first frame update
    void Start()
    {
        if (!_sr)
        {
            _sr = GetComponent<SpriteRenderer>();
        }
        if (!_collider)
        {
            _collider = GetComponent<BoxCollider2D>();
            if(_collider) _hasBoxCollider=true;
            else _hasBoxCollider=false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale != _prevScale)
        {
            _sr.size = 


            //update at end of statement
            _prevScale = transform.localScale;
        }
    }
}
