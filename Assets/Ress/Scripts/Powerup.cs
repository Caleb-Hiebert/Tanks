using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource), typeof(BoltEntity), typeof(CircleCollider2D))]
public abstract class Powerup : Bolt.EntityBehaviour<IOnOffState> {

    public float refreshTime;
    public AudioClip pickupSound;
    public AudioClip enabledSound;

    AudioSource soundSource;
		
	void Start()
    {
        //If we are the server then we want to attach the powerup so there will only be once instance in the scene
        if(BoltNetwork.isRunning && !entity.isAttached && BoltNetwork.isServer)
        {
            BoltNetwork.Attach(gameObject);

        //If we are not the server just disable the powerup
        } else if (BoltNetwork.isRunning && !entity.isAttached && BoltNetwork.isClient)
        {
            OnDisabled();
        }

        soundSource = GetComponent<AudioSource>();
    }

    public override void Attached()
    {
        soundSource = GetComponent<AudioSource>();

        //Register to status change events on clients and the server
        state.AddCallback("Enabled", Callback);

        //If we are the server, enable the powerup for use
        if (entity.isOwner)
        {
            Enable();
        }

        //Update the state manually
        Callback();
    }

    //When the state is update by the server
    void Callback()
    {
        if(state.Enabled)
        {
            //If an enabled sound has been assigned in the inspector, play it
            if(enabledSound != null && soundSource != null)
            {
                soundSource.clip = enabledSound;
                soundSource.Play();
            }

            //turn the powerup on (this should be overridden)
            OnEnabled();
        } else
        {
            //If a pickup sound has been assigned in the inspector, play it
            if (pickupSound != null && soundSource != null)
            {
                soundSource.clip = pickupSound;
                soundSource.Play();
            }

            //turn the powerup off (this should be overridden)
            OnDisabled();

            //if we are the server, change the powerup state back to enabled after a delay
            if (entity.isOwner)
            {
                Invoke("Enable", refreshTime);
            }
        }
    }

    //gets called when the powerup is created or when it is turned back on after being disabled
    public abstract void OnEnabled();

    //gets called when a tank picks up the powerup, or on local clients when its created
    public abstract void OnDisabled();

    //called when a tank collides with the powerup
    public abstract void OnCollided(BoltEntity tank);

    public void Enable()
    {
        state.Enabled = true;
        CancelInvoke();
    }

    public void Disable()
    {
        state.Enabled = false;
    }

    //When something comes into contact with the powerup
    void OnTriggerEnter2D(Collider2D stuff)
    {
        //if we are not the server, return
        if (!entity.isOwner)
            return;

        //if the object was a tank, retrieve the BoltEntity and call OnCollided
        if(stuff.gameObject.tag == "Player")
        {
            OnCollided(stuff.gameObject.GetComponentInParent<BoltEntity>());
        }
    }
}
