using BepInEx;
using GorillaExtensions;
using Mod4;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using Utilla;
using TMPro;

namespace DroneMod
{
    /// <summary>
    /// This is your mod's main class.
    /// </summary>

    /* This attribute tells Utilla to look for [ModdedGameJoin] and [ModdedGameLeave] */
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(Mod4.PluginInfo.GUID, Mod4.PluginInfo.Name, Mod4.PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public GameObject drone;
        public GameObject droneobj;
        public GameObject dronecontroller;
        public GameObject power;
        public static bool inRoom;
        public GameObject forwards;
        public GameObject right;
        public GameObject backwards;
        public GameObject left;
        public GameObject up;
        public GameObject down;
        public GameObject leftturn;
        public GameObject rightturn;
        public GameObject blade1;
        public GameObject blade2;
        public GameObject blade3;
        public GameObject blade4;



        void Start() => Events.GameInitialized += OnGameInitialized;


        void OnGameInitialized(object sender, EventArgs e)
        {
            static AssetBundle LoadAssetBundle(string path)
            {
                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
                AssetBundle bundle = AssetBundle.LoadFromStream(stream);
                stream.Close();
                return bundle;
            }

            var bundle = LoadAssetBundle("DroneMod.Resources.drone2");
            drone = Instantiate(bundle.LoadAsset<GameObject>("drone2"));

            SetUp();
        }

        public void SetUp()
        {
            dronecontroller = drone.transform.Find("GameObject").gameObject;
            blade1 = drone.transform.Find("drone obj/Prop4in").gameObject;
            blade2 = drone.transform.Find("drone obj/Prop4in (1)").gameObject;
            blade3 = drone.transform.Find("drone obj/Prop4in (2)").gameObject;
            blade4 = drone.transform.Find("drone obj/Prop4in (3)").gameObject;
            droneobj = drone.transform.Find("drone obj").gameObject;
            down = drone.transform.Find("GameObject/Panel/Fly Mode/key down").gameObject;
            up = drone.transform.Find("GameObject/Panel/Fly Mode/key up").gameObject;
            power = drone.transform.Find("GameObject/power button").gameObject;
            forwards = drone.transform.Find("GameObject/Panel/Fly Mode/forwards").gameObject;
            right = drone.transform.Find("GameObject/Panel/Fly Mode/right").gameObject;
            left = drone.transform.Find("GameObject/Panel/Fly Mode/left").gameObject;
            backwards = drone.transform.Find("GameObject/Panel/Fly Mode/backwards").gameObject;
            leftturn = drone.transform.Find("GameObject/Panel/Rot Mode/left turn").gameObject;
            rightturn = drone.transform.Find("GameObject/Panel/Rot Mode/right turn").gameObject;

            Rigidbody rb = droneobj.GetOrAddComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.mass = 50f;
            rb.drag = 5f;
            
            power.AddComponent<Drone>();
            forwards.AddComponent<Keys>().drone = droneobj;
            right.AddComponent<Keys>().drone = droneobj;
            backwards.AddComponent<Keys>().drone = droneobj;
            left.AddComponent<Keys>().drone = droneobj;
            up.AddComponent<Keys>().drone = droneobj;
            down.AddComponent<Keys>().drone = droneobj;
            leftturn.AddComponent<Keys>().drone = droneobj;
            rightturn.AddComponent<Keys>().drone = droneobj;

            blade1.AddComponent<Spin>();
            blade2.AddComponent<Spin>();
            blade3.AddComponent<Spin>();
            blade4.AddComponent<Spin>();

            forwards.layer = 18;
            right.layer = 18;
            backwards.layer = 18;
            left.layer = 18;
            up.layer = 18;
            down.layer = 18;
            leftturn.layer = 18;
            rightturn.layer = 18;
            power.layer = 18;
            droneobj.layer = 31;

            dronecontroller.transform.parent = GorillaTagger.Instance.offlineVRRig.leftHandTransform.parent;
            dronecontroller.transform.localPosition = new Vector3(-0.0858f, 0.116f, 0.0463f);
            dronecontroller.transform.localRotation = Quaternion.Euler(89.6678f, 160.0998f, 158.6724f);
            dronecontroller.SetActive(false);

            rb.MovePosition(new Vector3(-68.066f, 12.0949f, -78.966f));
            droneobj.transform.position = new Vector3(-68.066f, 12.0949f, -78.966f);
            drone.SetActive(false);
        }

        /* This attribute tells Utilla to call this method when a modded room is joined */
        [ModdedGamemodeJoin]
        public void OnJoin()
        {
            inRoom = true;
            drone.SetActive(true);
            dronecontroller.SetActive(true);

        }

        /* This attribute tells Utilla to call this method when a modded room is left */
        [ModdedGamemodeLeave]
        public void OnLeave()
        {
            inRoom = false;
            drone.SetActive(false);
            dronecontroller.SetActive(false);
        }
    }
}