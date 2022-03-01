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
    private bool canOpen;
    public Vector3 spawnPoint;
    public List<GameObject> collected;
    public List<GameObject> banked;
    public GameObject mainCamera;
    private ChestController chestController;

    // Start is called before the first frame update
    void Start(){
        rb = GetComponent<Rigidbody>();
        scoreText.text = "Score; " + score;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Jump") && grounded) {
            rb.AddForce(Vector3.up * jumpForce);
        }

        if (transform.position.y < -10){
            Death();
        }
        if (Input.GetButtonDown("Interact") && canOpen){
            chestController.OpenChest();
        }
    }
    void FixedUpdate(){
        float hMove = Input.GetAxis("Horizontal") * speed;
        float vMove = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3 (hMove, 0.0f, vMove);
        Vector3 rotatedMovement = Quaternion.AngleAxis(mainCamera.transform.rotation.eulerAngles.y, Vector3.up) * movement;
        rb.AddForce(rotatedMovement);
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
    void OnTriggerEnter(Collider other){
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
        if (other.gameObject.CompareTag("Chest")){
            other.gameObject.transform.parent.Find("Chest Text").gameObject.SetActive(true);
            canOpen = true;
            chestController = other.gameObject.transform.parent.GetComponent<ChestController>();
        }
    }
    void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Chest")){
            other.gameObject.transform.parent.Find("Chest Text").gameObject.SetActive(false);
            canOpen = false;
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
