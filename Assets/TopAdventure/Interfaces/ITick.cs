using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITick
{
    TickData tickData {get; set;}
    void Tick();
}
