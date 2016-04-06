using UnityEngine;
using System.Collections;

public interface IControllerConnect<T>
{
	void CheckConnection(T hasConnection);
}