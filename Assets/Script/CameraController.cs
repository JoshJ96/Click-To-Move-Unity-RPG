using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public Vector3 offset;

    public float minZoom = 3f;
    public float maxZoom = 15f;
    public float zoomSpeed = 10f;
    public float rotateSpeed = 100f;

    private float currentZoom = 10f;
    private float rotateInputX = 0f;
    private float rotateInputY = -1f;

    public float minRotateY;
    public float maxRotateY;

    void Update()
    {
        rotateInputX -= (Input.GetAxis("Horizontal")) * rotateSpeed * Time.deltaTime;
        rotateInputY -= ((Input.GetAxis("Vertical")) * rotateSpeed * Time.deltaTime) / 100;

        //Middle Mouse Affecting Rotation Variables
        if (Input.GetMouseButton(2))
        {
            rotateInputX += ((Input.GetAxisRaw("Mouse X")) * rotateSpeed * Time.deltaTime) / 2;
            rotateInputY += (Input.GetAxis("Mouse Y")) * 2 * Time.deltaTime;
        }
        rotateInputY = Mathf.Clamp(rotateInputY, minRotateY, maxRotateY);

        //Scrollwheel zoom in/out
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        }
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        //Camera positioning

        transform.position = (target.position - (offset * currentZoom));
        transform.LookAt(target.position + (Vector3.up * 2));

        //Set panning values to camera
        transform.RotateAround(target.position, Vector3.up, rotateInputX);
        offset = new Vector3(offset.x, Mathf.Clamp(rotateInputY, minRotateY, maxRotateY), offset.z);
    }

}