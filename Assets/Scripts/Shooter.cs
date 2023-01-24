using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject shellPrefab;
    private GameObject _shell;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_shell == null)
            {
                _shell = Instantiate(shellPrefab) as GameObject;
                _shell.transform.position = transform.TransformPoint(Vector3.forward * 0.8f);
                _shell.transform.rotation = transform.rotation;
            }
        }
    }
    
    private IEnumerator SphereIndicator(Vector3 pos) {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;
        yield return new WaitForSeconds(1);
        Destroy(sphere);
    }
}
