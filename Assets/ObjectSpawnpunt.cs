using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectSpawnpunt : MonoBehaviour
{
    public bool KanSpawnen = true;

    [SerializeField]
    float _secondenTussenSpawns = 2.0f;
    [SerializeField]
    private GameObject _object;
    [SerializeField]
    [Range(1, 20)] int _maxObjecten;

    private List<GameObject> _spawnedObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObject", Time.timeSinceLevelLoad,_secondenTussenSpawns);
    }

    public void KanSpawnenAanpassen(bool status)
    {
        KanSpawnen = status;
    }

    void SpawnObject()
    {
        if (KanSpawnen)
        {
            if (_spawnedObjects.Count > _maxObjecten)
            {
                Destroy(_spawnedObjects[0]);
                _spawnedObjects.RemoveAt(0);
            }
            _spawnedObjects.Add(Instantiate(_object, transform.position, transform.rotation));

        }

    }
}
