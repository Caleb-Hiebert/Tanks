using UnityEngine;
using System.Collections;

public class Invis : Bolt.EntityBehaviour<IPlayer> {

	[SerializeField] private SpriteRenderer[] _Sprites;
	[SerializeField] private ParticleSystem[] _Particles;
	[SerializeField] private float localInvisAlpha;

    void Start()
    {
        state.AddCallback("Invisible", OnInvis);
        state.AddCallback("HP", OnHealthChange);
        OnInvis();
    }

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
        bool friendly = entity.IsFriendly();

        if (friendly) {

            foreach (SpriteRenderer spr in _Sprites) {
                if(spr != null)
                    spr.color = SetAlpha(spr.color, localInvisAlpha);
            }

            foreach (ParticleSystem ps in _Particles) {
                if(ps != null)
                    ps.startColor = SetAlpha(ps.startColor, localInvisAlpha);
            }

        } else {

            foreach (SpriteRenderer spr in _Sprites) {
                spr.color = SetAlpha(spr.color, 0);
            }

            foreach (ParticleSystem ps in _Particles) {
                ps.Stop();
            }
        }
    }

	void InvisOFF() {

		foreach(SpriteRenderer spr in _Sprites) {
            if (spr != null)
                spr.color = SetAlpha(spr.color, 1);
		}
		
		foreach(ParticleSystem ps in _Particles) {
            if (ps != null)
                ps.startColor = SetAlpha(ps.startColor, 1);
		}
		
		foreach(ParticleSystem ps in _Particles) {
			ps.Play();
		}
	}

    void OnHealthChange()
    {
        if(state.HP == 0 && entity.isOwner)
        {
            state.Invisible = false;
        }
    }

	Color SetAlpha(Color cIn, float alpha) {
		return new Color(cIn.r, cIn.b, cIn.g, alpha);
	}
}
