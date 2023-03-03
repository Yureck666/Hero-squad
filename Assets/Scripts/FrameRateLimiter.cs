using System;
using UnityEngine;

namespace HeroSquad
{
    public class FrameRateLimiter : MonoBehaviour
    {
	    [SerializeField] private int limit;
	    
	    private void Awake()
	    {
		    SetLimit();
	    }

	    private void OnValidate()
	    {
		    SetLimit();
	    }

	    private void OnEnable()
	    {
		    SetLimit();
	    }
	    
	    private void OnDisable()
	    {
		    Application.targetFrameRate = 300;
	    }

	    private void SetLimit()
	    {
		    Application.targetFrameRate = limit;
	    }
    }
}
