using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    public Player player;
    public float respawnInvulnerabilityTime = 3.0f;
    public float respawnTime = 3.0f;
    public ParticleSystem explosion;
    public int lives = 3;
    public int score = 0;

    public void AsteroidDestroyed(Asteroid asteroid){
        this.explosion.transform.position = asteroid.transform.position;
        this.explosion.Play();
        if(asteroid.asteroidSize < 0.75f){ //small
            score += 100;
        } else if (asteroid.asteroidSize < 1.2f) { //med
            score += 50; //large
        } else{
            this.score += 25;
        }
}

    public void PlayerDied(){
        this.explosion.transform.position = this.player.transform.position; //play explosion where player died
        this.explosion.Play();

        this.lives--;

        if(this.lives <= 0){
            GameOver();
        } else{
        Invoke(nameof(Respawn),this.respawnTime);
        }
    }

    private void Respawn(){ 
        this.player.transform.position = Vector3.zero; //respawn to center of screen
        this.player.gameObject.layer = LayerMask.NameToLayer("Ignore_Collisions");
        this.player.gameObject.SetActive(true);
        Invoke(nameof(TurnOnCollisions), this.respawnInvulnerabilityTime); // invulnerability time after respawn
    }

    private void TurnOnCollisions(){
        this.player.gameObject.layer = LayerMask.NameToLayer("player");
    }
    private void GameOver(){
        this.lives = 3;
        this.score = 0;

        Invoke(nameof(Respawn), respawnTime);
        }
}
