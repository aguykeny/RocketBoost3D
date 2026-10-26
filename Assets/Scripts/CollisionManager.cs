using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip explosionAudioClip;
    [SerializeField] private AudioClip landingAudioClip;

    [Header("Particle Systems")]
    [SerializeField] private ParticleSystem explosionParticlesSystem;
    [SerializeField] private ParticleSystem landingParticlesSystem;

    AudioSource audioSource;
    bool isControllable = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isControllable) return;
        switch (collision.gameObject.tag)
        {
            case "LaunchPad":
                Debug.Log("LaunchPad");
                break;
            case "LandingPad":
                ManageCollision(landingAudioClip, landingParticlesSystem);
                break;
            case "Explosion":
                ManageCollision(explosionAudioClip, explosionParticlesSystem);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Manages the collision event by disabling player control, stopping any ongoing audio,
    /// playing the specified audio clip, disabling the PlayerMovement script,
    /// and playing the specified particle system for visual feedback.
    /// </summary>
    /// <param name="audioClip"></param>
    /// <param name="particlesSystem"></param>
    private void ManageCollision(AudioClip audioClip, ParticleSystem particlesSystem)
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(audioClip);
        GetComponent<PlayerMovement>().enabled = false;
        particlesSystem.Play();

    }
}
