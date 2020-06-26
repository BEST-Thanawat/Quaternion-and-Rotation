using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>
{
    public List<CharacterController> Characters = new List<CharacterController>();
}
