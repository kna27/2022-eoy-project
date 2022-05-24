using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 50f;
    public float shiftMultiplier;
    public float scrollSpeed = 250f;
    public float rotateSpeed = 150f;
    private Vector3 direction;

    void FixedUpdate()
    {

        shiftMultiplier = Input.GetKey(KeyCode.LeftShift) ? 1.5f : 1f;

        if (Input.GetKey("w"))
        {
            direction = Vector3.forward;
        }
        else if (Input.GetKey("s"))
        {
            direction = Vector3.back;
        }
        else if (Input.GetKey("a"))
        {
            direction = Vector3.left;
        }
        else if (Input.GetKey("d"))
        {
            direction = Vector3.right;
        }
        else
        {
            direction = Vector3.zero;
        }
        transform.Translate(direction * moveSpeed * shiftMultiplier * Time.deltaTime);
        GetComponent<Camera>().fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed * Time.deltaTime;
        GetComponent<Camera>().fieldOfView = Mathf.Clamp(GetComponent<Camera>().fieldOfView, 30, 90);

        if (Input.GetMouseButton(1))
        {
            transform.eulerAngles += rotateSpeed * new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * Time.deltaTime;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}