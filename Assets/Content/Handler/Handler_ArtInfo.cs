using Content.Models;
using UnityEngine;

namespace Content.Handler
{
    public class Handler_ArtInfo : MonoBehaviour
    {
        public ArtPiece Art;
        [SerializeField] Transform InfoTarget;


        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<Handle_VisionRange>().ActivateFieldOfView(true);
                Debug.Log("Display field of view");
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<Handle_VisionRange>().ActivateFieldOfView(false);
                Debug.Log("Hide field of view");
            }
        }

        public Vector3 GetInfoPosition_WorldSpace()
        {
            Vector3 art_pos_ws =  (InfoTarget.position);
            return art_pos_ws;
        }

    }
}