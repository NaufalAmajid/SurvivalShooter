using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;

    bool isDead;                                                
    bool damaged;                                               

    void Awake()
    {   
        //dapatkan reference component
        anim = GetComponent<Animator>();

        playerAudio = GetComponent<AudioSource>();

        playerMovement = GetComponent<PlayerMovement>();

        playerShooting = GetComponentInChildren<PlayerShooting>();

        currentHealth = startingHealth;
    }


    void Update()
    {   
        //jika terkena damage
        if(damaged)
        {   
            //merubah warna gambar jadi value dari flashcolor
            damageImage.color = flashColour;
        }
        else
        {   
            //pudarkan damage image
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        //set damage ke false
        damaged = false;
    }

    //metod mendapatkan damage
    public void TakeDamage(int amount)
    {
        damaged = true;

        //mengurangi health
        currentHealth -= amount;

        //rubah tampilan health slider
        healthSlider.value = currentHealth;

        //play sound effect
        playerAudio.Play();

        //panggil method Death() jika darahnya kurang dari sama dengan 10 dan belum mati
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;

        //disable shoot saat dead
        playerShooting.DisableEffects();

        //mentrigger animasi Die
        anim.SetTrigger("Die");

        //play sound saat dead
        playerAudio.clip = deathClip;
        playerAudio.Play();

        //nonaktifkan script playermovement
        playerMovement.enabled = false;

        //nonaktifkan script playershooting
        playerShooting.enabled = false;
    }

    public void RestartLevel()
    {
        //meload ulang scene dengan index 0 pada build setting
        SceneManager.LoadScene(0);
    }
}
