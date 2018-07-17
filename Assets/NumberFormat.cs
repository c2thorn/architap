using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NumberFormat {
	public static string format(double input) {
		return input >= 10000000 ? input.ToString("G3") : input.ToString("N0"); 
	}
}
