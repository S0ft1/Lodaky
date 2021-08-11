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
        private int numberOfShips = 4;
        private Ship[] allyFleet = new Ship[4];
        private Ship[] enemyFleet = new Ship[4];
        public bool rotation = false;
        private int rowLenght = 10;
        private bool isAiPlanning = false;
        private AI ai = new AI();

        public bool IsAiPlanning { get => isAiPlanning; set => isAiPlanning = value; }

        public Game()
        {
            fillUpFleets();
        }
        private void fillUpFleets()
        {
            allyFleet[0] = new Battleship();
            allyFleet[1] = new AircraftCarrier();
            allyFleet[2] = new Cruiser();
            allyFleet[3] = new Destroyer();
            enemyFleet[0] = new Battleship();
            enemyFleet[1] = new AircraftCarrier();
            enemyFleet[2] = new Cruiser();
            enemyFleet[3] = new Destroyer();
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
            for(int i = 0; i < numberOfShips; ++i)
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
            int shipLenght = 0;
            int shipWidth = 0;
           
            switch (chosenShip)
            {
                case FieldTypes.BB:
                    shipLenght = allyFleet[0].getLenght();
                    shipWidth = allyFleet[0].getWidth();
                    break;
                case FieldTypes.CV:
                    shipLenght = allyFleet[1].getLenght();
                    shipWidth = allyFleet[1].getWidth();
                    break;
                case FieldTypes.CA:
                    shipLenght = allyFleet[2].getLenght();
                    shipWidth = allyFleet[2].getWidth();
                    break;
                case FieldTypes.DD:
                    shipLenght = allyFleet[3].getLenght();
                    shipWidth = allyFleet[3].getWidth();
                    break;
                default:
                    return false;
            }

            if (!rotation)
            {
                return placeNonRotatedShip(position, shipLenght, shipWidth);
            }
            else
            {
                return placeRotatedShip(position, shipLenght, shipWidth);
            }

        }

        private bool placeNonRotatedShip(Position position, int shipLenght, int shipWidth)
        {

            FieldTypes[,] tempField = playersField;
            if (isAiPlanning)
            {
                tempField = enemysField;
            }
            //checknuti od hranice
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
            //checknuti ostatnich lodi
            if (possiblePosition(position, shipLenght, shipWidth,tempField))
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
                        tempField[position.X + i, position.Y+1] = chosenShip;
                    }
                }
               
                //allyFleet[0].setPosition(position);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool placeRotatedShip(Position position, int shipLenght, int shipWidth)
        {
            FieldTypes[,] tempField = playersField;
            if (isAiPlanning)
            {
                tempField = enemysField;
            }
            //checknuti od hranice
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
            //checknuti ostatnich lodi
            if (possiblePosition(position, shipLenght, shipWidth, tempField))
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
                        tempField[position.X+1, position.Y - i] = chosenShip;
                    }
                }
               // allyFleet[0].setPosition(position);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool possiblePosition(Position position, int shipLenght, int shipWidth, FieldTypes[,] tempField)
        {
            if (!rotation)
            {
                for (int i = 0; i < shipLenght; ++i)
                {
                    if (chosenShip == FieldTypes.CV)
                    {
                        if (tempField[position.X + i, position.Y] != FieldTypes.SEA || tempField[position.X + i, position.Y +1] != FieldTypes.SEA)
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
                        if (tempField[position.X, position.Y - i] != FieldTypes.SEA || tempField[position.X+1, position.Y - i] != FieldTypes.SEA)
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
            if (isAiPlanning)
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
            Console.WriteLine(position.X + "," + position.Y);
            return false;
        }
    }

}

    

