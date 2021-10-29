using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{
    //public GameObject guiObject; (Add to let player know they are spawning)

    // use scene name
    public string loadLevel;

    //stuff for black screen transition
    public CanvasGroup overlay;
    public float transitionSpeed;

    private bool warping = false;

    public static bool isFrozen = false;

    void Start()
    {
        overlay.alpha = 0;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            warping = true;// Note you are now warping
            Freeze();
            overlay.alpha = overlay.alpha + Time.deltaTime * transitionSpeed; // Gradually change the Opacity each time this is run
            if (overlay.alpha >= 1) // if its fully opaque
            {
                Resume();
                SceneManager.LoadScene(loadLevel); // Change Scene
               
            }
                    //if (Input.GetButtonDown("Use"))
                    //{
                    //fade
                    //  warping = true;// Note you are now warping
                    //  overlay.alpha = overlay.alpha + Time.deltaTime * transitionSpeed; // Gradually change the Opacity each time this is run
                    //  if (overlay.alpha >= 1) // if its fully opaque
                    //  {
                    //      SceneManager.LoadScene(loadLevel); // Change Scene
                    //  }

                    // }

                }
            }


            void OnTriggerExit(Collider player)
            {
                if (player.gameObject.tag == "Player")
                {
                    warping = false;

                if (overlay.alpha > 0)
                    {
                    overlay.alpha = 0;
                    Resume();

                    }
                }
            }

            void Freeze()
            {

                GameManager.IsInputEnabled = false; //lock controls
            }

            void Resume()
            {

                GameManager.IsInputEnabled = true; //unlock controls
            }
        }
    
