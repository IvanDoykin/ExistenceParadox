using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ISaver
{
    void OnDestroy();
    void Save();
}
