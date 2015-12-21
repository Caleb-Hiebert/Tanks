using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class Announcer : MonoBehaviour {
		
	public AudioClip[] soundClips;
	public AudioSource player;
	public float busyUntil = 0;

	void Start() {
		DontDestroyOnLoad (this.gameObject);
	}

	public void PlaySound(string clipName) {
		player.clip = Resources.Load("Sounds/Announcers/" + clipName) as AudioClip;
		busyUntil = Time.time + player.clip.length;
		player.Play();
	}
}
