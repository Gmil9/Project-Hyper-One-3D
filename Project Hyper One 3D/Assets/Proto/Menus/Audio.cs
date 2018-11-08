using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour {

    [SerializeField] AudioClip[] clips;
    AudioSource thisClip;

	void Awake () {

        thisClip = GetComponent<AudioSource>();
        thisClip.loop = false;

        GameObject[] go = GameObject.FindGameObjectsWithTag("audio");
        if(go.Length > 1){
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

	}

    private void Update()
    {
        if(!thisClip.isPlaying){
            thisClip.clip = getRandomClip();
            thisClip.Play();
        }
    }

    private AudioClip getRandomClip(){
        return clips[Random.Range(0, clips.Length)];
    }

}
