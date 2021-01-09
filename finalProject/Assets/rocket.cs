using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rocket : MonoBehaviour
{
    Rigidbody rocketRB;
    float nextFire;
    AudioSource rocketAudioSource;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] float thrustSpeed = 5f;

    [SerializeField] int loadingTime = 2;
    [SerializeField] GameObject winningFX;
    [SerializeField] GameObject loseFX;

  
    [SerializeField] GameObject shot;
    [SerializeField] float fireRate;
    [SerializeField] Transform shotSpawn;

    // Start is called before the first frame update
    void Start()
    {
        rocketRB = GetComponent<Rigidbody>();
        rocketAudioSource = GetComponent<AudioSource>();
    }
    
        

        // Update is called once per frame
        void Update()
        {
            Thrust();

        fire();
    }
    void fire()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }
    }



    void OnCollisionEnter(Collision collision)
        {
            print(collision.gameObject.tag);
            switch (collision.gameObject.tag)
            {
                case "Friendly":
                    print("no prob");
                    break;

                case "Finish":
                    print("you won!");
                    Invoke("LoadNextScene", loadingTime);
                    winningFX.SetActive(true);
              
                    rocketAudioSource.PlayOneShot(mainEngine);
           
                break;
                case "Respawn":
                    Invoke("returnScene", loadingTime);
                    loseFX.SetActive(true);


                    break;
                default:
                    print("you lose game over ");
                    Invoke("returnScene", loadingTime);
                    loseFX.SetActive(true);
                    break;
            }
        }
        void returnScene()
        {
            SceneManager.LoadScene(0);
        }
        void LoadNextScene()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;
            if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            {
                nextSceneIndex = 0;
            }
            SceneManager.LoadScene(nextSceneIndex);
        }
        void Thrust()
        {
            if (Input.GetKey(KeyCode.D))
            {
                // print("Thrusting!");
                rocketRB.AddRelativeForce(Vector3.right * thrustSpeed);

            }
            else if (Input.GetKey(KeyCode.A))
            {
                rocketRB.AddRelativeForce(Vector3.left * thrustSpeed);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                rocketRB.AddRelativeForce(Vector3.up * thrustSpeed);
            }
            else if (Input.GetKey(KeyCode.S))
        {
            rocketRB.AddRelativeForce(Vector3.down * thrustSpeed);
        }
            else
            {
                rocketAudioSource.Stop();
            }

        }
}
