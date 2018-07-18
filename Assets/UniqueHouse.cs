using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueHouse : House {
	protected override void rotate(){
		transform.Rotate(0, 0, Time.deltaTime+0.15f);
	}

	protected override IEnumerator startDying() {
		if (controller.boss || controller.uniqueBoss) {
            controller.checkBossReward(transform.position);
        }
		rend.material.shader = finished;
        double goldIncrement = controller.enemyDied();
        createFloatText(new Vector3(0,-400,0f), "+"+goldIncrement+"g", Color.yellow, true);
        yield return new WaitForSeconds(1f);
    }
}
