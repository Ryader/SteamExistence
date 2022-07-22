using UnityEngine;

public class Controller2 : MonoBehaviour 
{
	public CharacterMovement characterMovement;
	public CharacterAnimation characterAnimation;
	public CharacterInput characterInput;
	public CharacterInventory characterInventory;

	public void Update()
	{
		characterMovement.FixedUpdate();
		characterAnimation.AnimationUpdate();
		characterInput.InputUpdate();
		characterInventory.InventoryUpdate();
	}
}
