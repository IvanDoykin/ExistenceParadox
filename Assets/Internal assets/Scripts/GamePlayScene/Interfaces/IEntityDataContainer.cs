using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntityDataContainer
{
    void WriteCollectedData(params Data[] dataVariables);
}