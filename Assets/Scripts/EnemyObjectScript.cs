using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectScript : MonoBehaviour
{
    public Sprite[] sprites; //Container for the sprites being used.
    public float maxSize = 3.5f; //Max size of the sprite.
    public float Size = 1.0f; //Default size of the sprite.
    public float minSize = 0.5f; //Minimum size of the sprite.
    public float speed = 75.0f; //The speed at which the sprite will go in.
    public float enemyLifetime = 5.0f; //How long the sprite will last
    private SpriteRenderer _spriteRenderer; //Instance of the sprite renderer.
    private Rigidbody2D _rigidbody; //Instnace for phyisics on the object.

    //Called when a game object is initialized.
    void Awake()
    {
        //Gets the component for the object.
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    //Start is called when the script is first initialzed.
    private void Start()
    {
        //Grabs a random sprite from the array.
        _spriteRenderer.sprite = this.sprites[Random.Range(0, sprites.Length)];

        //Rotates the sprite in a random direction.
        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        this.transform.localScale = Vector3.one * this.Size;

        //Applies the mass to the object via the size of it.
        _rigidbody.mass = this.Size;

        //Destroyed after it's given lifetime.
        Destroy(this.gameObject, this.enemyLifetime);
    }

    //This method allows for the sprite to be given a force applied to it, and move in that vector.
    public void trajectory(Vector2 vector)
    {
        _rigidbody.AddForce(vector * this.speed);
        
        //Destroyed after it's given lifetime.
        Destroy(this.gameObject, this.enemyLifetime);
    }
}
