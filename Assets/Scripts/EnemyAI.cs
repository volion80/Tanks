using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private GameObject shellPrefab;

    public float speed = 3.0f;
    public float obstacleRange = 5.0f;
    public bool canShoot = false;
    
    private bool _alive;
    private GameObject _shell;
    
    void Start()
    {
        _alive = true;
    }
    
    void Update()
    {
        if (_alive)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.SphereCast(ray, 0.75f, out hit))
            {
                if (canShoot)
                {
                    GameObject hitObject = hit.transform.gameObject;
                    if (hitObject.GetComponent<PlayerMovement>())
                    {
                        if (_shell == null) {
                            _shell = Instantiate(shellPrefab) as GameObject;
                            _shell.transform.position = transform.TransformPoint(0,1.5f,3);
                            _shell.transform.rotation = transform.rotation;
                        }
                    }
                }
                if (!hit.collider.gameObject.CompareTag("Player") && hit.distance < obstacleRange)
                {
                    float angle = Random.Range(-110, 110);
                    transform.Rotate(0, angle, 0);
                }
            }
        }
    }

    public void SetAlive(bool alive)
    {
        _alive = alive;
    }
}
