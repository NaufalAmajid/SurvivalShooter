using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public float speed = 6f;
    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100f;

    private void Awake()
    {
        //dapatkan nilai mask dari layer yang bernama floor
        floorMask = LayerMask.GetMask("Floor");

        // dapatkan komponen animator
        anim = GetComponent<Animator>();

        //dapatkan rigidbody
        playerRigidbody = GetComponent<Rigidbody>();

    }

    private void FixedUpdate()
    {
        
        //dapatkan nilai input horizontal(-1, 0, 1)
        float h = Input.GetAxisRaw("Horizontal");

        //dapatkan nilai input vertikal(-1, 0, 1)
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        Turning();
        Animating(h, v);
    }

    //Method agar player dapat berjalan
    public void Move(float h, float v)
    {
        //set nilai x dan y
        movement.Set(h, 0f, v);

        //normalisasi nilai vektor agar total panjang dari vektor adl 1 
        movement = movement.normalized * speed * Time.deltaTime;

        //move to position
        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning()
    {

        //buat ray dari posisi mouse di layar
        Ray camray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //buat raycast untuk floorHit
        RaycastHit floorHit;

        //lakukan raycast
        if(Physics.Raycast(camray, out floorHit, camRayLength, floorMask))
        {
            //dapatkan vector dari posisi player dan floorhit
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            //mendapatkan look rotation baru ke hit position
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            //rotasi player
            playerRigidbody.MoveRotation(newRotation);
            
        }

    }

    public void Animating(float h, float v)
    {

        bool walking = h != 0f || v != 0f;

        anim.SetBool("IsWalking", walking);

    }

}   
