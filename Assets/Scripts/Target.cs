using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRB;
    private float minSpeed = 9.0f;
    private float maxSpeed = 13.0f;
    private float maxTorque = 10.0f;
    private float xRange = 4.0f;
    private float ySpawnPos = 0.0f;
    private GameManager gameManager;
    public int scoreValue;
    public ParticleSystem destroyParticle;
    // Start is called before the first frame update
    void Start()
    {
        //get the value of targetRB
        targetRB = GetComponent<Rigidbody>();
        //make the targets appears from the buttom
        targetRB.AddForce(RandomForce(), ForceMode.Impulse);
        //make the targets rotate randomly
        targetRB.AddTorque(RandomTorque(), ForceMode.Impulse);
        //set the spawn position of targets
        transform.position = RandomSpawnPos();
        //reference the gameManager script
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseEnter()
    {
        //only distroy target when game is active and user press the left mouse button
        if(gameManager.isGameActive && Input.GetMouseButton(0))
        {
            //when the mouse meet the target, destroy it
            Destroy(gameObject);
            //update score when meet the target
            gameManager.ScoreUpdate(scoreValue);
            //play partical when target is destroyed
            Instantiate(destroyParticle, transform.position, destroyParticle.transform.rotation);
            //play the audio effect
            if (gameObject.CompareTag("Bad"))
            {
                gameManager.gameSound.PlayOneShot(gameManager.badSound,0.5f);
            }
            else
            {
                gameManager.gameSound.PlayOneShot(gameManager.scoreSound);
            }
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //when target lower than sensor, destroy it
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad") && gameManager.live > 0)
        {
            gameManager.live--;
            gameManager.LiveUpdate();
        }
        if (gameManager.live == 0)
        {
            gameManager.GameOver();
        }
        
        

    }
    Vector3 RandomForce()
    {
        //generate a random up force
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }
    Vector3 RandomTorque()
    {
        //return a Vector3 torque for targets to rotate when they are spawned
        return new Vector3(Random.Range(-maxTorque, maxTorque), Random.Range(-maxTorque, maxTorque), Random.Range(-maxTorque, maxTorque));
    }
    Vector3 RandomSpawnPos()
    {
        //return a random spawn vector3 position
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
}
