using GorillaLocomotion.Climbing;
using UnityEngine;

public class Keys : MonoBehaviour
{
    public string key;
    public GameObject drone;
    public Drone controller;
 

    public bool buttonlclicked;


    private float touchTime = 0f;
    private const float debounceTime = 0.25f;

    private const float horizontalMultiplier = 6f, verticalMultiplier = 4.5f;

    void Start()
    {
        
        key = this.transform.name;
        controller = Drone.Instance;
     
        Destroy(GetComponent<Collider>());
        BoxCollider collider = gameObject.AddComponent<BoxCollider>();
        collider.isTrigger = true;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (touchTime + debounceTime >= Time.time) return;

        if (other.TryGetComponent(out GorillaTriggerColliderHandIndicator component) && !component.isLeftHand)
        {
            GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(211, component.isLeftHand, 0.12f);
            GorillaTagger.Instance.StartVibration(component.isLeftHand, GorillaTagger.Instance.tapHapticStrength / 2f, GorillaTagger.Instance.tapHapticDuration);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (touchTime + debounceTime >= Time.time) return;

        if (other.TryGetComponent(out GorillaTriggerColliderHandIndicator component) && !component.isLeftHand)
        {
            touchTime = Time.time;

            GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(212, component.isLeftHand, 0.12f);
            GorillaTagger.Instance.StartVibration(component.isLeftHand, GorillaTagger.Instance.tapHapticStrength / 2f, GorillaTagger.Instance.tapHapticDuration);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out GorillaTriggerColliderHandIndicator component) && !component.isLeftHand)
        {
            if (controller.on)
            {
                if (key == "forwards")
                {
                    drone.GetComponent<Rigidbody>().AddRelativeForce(Vector3.down * horizontalMultiplier, ForceMode.Impulse);
                }
                if (key == "left")
                {
                    drone.GetComponent<Rigidbody>().AddRelativeForce(Vector3.left * horizontalMultiplier, ForceMode.Impulse);
                }
                if (key == "right")
                {
                    drone.GetComponent<Rigidbody>().AddRelativeForce(Vector3.right * horizontalMultiplier, ForceMode.Impulse);
                }
                if (key == "backwards")
                {
                    drone.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * horizontalMultiplier, ForceMode.Impulse);
                }
                if (key == "key up")
                {
                    drone.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * verticalMultiplier, ForceMode.Impulse);
                }
                if (key == "key down")
                {
                    drone.GetComponent<Rigidbody>().AddRelativeForce(Vector3.back * verticalMultiplier, ForceMode.Impulse);
                }
                if (key == "left turn")
                {
                    drone.transform.Rotate(0, 0, -3.1f);
                }
                if (key == "right turn")
                {
                    drone.transform.Rotate(0, 0, 3.1f);
                }

            }

        }
    }

}