using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.XR;

public class Pig : MonoBehaviour
{
    public int health;
    public float speed;
    public float moveDistance = 2.0f;
    public float waitTime = 0.5f;
    public Transform player;
    public LayerMask playerLayer;//referencias al jugador
   

    private IEnemyState currentState;
    private IEnemyState previousState;

    //State
    public void Start()
    {
        ChangeState(new WalkState());
        player = FindObjectOfType<Player>().gameObject.transform;
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.Execute();
        }

        Vector2 direction = transform.localScale.x == 1 ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 5f, playerLayer);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
            //se chequea el posible cambio de estado cada vez que se hace un update
        {
            ChangeState(new DetectionState());
        }
       
    }
   
    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null) 
        {
            currentState.Exit();
        }
        previousState = currentState;
        currentState = newState;
        currentState.Enter(this);

        currentState = newState;
    }
    public IEnemyState GetPreviousState()
    {
        return previousState;
    }

   private void OnTriggerEnter2D(Collider2D other)
    {
        // Si la bala colisiona con un objeto distinto al jugador, desactivar la bala y devolverla al pool
        if (other.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }


    //EndState
    /*
    private IEnumerator MoveBackAndForth()
    {
        float movedDistance = 0;
        float maxDistance = 2.0f;
        while (movedDistance < maxDistance)
        {

            yield return Move(Vector3.left, moveDistance);
            movedDistance += moveDistance;
            Debug.Log("Moved left");

        }
        ChangeState(new IdleState());
        Debug.Log("Idle ");

        while (movedDistance > 0)
        {
            yield return Move(Vector3.right, moveDistance);
            movedDistance -= moveDistance;
            Debug.Log("Moved right");
        }
        ChangeState(new IdleState());
        Debug.Log("Idle ");
    }

    private IEnumerator Move(Vector3 direction, float distance)
    {

        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = initialPosition + direction * distance;

        while ((targetPosition - transform.position).magnitude > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;
    }*/

}
