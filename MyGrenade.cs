using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class MyGrenade : MonoBehaviour
{
    public GameObject grenade;

    public float throwPower = 1.5f;

    public float grenadeInterval = 2f;
    public float explosionInterval = 2.5f;

    Vector3 offset;

    Vector3 originPosition;
    Quaternion originRotation;

    Ray ray;
    RaycastHit raycastHit;

    #region GetFromGrenade
    ParentConstraint parentIsPlayer;
    Rigidbody grenadeRigidbody;
    TrailRenderer grenadeTrail;
    AudioSource grenadeAudioSource;
    public ParticleSystem grenadeParticle;
    #endregion
    public AudioClip throwClip;
    public AudioClip explosionClip;
    AudioSource myAudio;

    WaitForSeconds sec;
    WaitForSeconds exSec;

    private void Awake()
    {
        parentIsPlayer = grenade.GetComponent<ParentConstraint>();
        grenadeRigidbody = grenade.GetComponent<Rigidbody>();
        grenadeTrail = grenade.GetComponent<TrailRenderer>();
        grenadeAudioSource = grenade.GetComponent<AudioSource>();

        myAudio = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        myAudio.clip = throwClip;
        SetZero(true);     // 초기에는 안전핀 꼽힌 상태
        offset = transform.position - grenade.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && grenadeRigidbody.isKinematic)
        {
            Debug.Log("Hello!");
            SetZero(false);     // 안전핀 뽑음
            myAudio.Play();
            ThrowMe();
        }
    }

    void ThrowMe()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycastHit, 100f))
        {
            Debug.Log("던져!");

            Vector3 forceVector = raycastHit.point - transform.position;
            forceVector.y = 2f;

            grenadeRigidbody.AddForce(forceVector * throwPower, ForceMode.Impulse);
            grenadeRigidbody.AddTorque(Vector3.back * 10f, ForceMode.Impulse);
            StartCoroutine(Explosion());
        }
    }

    IEnumerator reloadGrenade()
    {
        Debug.Log("꺼졌구만");
        sec = new WaitForSeconds(grenadeInterval);      // 사라진 후 부터 재충전까지기다림
        Debug.Log("리로드시작");
        yield return sec;
        Debug.Log("리로드 완료");

        grenade.transform.position = transform.position + offset;

        grenade.SetActive(true);
        SetZero(true);     // 재충전되면 안전핀 꼽음
        grenade.GetComponent<MeshRenderer>().enabled = true;
    }

    IEnumerator Explosion()
    {
        grenadeAudioSource.clip = explosionClip;
        exSec = new WaitForSeconds(explosionInterval);
        yield return exSec;
        grenadeAudioSource.Play();
        grenadeParticle.Play();
        RaycastHit[] rayHits = Physics.SphereCastAll(grenade.transform.position, 15, Vector3.up, 0, LayerMask.GetMask("Enemy"));
        foreach(RaycastHit hitObj in rayHits) 
        { 
            // 적이 데미지 입음
        }
        grenade.GetComponent<MeshRenderer>().enabled = false;
        grenade.GetComponent<TrailRenderer>().enabled = false;
        StartCoroutine(WaitForSound());
    }

    IEnumerator WaitForSound()
    {
        sec = new WaitForSeconds(explosionClip.length);
        yield return sec;
        grenade.SetActive(false);
        StartCoroutine(reloadGrenade());
    }



    private void SetZero(bool state)        // Kinematic과 parent Constraint 조절
    {
        grenadeRigidbody.isKinematic = state;
        parentIsPlayer.enabled = state;
        grenadeTrail.enabled = !state;
    }
}
