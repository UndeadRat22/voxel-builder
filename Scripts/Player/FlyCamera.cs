using UnityEngine;
using System.Collections;
namespace Deadrat22
{
    public class FlyCamera : MonoBehaviour
    {
        /*wasd : basic movement
        shift : Makes camera accelerate
        space : Moves camera on X and Z axis only.  So camera doesn't gain any height*/


        public float mainSpeed = 50.0f; //regular speed
        public float camSens = 0.41f; //How sensitive it with mouse
        private Vector3 lastMouse;

        bool esc = true;
        bool started = false;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
                esc = !esc;
            if (esc)
                return;

            if (!started)
            {
                lastMouse = Input.mousePosition;
                started = true;
            }
            lastMouse = Input.mousePosition - lastMouse;
            lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0);
            lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x, transform.eulerAngles.y + lastMouse.y, 0);
            transform.eulerAngles = lastMouse;
            lastMouse = Input.mousePosition;
            //Mouse  camera angle done.  

            //Keyboard commands

            Vector3 p = GetBaseInput();

            p = p * Time.deltaTime * mainSpeed;
            transform.Translate(p);

        }

        private Vector3 GetBaseInput()
        { //returns the basic values, if it's 0 than it's not active.
            Vector3 p_Velocity = new Vector3();
            if (Input.GetKey(KeyCode.W))
            {
                p_Velocity += new Vector3(0, 0, 1);
            }
            if (Input.GetKey(KeyCode.S))
            {
                p_Velocity += new Vector3(0, 0, -1);
            }
            if (Input.GetKey(KeyCode.A))
            {
                p_Velocity += new Vector3(-1, 0, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                p_Velocity += new Vector3(1, 0, 0);
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                p_Velocity += new Vector3(0, -1, 0);
            }
            if (Input.GetKey(KeyCode.Space))
            {
                p_Velocity += new Vector3(0, 1, 0);
            }
            return p_Velocity;
        }
    }
}