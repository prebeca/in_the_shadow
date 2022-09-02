using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] Transform form = null;

    [SerializeField] int RotationSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // Debug.Log("MouseButtonDown 0");
            if (Mathf.Abs(Input.GetAxis("Mouse X")) > Mathf.Abs(Input.GetAxis("Mouse Y")))
                form.GetComponent<Rigidbody>().AddTorque(Vector3.down * Input.GetAxis("Mouse X") * RotationSpeed * Time.deltaTime);
            else
                form.GetComponent<Rigidbody>().AddTorque(Vector3.right * Input.GetAxis("Mouse Y") * RotationSpeed * Time.deltaTime);
        }
    }
}
