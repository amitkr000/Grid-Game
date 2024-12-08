using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AI
{
    public IEnumerator MoveTowardsPlayer(GridUnit playerGridUnit);
}
