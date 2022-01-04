using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;    //Inspectorâ�� ���� �ٸ� ������Ʈ(��ü)�� Transform���� �����ҷ��� ��
    public float speed = 5f;        
    private Vector3 offset;     //ī�޶�� ��� ������Ʈ ������ �Ÿ�.

    public void Awake()
    {
        offset = transform.position - target.position;  //ī�޶� ��� ������Ʈ ������ �Ÿ� = ī�޶��� *�ʱ�* ��ġ - ��� ������Ʈ�� *�ʱ�* ��ġ
    }

    void Update()
    {
        var goalPos = target.position + offset; //ī�޶� �־���� position(��ġ)�� ���� ī�޶� ��ġ + offset ���� ������.
        var nextPos = Vector3.Lerp(transform.position, goalPos, speed * Time.deltaTime);    //������������ Ȱ���Ͽ� ī�޶� �̵� �ÿ� Vector�� ������ ������ ����.
                                                //Player���� ����� normalize�� ����ϰ� ������ ������ �̿��Ͽ� ���� �ܼ�ȭ ��.
                                                //���⼭�� ī�޶� �̵��� �������� �ε巯�� ����.
        transform.position = nextPos;
    }
}
