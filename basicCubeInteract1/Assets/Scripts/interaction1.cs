using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Valve.VR.InteractionSystem
{

    public class interaction1 : MonoBehaviour
    {
        public SteamVR_ActionSet activateActionSetOnAttach;
        public GameObject target; 
        public Material activeMaterial; 

        public delegate void OnAttachedToHandDelegate(Hand hand);
        public delegate void OnDetachedFromHandDelegate(Hand hand);
        public event OnAttachedToHandDelegate onAttachedToHand;
        public event OnDetachedFromHandDelegate onDetachedFromHand;

        [System.NonSerialized]
        public Hand attachedToHand;

        protected virtual void OnAttachedToHand(Hand hand)
        {
            if (activateActionSetOnAttach != null)
                activateActionSetOnAttach.Activate(hand.handType);

            if (onAttachedToHand != null)
            {
                onAttachedToHand.Invoke(hand);
            }

            attachedToHand = hand;
        }

        void Update()
        {
            if (attachedToHand != null)
            {
                target.GetComponent<MeshRenderer>().material = activeMaterial;
            }
        }
    }
}
