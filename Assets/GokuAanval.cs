using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GokuAanval : MonoBehaviour
{
    public float MaxAfstand = 1;
    [SerializeField]
    private LayerMask DoelwitLaag;
    [SerializeField]
    private GameObject AanvalVisual;
    

    private GameManager _gm;
    private int _sterkte;

    // Start is called before the first frame update
    void Start()
    {
        _gm = GameManager.Instantie;
        StopAanvalVisual();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit2D aanval = Physics2D.Raycast(transform.position, transform.right, MaxAfstand, DoelwitLaag);
            Debug.DrawRay(transform.position, transform.right * MaxAfstand, Color.cyan, 0.1f);
            StartAanvalVisual();
            if (aanval)
            {
                GokuVijand gokuVijand = aanval.collider.gameObject.GetComponent<GokuVijand>();
                _sterkte = _gm.Punten;
                gokuVijand.RaakMe(_sterkte);
                
            }
        }
    }

    void StartAanvalVisual()
    {
        if(AanvalVisual) AanvalVisual.SetActive(true);
        Invoke("StopAanvalVisual", 0.5f);
    }

    void StopAanvalVisual()
    {
        if (AanvalVisual) AanvalVisual.SetActive(false);
    }
}
