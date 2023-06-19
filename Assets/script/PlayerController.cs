using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _body;
    private Camera _myCamera;

    private Vector2 _Rotate = new(0, 0);

    [SerializeField] private Vector2 _mouseSensitivity = new(1, 1);
    [SerializeField] private float _speed, _jumpForce, _detectionDistance;
    [SerializeField] private KeyCode _activatorKey = KeyCode.E;

    private GameObject _heldObject;

    private Rigidbody _heldBody;

    private void Awake()
    {
        _body = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        _myCamera = Camera.main;
    }

    private void Update()
    {
        _Rotate.x += Input.GetAxis("Mouse X") * _mouseSensitivity.x * Time.deltaTime;

        _Rotate.y -= Input.GetAxis("Mouse Y") * _mouseSensitivity.y * Time.deltaTime;

        _Rotate.y = Mathf.Clamp(_Rotate.y, -90, 90);

        transform.localRotation = Quaternion.Euler(0, _Rotate.x, 0);
        _myCamera.transform.localRotation = Quaternion.Euler(_Rotate.y, 0, 0);

        if (Input.GetButton("Jump"))
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out RaycastHit hit, Mathf.Infinity))
            {
                if(hit.distance <= 1.1f)
                    _body.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
            }
        }

        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(_activatorKey))
        {
            if (_heldObject == null)
            {
                if (Physics.Raycast(transform.position, DegreeToVector3(_detectionDistance, new Vector2(-_Rotate.y, _Rotate.x)), out RaycastHit hit, Mathf.Infinity) && hit.distance < _detectionDistance)
                {
                    if(hit.collider.gameObject.tag == "door")
                    {
                        DoorController controller = hit.collider.gameObject.transform.parent.GetComponent<DoorController>();

                        if(controller.isOpenable)
                            controller.Open();
                    }
                    else if (hit.collider.gameObject.tag == "helded")
                    {
                        _heldObject = hit.collider.gameObject;
                        _heldObject.transform.position = transform.position + DegreeToVector3(1.45f, new Vector2(-_Rotate.y, _Rotate.x));

                        _heldObject.transform.parent = _myCamera.transform;

                        _heldBody = _heldObject.GetComponent<Rigidbody>();

                        _heldBody.isKinematic = true;
                    }
                }
            }
            else
            {
                _heldBody.isKinematic = false;

                _heldObject.transform.parent = DimensionManager.ActiveDimension.transform;
                _heldObject = null;
            }
        }
    }

    private void FixedUpdate()
    {
        _body.AddForce(transform.forward * Input.GetAxisRaw("Vertical") * _speed + transform.right * Input.GetAxisRaw("Horizontal") * _speed, ForceMode.Force);
    }

    private Vector3 DegreeToVector3(float force, float degree)
    {
        float radDegree = -degree * Mathf.PI / 180;

        return new(force * Mathf.Cos(radDegree), 0, force * Mathf.Sin(radDegree));
    }

    private Vector3 DegreeToVector3(double force, Vector2 rot)
    {
        double yC = rot.y * Math.PI / 180;
        double xC = rot.x * Math.PI / 180;

        double p = force * Math.Cos(xC);
        return new((float)(p * Math.Sin(yC)), (float)(force * Math.Sin(xC)), (float)(p * Math.Cos(yC)));
    }
}