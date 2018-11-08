using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BallController : MonoBehaviour {
    //paired with frequency to create object respawn time
    private float timer;

    //Gameover Panel
    [SerializeField] GameObject panel;
    //New High Score Text
    [SerializeField] GameObject newText;
    //Current Score in top left of screen
    [SerializeField] Text scoreText;
    //Gameover final current game score
    [SerializeField] TMP_Text totalText;
    //Overall user high score
    [SerializeField] TMP_Text bestScoreText;

    //Current Game Score
    private int score;
    //User's high score overall
    private int bestScore = 0;

    //switches between playing the game on PC(false) and mobile(true)
    bool toggleTouch = false;

    //0 = red, 1 = green, 2 = blue
    int toggleColor = 0;

    //random ball color when starting
    Color[] rancolors = { Color.red, Color.blue, Color.green };

    Rigidbody rb;
    Renderer rend;
    SphereCollider sc;
    Transform currentSwitch;
    Vector3 tempPos;

    int lane;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
        sc = GetComponent<SphereCollider>();

        rend.material.color = rancolors[Random.Range(0, rancolors.Length)];
        if(rend.material.color == Color.red){
            toggleColor = 0;
        }else if(rend.material.color == Color.green){
            toggleColor = 1;
        }else{
            toggleColor = 2;
        }


        timer = 0.5f;
        score = 0;

        lane = 0;
    }

    void Update () {
        //adds a point to your score every half second
        if (timer <= 0f){
            timer = 0.5f;
            displayText(1);
        }
        else
        {
            timer -= Time.deltaTime;
        }

        if (toggleTouch == false)
        {
            if (Input.GetKeyDown("left") && lane != 1)
            {
                lane += 1;
                MoveBall();
            }
            if (Input.GetKeyDown("right")&& lane != -1)
            {
                lane -= 1;
                MoveBall();
            }
            if(Input.GetKey("r")){
                PlayerPrefs.SetInt("bestScore", 0);
                print("Score Reset");
            }
        }else if(toggleTouch == true){
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began){
                if (touch.position.x < 277 && lane != 1)
                {
                    lane += 1;
                    MoveBall();
                }
                else if (touch.position.x > 277 && lane != -1)
                {
                    lane -= 1;
                    MoveBall();
                }
            }
        }

        if(transform.position.z > 9f){
            Destroy(gameObject);
            gameOver();
        }
    }

    void MoveBall()
    {
        transform.position = new Vector3(lane * 1.85f, 0f, transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "gem")
        {
            Destroy(other.gameObject);
            displayText(10);
        }
        if (other.gameObject.tag == "saw")
        {
            Destroy(gameObject);
            gameOver();
        }

        //checks if the balls color matches the portal color and then relocates the ball
        if (other.gameObject.tag == "portal")
        {
            if (other.gameObject.name == "RedPortal" && toggleColor == 0)
            {
                enterPortal(other.gameObject);
            }
            else if (other.gameObject.name == "GreenPortal" && toggleColor == 1)
            {
                enterPortal(other.gameObject);
            }
            else if (other.gameObject.name == "BluePortal" && toggleColor == 2)
            {
                enterPortal(other.gameObject);
            }else{
                gameOver();
            }
        }

        //changes the balls color when it hits a "colorChanger"
        if (other.gameObject.tag == "red")
        {
            rend.material.color = Color.red;
            toggleColor = 0;
        }
        if (other.gameObject.tag == "green")
        {
            rend.material.color = Color.green;
            toggleColor = 1;
        }
        if (other.gameObject.tag == "blue")
        {
            rend.material.color = Color.blue;
            toggleColor = 2;
        }
    }

    void enterPortal(GameObject other){
        rend.enabled = false;
        sc.enabled = false;
        lane = other.GetComponent<PortalPartner>().GetPartnerLane();
        tempPos = transform.position;
        currentSwitch = other.transform.parent;
        StartCoroutine(Relocateball());
    }

    //called when a ball hits a portal
    //ball respawns when the "switch" passes it
    //this is done so the ball respawns at the same time no matter the speed of the objects
    IEnumerator Relocateball(){
        yield return new WaitUntil(() => currentSwitch.position.z >= 6.6f);
        transform.position = new Vector3(lane * 1.85f, 0, 6f);
        rend.enabled = true;
        sc.enabled = true;
        rb.velocity = Vector3.zero;
    }

    void gameOver(){
        panel.SetActive(true);
        Destroy(gameObject);

        //checks if most recent score is higher than the best score
        //possibly setting a new high score
        if(score > PlayerPrefs.GetInt("bestScore", 0)){
            PlayerPrefs.SetInt("bestScore", score);
            newText.SetActive(true);
        }
        totalText.text = "Score: " + score.ToString();
        bestScoreText.text = "High Score: " + PlayerPrefs.GetInt("bestScore", 0).ToString();
    }

    //"scorekeeper"
    void displayText(int increment){
        score += increment;
        scoreText.text = score.ToString();
    }
}
