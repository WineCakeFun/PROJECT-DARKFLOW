using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private AudioSource audioSrc;
    private bool facingRight = true;
    [SerializeField]
    private float speedMultiplier;
    public SpriteRenderer sprite;
    private Animator anim;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //audioSrc = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();  
    }

    void FixedUpdate()
    {
        rb2d.velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal") * speedMultiplier, 0.8f),
                                    Mathf.Lerp(0, Input.GetAxis("Vertical") * speedMultiplier, 0.8f));
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow)||Input.GetKey(KeyCode.A))
        {
			if(facingRight)
			{
				sprite.flipX = true;
				facingRight = false;

			}

		}
		if(Input.GetKey(KeyCode.RightArrow)||Input.GetKey(KeyCode.D))
        {
			if(!facingRight)
			{
				sprite.flipX = false;
				facingRight = true;

			}

		}
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            anim.SetBool("isRunning", true);
            /*if (!audioSrc.isPlaying)
            {
                audioSrc.Play();
            }*/
        }else{
            anim.SetBool("isRunning", false);
        }
    }
    private void Flip()
	{
		facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
		
	}
}
