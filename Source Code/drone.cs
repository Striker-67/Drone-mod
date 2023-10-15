using DroneMod;
using GorillaLocomotion.Climbing;
using UnityEngine;
using TMPro;

public class Drone : MonoBehaviour
{
    public static Drone Instance { get; private set; }
    public GameObject droneobj;
    public bool on;

    private float touchTime = 0f;
    private const float debounceTime = 0.1f;

    public void Awake() => Instance ??= this;
    public void Start()
    {
        droneobj = GameObject.Find("drone obj");
    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
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

    public void clicked(bool ison) => on = !ison;

    // Update is called once per frame
    void Update()
    {
        if (on)
        {
            droneobj.GetComponent<Rigidbody>().useGravity = false;
            droneobj.GetComponent<Rigidbody>().drag = 5f;
            this.GetComponent<MeshRenderer>().material.color = Color.green;
        }
        else
        {
            droneobj.GetComponent<Rigidbody>().useGravity = true;
            droneobj.GetComponent<Rigidbody>().drag = 0f;
            this.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }
}