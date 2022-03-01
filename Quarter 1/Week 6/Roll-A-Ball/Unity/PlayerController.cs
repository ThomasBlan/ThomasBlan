using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public Text scoreText;
    public Text winText;
    public int score;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        scoreText.text = "Score; " + score;
    }

    // Update is called once per frame
    void Update()
    {
        float hMove = Input.GetAxis("Horizontal") * speed;
        float vMove = Input.GetAxis("Vertical") * speed;
        rb.AddForce(new Vector3 (hMove, 0.0f, vMove));
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectible")){
            score++;
            scoreText.text = "Score; " + score;

            if (score == 8) {
                winText.text = "You win!!!";
            }

            Destroy(other.gameObject);
        }
    }

}
