using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public Text scoreText;
    public Text winText;
    public int score;
    public float speed;
    public float jumpForce;
    private bool grounded;
    public Vector3 spawnPoint;
    public List<GameObject> collected;
    public List<GameObject> banked;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        scoreText.text = "Score; " + score;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Jump") && grounded) {
            rb.AddForce(Vector3.up * jumpForce);
        }

        if (transform.position.y < -10){
            Death();
        }
    }
    void FixedUpdate()
    {
        float hMove = Input.GetAxis("Horizontal") * speed;
        float vMove = Input.GetAxis("Vertical") * speed;
        rb.AddForce(new Vector3 (hMove, 0.0f, vMove));
    }

    void OnCollisionEnter(Collision other) {
        grounded = true;
    }
    private void OnCollisionStay(Collision other) {
        grounded = true;
    }
    private void OnCollisionExit(Collision other) {
        grounded = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectible")){
            //Destroy(other.gameObject);
            collected.Add(other.gameObject);
            other.gameObject.SetActive(false);
            Score();
        }
        if (other.gameObject.CompareTag("Checkpoint")){
            spawnPoint = other.gameObject.transform.position;
            foreach (GameObject point in collected){
                banked.Add(point);
            }
            collected.Clear();
        }
        if (other.gameObject.CompareTag("Obstacles")){
            Death();
        }
    }

    void Death(){
        foreach (GameObject point in collected){
            point.SetActive(true);
        }
        collected.Clear();
        Score();
        transform.position = spawnPoint;
        transform.rotation = Quaternion.identity;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void Score(){
        score = collected.Count + banked.Count;
        scoreText.text = "Score; " + score;
        if (score >= 34) {
            winText.text = "You win!!!";
        }
    }

}
