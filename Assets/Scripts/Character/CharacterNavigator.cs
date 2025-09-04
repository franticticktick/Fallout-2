using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class CharacterNavigator
{
    private readonly NavMeshAgent agent;
    private readonly NavMeshObstacle obstacle;

    //Distance
    protected float distance;
    private float speed;

    private const float START_MOVE_DISTANCE = 0.6f;
    private const float STOP_MOVE_DISTANCE = 0.5f;
    private const float ROTATION_THRESHOLD = 8f;
    private const float WALKING_STOP_SPEED = 0.2f;
    private const float RUNNING_STOP_SPEED = 0.6f;

    public event Action<bool> OnStopMove;
    public event Action<bool> OnStartComingUp;
    public event Action<bool> OnStartMoving;
    public event Action<bool> OnTurnTowardsTheTarge;

    private float stopSpeed = WALKING_STOP_SPEED;

    public CharacterNavigator(NavMeshAgent agent, NavMeshObstacle obstacle)
    {
        this.agent = agent;
        this.agent.enabled = false;
        this.obstacle = obstacle;
    }

    public virtual async Task MoveAsync(Vector3 characterPosition, Vector3 targetPosition)
    {
        await EnableAgent();

        distance = Vector3.Distance(characterPosition, targetPosition);

        if (distance > START_MOVE_DISTANCE)
        {
            StartMove(targetPosition);
        }
        else if (distance <= STOP_MOVE_DISTANCE)
        {
            StopMove(characterPosition, targetPosition);
        }

        speed = Mathf.Clamp(speed, 0, 1);
    }

    public bool TurnTowardsTheTarget(Transform characherTransform, Vector3 enemyPosition)
    {
        Vector3 direction = (enemyPosition - characherTransform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        characherTransform.rotation = Quaternion.Slerp(
            characherTransform.rotation,
            lookRotation,
            Time.deltaTime * 10);

        var angle = Vector3.Angle(characherTransform.forward, enemyPosition - characherTransform.position);
        return angle <= ROTATION_THRESHOLD;
    }

    protected void StartComingUp(Vector3 position)
    {
        agent.SetDestination(position);
        agent.isStopped = false;

        OnStartComingUp?.Invoke(true);
    }

    protected virtual void StartMove(Vector3 targetPosition)
    {
        agent.SetDestination(targetPosition);
        agent.isStopped = false;

        if (distance > 0.7f)
            speed += 3.5f * Time.deltaTime;

        else if (distance < 0.7f)
        {
            if (speed > distance)
                speed -= 2 * Time.deltaTime;
            else speed += 2 * Time.deltaTime;
        }
        OnStartMoving.Invoke(true);
    }

    private void StopMove(Vector3 characterPosition, Vector3 targetPosition)
    {
        speed -= 5 * Time.deltaTime;
        targetPosition = characterPosition;

        if (speed <= stopSpeed)
        {
            agent.isStopped = true;
            OnStopMove.Invoke(true);

            speed = 0;
            DisableAgent();
        }
    }

    public void StopMove()
    {
        speed -= 5 * Time.deltaTime;

        if (speed <= stopSpeed)
        {
            agent.isStopped = true;
            speed = 0;
            DisableAgent();
        }
    }

    public void StopInPlace()
    {
        if (agent.enabled)
        {
            agent.isStopped = true;
            DisableAgent();
        }
    }

    public async Task ComeUpAsync(Transform characherTransform, Vector3 targetInteractPosition)
    {
        await EnableAgent();
        distance = Vector3.Distance(characherTransform.position, targetInteractPosition);

        if (distance > START_MOVE_DISTANCE)
        {
            StartComingUp(targetInteractPosition);
        }
        if (distance <= 1)
        {
            StopMove(characherTransform.position, targetInteractPosition);
            OnStartComingUp?.Invoke(false);
        }

        speed = Mathf.Clamp(speed, 0, 1);
    }

    public async Task<bool> EnableAgent()
    {
        if (!agent.isActiveAndEnabled)
        {
            if (this.obstacle != null)
            {
                this.obstacle.enabled = false;
                await Task.Delay(TimeSpan.FromSeconds(0.01));
            }
            this.agent.enabled = true;
            return true;
        }
        return true;
    }

    private void DisableAgent()
    {
        if (agent.isActiveAndEnabled)
        {
            this.agent.enabled = false;
            if (this.obstacle != null)
            {
                this.obstacle.enabled = true;
            }
        }
    }

    public void SetStopWalkingSpeed()
    {
        this.stopSpeed = WALKING_STOP_SPEED;
    }

    public void SetStopRunningSpeed()
    {
        this.stopSpeed = RUNNING_STOP_SPEED;
    }

    public void SetWalkingSpeed()
    {
        agent.speed = 2;
    }

    public void SetRunningSpeed()
    {
        agent.speed = 5;
    }
}
