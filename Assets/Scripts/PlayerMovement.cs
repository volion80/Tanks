using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Transform target;
    
    public float rotSpeed = 10.0f;
    public float moveSpeed = 200.0f;
    
    private Rigidbody _rigidbody;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        target = Camera.main.transform;
    }
    
    void Update()
    {
        Vector3 movement = Vector3.zero;
        
        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        if (horInput != 0 || vertInput != 0) {
            movement.x = horInput * moveSpeed;
            movement.z = vertInput * moveSpeed;
            movement = Vector3.ClampMagnitude(movement, moveSpeed);
            
            Quaternion tmp = target.rotation;
            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
            movement = target.TransformDirection(movement);
            target.rotation = tmp;
            
            Quaternion direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);
        }
        movement *= Time.deltaTime;
        
        _rigidbody.velocity = movement;
    }

    void OnCollisionEnter(Collision collision)
    {
        EnemyTarget enemy = collision.collider.GetComponent<EnemyTarget>();
        
        if (enemy != null) {
            Hurt();
        }
        
    }

    public void Hurt()
    {
        Destroy(this.gameObject);
        Managers.Player.UpdateHurt();
    }
}
