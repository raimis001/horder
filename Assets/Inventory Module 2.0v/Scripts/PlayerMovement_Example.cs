using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_Example : MonoBehaviour {

    GameObject player;
    Vector3 _velocity;  

    [Header("Adjustables")]
    public float moveSpeed = 5f;

    bool moving;

    private void Start()
    {
        InitializeScriptReferences();
    }

    /// <summary>
    /// Contains all outside script references
    /// </summary>
    private void InitializeScriptReferences()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        if (moving)
            player.GetComponent<Rigidbody>().MovePosition(player.GetComponent<Rigidbody>().position + _velocity * Time.fixedDeltaTime);
    }

    public void Move()
    {
        float Vertical = Input.GetAxisRaw("Vertical");
        float Horizontal = Input.GetAxisRaw("Horizontal");

        Vector3 _moveHorz = transform.right * Horizontal;
        Vector3 _moveVert = transform.forward * Vertical;

        _velocity = (_moveHorz + _moveVert).normalized * moveSpeed;

        if (_velocity != Vector3.zero)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
    }
}
