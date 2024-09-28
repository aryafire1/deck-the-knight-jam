using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICard
{
    void Roll(int max);

    void Result();

    void Positive();

    void Negative();
}
