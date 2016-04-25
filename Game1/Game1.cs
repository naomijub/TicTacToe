using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;
using Game1.Minimax;


namespace Game1
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D lines, xTexture, oTexture, compButton, persButton, button, bowser;
        SpriteFont font;
        SoundEffect luigi, mario, marioWins, luigiWins;
        Button bEasy, bHard, b1P, b2P, bReset;
        Lines liner;

        MouseState prevState;
        MouseState mouse;
        Board table;
        
        int[] board;
        int player;
        bool but1Select, but2Select, endGame, draw, xWins, oWins, easy, hard, compStarts, userStarts, empty;

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

            bEasy = new Button(40, 540, 70, 550, "Easy", button, Color.Red, font);
            bHard = new Button(40, 600, 70, 610, "Hard", button, Color.Red, font);
            bReset = new Button(350, 570, 375, 580, "Reset", button, Color.Black, font);
            b1P = new Button(278, 5, 298, 15, "1 Player", button, Color.Black, font);
            b2P = new Button(402, 5, 422, 15, "2 Player", button, Color.Black, font);
            liner = new Lines(lines);

            mouse = Mouse.GetState();
            table = new Board();

            board = new int[9];
            but1Select = but2Select = endGame = draw = xWins = oWins = easy = hard = compStarts = userStarts = empty = false;

        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //Exit();
            board = table.board;
            buttonclick();
            if (but1Select && !endGame) {
                if (easy)
                {
                    endGame = gameEnded();
                    play1Easy(gameTime);
                }
                if(hard) {
                    endGame = gameEnded();
                    play1Hard(gameTime);
                }
            }
            if (but2Select && !endGame) {
                endGame = gameEnded();
                play2Players();
                
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
                bEasy.draw(sb, false);
                bHard.draw(sb, false);
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
            int xo = 180;
            int yo = 80;
            for (int i = 0; i < board.Length; i++) {
                int x = i % 3;
                int y = (int)(i / 3);
                Vector2 pos = new Vector2(xo + (150 * x) + 50, yo + (150 * y) + 50);
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

        public void buttonclick() {
            prevState = mouse;
            mouse = Mouse.GetState();
            int x = mouse.X, y = mouse.Y;

            if ((x >= 278 && x <= 398) && (y >= 5 && y <= 55) && (mouse.LeftButton == ButtonState.Pressed && prevState.LeftButton == ButtonState.Released)) {
                but1Select = true;
                but2Select = false;
                player = 0;
            }
            if ((x >= 402 && x <= 520) && (y >= 5 && y <= 55) && (mouse.LeftButton == ButtonState.Pressed && prevState.LeftButton == ButtonState.Released))
            {
                but1Select = false;
                but2Select = true;
                player = 0;
            }
            if ((x >= 350 && x <= 470) && (y >= 570 && y <= 620) && (mouse.LeftButton == ButtonState.Pressed && prevState.LeftButton == ButtonState.Released)
                && (but1Select || but2Select))
            {
                but1Select = but2Select = endGame = draw = xWins = oWins = compStarts = userStarts = easy = hard = false;
                table.reset();
                player = 1;
            }
            if ((x >= 240 && x <= 320) && (y >= 570 && y <= 650) && (mouse.LeftButton == ButtonState.Pressed && prevState.LeftButton == ButtonState.Released)
                && but1Select && !userStarts)
            {
                compStarts = true;
                userStarts = false;
                player = 0;
            }
            if ((x >= 480 && x <= 560) && (y >= 570 && y <= 650) && (mouse.LeftButton == ButtonState.Pressed && prevState.LeftButton == ButtonState.Released)
                && but1Select && !compStarts)
            {
                userStarts = true;
                compStarts = false;
                player = 1;
            }
            if ((x >= 40 && x <= 160) && (y >= 540 && y <= 590) && (mouse.LeftButton == ButtonState.Pressed && prevState.LeftButton == ButtonState.Released)
                && !hard){
                easy = true;
                hard = false;
                Console.WriteLine("Easy");
            }
            if ((x >= 40 && x <= 160) && (y >= 600 && y <= 650) && (mouse.LeftButton == ButtonState.Pressed && prevState.LeftButton == ButtonState.Released)
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
                if (empty)
                {
                    randComputer();
                    empty = false;
                }
                else {
                    if ((player % 2) == 1)
                    {
                        play2Players();
                    }
                    if ((player % 2) == 0)
                    {
                        Minimax.Minimax minimax = new Minimax.Minimax(board);
                        board = minimax.tree.root.nodes[minimax.run(minimax.tree.root)].state.board;
                    }
                }
            }
            if (userStarts)
            {
                if ((player % 2) == 1)
                {
                    play2Players();
                }
                if ((player % 2) == 0)
                {
                    Minimax.Minimax minimax = new Minimax.Minimax(board);
                    board = minimax.tree.root.nodes[minimax.run(minimax.tree.root)].state.board;
                }
            }
        }

        public void play2Players() {
            int x = mouse.X, y = mouse.Y;

            if ((x >= 175 && x <= 635) && (y >= 75 && y <= 535)) {
                if (mouse.LeftButton == ButtonState.Pressed && prevState.LeftButton == ButtonState.Released) {
                    //Console.WriteLine("Mouse X/Y: " + x + " / " + y);
                    //Console.WriteLine("Player: " + ((player % 2) + 1));
                    int index = (int)((x - 175) / 150) + (3 * (int)((y - 75) / 150));
                    if (table.board[index] == 0)
                    {
                        table.changeState((x - 175), (y - 75), ((player % 2) + 1));
                        playSound();
                        changePlayer();
                    }
                    board = table.board;
                    //printBoard();
                }
            }
        }

        public void changePlayer() {
            player++;
            
        }

        public void printBoard() {
            Console.WriteLine("[" + board[0] + "|" + board[1] + "|" + board[2] + "]");
            Console.WriteLine("[" + board[3] + "|" + board[4] + "|" + board[5] + "]");
            Console.WriteLine("[" + board[6] + "|" + board[7] + "|" + board[8] + "]");
        }

        public bool gameEnded() {
            bool aux = false;
            if ('x' == table.getState(board)) {
                aux = true;
                xWins = true;
                oWins =  false;
                luigiWins.Play(1.0f, 0.0f, 0.0f);
                Console.WriteLine("X Wins");
            }
            if ('o' == table.getState(board))
            {
                aux = true;
                oWins = true;
                xWins = false;
                Console.WriteLine("O Wins");
                marioWins.Play(1.0f, 0.0f, 0.0f);
            }
            if ('d' == table.getState(board))
            {
                aux = true;
                draw = true;
                Console.WriteLine("Draw");
            }

            return aux;
        }

        public void playSound() {
            if ((player % 2) == 0)
            {
                if (!xWins)
                {
                    luigi.Play(1.0f, 0.0f, 0.0f);
                }
            }
            else {
                if (!oWins)
                {
                    mario.Play(1.0f, 0.0f, 0.0f);
                }
            }
        }

        public int availableSpace() {
            Random rg = new Random();
            bool free = false;
            int index = 0;

            while (!free)
            {
                index = rg.Next(0, 9);
                if (table.board[index] == 0) { free = true; }
            }

            return index;
        }

        public void play(GameTime gameTime) {
            if (timer(gameTime))
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

        public void randComputer() {
            int index = availableSpace();
            int x = ((index % 3) * 150) + 175;
            int y = ((int)(index / 3) * 150) + 75;
            table.changeState((x - 175), (y - 75), ((player % 2) + 1));
            playSound();
            changePlayer();
        }

        public bool timer(GameTime gameTime) {
            int time = (int)gameTime.TotalGameTime.Milliseconds;
            Console.WriteLine(time);
            if ((time % 2500) == 0)
            {
                return true;
            }
            else {
                return false;
            }
        }
    }
}

