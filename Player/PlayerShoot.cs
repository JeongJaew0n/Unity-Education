using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform gunBerrelEnd;   //Inspector창에서 GunberrelEnd 객체(게임 오브젝트)를 넣어 줄 거임
    private LineRenderer gunLine;   //Renderer는 그려주는 역할. Line을 그려준다. 여기서는 총의 발사 범위를 그려준다.

    public int damage = 30;
    public float range = 100f;      //총의 발사 범위(레이저가 눈에 보이는 거리)
    private Ray ray = new Ray();    //Ray가 뭘까요
    private RaycastHit hit;         //Ray가 뻗어 나와 hit한 경우일듯?
    private int layerMask;


    public float shootInterval = 0.2f;
    private float timer = 0.0f;

    private void Awake()
    {
        gunLine = GetComponent<LineRenderer>(); //컴포넌트 가져오기(객체 생성).
        LIneOff();

        layerMask = LayerMask.GetMask("Enemy");
    }

    private void Update()
    {
        timer += Time.deltaTime;    //타이머 만들기 가장 효과 좋음.
        if (Input.GetMouseButton(0) && shootInterval <= timer)
        {
            timer = 0f;
            Shoot();
        }
    }

    private void Shoot()
    {
        gunLine.SetPosition(0, gunBerrelEnd.position);  //Position 세팅인데... 앞의 index는 아마 점 하나 인듯. index마다 선을 연결 해줄텐데
                                                        //0과 1을 지정해주면 직선을 긋는 방식이겠지.

        ray.origin = gunBerrelEnd.position;     //레이저의 원본(시작) 포지션.
        ray.direction = gunBerrelEnd.forward;   //forward가 파란 축 벡터값을 정규화해서 가져옴. 

        if (Physics.Raycast(ray, out hit, range, layerMask))
        {//물리효과 (레이저, 맞았을 경우 hit정보가 저장될 변수, 레이저의 최대 범위, 효과가 발생할 레이어) 형식 인듯.
            //여기서 out 키워드는 매개변수로 끝나는게 아니라 가공해서 주소에 값이 저장되는 듯.

            gunLine.SetPosition(1, hit.point);      //라인이 오브젝트와 만나는(hit)하는 부위까지만 레이저 표시.
            //Destroy(hit.transform.gameObject);      //삭제. hit에서 바로 gameObject를 못 불러서 transform을 경유
            EnemyHealth eh = hit.transform.GetComponent<EnemyHealth>(); //hp를 깍기 위해선 hit한 object를 불러 와야하는데 적의 타입은 enemyHealth이기 때문에
            //EnemyHealth를 가져올 수 있는 방법이 바로 위의 이 방법
            if (eh != null)     //hit를 통해 enemy객체를 들고 와야하는데 해당 오브젝트가 EnemyHealth타입이 아닐 경우 에러가 발생하기 때문에 조건문 사용
            {
                eh.TakeDamage(damage, hit.point);
            }
        }
        else
        {
            var goalPosition = gunBerrelEnd.position + (gunBerrelEnd.forward * range);  //LineRenderer가 해당 범위로 그림.
            //여기서는 gunBerrelEnd(총구위치)부터 gunBerrelEnd.forward(총구의 방향, forward는 z축) * range(설정해준 범위, 여기서는 100f) 까지 라인이 그려짐.

            gunLine.SetPosition(1, goalPosition);       //아마 1이 레이저를 표시 한다는거고 goalPosition이 레이저가 끝나는 지점의 벡터 값인 듯.

        }

        StartCoroutine(ShootLineOff());
    }

    IEnumerator ShootLineOff()
    {
        gunLine.enabled = true;     //LineOff()가 gunLIne을 아예 비활성화 하기 때문에 여기서 활성화를 해줘야 보인다.
        yield return new WaitForSeconds(0.01f);      //shootInterval보다는 작아야 오류가 안남.
        LIneOff();
    }

    private void LIneOff()
    {
        gunLine.enabled = false;
    }
}
