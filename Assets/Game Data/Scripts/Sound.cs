using UnityEngine;
using System.Collections;

public class Sound : Bolt.EntityBehaviour<IPlayer> {

	public AudioSource drivingSound;
	public AudioSource[] sounds;

	public float enginePitchModifier = 1;
	public float idlePitch;
	public float engineVolume;

	void Update() {
        if(entity.Team() != Player.localTeam && state.Invisible)
        {
            drivingSound.enabled = false;
        } else
        {
            drivingSound.enabled = true;

            drivingSound.pitch = Mathf.Clamp(state.Movement.RPM * enginePitchModifier, idlePitch, 20f);
        }
	}

	public void PlaySound(string soundName) {
		foreach (AudioSource s in sounds) {
			if(s.clip.name == soundName) {
				s.Play();
			}
		}
	}
}
