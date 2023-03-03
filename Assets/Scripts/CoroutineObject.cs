using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace HeroSquad
{
	public sealed class CoroutineObject : CoroutineObjectBase
	{
		public Func<IEnumerator> Routine { get; private set; }
		public UnityEvent<bool> OnActiveEvent { get; private set; }

		public override event Action Finished;

		public CoroutineObject(MonoBehaviour owner, Func<IEnumerator> routine)
		{
			Owner = owner;
			Routine = routine;
			OnActiveEvent = new UnityEvent<bool>();
		}

		private IEnumerator Process()
		{
			yield return Routine.Invoke();

			Coroutine = null;

			Finished?.Invoke();
		}

		public void Start()
		{
			if (!IsProcessing)
			{
				Coroutine = Owner.StartCoroutine(Process());
				OnActiveEvent.Invoke(true);
			}
		}

		public void Stop()
		{
			if(IsProcessing)
			{
				Owner.StopCoroutine(Coroutine);

				Coroutine = null;
				OnActiveEvent.Invoke(false);
			}
		}

		public void Restart()
		{
			Stop();
			OnActiveEvent.Invoke(false);
			
			Coroutine = Owner.StartCoroutine(Process());
			OnActiveEvent.Invoke(true);
		}

		public void SetActive(bool active)
		{
			if (active) 
				Start();
			else 
				Stop();
		}
	}
}