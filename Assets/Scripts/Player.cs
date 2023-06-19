using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] float movespeed = 5f;

    GameObject currentFloor;

    [SerializeField] int HP;
    [SerializeField] GameObject HPBar;

    [SerializeField] Text scoreText;
    int score;
    float scoreTime;
    Animator anim;
    SpriteRenderer render;
       // Start is called before the first frame update
    void Start()
    {
       HP = 10;
       score = 0;
       scoreTime = 0;
       anim = GetComponent<Animator>();
       render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(movespeed * Time.deltaTime, 0, 0);
            render.flipX = false;
            anim.SetBool("run", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-movespeed * Time.deltaTime, 0, 0);
            render.flipX = true;
            anim.SetBool("run", true);
        }
        else
        {
            anim.SetBool("run", false);
        }
        UpdateScore();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Normal")
        {
            if (other.contacts[0].normal == new Vector2(0f, 1f))
            {
                Debug.Log("第一種階梯");
                currentFloor = other.gameObject;
                ModifyHP(1);
            }

        }
        else if (other.gameObject.tag == "Nails")
        {
            if (other.contacts[0].normal == new Vector2(0f, 1f))
            {
                Debug.Log("第二種階梯");
                currentFloor = other.gameObject;
                ModifyHP(-3);
            }
        }    
        
        else if (other.gameObject.tag == "Ceiling")
        {
            {
                Debug.Log("碰到天花板");
                currentFloor.GetComponent<BoxCollider2D>().enabled = false;
                ModifyHP(-3);
            }

        }
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "DeathLine")
            {
                Debug.Log("你輸了");
            }
        }
        void ModifyHP(int num)
        {
            HP += num;
            if(HP>10)
            {
                HP=10;
            }
            else if(HP<0)
            {
                HP=0;
            }
            UpdateHPBar();
        }
        
        void UpdateHPBar()
        {
            for(int i=0; i<HPBar.transform.childCount; i++)
            {
                if(HP>i)
                {
                    HPBar.transform.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    HPBar.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }   

    }   
void UpdateScore()
    {
        scoreTime += Time.deltaTime;
        if(scoreTime>2f)
        {
            score++;
            scoreTime = 0f;
            scoreText.text = "地下" + score.ToString() + "層";
        }

    }

}
