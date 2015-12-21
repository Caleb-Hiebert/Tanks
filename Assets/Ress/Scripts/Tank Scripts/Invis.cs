using UnityEngine;
using System.Collections;

public class Invis : Bolt.EntityBehaviour<IPlayer> {

	[SerializeField] private SpriteRenderer[] _Sprites;
	[SerializeField] private ParticleSystem[] _Particles;
	[SerializeField] private float localInvisAlpha;

    void Start()
    {
        //subscribe to changes in invisibility state
        state.AddCallback("Invisible", OnInvis);

        //subscribe to changes in health
        state.AddCallback("HP", OnHealthChange);

        //update the invisibility state
        OnInvis();
    }

    //When the invisibility state changes, update the tank
    void OnInvis()
    {
        if(state.Invisible)
        {
            InvisON();
        } else
        {
            InvisOFF();
        }
    }

    void InvisON() {
        //Is this tank friendly on the local computer
        bool friendly = entity.IsFriendly();

        if (friendly) {
            //set the alpha of all sprites
            foreach (SpriteRenderer spr in _Sprites) {
                spr.color = SetAlpha(spr.color, localInvisAlpha);
            }

            //set the alpha of all particle systems
            foreach (ParticleSystem ps in _Particles) {
                ps.startColor = SetAlpha(ps.startColor, localInvisAlpha);
            }

        } else {

            //set the alpha of all sprites to 0
            foreach (SpriteRenderer spr in _Sprites) {
                spr.color = SetAlpha(spr.color, 0);
            }

            //set the alpha of all particle systems to 0
            foreach (ParticleSystem ps in _Particles) {
                ps.Stop();
            }
        }
    }

	void InvisOFF() {

        //restore alpha for all sprites to full
		foreach(SpriteRenderer spr in _Sprites) {
            spr.color = SetAlpha(spr.color, 1);
		}
		
        //restore alpha for all particle systesm to full
		foreach(ParticleSystem ps in _Particles) {
            ps.startColor = SetAlpha(ps.startColor, 1);
		}
		

        //resume playing all particle systems (in case they were stopped when the tank went invisible)
		foreach(ParticleSystem ps in _Particles) {
			ps.Play();
		}
	}

    void OnHealthChange()
    {
        //If the tank is dead, become visible again
        if(state.HP == 0 && entity.isOwner)
        {
            state.Invisible = false;
        }
    }

    //returns a new color with the alpha changed from the original color
	Color SetAlpha(Color cIn, float alpha) {
		return new Color(cIn.r, cIn.b, cIn.g, alpha);
	}
}
