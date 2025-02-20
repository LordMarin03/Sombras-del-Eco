using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Eco
{
    public class BackgroundController : MonoBehaviour
    {
        [SerializeField]
        private float startPos, length;

        public GameObject cam;

        public float parallaxEffect; //La velocitat la cual el fons es mourà amb la camera


        void Start()
        {
            startPos = transform.position.x;
            length = GetComponent<SpriteRenderer>().bounds.size.x;
        }

        void FixedUpdate()
        {
            float distance = cam.transform.position.x * parallaxEffect; // 0 = moviment amb camera || 1 = no moure || 0.5 = meitat
            float movement = cam.transform.position.x * (1 - parallaxEffect);
            float movementy = cam.transform.position.y * (1 - parallaxEffect);


            transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

            if(movement > startPos + length)
            {
                startPos += length;
            }
            else if(movement < startPos - length)
            {
                startPos -= length;
            }
        }
    }
}

