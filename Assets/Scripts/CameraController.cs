using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;

public class CameraController : MonoBehaviour
{
    public InputManager inputManager;

    private InputAction move;

    private Vector3 moveDirection;
    public Vector3 finalDirection {get; private set;}
    Vector3 newpos;

    [SerializeField]private Transform playerTransform;
    [SerializeField]private float lerpSpeed;
    [SerializeField]private float offset;
    [SerializeField]private float distance;
    [SerializeField]public float speed;
    
    // Start is called before the first frame update
    void Awake()
    {
        inputManager = new InputManager();
        moveDirection = Vector2.zero;
        newpos = transform.position;
    }

    void OnEnable() {
        move = inputManager.Keyboard.CameraMovement;
        move.Enable();
    }

    void OnDisable() {
        move.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        moveDirection = move.ReadValue<Vector2>();
        // Debug.Log(moveDirection);

        // Move fake transform
        moveCam();
        lerpCam();
    }

    void moveCam() {
        finalDirection = new Vector3(moveDirection.x, 0, moveDirection.y);

        finalDirection = Quaternion.AngleAxis(45, Vector3.up) * finalDirection;

        // playerTransform.gameObject.GetComponent<fakeObjectController>().move(finalDirection*speed*Time.deltaTime);
    }

    void lerpCam() {
        transform.position = Vector3.Lerp(
            transform.position, 
            playerTransform.position + new Vector3(-distance+offset, distance-0.2f, -distance),
            lerpSpeed*Time.deltaTime);
    }
}
