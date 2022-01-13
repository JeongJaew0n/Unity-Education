using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDestory : MonoBehaviour
{
    [SerializeField] float downForce = 5f;

    private void Awake()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            FallCube();
        }

    }

    private void Update()
    {
        
    }

    IEnumerator FallCube()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            transform.Translate(new Vector3(0f, downForce, 0f));
        }
    }
}


