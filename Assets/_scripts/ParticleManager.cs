using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {

    public ParticleSystem kickParticle;
    public ParticleSystem hatsParticle;
    public ParticleSystem bassParticle;
    public ParticleSystem deathParticle;


    public static ParticleManager instance = null;
    private Vector2 zeroConst { get { return new Vector2(0, 0); } }

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)
            Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EmittParticles(GameManager.HealthCategory type, Vector3 position, Vector2 zAxis = default(Vector2))
    {
        ParticleSystem tospawn = null;

        if (type == GameManager.HealthCategory.Kick)
            tospawn = kickParticle;

        if (type == GameManager.HealthCategory.Hats)
            tospawn = hatsParticle;

        if(type== GameManager.HealthCategory.Bass)
            tospawn = bassParticle;

        if (tospawn != null)
        {
            tospawn.transform.position = position;
            tospawn.transform.up = zAxis;
            tospawn.Play();
        }
    }
}
