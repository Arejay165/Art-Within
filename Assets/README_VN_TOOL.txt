README VISUAL NOVEL PLUGIN 

Please look at Prompts, Cutscene Related, and Managers for more info.
Every Implementation of Visual Novel Plugin must have a dialogue manager present in the scene. 
Please look at Arjay Scene for example of implementations. 

Please don't delete the tests scriptableobjects for reference purpose. 
==========================

Prompts 

Prompt = Displays both icon and message. Two component's position can be seen and customizable in Player_Mock PromptCanvas.
       = Only Contains one message, if you enable Has Cutscene Dialogue bool you can insert AI convo and create a DialogueCanvas  
	 to have AIConvo.
       = Interable Sprite in the Prompt's inspector lets you change the icon's images to your liking. 
       = Player Icon Variable refers to Image from the hierarchy please don't change it. 

Environmental Prompt
	= Used in Focus Narrator Cutscenes (All dialogues/message are all narrators).
	= Also have DialogueCanvas to put the AI Convo and add Cutscene
	= Panel refers to the Dialogue Canvas 
	= Text refers to the DialogueCanvas text 
	= Can Stop Player bool refers if you want to stop the player when cutscene starts.

==========================================================================================================

Cutscene Related

AIConvo = Visual Novel Cutscene. Press Q for the next message. 
	= Can insert and add conversation in the dialogues variable. Please use Scriptable Objects.
	= Text refers to the DialogueCanvas text they are also customizable there 
	= NPC Panel refers to the DialogueCanvas Panel they are also customizable there 
	= Can Stop Player bool refers if you want to stop the player when cutscene starts.
	= Will Destroy bool refers if you want to destroy the AI after the cutscene. 

Dialouge
	= Create Scriptable Object to create cutscenes. 
	= Conversations are a lists of dialogue properties that need to be filled. 
		= Message refers to the content of the dialogue.
		= Sound sfx refers to the sounds the typewriter will produce every letter.
		= Speaker: refers to the question on Who will be talking one who will talk? 
			= Player: If player will be the one talk. 
			= NPC: If NPC will be the one talk. 
			= Narrator: If Narrator will be the one talk. 
		= Speaker Animation: refers on who will use an animaton during their turn in dialogue.
			= Player Only: Only the Player will play an animation 
			= NPC Only: Only the NPC will play an animation 
			= Both: Both will play an animation 
		= Animation Name refers to what particular animation would you like them to play (strings)
			Note: This is interconnected to Speaker Animation, so if you choose the Only enums please don't add more than 
			one animation name but if you choose Both please add two elements in them. 
			Note: The first element of the animation name will refer to the player so choose any animation that only the player has
			      The second element of the  animation name will refer to the NPC so choose any animation that only the NPC has.
	= After Dialogue refers to if you want to replace the first conversation after you first started talking to the npc.
		= E.G. 1st Convo = "Take the sword and brandish it"
		= After dialogue / second encounter = "Didn't you hear what i just said?"
	= Repeat Lines lets you ignore lines to your second encounter to the npc. accepts integer. 
	  This will only occur if you don't have any after dialogue and you have more than 0 repeat lines at. 
		= if you put two to the variable.
		= E.G 1st Convo = "Take the sword and brandish it", "Go forth defeat the demon lord", "For that is your duty"
		= After dialogue / second encounter = "Go forth defeat the demon lord", "For that is your duty"
==========================================================================================================
Managers
 
Every Implementation of Visual Novel Plugin must have a dialogue manager present in the scene. 
Nothing will work if this doesn't have it. 

Dialouge Manager
	= Player Dialogue Panel refers to the Panel of the Dialouge Canvas of the Player_Mock.
	= Text refers to the to the text of the Dialouge Canvas of the Player_Mock.
	= Narrator Canvas refers to the Narrator Canvas of the DialogueManager
	= Narrator Canvas refers to the text of the DialogueManager
	= Typing Speed refers to how fast the text will move. 


