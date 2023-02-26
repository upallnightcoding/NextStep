using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnCntrl : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float moveSpeed;

    private CharacterController controller;
    private Animator animator;
    private Vector3 target;

    private PawnStateType state;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        state = PawnStateType.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state) {
            case PawnStateType.IDLE:
                break;
            case PawnStateType.RUNNING:
                state = Chase(target, Time.deltaTime);
                break;
        }
    }

    public void StartRunning(Vector3 target) 
    {
        Vector2 randomPoint = Random.insideUnitCircle;

        this.target.x = target.x + randomPoint.x * gameData.targetDistance;
        this.target.y = 0.0f;
        this.target.z = target.z + randomPoint.y * gameData.targetDistance;

        state = PawnStateType.RUNNING;
        animator.SetInteger("state", (int) PawnStateType.RUNNING);
    }

    private PawnStateType Chase(Vector3 target, float dt)
    {
        float distance = Vector3.Distance(target, transform.position);
        Vector3 direction = (target - transform.position).normalized;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * dt);

        transform.rotation = rotation;

        controller.Move(transform.forward * moveSpeed * Time.deltaTime);

        PawnStateType state = PawnStateType.IDLE;

        if (distance < 0.1f)
        {
            state = PawnStateType.IDLE;
            animator.SetInteger("state", (int) PawnStateType.IDLE);
        } else {
            state = PawnStateType.RUNNING;
        }

        return(state);
    }

    private void OnEnable() 
    {
        PawnManager.OnTarget += StartRunning;
    }

    private void OnDisable() 
    {
        PawnManager.OnTarget -= StartRunning;
    }
    
}

public enum PawnStateType 
    {
        IDLE,
        RUNNING
    }
