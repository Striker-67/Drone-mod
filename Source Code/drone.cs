using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drone : MonoBehaviour
{
    public bool on = false;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {

        clicked(on);
    }
    public void clicked(bool ison)
    {
        if (!ison)
        {
            on = true;
        }
        if (ison)
        {
            on = false;
        }
    }



    // Update is called once per frame
    void Update()
    {


        if (on)
        {

            this.GetComponent<MeshRenderer>().material.color = Color.green;
        }
        else
        {

            this.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }
}
