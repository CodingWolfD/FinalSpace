using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    private Light light; // CREATES A REFERENCE TO THE LIGHT COMPONENT
    private float timer; // CREATES A NEW INTEGER TO KEEP TRACK OF THE TIME PASSED
    private int rnd; // USED TO STORE THE RANDOM NUMBER GENERATED
    private bool lightEnabled; // USED TO TELL THE SCRIPT IF THE LIGHT IS ENABLED OR DISABLED

    private void Start()
    {
        light = this.GetComponent<Light>(); // FINDS THE FLICKERING LIGHT GAMEOBJECT FROM THE SCENE
        timer = 0; // INSTANTIATES THE TIMER AS 0
    }

    private void Update()
    {
        increaseTimer(); // CALLS THE INCREASETIMER METHOD EVERY TICK
    }
    
    private void increaseTimer()
    {
        rnd = Random.Range(0, 150); // GENERATES A RANDOM NUMBER FROM 0 - 150 AND STORES IT INSIDE A VARIABLE CALLED RND

        timer += Time.unscaledDeltaTime; // INCREASES THE TIMER EVERY TICK

        lightEnabled = light.isActiveAndEnabled; // SETS LIGHTENABLED TO THE OPPOSITE OF THE CURRENT LIGHT STATE

        if((int)timer >= rnd) // IF THE INTEGER TIMER IS LESS THAN THE RANDOM NUMBER GENERATED
        {
            light.enabled = !lightEnabled; // SETS THE LIGHTS ACTIVE STATE TO THE OPPOSITE OF THE LIGHTENABLED BOOL
            timer = 0; // RESETS THE TIMER BACK TO 0
        }
    }
}