using System.Collections;
using System.Collections.Generic;
using UnityEngine;  
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour

{
    public FixedJoystick joystick;
    public float moveSpeed = 5f;
    public GameObject projectilePrefab;
    public Transform shootPoint;

    private Vector3 moveDirection;
    private bool isShooting = false;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleTapToShoot();
    }

    void HandleMovement()
    {
        Vector2 input = joystick.Direction;
        moveDirection = new Vector3(input.x, 0f, input.y);
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    void HandleRotation()
    {
        if (isShooting) return;

        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 0.2f);
        }
    }

    void HandleTapToShoot()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == UnityEngine.TouchPhase.Began)
            {
                Vector2 tapPos = touch.position;

                if (tapPos.x < 200 && tapPos.y < 200)
                    return;

                Vector3 worldTap = mainCamera.ScreenToWorldPoint(new Vector3(tapPos.x, tapPos.y, 10f));
                Vector3 shootDir = (worldTap - transform.position);
                shootDir.y = 0;

                if (shootDir.magnitude > 0.1f)
                {
                    Shoot(shootDir.normalized);
                }
            }
        }
    }
    void Shoot(Vector3 direction)
    {
        isShooting = true;

        Quaternion lookRot = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = lookRot;

        GameObject bullet = Instantiate(projectilePrefab, shootPoint.position, lookRot);
        bullet.GetComponent<Rigidbody>().velocity = direction * 10f;

        Invoke(nameof(ResetShoot), 0.2f);
    }


    void ResetShoot()
    {
        isShooting = false;
    }
}
