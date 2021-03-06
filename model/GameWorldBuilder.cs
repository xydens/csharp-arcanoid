using System.Collections.Generic;
using System.Drawing;
using ConsoleApplication1.model.entities;

namespace ConsoleApplication1.model
{
    public class GameWorldBuilder
    {
        private const int WALL_WIDTH = 10;

        private GameWorld world;

        public GameWorldBuilder()
        {
            Reset();
        }

        public void BuildField(Size fieldSize)
        {
            world.Field = new Entity(
                new Point(WALL_WIDTH + fieldSize.Width / 2, WALL_WIDTH + fieldSize.Height / 2),
                fieldSize
            );
        }

        public void BuildWalls()
        {
            var sideWallsSize = new Size(WALL_WIDTH, world.Field.Size.Height + WALL_WIDTH * 2);

            world.LeftWall = new Entity(
                new Point(WALL_WIDTH / 2, sideWallsSize.Height / 2),
                sideWallsSize
            );
            world.RightWall = new Entity(
                new Point(world.Field.Size.Width + WALL_WIDTH + WALL_WIDTH / 2, sideWallsSize.Height / 2),
                sideWallsSize
            );

            world.TopWall = new Entity(
                new Point(GetFieldCenter().X, WALL_WIDTH / 2),
                new Size(world.Field.Size.Width, WALL_WIDTH)
            );
        }

        private Point GetFieldCenter()
        {
            return new Point(
                WALL_WIDTH + world.Field.Size.Width / 2,
                world.Field.Size.Height / 2 + WALL_WIDTH
            );
        }

        public void BuildVoid()
        {
            world.BottomVoid = new Entity(
                new Point(GetFieldCenter().X, world.Field.Size.Height + WALL_WIDTH + WALL_WIDTH / 2),
                new Size(world.Field.Size.Width, WALL_WIDTH)
            );
        }

        public void BuildBall()
        {
            world.Ball = new Ball(
                GetFieldCenter(),
                new Size(20, 20),
                new Vector()
                {
                    Y = 10,
                    X = 1
                }
            );
        }

        private Size GetBrickSize()
        {
            return new Size(30, WALL_WIDTH);
        }

        private int GetBricksInRowCount()
        {
            return world.Field.Size.Width / GetBrickSize().Width;
        }

        public void BuildBricks(int layersCount)
        {
            var brickSize = GetBrickSize();

            var fieldRectangle = world.Field.GetRectangle();

            for (int row = 0; row < layersCount; row++)
            {
                for (int column = 0; column < GetBricksInRowCount(); column++)
                {
                    var brickPosition = new Point(
                        fieldRectangle.Left + brickSize.Width * column + brickSize.Width / 2,
                        fieldRectangle.Top + brickSize.Height * row + brickSize.Height / 2
                    );
                    var brick = new Brick(brickPosition, brickSize);
                    world.Bricks.Add(brick);
                }
            }
        }

        public void BuildPaddle()
        {
            world.Paddle = new Entity(
                new Point(GetFieldCenter().X, world.Field.Size.Height - WALL_WIDTH),
                new Size(30, WALL_WIDTH)
            );
        }

        public void Reset()
        {
            world = new GameWorld();
        }

        public GameWorld GetResult()
        {
            return world;
        }
    }
}