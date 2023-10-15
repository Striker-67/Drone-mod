using UnityEngine;

namespace DroneMod
{

    public class Spin : MonoBehaviour
    {
        float speed;

        public void Update()
        {
            if (!Plugin.inRoom) return;
            speed = Mathf.Lerp(speed, Drone.Instance.on ? 400f : 0f, 3f * Time.deltaTime);

            transform.Rotate(0, 0, speed);
        }
    }
}
