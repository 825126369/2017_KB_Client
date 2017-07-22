using UnityEngine;
using System;
using System.Collections;

public class CustomCursor : MonoBehaviour {
	public Texture2D cursor = null;
	public Texture2D[] cursors = null;
     
	public static bool showCursor = true;
	
	public enum STATE
	{
		NORMAL = 0,
		QUEST = 1,
		MONEY = 2,
		FIGHT = 3,
		KEY = 4,
		GOSSIP = 5,
		REPAIR = 6,
		HAND = 7
	};
	
	STATE state = STATE.NORMAL;
	
    void Awake (){
			setUp();
			Cursor.visible = false;
        } 
       
       
    /*Make the "link" texture follow the cursor.*/
    void OnGUI () {
    	if(showCursor == true)
    	{
    		GetInput();
			GUI.Label(new Rect(Input.mousePosition.x - 12, Screen.height - Input.mousePosition.y - 10, 100, 100), cursor);
		}
    }
    
    void OnApplicationFocus(bool focus)
    { 
		Cursor.visible = false; 
    }

	void OnMouseEnter(){
	    Cursor.visible = false;
	}
	 
	void OnMouseExit(){
	    Cursor.visible = true;
	}
	 
    void setUp()
    {
    	switch(state)
    	{
    		case STATE.HAND:
    			cursor = cursors[7];
    			break;
    		case STATE.QUEST:
    			cursor = cursors[5];
    			break;
    		case STATE.MONEY:
    			cursor = cursors[4];
    			break;
    		case STATE.FIGHT:
    			cursor = cursors[1];
    			break;
    		case STATE.KEY:
    			cursor = cursors[3];
    			break;
    		case STATE.GOSSIP:
    			cursor = cursors[2];
    			break;
    		case STATE.REPAIR:
    			cursor = cursors[6];
    			break;
    		default:
    			cursor = cursors[0];
    			break;
    	};
    }
    
    void setDown()
    {
    	switch(state)
    	{
    		case STATE.HAND:
    			cursor = cursors[8];
    			break;
    		case STATE.QUEST:
    			cursor = cursors[5];
    			break;
    		case STATE.MONEY:
    			cursor = cursors[4];
    			break;
    		case STATE.FIGHT:
    			cursor = cursors[1];
    			break;
    		case STATE.KEY:
    			cursor = cursors[3];
    			break;
    		case STATE.GOSSIP:
    			cursor = cursors[2];
    			break;
    		case STATE.REPAIR:
    			cursor = cursors[6];
    			break;
    		default:
    			cursor = cursors[0];
    			break;
    	};
    }
    
    void GetInput() {
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1)) {
        	setDown();
        } else
        {
        	setUp();
        }
    }
}