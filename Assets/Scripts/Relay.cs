using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relay : MonoBehaviour
{
    public Transform[] runners; // ������ � ���������-��������
    public float passDistance = 10f; // ���������, ��� ������� ������ ��������� ���� "�������"
    public float speed = 2f; // �������� ������
    public GameObject banana; // ������, ������� �������� ���� ����� ������
    private Vector3 positionBanana = new Vector3(-0.94f, 0.75f, 0.38f);

    private int currentRunnerIndex = 0; // ������ �������� "������"
    private Transform currentRunner; // ������� "�����"

    private void Start()
    {
        // ��������, ��� ������ � "��������" �� ������
        if (runners.Length > 0)
        {
            currentRunner = runners[currentRunnerIndex];
            TransferBanana(currentRunner);
        }


    }

    private void Update()
    {
        if (currentRunner != null)
        {
            // ������������ ����������� � ���������� �������� "������"
            Vector3 direction = runners[(currentRunnerIndex + 1) % runners.Length].position - currentRunner.position;
            direction.y = 0f; // ��������� �������� ������ �� �����������
            currentRunner.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
            currentRunner.LookAt(runners[(currentRunnerIndex + 1) % runners.Length]);

            // ���������, ������ �� ������� "�����" ��������� passDistance �� ����������
            if (Vector3.Distance(currentRunner.position, runners[(currentRunnerIndex + 1) % runners.Length].position) < passDistance)
            {
                TransferBanana(runners[(currentRunnerIndex + 1) % runners.Length]); // �������� ������� ���������� ������
                // ������������� �� ���������� "������"
                currentRunnerIndex = (currentRunnerIndex + 1) % runners.Length;
                runners[currentRunnerIndex].LookAt(runners[(currentRunnerIndex + 1) % runners.Length]);
                currentRunner = runners[currentRunnerIndex];
            }
        }
    }
    void TransferBanana(Transform newRunner)
    {
        // ��������� ������ ��������
        banana.transform.SetParent(newRunner, false);

        // ��������� ��������� �������-������ ������������ ������ �������� (������)
        banana.transform.localPosition = positionBanana;
        banana.transform.localRotation = Quaternion.identity; // ���������� ������� 
    }
}