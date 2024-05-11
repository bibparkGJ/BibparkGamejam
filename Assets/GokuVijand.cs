using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GokuVijand : MonoBehaviour
{
    public int Sterkte = 9000;

    public UnityEvent VoorIkVernietigdWordt;
    [SerializeField]
    private float _delayVoorVernietiging = 1;
    [SerializeField]
    AnimationClip _welPijn;
    [SerializeField]
    AnimationClip _geenPijn;

    private Animation _anim;

    private void Start()
    {
        _anim = GetComponent<Animation>();
        if(!_anim) _anim = transform.AddComponent<Animation>();
    }
    public void RaakMe(int sterkte)
    {
        if (sterkte > Sterkte)
        {
            VoorIkVernietigdWordt.Invoke();
            _anim.clip = _welPijn;
            _anim.Play();
            Invoke("VernietigMe", _delayVoorVernietiging);
        }
        else
        {
            _anim.clip = _geenPijn;
            _anim.Play();
        }
    }

    private void VernietigMe()
    {
        gameObject.SetActive(false);
    }

}
