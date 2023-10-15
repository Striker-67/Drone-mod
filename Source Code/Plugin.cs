using BepInEx;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Utilla;

namespace Mod4
{
    /// <summary>
    /// This is your mod's main class.
    /// </summary>

    /* This attribute tells Utilla to look for [ModdedGameJoin] and [ModdedGameLeave] */
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public GameObject drone;
        public GameObject droneobj;
        public GameObject dronecontroller;
        public GameObject power;
        bool inRoom;
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



        void Start()
        {
            /* A lot of Gorilla Tag systems will not be set up when start is called /*
			/* Put code in OnGameInitialized to avoid null references */

            Utilla.Events.GameInitialized += OnGameInitialized;

        }

        void OnEnable()
        {
            /* Set up your mod here */
            /* Code here runs at the start and whenever your mod is enabled*/

            HarmonyPatches.ApplyHarmonyPatches();
        }

        void OnDisable()
        {
            /* Undo mod setup here */
            /* This provides support for toggling mods with ComputerInterface, please implement it :) */
            /* Code here runs whenever your mod is disabled (including if it disabled on startup)*/

            HarmonyPatches.RemoveHarmonyPatches();
        }

        void OnGameInitialized(object sender, EventArgs e)
        {
            var bundle = LoadAssetBundle("Mod4.AssetBundle.drone2");
           drone = bundle.LoadAsset<GameObject>("drone2");
          Debug.Log(drone.name);

            SetUp();
            dronecontroller = GameObject.Find("drone2(Clone)/GameObject/");
        }
        public void SetUp()
        {


            drone = Instantiate(drone);

            blade1 = GameObject.Find("Prop4in");
                blade2 = GameObject.Find("Prop4in (2)");
                blade3 = GameObject.Find("Prop4in (3)");
                blade4 = GameObject.Find("Prop4in (1)");
                droneobj = GameObject.Find("drone obj");
                down = GameObject.Find("key down");
                up = GameObject.Find("key up");
                power = GameObject.Find("power button");
                forwards = GameObject.Find("forwards");
                right = GameObject.Find("right");
                left = GameObject.Find("left");
                backwards = GameObject.Find("backwards");
                leftturn = GameObject.Find("left turn");
                rightturn = GameObject.Find("right turn");
                power.AddComponent<drone>();
                forwards.AddComponent<keys>();
                right.AddComponent<keys>();
                backwards.AddComponent<keys>();
                left.AddComponent<keys>();
                up.AddComponent<keys>();
                down.AddComponent<keys>();
                leftturn.AddComponent<keys>();
                rightturn.AddComponent<keys>();

                blade1.AddComponent<Spin>();
                blade2.AddComponent<Spin>();
                blade3.AddComponent<Spin>();
                blade4.AddComponent<Spin>();

                droneobj.transform.position = new Vector3(-65.0292f, 11.8309f, -84.2629f);
                forwards.layer = 18;
                right.layer = 18;
                backwards.layer = 18;
                left.layer = 18;
                up.layer = 18;
                down.layer = 18;
                leftturn.layer = 18;
                rightturn.layer = 18;
                power.layer = 18;
             


        }

        void Update()
        {

            if (inRoom)
            {
                dronecontroller.transform.parent = GorillaLocomotion.Player.Instance.leftControllerTransform;
                dronecontroller.transform.localPosition = new Vector3(0.1527f, 0f, 0.2f);
                dronecontroller.transform.localRotation = Quaternion.Euler(0.4527f, 288.9785f, 284.3723f);
            }
            else
            {
                dronecontroller.transform.parent = drone.transform;
                dronecontroller.transform.localPosition = new Vector3(0, 0f, 0);
                dronecontroller.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            }
                
            

        }
        public AssetBundle LoadAssetBundle(string path)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            AssetBundle bundle = AssetBundle.LoadFromStream(stream);
            stream.Close();
            return bundle;
        }

        /* This attribute tells Utilla to call this method when a modded room is joined */
        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {


            inRoom = true;
           drone.SetActive(true);
            
        }

        /* This attribute tells Utilla to call this method when a modded room is left */
        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {

            inRoom = false;
           
            drone.SetActive(false);
        }
    }
}
