using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int hp = 100;        //적체력
    public int score = 30;      //적을 잡았을 때 얻는 점수
    public float sinkDownDrag = 10f;    //
    public AudioClip hurtClip;  //적이 맞았을 때 내는 소리
    public AudioClip deathClip; //적이 죽었을 때 내는 소리
    public ParticleSystem hitParticle;  //적이 맞았을 때 나오는 효과(파티클)
    public NavMeshAgent navMeshAgent;   //

    private new AudioSource audio;

    IEnumerator Start()
    {
        audio = GetComponent<AudioSource>();
        audio.clip = hurtClip;

        yield return new WaitUntil(()=> (transform.position.y <= -3f));   //WaitUntil-> 조건 안에 있는 것이 만족될 때 까지 기다린다. 여기서 조건: 내 높이가 -3밑으로 떨어지면
        Destroy(gameObject);                                            //gameObject 파괴.
    }

    IEnumerator WaitGameOver()
    {
        yield return null;
    }

    public void TakeDamage(int dam, Vector3 hitPoint)
    {
        hp -= dam;
        if(hitParticle != null)
        {
            hitParticle.transform.position = hitPoint;
            hitParticle.Play();
        }
        
        if(hp <=0)
        {
            EnemyDeath();
        }

        audio.Play();

       
    }

    private void EnemyDeath()
    {
        GetComponent<Rigidbody>().drag = sinkDownDrag;
        GetComponent<Rigidbody>().angularDrag = 0f;
        Destroy(GetComponent<Collider>());  //직접 지정을 안해줘도 해당 Colider를 가져옴.
        // 적 이동 컴포넌트 파괴

        audio.clip = deathClip;
        GetComponent<Animator>().SetTrigger("IsDie");        //animation

        // 스코어 적산 가능
    }


}
