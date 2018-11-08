using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alternator : MonoBehaviour
{

    //GameObject switchSpawner, midwaySpawner, colorChanger;
    List<GameObject> scrolls = new List<GameObject>();

    [SerializeField] Transform spawnPoint;

    [SerializeField] GameObject[] colors;
    [SerializeField] GameObject[] switches;
    [SerializeField] GameObject[] midways;
    [SerializeField] GameObject[] saws;

    [SerializeField] float scrollSpeed = 1.0f;
    [SerializeField] float frequency;
    float counter;
    float timer;

	void Start () {
        timer = 0f;
        counter = 0f;
	}
	
	void Update () {
        if(scrollSpeed > 12f){
            scrollSpeed = 12f;
        }else{
            scrollSpeed += 0.001f;
        }

        if(timer > 100f){
            frequency = 2f;
        }else{
            frequency = scrollSpeed * 0.14f;
        }
        
        timer += Time.deltaTime;


        if(counter <= 0f){
            randomSpawn(Random.value);
            counter = 1f;
        }else{
            counter -= Time.deltaTime * frequency;
        }

        foreach (GameObject scroll in scrolls.ToArray())
        {
            //scroll.transform.position += Vector3.forward * (scrollSpeed * Time.deltaTime);
            scroll.transform.Translate(Vector3.forward * scrollSpeed * Time.deltaTime);

            if (scroll.transform.position.z >= 17f)
            {
                Destroy(scroll);
                scrolls.Remove(scroll);
            }
        }
	}

    void randomSpawn(float r){
        if(r < 0.5f){
            nextObject(switches);
        }else if (r < 0.75f){
            nextObject(colors);
        }else if (r < 0.9f){
            nextObject(midways);
        }else{
            nextObject(saws);
        }
    }

    void nextObject(GameObject[] thisArray){
        GameObject newObject = Instantiate(thisArray[Random.Range(0, thisArray.Length)], spawnPoint.position, Quaternion.identity) as GameObject;
        newObject.transform.parent = transform;
        scrolls.Add(newObject);
    }
}
