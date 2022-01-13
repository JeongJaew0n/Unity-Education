using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeFloorGenerator : MonoBehaviour
{
    public GameObject cube;     // 떨어지는 바닥을 구성할 큐브 프리팹
    [SerializeField] Vector3 floorSize = new Vector3(10f,0f,10f);   // 떨어지는 바닥의 크기

    // Start is called before the first frame update
    void Start()
    {
        for(int i= (int)-floorSize.x; i<floorSize.x; i++)
        {
            for(int j=(int)-floorSize.z; j<floorSize.z; j++)
            {
                GameObject cubeBlock = Instantiate(cube);
                cubeBlock.transform.position = new Vector3((float)i, -1.5f, (float)j);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
