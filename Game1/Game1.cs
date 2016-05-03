using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;
using Game1.Minimax;
using Game1.Contents;


namespace Game1
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D lines, xTexture, oTexture, compButton, persButton, button, bowser;
        SpriteFont font;
        SoundEffect luigi, mario, marioWins, luigiWins, drawSound;
        Button bEasy, bHard, b1P, b2P, bReset;
        Lines liner;
        Random rg;
        ButtonClick but1, but2, butReset, butComp, butUser, butEasy, butHard;

        MouseState prevState;
        MouseState mouse;
        Board table;
        
        int[] board;
        int player;
        bool but1Select, but2Select, endGame, draw, xWins, oWins, easy, hard, compStarts, userStarts, empty;

        const int BOARD_MIN_X = 175, BOARD_MAX_X = 635, BOARD_MIN_Y = 75, BOARD_MAX_Y = 525, BOARD_CELL = 150, IMAGE_CELL_POSITION = 50;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 700;
            graphics.PreferredBackBufferWidth = 800;
            IsMouseVisible = true;

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            lines = new Texture2D(GraphicsDevice, 1, 1);
            lines.SetData<Color>(new Color[] { Color.Maroon });

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            button = Content.Load<Texture2D>("button.png");
            xTexture = Content.Load<Texture2D>("Char21.png");
            oTexture = Content.Load<Texture2D>("Char33.png");
            compButton = Content.Load<Texture2D>("console.jpg");
            persButton = Content.Load<Texture2D>("Person.jpg");
            bowser = Content.Load<Texture2D>("bowser.jpg");
            font = Content.Load<SpriteFont>("font");
            luigi = Content.Load<SoundEffect>("luigi");
            mario = Content.Load<SoundEffect>("mario");
            luigiWins = Content.Load<SoundEffect>("luigiWins");
            marioWins = Content.Load<SoundEffect>("marioWins");
            drawSound = Content.Load<SoundEffect>("draw");

            bEasy = new Button(40, 540, 70, 550, "Easy", button, Color.Red, font);
            bHard = new Button(40, 600, 70, 610, "Hard", button, Color.Red, font);
            bReset = new Button(350, 570, 375, 580, "Reset", button, Color.Black, font);
            b1P = new Button(278, 5, 298, 15, "1 Player", button, Color.Black, font);
            b2P = new Button(402, 5, 422, 15, "2 Player", button, Color.Black, font);
            but1 = new ButtonClick(278, 398, 5, 55);
            but2 = new ButtonClick(402, 520, 5, 55);
            butReset = new ButtonClick(350, 470, 570, 620);
            butComp = new ButtonClick(240, 320, 570, 650);
            butUser = new ButtonClick(480, 560, 570, 650);
            butEasy = new ButtonClick(40, 160, 540, 590);
            butHard = new ButtonClick(40, 160, 600, 650);
            liner = new Lines(lines);

            rg = new Random();

            mouse = Mouse.GetState();
            table = new Board();

            board = new int[9];
            but1Select = but2Select = endGame = draw = xWins = oWins = easy = hard = compStarts = userStarts = false;
            empty = true;

        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            board = table.board;
            buttonclick();
            if (but1Select && !endGame) {
                if (easy)
                {
                    endGame = gameEnded(gameTime);
                    if (!endGame)
                    {
                        play1Easy(gameTime);
                    }
                    endGame = gameEnded(gameTime);
                }
                if(hard) {
                    endGame = gameEnded(gameTime);
                    if (!endGame)
                    {
                        play1Hard(gameTime);
                    }
                    endGame = gameEnded(gameTime);
                }
            }
            if (but2Select && !endGame) {
                play2Players();
                endGame = gameEnded(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.WhiteSmoke);

            spriteBatch.Begin();
            drawButtons(spriteBatch);
            drawLines(spriteBatch);
            drawBoard(spriteBatch);


            drawBowser(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        //draw methods
        public void drawButtons(SpriteBatch sb)
        {
            b1P.draw(sb, but1Select);
            b2P.draw(sb, but2Select);
            if (but1Select || but2Select)
            {
                bReset.draw(sb, false);
            }
            if (but1Select)
            {
                liner.playerButtons(sb, compButton, persButton);
                bEasy.draw(sb, easy);
                bHard.draw(sb, hard);
            }
        }

        public void drawLines(SpriteBatch sb)
        {
            if (but1Select || but2Select)
            {
                liner.draw(sb);
            }
        }

        public void drawBoard(SpriteBatch sb) {
            int x0 = 180, y0 = 80;
            
            
            for (int i = 0; i < board.Length; i++) {
                int x = i % 3;
                int y = (int)(i / 3);
                Vector2 pos = new Vector2(x0 + (BOARD_CELL * x) + IMAGE_CELL_POSITION, y0 + (BOARD_CELL * y) + IMAGE_CELL_POSITION);
                if (board[i] == 1)
                {
                    sb.Draw(xTexture, pos, Color.White);
                }
                if (board[i] == 2)
                {
                    sb.Draw(oTexture, pos, Color.White);
                }
            }

            if (xWins) {
                sb.DrawString(font, "Luigi Wins", new Vector2(335, 630), Color.Red, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
            }
            if (oWins)
            {
                sb.DrawString(font, "Mario Wins", new Vector2(335, 630), Color.Red, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
            }
        }

        public void drawBowser(SpriteBatch sb) {
            if (draw) {
                sb.Draw(bowser, new Vector2(65, 5), Color.White);
            }
        }

        //Updates
        public void buttonclick() {
            prevState = mouse;
            mouse = Mouse.GetState();
            int x = mouse.X, y = mouse.Y;

            if (!but2Select)
            {
                if (but1.state(x, y, mouse, prevState))
                {
                    but1Select = true;
                    but2Select = false;
                    player = 0;
                }
            }
            if (!but1Select)
            {
                if (but2.state(x, y, mouse, prevState))
                {
                    but1Select = false;
                    but2Select = true;
                    player = 0;
                }
            }
            if (butReset.state(x, y, mouse, prevState)
                && (but1Select || but2Select))
            {
                but1Select = but2Select = endGame = draw = xWins = oWins = compStarts = userStarts = easy = hard = false;
                empty = true;
                table.reset();
                player = 1;
                Console.WriteLine("Reset");
            }
            if (butComp.state(x, y, mouse, prevState)
                && but1Select && !userStarts)
            {
                compStarts = true;
                userStarts = false;
                player = 0;
                Console.WriteLine("Computer Starts");
            }
            if (butUser.state(x, y, mouse, prevState)
                && but1Select && !compStarts)
            {
                userStarts = true;
                compStarts = false;
                player = 1;
                Console.WriteLine("User starts");
            }
            if (butEasy.state(x, y, mouse, prevState)
                && !hard){
                easy = true;
                hard = false;
                Console.WriteLine("Easy");
            }
            if (butHard.state(x, y, mouse, prevState)
                && !easy){
                easy = false;
                hard = true;
                Console.WriteLine("Hard");
            }


        }

        public void play1Easy(GameTime gameTime) {
            if (compStarts)
            { 
                play(gameTime);
            }
            if (userStarts) {
                play(gameTime);
            }
        }

        public void play1Hard(GameTime gameTime) {
            if (compStarts)
            {
                if (empty && ((player % 2) == 0))
                {
                    randComputer();
                    empty = false;
                }
                else {
                    minimaxPlay(gameTime);
                }
            }
            if (userStarts)
            {
                minimaxPlay(gameTime);
            }
        }

        public void play2Players() {
            int x = mouse.X, y = mouse.Y;

            if ((x >= BOARD_MIN_X && x <= BOARD_MAX_X) && (y >= BOARD_MIN_Y && y <= BOARD_MAX_Y )) {
                if (mouse.LeftButton == ButtonState.Pressed && prevState.LeftButton == ButtonState.Released) {
                    int index = (int)((x - BOARD_MIN_X) / BOARD_CELL) + (3 * (int)((y - BOARD_MIN_Y) / BOARD_CELL));
                    if (table.board[index] == 0)
                    {
                        table.changeState((x - BOARD_MIN_X), (y - BOARD_MIN_Y), ((player % 2) + 1));
                        playSound();
                        changePlayer();
                    }
                    board = table.board;
                }
            }
        }

        public bool gameEnded(GameTime gameTime)
        {
            bool aux = false;
            if (timer(gameTime, 1500))
            {
                if ('x' == Board.getState(board))
                {
                    aux = true;
                    xWins = true;
                    oWins = false;
                    luigiWins.Play(1.0f, -0.05f, 0.0f);
                    Console.WriteLine("X Wins");
                }
                if ('o' == Board.getState(board))
                {
                    aux = true;
                    oWins = true;
                    xWins = false;
                    Console.WriteLine("O Wins");
                    marioWins.Play(1.0f, 0.05f, 0.0f);
                }
                if ('d' == Board.getState(board))
                {
                    aux = true;
                    draw = true;
                    Console.WriteLine("Draw");
                    drawSound.Play(1.0f, -1.0f, 0.0f);
                }
            }
            return aux;
        }


        //Aux update mathods
        public void play(GameTime gameTime)
        {
            if (timer(gameTime, 2500))
            {
                if ((player % 2) == 0)
                {
                    randComputer();
                }
            }
            if ((player % 2) == 1)
            {
                play2Players();
            }
        }

        public void randComputer()
        {
            int index = availableSpace();
            int x = ((index % 3) * 150) + 175;
            int y = ((int)(index / 3) * 150) + 75;
            table.changeState((x - 175), (y - 75), ((player % 2) + 1));
            playSound();
            changePlayer();
        }

        public void minimaxPlay(GameTime gameTime)
        {
            if ((player % 2) == 1)
            {
                play2Players();
            }
            if (timer(gameTime, 1000))
            {
                if ((player % 2) == 0)
                {
                    Minimax.Minimax minimax = new Minimax.Minimax();
                    table.board = (int[])minimax.bestBoard(board, 1).Clone();
                    playSound();
                    changePlayer();
                }
            }
        }

        public bool timer(GameTime gameTime, int timeMod)
        {
            int time = (int)gameTime.TotalGameTime.Milliseconds;
            Console.WriteLine(time);
            if ((time % timeMod) == 0)
            {
                return true;
            }
            else {
                return false;
            }
        }

        public void changePlayer() {
            player++;
            
        }
        
        public void playSound() {
            if (!endGame)
            {
                if ((player % 2) == 0)
                {
                    luigi.Play(1.0f, -0.05f, 0.0f);
                }
                else {
                    mario.Play(1.0f, 0.05f, 0.0f);
                }
            }
        }

        public int availableSpace() {
            bool free = false;
            int index = 0;

            while (!free)
            {
                index = ((rg.Next(0, 9 + DateTime.Now.Second + DateTime.Now.Minute) % 3) + ((rg.Next(0, 13) + DateTime.Now.Millisecond) % 3) 
                    + ((rg.Next(0, 41) + DateTime.Now.Second) % 5)) ;
                if (table.board[index] == 0) { free = true; }
            }

            return index;
        }
    }
}

