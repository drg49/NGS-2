using UnityEngine;

public class LevelTwoFadePanel : MonoBehaviour
{
    [SerializeField] private InstructionSequence firstInstruction;

    // Meet Marcus & Dave at Big Burger
    public void PlayFirstInstruction()
    {
        firstInstruction.Play();
    }
}
