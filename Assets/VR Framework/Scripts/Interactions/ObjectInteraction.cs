using UnityEngine;

namespace VRFramework.Interactions
{
	[RequireComponent(typeof(Rigidbody))]
	public class ObjectInteraction : MonoBehaviour
	{
		private Rigidbody rigidbody;
		private Collider collider;

		public InteractionEvent onStartInteract = new InteractionEvent();
		public InteractionEvent onEndInteract = new InteractionEvent();

		private InteractableObject interactingObject;

		// Gives us an easy way to generate interaction data for events
		protected InteractEventData GenerateData(Collider _collider)
		{
			return new InteractEventData(interactingObject, null, _collider, _collider.GetComponent<Rigidbody>(), InteractEventData.Interaction.Object);
		}

		private void Awake()
		{
			rigidbody = gameObject.GetComponent<Rigidbody>();
			collider = gameObject.GetComponent<Collider>();
			if(collider == null)
			{
				collider = gameObject.AddComponent<BoxCollider>();
			}

			collider.isTrigger = true;
		}

		private void OnTriggerEnter(Collider _other)
		{
			InteractableObject interactable = _other.GetComponent<InteractableObject>();
			if(interactable != null)
			{
				onStartInteract.Invoke(GenerateData(_other));
			}
		}

		private void OnTriggerExit(Collider _other)
		{
			InteractableObject interactable = _other.GetComponent<InteractableObject>();
			if(interactable != null)
			{
				onEndInteract.Invoke(GenerateData(_other));
			}
		}
	}
}