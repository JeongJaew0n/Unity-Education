using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;    //Inspector창을 통해 다른 오브젝트(물체)의 Transform값을 참조할려는 듯
    public float speed = 5f;        
    private Vector3 offset;     //카메라와 대상 오브젝트 사이의 거리.

    public void Awake()
    {
        offset = transform.position - target.position;  //카메라 대상 오브젝트 사이의 거리 = 카메라의 *초기* 위치 - 대상 오브젝트의 *초기* 위치
    }

    void Update()
    {
        var goalPos = target.position + offset; //카메라가 있어야할 position(위치)은 현재 카메라 위치 + offset 으로 결정됨.
        var nextPos = Vector3.Lerp(transform.position, goalPos, speed * Time.deltaTime);    //선형보간법을 활용하여 카메라 이동 시에 Vector값 연산을 가볍게 해줌.
                                                //Player에서 사용한 normalize와 비슷하게 수학적 연산을 이용하여 값을 단순화 함.
                                                //여기서는 카메라 이동의 움직임이 부드러워 진다.
        transform.position = nextPos;
    }
}
