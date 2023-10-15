using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Utilla;

namespace Mod4
{
   
    public class Spin : MonoBehaviour
    {
        public bool InRoom;
        public void Update()
        {
            if (InRoom)
            {
                if (!GameObject.Find("power button").gameObject.GetComponent<drone>().on)
                {
                    this.transform.Rotate(0, 0, 100);
                }
            }
           



            
        }
        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {


            InRoom = true;

        }

        /* This attribute tells Utilla to call this method when a modded room is left */
        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {

            InRoom = false;

        }

    }
}
