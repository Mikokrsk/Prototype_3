using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class IntroScript : MonoBehaviour
{
    public float speed = 1f;
    public float startTime = 1f;
    public bool isIntro = true;
    public GameObject spawnManager;
    public MoveLeft moveLeft;
    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("SpawnManager");
        moveLeft = GameObject.Find("Background").GetComponent<MoveLeft>();
        spawnManager.SetActive(false);
        moveLeft.enabled = false;
        StartCoroutine(PlayIntro());
    }

    // Update is called once per frame
    void Update()
    {
        if (isIntro)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
    private void EndIntro()
    {
        isIntro = false;
        spawnManager.SetActive(true);
        moveLeft.enabled = true;
    }

    IEnumerator PlayIntro()
    {
        speed += 0.5f;
        if (transform.position.x >= 0f)
        {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
            EndIntro();
            StopCoroutine(PlayIntro());            
        }
        else
        {
            yield return null;
            StartCoroutine(PlayIntro());
        }
       
    }
}
