using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
	//переменные для текста
	public Text name;
	public Text lvl;
	public Slider hpSlider;
	
	//обновление игформации в интерфейсе
    public void updateHUD(Unit u)
    {
        name.text = u.unitName;
		lvl.text = u.unitLvl.ToString();
		hpSlider.maxValue = u.unitMaxHP;
		hpSlider.value = u.unitCurrentHP;
		setHP(u.unitCurrentHP);
    }
	//обновление информации о здоровье 
	void setHP(int hp)
	{
		hpSlider.value = hp;
	}
}
