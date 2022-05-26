using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 50f;
    private float shiftMultiplier;
    public float scrollSpeed = 250f;
    public float rotateSpeed = 150f;

    void FixedUpdate()
    {
        
        shiftMultiplier = Input.GetKey(KeyCode.LeftShift) ? 1.5f : 1f;

        if (Input.GetKey("w"))
        {
            transform.Translate(Vector3.forward * moveSpeed * shiftMultiplier * Time.deltaTime);
        }
        if (Input.GetKey("s"))
        {
            transform.Translate(Vector3.back * moveSpeed * shiftMultiplier * Time.deltaTime);
        }
        if (Input.GetKey("a"))
        {
            transform.Translate(Vector3.left * moveSpeed * shiftMultiplier * Time.deltaTime);
        }
        if (Input.GetKey("d"))
        {
            transform.Translate(Vector3.right * moveSpeed * shiftMultiplier * Time.deltaTime);
        }

        GetComponent<Camera>().fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed * Time.deltaTime;
        GetComponent<Camera>().fieldOfView = Mathf.Clamp(GetComponent<Camera>().fieldOfView, 30, 90);

        
        if (Input.GetMouseButton(1))
        {
            //Clamp this
            transform.eulerAngles += rotateSpeed * new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * Time.deltaTime;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}