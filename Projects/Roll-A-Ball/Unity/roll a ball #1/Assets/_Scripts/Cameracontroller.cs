using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameracontroller : MonoBehaviour
{
    public GameObject player; 
    public Vector3 offset;
    public float rotSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(player.transform.position, Vector3.up, Input.GetAxis("Mouse X") * Time.deltaTime);
        Vector3 rotatedOFFset = Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * offset;
        transform.position = player.transform.position + rotatedOFFset;
    }
}
