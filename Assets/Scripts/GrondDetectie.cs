using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

//[RequireComponent(typeof(Collider2D))]
public class GrondDetectie : MonoBehaviour
{
    public bool RaaktOndergrond = false;
    public CircleCollider2D Cc { get; private set; }

    public float Radius { get; private set; }
    public Vector2 Offset { get; private set; }
    private Speler _speler;

    [SerializeField]
    private float _grondCheckAfstand = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        _speler = Speler.Instance;
        _speler._grondDetectieSensors.Add(this);

        Cc = this.GetComponent<CircleCollider2D>();
        

        Radius = Cc.radius;
        Offset =  Cc.offset;
    }

    // Update is called once per frameada
    void Update()
    {
        
    }

    public bool GrondCheck()
    {
        RaaktOndergrond = Physics2D.OverlapCircle((Vector2) transform.position + Offset , Radius + _grondCheckAfstand, gameObject.layer);

        return RaaktOndergrond;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null) 
        {
            //print(collision.gameObject.name);
            //_collision = collision;
            //RaaktOndergrond = true;
            //_speler._raaktOndergrond = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision != null)
        {
            //_collision = null;
            //RaaktOndergrond = false;
            //_speler._raaktOndergrond = false;
        }
    }

    private void OnDrawGizmos()
    {
        if(!Application.isPlaying) return;
        if (RaaktOndergrond) Gizmos.color = Color.green;
        else Gizmos.color = Color.red;

        
        Gizmos.DrawWireSphere( transform.position + (Vector3)Offset, Radius + _grondCheckAfstand);

    }
}
