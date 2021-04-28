using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketObject : MonoBehaviour
{
    public ProjectileObject projectilePrefab;
    private Rigidbody2D _rigidbody;
    public float maxVelocity = 3;
    public float rotationSpeed = 3;

    //This the start point of the program. In this specific program this is assigning _rigidbody to the rockets
    //Physics.
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        
        #region Monobehaviour API
    }

    private void Update()
    {
        //This is to get up down input from the keyboard.(Note input is based on sprites rotation.
        //if you turn the sprite sideways up becomes forward.
        float yaxis = Input.GetAxis("Vertical");
        float xaxis = Input.GetAxis("Horizontal");

        if (transform.position.y > 10)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Thrustforward(yaxis);
        Rotate(transform, xaxis * rotationSpeed);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }
    #endregion

    #region Shipmovement API
    //This function is used to stop infite speed ups and make sure of no negative velocities.
    private void Clampvelocity()
    {
        float x = Mathf.Clamp(_rigidbody.velocity.x, -maxVelocity, maxVelocity);
        float y = Mathf.Clamp(_rigidbody.velocity.y, -maxVelocity, maxVelocity);

        _rigidbody.velocity = new Vector2(x, y);
    }

    private void Thrustforward(float amount)
    {
        Vector2 force = transform.up * amount;
        _rigidbody.AddForce(force);
    }
    #endregion

    private void Rotate(Transform t, float amount)
    {
        t.Rotate(0, 0, amount);
    }

    private void Shoot()
    {
        ProjectileObject projectile = Instantiate(this.projectilePrefab, this.transform.position, this.transform.rotation);
        projectile.Project(this.transform.up);
    }
}