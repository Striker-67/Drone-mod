using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keys : MonoBehaviour
{
    public string key;
    public GameObject drone;
    public drone controller;


    void Start()
    {
        key = this.transform.name;
        drone = GameObject.Find("drone obj");

        drone.GetComponent<Rigidbody>().drag = 5f;
        controller = GameObject.Find("power button").GetComponent<drone>();
    }
    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if (controller.on)
        {
            if (key == "forwards")
            {
                drone.GetComponent<Rigidbody>().AddForce(Vector3.right, ForceMode.Impulse);
            }
            if (key == "left")
            {
                drone.GetComponent<Rigidbody>().AddForce(Vector3.back, ForceMode.Impulse);
            }
            if (key == "right")
            {
                drone.GetComponent<Rigidbody>().AddForce(Vector3.forward, ForceMode.Impulse);
            }
            if (key == "backwards")
            {
                drone.GetComponent<Rigidbody>().AddForce(Vector3.left, ForceMode.Impulse);
            }
            if (key == "key up")
            {
                drone.GetComponent<Rigidbody>().AddForce(Vector3.up, ForceMode.Impulse);
            }
            if (key == "key down")
            {
                drone.GetComponent<Rigidbody>().AddForce(Vector3.down, ForceMode.Impulse);
            }
            if (key == "left turn")
            {
                drone.transform.Rotate(0, 0, 3);
            }
            if (key == "right turn")
            {
                drone.transform.Rotate(0, 0, -3);
            }
        }
    }
}
