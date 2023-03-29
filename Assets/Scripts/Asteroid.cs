using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private Rigidbody2D _asteroidRigidbody;

    public Sprite[] mySprites;

    public float asteroidSize = 1.0f;

    public float asteroidMaxSize = 1.5f;

    public float asteroidMinSize = 0.5f;

    public float asteroidSpeed = 70.0f;

    public float asteroidMaxLifeTime = 30.0f;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _asteroidRigidbody = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        _spriteRenderer.sprite = mySprites[Random.Range(0, mySprites.Length)];
        transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        transform.localScale = Vector3.one * asteroidSize;
        _asteroidRigidbody.mass = asteroidSize;
        Destroy(gameObject, asteroidMaxLifeTime);

    }

    public void setTrajectory(Vector2 direction)
    {
        _asteroidRigidbody.AddForce(direction * asteroidSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (asteroidSize/2 >= asteroidMinSize)
            {
                CreateSplit();
                CreateSplit();
            }
            FindObjectOfType<GameManager>().AsteroidDestroyed(this);
            Destroy(this.gameObject);
        }
    }

    private void CreateSplit()
    {
        Asteroid childAsteroid = Instantiate(this, transform.position, transform.rotation);
        childAsteroid.asteroidSize = asteroidSize / 2;
        childAsteroid.setTrajectory(Random.insideUnitCircle.normalized);
    }
}