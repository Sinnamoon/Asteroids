using UnityEngine;

public class Player : MonoBehaviour
{
    public Bullet bulletPrefab;
    public float _thrustSpeed = 1.0f;
    public float _turnSpeed = 1.0f;
    private Rigidbody2D _rigidbody;
    private bool _thrusting;
    private float _turnDirection;

    private void Awake(){
        _rigidbody = GetComponent<Rigidbody2D>();
    } 

    private void Update()
    {
        _thrusting = (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow));
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow)){
            _turnDirection=1.0f;
        }else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            _turnDirection=-1.0f;
        }else {
            _turnDirection = 0.0f;
        }
        if (Input.GetKeyDown(KeyCode.Space) || (Input.GetMouseButtonDown(0))){
            shoot();
        }

    }

    private void FixedUpdate()
    {
        if (_thrusting) {
            _rigidbody.AddForce(this.transform.up * this._thrustSpeed);
        }

        if (_turnDirection != 0.0f){
            _rigidbody.AddTorque(_turnDirection * this._turnSpeed);
        }

    }

    private void shoot(){
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up); //project bullet in direction player is facing  

    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Asteroid"){ //check if the player is colliding w/ an asteroid
            _rigidbody.velocity = Vector3.zero; //stop movement  
            _rigidbody.angularVelocity = 0.0f;  //stop movement

            this.gameObject.SetActive(false); //turn off game object

           FindObjectOfType<GameManager>().PlayerDied();
        }
    }

}
