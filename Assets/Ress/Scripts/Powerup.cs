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
        if(BoltNetwork.isRunning && !entity.isAttached && BoltNetwork.isServer)
        {
            BoltNetwork.Attach(gameObject);
        } else if (BoltNetwork.isRunning && !entity.isAttached && BoltNetwork.isClient)
        {
            OnDisabled();
        }

        soundSource = GetComponent<AudioSource>();
    }

    public override void Attached()
    {
        soundSource = GetComponent<AudioSource>();
        state.AddCallback("Enabled", Callback);

        if (entity.isOwner)
        {
            Enable();
        }

        Callback();
    }

    void Callback()
    {
        if(state.Enabled)
        {
            if(enabledSound != null && soundSource != null)
            {
                soundSource.clip = enabledSound;
                soundSource.Play();
            }
            OnEnabled();
        } else
        {
            if (pickupSound != null && soundSource != null)
            {
                soundSource.clip = pickupSound;
                soundSource.Play();
            }
            OnDisabled();
            if (entity.isOwner)
            {
                Invoke("Enable", refreshTime);
            }
        }
    }

    public abstract void OnEnabled();

    public abstract void OnDisabled();

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

    void OnTriggerEnter2D(Collider2D stuff)
    {
        if (!entity.isOwner)
            return;

        if(stuff.gameObject.tag == "Player")
        {
            OnCollided(stuff.gameObject.GetComponentInParent<BoltEntity>());
        }
    }
}
