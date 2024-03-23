using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float gravity = 9.8f;
    private float _fallVelocity = 0;
    
    public float jumpForcce;
    public float speed;
    private Vector3 _moveVector;

    private CharacterController _characterController;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _moveVector = Vector3.zero; 
        //Перемещение
        if (Input.GetKey(KeyCode.W)) 
        {
            _moveVector += transform.forward;
        }

        if (Input.GetKey(KeyCode.D))
        {
            _moveVector += transform.right;
        }

        if (Input.GetKey(KeyCode.S))
        {
            _moveVector -= transform.forward;
        }

        if (Input.GetKey(KeyCode.A))
        {
            _moveVector -= transform.right;
        }
        //Прыжок
        if (Input.GetKeyDown(KeyCode.Space) && _characterController.isGrounded) 
        {
            _fallVelocity = -jumpForcce;
        };
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _characterController.Move(_moveVector * speed * Time.deltaTime);

        _fallVelocity += gravity * Time.fixedDeltaTime;
       _characterController.Move(Vector3.down * _fallVelocity * Time.fixedDeltaTime);
    }
}
