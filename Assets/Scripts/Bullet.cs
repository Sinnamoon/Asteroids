using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rigidBody; 
    public float speed = 500f; //public so it can be changed in Unity editor
    public float maxLifetime = 10.0f; //How long the bullet exists
    private void Awake(){
        _rigidBody = GetComponent<Rigidbody2D>();
    }


    public void Project(Vector2 direction) // player tells the bullet in which direction to move 
    {
        _rigidBody.AddForce(direction * this.speed); 

        Destroy(this.gameObject, maxLifetime);
    }

    private void OnCollisionEnter2D (Collision2D collsion){//called anytime bullet collides with something
        Destroy(this.gameObject);
    } 


    
}
