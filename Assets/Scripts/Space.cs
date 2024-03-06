using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space : MonoBehaviour
{


    void Start()
    {
       
    }

    void Update()
    {
        //Scroll infinito veretical
        transform.position +=new Vector3(0, -2 * Time.deltaTime);

        if(transform.position.y < -15.92)
        {
            transform.position=new Vector3(transform.position.x, 15.92f);
        }
    }

}
