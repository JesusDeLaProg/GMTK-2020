using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSpaceshipCaller : MonoBehaviour
{
    public void InPlace()
    {
        FindObjectOfType<MainMenu>().ShipMiddle();
    }

    public void Left()
    {
        FindObjectOfType<MainMenu>().ShipLeft();
    }
}
