using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody rb;
    private float moveSpeed = 5f;
    private Animator anim;
    private float maxDistance = 100;
    private LayerMask floorLayerMask;       //LayerMask도 int도 상관없음.

    private void Start()
    {
        anim = GetComponent<Animator>();
        floorLayerMask = LayerMask.GetMask("Floor");
    }

    private void FixedUpdate()
    {
        Move();
        Turning();

    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float p = Input.GetAxisRaw("Horizontal");

        float v = Input.GetAxisRaw("Vertical");
        bool isWorking = h != 0f || v != 0f;

        anim.SetBool("IsWorking", isWorking);
        Debug.Log(anim.GetBool("IsWorking")?"walking":"idle");

        if (isWorking)
        {
            transform.position += (new Vector3(h, 0f, v) * Time.deltaTime * moveSpeed);
        }
    }

    /*
     * 이동 함수

        키보드 입력을 받음.
        이번에는 애니메이션을 위해
        isWorking이라는 bool타입 변수를 선언
        걷고 있으면(키보드로부터 입력 값을 받고 있으면) True
        가만히 있으면(키보드로부터 아무런 입력 값이 없으면) False

        animation을 설정해 줄 때 해당 상태이름과 ture, false를 매개변수로 넣어줌
        해당 애니메이션 상태를 String타입 이름으로 구별하고 bool타입 true, false로 켜짐,꺼짐을 판별함
        예를들어 걷는 모션이 있을 때
        애니메이션을 생성하고 파라미터(Parameters)에 IsWorking을 추가해 준다.
        그리고 animation스크립트를 생성하고 스크립트 안에
        Animator타입의 변수를 선언해준다 여기서는 예제로  anim 변수가 있다고 가정한다.
        anim.setBool("애니메이션이름", Bool 애니메이션 상태) 이런식으로 넣어준다.
        anim.setBool("IsWorking", IsWorking) 이런식으로 사용하면 된다.
        "IsWorking"은 애니메이션 파라미터 값에 있는 걸로 하면 되고
        IsWorking은 스크립트 내의 변수 이름이다.
     */


    private void Turning()
    {
        //raycast는 정말 중요하다!
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);       //메인 카메라는 단 하나 밖에 없음. Camera.main은 바로 접근 가능
        RaycastHit hit;

        //raycast는 Ray와 카메라가 만나는 지점 hitpoint이다.

        if (!Physics.Raycast(ray, out hit, maxDistance, floorLayerMask))        //만약 화면에 아무것도 감지가 안된다면 종료
        {
            return;
        }

        transform.LookAt(hit.point);
    }
}
