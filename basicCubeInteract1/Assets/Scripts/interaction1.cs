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

        public float smoothing = 1f;
        public Transform constraintArea;

        public delegate void OnAttachedToHandDelegate(Hand hand);
        public delegate void OnDetachedFromHandDelegate(Hand hand);
        public event OnAttachedToHandDelegate onAttachedToHand;
        public event OnDetachedFromHandDelegate onDetachedFromHand;

        [System.NonSerialized]
        public Hand attachedToHand;


        void Start()
        {
            StartCoroutine(locationTracker(constraintArea));
        }

        IEnumerator locationTracker(Transform constraintArea)
        {
            yield return new WaitForSeconds(1f);
            float dist = Vector3.Distance(transform.position, constraintArea.position);

            if (dist < 10f) {
                StartCoroutine(objEaseIn(constraintArea)); 
            }
            
        }

        IEnumerator objEaseIn(Transform constraintArea) 
            { 
                print("space found");
                while (Vector3.Distance(transform.position, constraintArea.position) > 0.05f)
                {
                    transform.position = Vector3.Lerp(transform.position, constraintArea.position, smoothing * Time.deltaTime);

                    yield return null;
                }
            

            print("Reached the target.");

            yield return new WaitForSeconds(3f);

            print("MyCoroutine is now finished.");
        }




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
