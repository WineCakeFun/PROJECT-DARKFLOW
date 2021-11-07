using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime;

public enum battleState{START, PLAYER_TURN, ENEMY_TURN, WON, LOST}

public class BattleSystem : MonoBehaviour
{
	Unit player;
	Unit enemy;		
	
	public Text dialogue;	
	
	//стадия битвы
	public battleState state;

    void Start()
    {        
		StartCoroutine(prepareBattle());
    }
    
	public GameObject playerPrefab;
	public GameObject enemyPrefab;
	//BattleStation для местоположения юнитов
	public Transform playerBattleStation;
	public Transform enemyBattleStation;
	
	public BattleHUD playerHUD;
	public BattleHUD enemyHUD;
	
	//IEnumerator нужен чтобы использовать yield return new WaitForSeconds (местный sleep)
	//подготовка текстур и прочего перед битвой
    IEnumerator prepareBattle()
    {
		Debug.Log("Битва начивается");
		state = battleState.START;
		//spawn игрока и врага на BattleStation
        GameObject playerObject = Instantiate(playerPrefab, playerBattleStation);
		player = playerObject.GetComponent<Unit>();
		
		GameObject enemyObject = Instantiate(enemyPrefab, enemyBattleStation);
		enemy = enemyObject.GetComponent<Unit>();
		
		//надпись в диалоге		
		dialogue.text = "Suddenly " + enemy.unitName + " attacks you!";
		
		//обновление интерфейса
		playerHUD.updateHUD(player);
		enemyHUD.updateHUD(enemy);
		
		//ждём 1 секунду
		yield return new WaitForSeconds (2f);
		
		playerTurn();		
    }
	
	//ход игрока (зависит от нажатых кнопок)
	void playerTurn()
	{
		state = battleState.PLAYER_TURN;
		dialogue.text = "You decide to...";		
	}
	
	IEnumerator enemyTurn()
	{
		yield return new WaitForSeconds (1f);
		state = battleState.ENEMY_TURN;
		
		//70% attack 30% heal
		int rnd = Random.Range(0,100+1);
		//Debug.Log(rnd.ToString());
		//враг атакует
		if(rnd <= 70)
		{
			if(player.takeDamage(enemy.unitDamage))
			{
				yield return new WaitForSeconds (2f);
//можно каку-то анимацию добавить
				//игрок проиграл				
				StartCoroutine(lost_scene());
			}
			else
			{
				dialogue.text = enemy.unitName + " hits you.";
				playerHUD.updateHUD(player);
				yield return new WaitForSeconds (2f);
//можно каку-то анимацию добавить
				playerTurn();
				//конец функциии
				//yield return 0;
			}
		}
		//враг исцеляется
		else if(rnd > 70)
		{
			enemy.heal();
			enemyHUD.updateHUD(enemy);	
			dialogue.text = enemy.unitName + " licks wounds.";
			yield return new WaitForSeconds (2f);
//можно каку-то анимацию добавить
			
			playerTurn();
		}
//тут можно добавить ещё что-то врагу
	}
	
	public void attackButtonPressed()
	{
		if (state != battleState.PLAYER_TURN)
			return;
		//чтобы не спамить кнопками (костыль против бага)
		state = battleState.ENEMY_TURN;
	//нанести урон				
		dialogue.text = "Attack";
		//противник всё ещё жив
		if(!enemy.takeDamage(player.unitDamage))
		{
			dialogue.text = "Attack " + enemy.unitName;
//можно каку-то анимацию добавить
			//передача хода
			enemyHUD.updateHUD(enemy);			
			StartCoroutine(enemyTurn());
		}
		
		//если противник умер
		else
		{
			dialogue.text = "Attack was to strong for " + enemy.unitName + ".";
			StartCoroutine(won_scene());						
		}		
	}
	public void healButtonPressed()
	{
		if (state!=battleState.PLAYER_TURN)
			return;
		//чтобы не спамить кнопками (костыль против бага)
		state = battleState.ENEMY_TURN;
		dialogue.text ="Heal";
		
		player.heal();
		playerHUD.updateHUD(player);
		
		//передача хода		
		StartCoroutine(enemyTurn());		
	}
	IEnumerator lost_scene()
	{
//ДОПИСАТЬ	
		state = battleState.LOST;
//для дебага можно потом удалить єту 2 строчки
		dialogue.text = "You lost to " + enemy.unitName;
		yield return new WaitForSeconds (2f);
		StartCoroutine(prepareBattle());
	}
	IEnumerator won_scene()
	{
//ДОПИСАТЬ		
		state = battleState.WON;
//для дебага можно потом удалить єту 2 строчки
		dialogue.text = "You lost to " + enemy.unitName;
		yield return new WaitForSeconds (2f);
		StartCoroutine(prepareBattle());
	}
}
