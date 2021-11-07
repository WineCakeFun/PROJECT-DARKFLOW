using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	//unit info stat
    public string unitName;
	public int unitLvl;
	
	//def stats
	public int unitMaxHP;
	public int unitCurrentHP;
	public int unitDefense;
	
	//atack stats
	public int unitDamage;
		//возможно на будущее
	private int initiative;
	
	//true - умер мужик | false - жив мужик
	public bool takeDamage(int dmg)
	{
		unitCurrentHP -= dmg;
		if(unitCurrentHP <= 0)
		{	
			unitCurrentHP=0;
			return true;			
		}
		else
			return false;
	}
	public void heal()
	{
//изменить как-то количество лечения?
		unitCurrentHP += unitDamage;
		if(unitCurrentHP > unitMaxHP)
			unitCurrentHP = unitMaxHP;
	}
}
