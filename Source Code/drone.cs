using GorillaLocomotion.Climbing;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public static Drone Instance { get; private set; }

    public bool on;

    private float touchTime = 0f;
    private const float debounceTime = 0.7f;

    public void Awake() => Instance ??= this;

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

            this.GetComponent<MeshRenderer>().material.color = Color.green;
        }
        else
        {

            this.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }
}
