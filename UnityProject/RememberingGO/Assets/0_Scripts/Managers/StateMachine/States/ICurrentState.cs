using UnityEngine;
using System.Collections;

public interface ICurrentState {

    void UpdateState();

    void ToNewState();

}
