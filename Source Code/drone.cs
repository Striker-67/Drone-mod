using DroneMod;
using GorillaLocomotion.Climbing;
using UnityEngine;
using TMPro;

public class Drone : MonoBehaviour
{
    public static Drone Instance { get; private set; }
    public GameObject droneobj;
    public GameObject dronecontroller;
    public GameObject Enablebutton;
    public GameObject powerbutton;
    public bool on;
    public bool buttonlclicked;

    private float touchTime = 0f;
    private const float debounceTime = 0.1f;

    public void Awake() => Instance ??= this;
    public void Start()
    {


    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(this.name == "power button")
        {
            if (touchTime + debounceTime >= Time.time) return;

            if (other.TryGetComponent(out GorillaTriggerColliderHandIndicator indicator))
            {

                touchTime = Time.time;

                GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(67, indicator.isLeftHand, 0.05f);
                GorillaTagger.Instance.StartVibration(indicator.isLeftHand, GorillaTagger.Instance.tapHapticStrength / 2f, GorillaTagger.Instance.tapHapticDuration);

                clicked(on);
            }
        }
        else
        {
            if (touchTime + debounceTime >= Time.time) return;

            if (other.TryGetComponent(out GorillaTriggerColliderHandIndicator indicator))
            {

                touchTime = Time.time;

                GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(67, indicator.isLeftHand, 0.05f);
                GorillaTagger.Instance.StartVibration(indicator.isLeftHand, GorillaTagger.Instance.tapHapticStrength / 2f, GorillaTagger.Instance.tapHapticDuration);

                didclick(buttonlclicked);
            }
        }

    }
    public void didclick(bool hasclicked)
    {
        if (hasclicked)
        {
            Enablebutton.GetComponent<MeshRenderer>().material.color = Color.red;
            buttonlclicked = false;
            droneobj.SetActive(false);
            dronecontroller.SetActive(false);
        }
        if (!hasclicked)
        {
            Enablebutton.GetComponent<MeshRenderer>().material.color = Color.green;
            buttonlclicked = true;
            droneobj.SetActive(true);
            dronecontroller.SetActive(true);
        }
    }
    public void clicked(bool ison) => on = !ison;

    // Update is called once per frame
    void Update()
    {
        if (on)
        {
            if (ControllerInputPoller.instance.rightControllerPrimaryButton)
            {
                droneobj.transform.position = GorillaLocomotion.Player.Instance.rightControllerTransform.position;
            }
            droneobj.GetComponent<Rigidbody>().useGravity = false;

            droneobj.GetComponent<Rigidbody>().drag = 5f;

            powerbutton.GetComponent<MeshRenderer>().material.color = Color.green;
        }
        else
        {

            droneobj.GetComponent<Rigidbody>().useGravity = true;
            droneobj.GetComponent<Rigidbody>().drag = 0f;
            powerbutton.GetComponent<MeshRenderer>().material.color = Color.red;
        }

    }
}