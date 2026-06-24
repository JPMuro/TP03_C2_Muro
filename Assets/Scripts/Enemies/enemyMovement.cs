using UnityEngine;

public class EnemyRandomMovement : MonoBehaviour
{
    public float speed = 2f;
    public float changeDirectionTime = 2f;

    private Vector3 direction;
    private float timer;

    void Start()
    {
        ChooseNewDirection();
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        timer += Time.deltaTime;

        if (timer >= changeDirectionTime)
        {
            ChooseNewDirection();
            timer = 0f;
        }
    }

    void ChooseNewDirection()
    {
        float x = Random.Range(-1f, 1f);
        float z = Random.Range(-1f, 1f);

        direction = new Vector3(x, 0, z).normalized;
    }
}