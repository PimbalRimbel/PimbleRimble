using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFlyweight
{
    // Estado intrinseco
    int Color { get; } // 0 => Verde, 1 => Rojo
    void Roam();
    
    
}
