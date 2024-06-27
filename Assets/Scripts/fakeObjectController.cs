using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fakeObjectController : MonoBehaviour
{
    public static fakeObjectController instance;

    [SerializeField]
    private CameraController cameraController;
    private float speed;

    private bool cannotMove;

    private Rigidbody rb;

    void Awake() {
        if(instance == null) {
            instance = this;
        } else if(instance != this) {
            Destroy(this);
        }

        speed = cameraController.speed;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        move(cameraController.finalDirection*speed*Time.deltaTime);

        if(cannotMove) move(Vector3.zero);
    }

    private void move(Vector3 newvel) {
        rb.velocity = newvel;
    }

    public void setMove(bool b) {
        cannotMove = !b;
    }
}
