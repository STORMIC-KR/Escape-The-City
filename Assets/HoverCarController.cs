using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverCarController : MonoBehaviour
{
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public float multiplier;
    public float moveForce, turnTorque;

    public Transform[] anchors = new Transform[4];
    RaycastHit[] hits = new RaycastHit[4];

    private void FixedUpdate()
    {
        for (int i = 0; i < 4; i++)
        {
            ApplyForce(anchors[i], hits[i]);
        }
    }

    private void Update()
    {
        rb.AddForce(Input.GetAxis("Vertical") * moveForce * transform.forward);
        rb.AddTorque(Input.GetAxis("Horizontal") * turnTorque * transform.up);
    }

    void ApplyForce(Transform anchor, RaycastHit hit)
    {
        if(Physics.Raycast(anchor.position, -anchor.up, out hit))
        {
            float force = 0;
            force = Mathf.Abs(1 / (hit.point.y - anchor.position.y));
            rb.AddForceAtPosition(transform.up * force * multiplier, anchor.position, ForceMode.Acceleration);
        }
    }
}
