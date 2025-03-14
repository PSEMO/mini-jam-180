using UnityEngine;

public class MoveForEnemy : MonoBehaviour
{
    [SerializeField] float LimitLeftX;
    [SerializeField] float LimitRightX;

    [SerializeField] float speed = 1;

    bool MovingRight;

    void Start()
    {
        MovingRight = Random.Range(0f, 1f) > 0.5f;
        
        if(!MovingRight)
        {
            Turn();
        }
    }


    void Update()
    {
        if(MovingRight)
        {
            if(transform.position.x > LimitRightX)
            {
                transform.position = new Vector3(LimitRightX, transform.position.y, transform.position.z);
                MovingRight = false;
                Turn();
            }
            else
            {
                transform.position += new Vector3(+1 * Time.deltaTime * speed, 0, 0);
            }
        }
        else
        {
            if (transform.position.x < LimitLeftX)
            {
                transform.position = new Vector3(LimitLeftX, transform.position.y, transform.position.z);
                MovingRight = true;
                Turn();
            }
            else
            {
                transform.position += new Vector3(-1 * Time.deltaTime * speed, 0, 0);
            }
        }
    }

    void Turn()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
    }
}
