using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relay : MonoBehaviour
{
    public Transform[] runners; // Массив с объектами-бегунами
    public float passDistance = 10f; // Дистанция, при которой объект перестает быть "бегуном"
    public float speed = 2f; // Скорость бегуна
    public GameObject banana; // Объект, который передают друг другу бегуны
    private Vector3 positionBanana = new Vector3(-0.94f, 0.75f, 0.38f);

    private int currentRunnerIndex = 0; // Индекс текущего "бегуна"
    private Transform currentRunner; // Текущий "бегун"

    private void Start()
    {
        // Проверка, что массив с "бегунами" не пустой
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
            // Рассчитываем направление и перемещаем текущего "бегуна"
            Vector3 direction = runners[(currentRunnerIndex + 1) % runners.Length].position - currentRunner.position;
            direction.y = 0f; // Оставляем движение только по горизонтали
            currentRunner.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
            currentRunner.LookAt(runners[(currentRunnerIndex + 1) % runners.Length]);

            // Проверяем, достиг ли текущий "бегун" дистанцию passDistance до следующего
            if (Vector3.Distance(currentRunner.position, runners[(currentRunnerIndex + 1) % runners.Length].position) < passDistance)
            {
                TransferBanana(runners[(currentRunnerIndex + 1) % runners.Length]); // Передача объекта следующему бегуну
                // Переключаемся на следующего "бегуна"
                currentRunnerIndex = (currentRunnerIndex + 1) % runners.Length;
                runners[currentRunnerIndex].LookAt(runners[(currentRunnerIndex + 1) % runners.Length]);
                currentRunner = runners[currentRunnerIndex];
            }
        }
    }
    void TransferBanana(Transform newRunner)
    {
        // Установка нового родителя
        banana.transform.SetParent(newRunner, false);

        // Установка положения объекта-банана относительно нового родителя (бегуна)
        banana.transform.localPosition = positionBanana;
        banana.transform.localRotation = Quaternion.identity; // Сбрасываем поворот 
    }
}