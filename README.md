**Key Features of the Game**

🟩 Grid-Based Area: At the start of the game, a grid-based game area is dynamically generated through code. Other game elements (breakable walls, enemies, power-ups) are randomly placed in empty spaces. This ensures a unique experience in each level.

📡 Event-Based Architecture: The game is built on an event-based architecture.

📄 Data Management: Game and player data are managed using Scriptable Objects. When the game starts, the Splash Screen initializes the start screen, preparing the data. Level information is saved in a JSON file, allowing data sharing between levels.

👾 AI Movements: Enemies move according to the created map algorithm. Different enemy types follow the player based on a defined follow distance.

Power-Up Management:

• 💥 Power-Up Ratios: Each level contains a certain number (minimum and maximum) of power-ups. Used power-ups are not reused in subsequent levels.

• 🔄 x2 Explosion Power-Up: When the x2 explosion power-up is collected, additional explosion pieces are added to the explosion list by shifting the existing x1 explosion objects by one unit.
