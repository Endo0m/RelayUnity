using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTransform : MonoBehaviour
{
    public Transform[] waypoints; // массив точек для движения\
    private int currentWaypointIndex = 0; // индекс текущей точки

    public float speed = 5f; // скорость движения объекта
    private bool forward = true; // направление движения
    public float rotationSpeed = 2.0f;

    private void Start()
    {
        // Проверка наличия хотя бы одной точки в массиве
        if (waypoints.Length > 0)
        {
            // Установка начальной позиции в первой точке
            transform.position = waypoints[0].position;
        }
    }

    private void Update()
    {
        if (forward)
        {
            // Движение от 0 до N
            MoveToNextWaypoint();
        }
        else
        {
            // Движение от N до 0
            MoveToPreviousWaypoint();
        }
    }

    private void MoveToNextWaypoint()
    {
        // Проверка достижения последней точки
        if (currentWaypointIndex >= waypoints.Length - 1)
        {
            // Смена направления движения
            forward = false;
        }
        else
        {
            // Перемещение объекта к следующей точке
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex + 1].position, speed * Time.deltaTime);
            Quaternion targetRotation = Quaternion.LookRotation(waypoints[currentWaypointIndex + 1].position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Проверка достижения следующей точки
            if (transform.position == waypoints[currentWaypointIndex + 1].position)
            {
                // Увеличение индекса текущей точки
                currentWaypointIndex++;
            }
        }
    }

    private void MoveToPreviousWaypoint()
    {
        // Проверка достижения первой точки
        if (currentWaypointIndex <= 0)
        {
            // Смена направления движения
            forward = true;
        }
        else
        {
            // Перемещение объекта к предыдущей точке
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex - 1].position, speed * Time.deltaTime);
            Quaternion targetRotation = Quaternion.LookRotation(waypoints[currentWaypointIndex - 1].position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Проверка достижения предыдущей точки
            if (transform.position == waypoints[currentWaypointIndex - 1].position)
            {
                // Уменьшение индекса текущей точки
                currentWaypointIndex--;
            }
        }
    }
}