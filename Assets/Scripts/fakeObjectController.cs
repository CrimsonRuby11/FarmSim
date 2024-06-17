using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fakeObjectController : MonoBehaviour
{
    [SerializeField]
    private CameraController cameraController;
    private float speed;

    private Rigidbody rb;

    void Awake() {
        speed = cameraController.speed;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        move(cameraController.finalDirection*speed*Time.deltaTime);
    }

    private void move(Vector3 newvel) {
        rb.velocity = newvel;
    }
}
