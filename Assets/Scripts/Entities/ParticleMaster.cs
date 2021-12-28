using UnityEngine;

public class ParticleMaster : MonoBehaviour
{
    //static here for ez cod
    //public static ParticleMaster partInstance;

    public GameObject entity { get; private set; }

    /*
     public void OnValidate()
     {
         //make sure i don't crash unity with making sure i get the instantiate
         if (partInstance != null)
         {
             partInstance = this;
         }
     }
    */ 
    public void setEntity(GameObject entityObject)
    {
        // May still be used but can be unnecesary
        entity = entityObject;
        Debug.Log(entityObject.name);
        return;
    }

    public void InstantiateParticle(ParticleSystem particle, string particleType, string particleName, Transform objectTransform)
    {
        if (particleType == "movement" && entity != null)
        {
            if (particleName == "dustOnJump") // you jump, dust below your feet
            {
                //loading the particle and printing it out to ensure it is the right one

                Debug.Log(particle);

                //instantiating the particle, playing, and destroying it after it's done playing. won't comment the next ones, but will split just like this so you know which is which

                ParticleSystem spawnParticle = Instantiate(particle, objectTransform.position, objectTransform.rotation, entity.transform);
                spawnParticle.Play();
                Destroy(spawnParticle.gameObject, spawnParticle.main.duration);
            }
            else if (particleName == "dustOnImpact") // you fall, dust on your feet when impact
            {

                Debug.Log(particle);

                ParticleSystem spawnParticle = Instantiate(particle, objectTransform.position, objectTransform.rotation, entity.transform);
                spawnParticle.Play();
                Destroy(spawnParticle.gameObject, spawnParticle.main.duration);

            }
            else if (particleName == "dustOnMovement")// while running, dust on feet will happen.
            {

                Debug.Log(particle);

                ParticleSystem spawnParticle = Instantiate(particle, objectTransform.position, Quaternion.identity, entity.transform);
                spawnParticle.Play();
                Destroy(spawnParticle.gameObject, spawnParticle.main.duration);

            }
            else
            {
                Debug.Log("Invalid arguments! Did you type something wrong? Check your capitalization, strings are stupid and can't tell if you did that wrong.");
            }
        }
        if (particleType == "combat" && entity != null)
        {
            if (particleName == "bleedOnHit") // bleed on damage
            {
                Debug.Log(particle);

                ParticleSystem spawnParticle = Instantiate(particle, objectTransform.position, Quaternion.identity, entity.transform);
                spawnParticle.Play();
                Destroy(spawnParticle.gameObject, 5f);

            }
            else if (particleName == "deathOnHit") // massive blood on death
            {

                Debug.Log(particle);

                ParticleSystem spawnParticle = Instantiate(particle, objectTransform.position, objectTransform.rotation, entity.transform);
                spawnParticle.Play();
                Destroy(spawnParticle.gameObject, .5f);

            }
        }
    }
}
