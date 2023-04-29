using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float rollSpeed = 5.0f; // The speed at which the cube rolls
    [SerializeField] private float gravity = 9.81f; // The strength of gravity affecting the cube
    private Rigidbody rb;
    private bool isMoving = false;
    private int i = 0;
    public GameObject Player;

    public event Action Assemble; // Declare the Assemble event

    public LevelController levelController;
 
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
 
    private void Update()
    {
        if (isMoving) return;
 
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        if (horizontalMovement < 0) TriggerAssemble(Vector3.left);
        else if (horizontalMovement > 0) TriggerAssemble(Vector3.right);
        else if (verticalMovement > 0) TriggerAssemble(Vector3.forward);
        else if (verticalMovement < 0) TriggerAssemble(Vector3.back);

        void TriggerAssemble(Vector3 dir)
        {
            if (dir != Vector3.forward && dir != Vector3.back && dir != Vector3.left && dir != Vector3.right)
            {
                return; // don't allow movement in other directions
            }
            var anchor = transform.position + (Vector3.down + dir) * 0.5f;
            var axis = Vector3.Cross(Vector3.up, dir);
            StartCoroutine(Roll(anchor, axis));
            i++;
            Debug.Log(i);

            // Raise the Assemble event
            if (Assemble != null)
            {
                Assemble.Invoke();
            }
        }
    }
 
    private IEnumerator Roll(Vector3 anchor, Vector3 axis)
    {
        isMoving = true;
        for (var i = 0; i < 90 / rollSpeed; i++)
        {
            transform.RotateAround(anchor, axis, rollSpeed);
            yield return new WaitForSeconds(0.01f);
        }
        isMoving = false;
    }

    private void FixedUpdate()
    {
        // Apply gravity to the cube
        rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
    }
     private void OnCollisionExit(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Platform"))
        if (rb.position.y < 0 )
        {
            // Player is no longer in contact with a platform, reload the level
            Debug.Log("GIR_GIA");
            levelController.LevelReload();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        levelController.NextLevel();
    }
}