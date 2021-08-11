using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Lodaky
{
    class Game
    {
        public GameStates currentGameState = GameStates.PLANNING;
        public FieldTypes[,] playersField = new FieldTypes[10, 10];
        public FieldTypes[,] enemysField = new FieldTypes[10, 10];
        public FieldTypes chosenShip = FieldTypes.SEA;
        private ushort numberOfShips = 4;
        private Ship[] allyFleet = new Ship[4];
        private Ship[] enemyFleet = new Ship[4];
        public bool rotation = false;
        private ushort rowLenght = 10;
        private bool aiTurn = false;
        private AI ai = new AI();

        public bool AiTurn { get => aiTurn; set => aiTurn = value; }

        public Game()
        {
            fillUpFleets();
        }
        private void fillUpFleets()
        {
            Position pos = new Position(0, 0);
            allyFleet[0] = new Battleship(pos,false);
            allyFleet[1] = new AircraftCarrier(pos, false);
            allyFleet[2] = new Cruiser(pos, false);
            allyFleet[3] = new Destroyer(pos, false);
            enemyFleet[0] = new Battleship(pos, false);
            enemyFleet[1] = new AircraftCarrier(pos, false);
            enemyFleet[2] = new Cruiser(pos, false);
            enemyFleet[3] = new Destroyer(pos, false);
        }
        #region planning
        private Position aiPlanningController()
        {
            Position pos = new Position(0, 0);
            pos.X = ai.getRndPos();
            pos.Y = ai.getRndPos();
            if (ai.getAiRotation())
            {
                rotation = !rotation;
            }
            return pos;
        }

        public void aiPlanning()
        {
            numberOfShips = 4;
            for (int i = 0; i < numberOfShips; ++i)
            {
                Position pos = aiPlanningController();
                chosenShip = enemyFleet[i].getType();
                while (!placeShip(pos))
                {
                    pos = aiPlanningController();
                    Console.WriteLine("AI failed to place a ship");
                };
            }
        }


        public void chooseShip(FieldTypes ship)
        {
            chosenShip = ship;
        }

        public bool placeShip(Position position)
        {
            ushort shipLenght = 0;

            switch (chosenShip)
            {
                case FieldTypes.BB:
                    shipLenght = allyFleet[0].getLenght();
                    break;

                case FieldTypes.CV:
                    shipLenght = allyFleet[1].getLenght();
                    break;

                case FieldTypes.CA:
                    shipLenght = allyFleet[2].getLenght();
                    break;

                case FieldTypes.DD:
                    shipLenght = allyFleet[3].getLenght();
                    break;

                default:
                    return false;
            }

            if (!rotation)
            {
                return placeNonRotatedShip(position, shipLenght);
            }
            else
            {
                return placeRotatedShip(position, shipLenght);
            }

        }

        private Position checkBorders(Position position, ushort shipLenght)
        {
            if (position.X + shipLenght > rowLenght)
            {
                position.X = rowLenght - shipLenght;
            }

            if (chosenShip == FieldTypes.CV)
            {
                if (position.Y + 1 >= rowLenght)
                {
                    position.Y = position.Y - 1;
                }
            }
            return position;
        }

        private Position checkBordersRotated(Position position, ushort shipLenght)
        {
            if (position.Y - shipLenght + 1 <= 0)
            {
                position.Y = shipLenght - 1;
            }
            if (chosenShip == FieldTypes.CV)
            {
                if (position.X + 1 >= rowLenght)
                {
                    position.X = position.X - 1;
                }
            }
            return position;
        }

        private void drawShip(FieldTypes[,] tempField, Position position, ushort shipLenght)
        {
            for (int i = 0; i < shipLenght; ++i)
            {
                if (chosenShip != FieldTypes.CV)
                {
                    tempField[position.X + i, position.Y] = chosenShip;
                }
                else
                {
                    tempField[position.X + i, position.Y] = chosenShip;
                    tempField[position.X + i, position.Y + 1] = chosenShip;
                }
            }
        }
        private void drawRotatedShip(FieldTypes[,] tempField, Position position, ushort shipLenght)
        {
            for (int i = 0; i < shipLenght; ++i)
            {
                if (chosenShip != FieldTypes.CV)
                {
                    tempField[position.X, position.Y - i] = chosenShip;
                }
                else
                {
                    tempField[position.X, position.Y - i] = chosenShip;
                    tempField[position.X + 1, position.Y - i] = chosenShip;
                }
            }
        }
        private bool placeNonRotatedShip(Position position, ushort shipLenght)
        {

            FieldTypes[,] tempField = playersField;
            if (aiTurn)
            {
                tempField = enemysField;
            }

            //checknuti od hranice
            position = checkBorders(position, shipLenght);

            //checknuti ostatnich lodi
            if (possiblePosition(position, shipLenght, tempField))
            {
                drawShip(tempField, position, shipLenght);
                createShip(position);
                return true;
            }
            else
            {
                return false;
            }
        }


        private void createShip(Position position)
        {
            if (!aiTurn)
            {
                switch (chosenShip)
                {
                    case FieldTypes.BB:
                        allyFleet[0] = new Battleship(position, rotation);
                        break;

                    case FieldTypes.CV:
                        allyFleet[1] = new AircraftCarrier(position, rotation);
                        break;

                    case FieldTypes.CA:
                        allyFleet[2] = new Cruiser(position, rotation);
                        break;

                    case FieldTypes.DD:
                        allyFleet[3] = new Destroyer(position, rotation);
                        break;

                }

            }
            else
            {
                switch (chosenShip)
                {
                    case FieldTypes.BB:
                        enemyFleet[0] = new Battleship(position, rotation);
                        break;

                    case FieldTypes.CV:
                        enemyFleet[1] = new AircraftCarrier(position, rotation);
                        break;

                    case FieldTypes.CA:
                        enemyFleet[2] = new Cruiser(position, rotation);
                        break;

                    case FieldTypes.DD:
                        enemyFleet[3] = new Destroyer(position, rotation);
                        break;

                }
            }
        }
        private bool placeRotatedShip(Position position, ushort shipLenght)
        {
            FieldTypes[,] tempField = playersField;
            if (aiTurn)
            {
                tempField = enemysField;
            }

            //checknuti od hranice
            position = checkBordersRotated(position, shipLenght);

            //checknuti ostatnich lodi
            if (possiblePosition(position, shipLenght, tempField))
            {

                drawRotatedShip(tempField, position, shipLenght);
                createShip(position);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool possiblePosition(Position position, ushort shipLenght, FieldTypes[,] tempField)
        {
            if (!rotation)
            {
                for (int i = 0; i < shipLenght; ++i)
                {
                    if (chosenShip == FieldTypes.CV)
                    {
                        if (tempField[position.X + i, position.Y] != FieldTypes.SEA || tempField[position.X + i, position.Y + 1] != FieldTypes.SEA)
                        {

                            selectCorrectField(tempField);
                            return false;
                        }
                    }
                    else
                    {
                        if (tempField[position.X + i, position.Y] != FieldTypes.SEA)
                        {

                            selectCorrectField(tempField);
                            return false;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < shipLenght; ++i)
                {
                    if (chosenShip == FieldTypes.CV)
                    {
                        if (tempField[position.X, position.Y - i] != FieldTypes.SEA || tempField[position.X + 1, position.Y - i] != FieldTypes.SEA)
                        {
                            selectCorrectField(tempField);
                            return false;
                        }
                    }
                    else
                    {
                        if (tempField[position.X, position.Y - i] != FieldTypes.SEA)
                        {

                            selectCorrectField(tempField);
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        private void selectCorrectField(FieldTypes[,] tempField)
        {
            if (aiTurn)
            {
                enemysField = tempField;
            }
            else
            {
                playersField = tempField;
            }
        }

        public bool planningRequest(Position position)
        {
            if (placeShip(position))
            {
                --numberOfShips;
                if (numberOfShips == 0)
                {
                    currentGameState = GameStates.PLAYER;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        public bool battleRequest(Position position)
        {
            aiTurn = false;
            Console.WriteLine(position.X + "," + position.Y);
            if (!aiTurn)
            {
                if (enemysField[position.X, position.Y] != FieldTypes.SEA && enemysField[position.X, position.Y] != FieldTypes.SPOT)
                {
                    Console.WriteLine("hit");
                }
                else
                {
                    Console.WriteLine("miss");
                }
            }
            else
            {
                if (playersField[position.X, position.Y] != FieldTypes.SEA && playersField[position.X, position.Y] != FieldTypes.SPOT)
                {
                    Console.WriteLine("hit");
                }
                else
                {
                    Console.WriteLine("miss");
                }
            }

            Console.WriteLine(enemysField[position.X, position.Y]);
            return false;
        }
    }

}



