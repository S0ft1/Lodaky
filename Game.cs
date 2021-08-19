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
        public Field[,] playersField = new Field[10, 10];
        public Field[,] enemysField = new Field[10, 10];
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
            fillUpFields();
        }
        private void fillUpFleets()
        {
            Position pos = new Position(0, 0);
            allyFleet[0] = new Battleship(pos, false);
            allyFleet[1] = new AircraftCarrier(pos, false);
            allyFleet[2] = new Cruiser(pos, false);
            allyFleet[3] = new Destroyer(pos, false);
            enemyFleet[0] = new Battleship(pos, false);
            enemyFleet[1] = new AircraftCarrier(pos, false);
            enemyFleet[2] = new Cruiser(pos, false);
            enemyFleet[3] = new Destroyer(pos, false);
        }

        private void fillUpFields()
        {
            for (int i = 0; i < rowLenght; ++i)
            {
                for (int j = 0; j < rowLenght; ++j)
                {
                    playersField[i, j] = new Field(FieldTypes.SEA);
                    enemysField[i, j] = new Field(FieldTypes.SEA);
                }
            }
        }
        #region planning
        private Position aiPlanningController()
        {
            Position pos = ai.getRndPos();
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

        private void drawShip(Field[,] tempField, Position position, ushort shipLenght)
        {
            for (int i = 0; i < shipLenght; ++i)
            {
                if (chosenShip != FieldTypes.CV)
                {
                    tempField[position.X + i, position.Y].type = chosenShip;
                }
                else
                {
                    tempField[position.X + i, position.Y].type = chosenShip;
                    tempField[position.X + i, position.Y + 1].type = chosenShip;
                }
            }
        }
        private void drawRotatedShip(Field[,] tempField, Position position, ushort shipLenght)
        {
            for (int i = 0; i < shipLenght; ++i)
            {
                if (chosenShip != FieldTypes.CV)
                {
                    tempField[position.X, position.Y - i].type = chosenShip;
                }
                else
                {
                    tempField[position.X, position.Y - i].type = chosenShip;
                    tempField[position.X + 1, position.Y - i].type = chosenShip;
                }
            }
        }
        private bool placeNonRotatedShip(Position position, ushort shipLenght)
        {

            Field[,] tempField = playersField;
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
            Field[,] tempField = playersField;
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

        private bool possiblePosition(Position position, ushort shipLenght, Field[,] tempField)
        {
            if (!rotation)
            {
                for (int i = 0; i < shipLenght; ++i)
                {
                    if (chosenShip == FieldTypes.CV)
                    {
                        if (tempField[position.X + i, position.Y].type != FieldTypes.SEA || tempField[position.X + i, position.Y + 1].type != FieldTypes.SEA)
                        {

                            selectCorrectField(tempField);
                            return false;
                        }
                    }
                    else
                    {
                        if (tempField[position.X + i, position.Y].type != FieldTypes.SEA)
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
                        if (tempField[position.X, position.Y - i].type != FieldTypes.SEA || tempField[position.X + 1, position.Y - i].type != FieldTypes.SEA)
                        {
                            selectCorrectField(tempField);
                            return false;
                        }
                    }
                    else
                    {
                        if (tempField[position.X, position.Y - i].type != FieldTypes.SEA)
                        {

                            selectCorrectField(tempField);
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        private void selectCorrectField(Field[,] tempField)
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

        public void battleRequest(Position position)
        {
            playerAttacks(position);
            aiAttacks();
        }

        private bool playerAttacks(Position position)
        {
            Position[] area = chooseAttack(allyFleet, position);
            bool result = XAttacks(area, enemysField, enemyFleet);
            if (checkFleetIfDestroyed(enemyFleet))
            {
                Console.WriteLine("enemy fleet sunk");
            }
            return result;
        }

        private bool aiAttacks()
        {
            chosenShip = ai.getShip();
            Position[] area = chooseAttack(allyFleet, ai.getRndPos());
            bool result = XAttacks(area, playersField, allyFleet);
            if (checkFleetIfDestroyed(allyFleet))
            {
                Console.WriteLine("ally fleet sunk");
            }
            chosenShip = FieldTypes.SEA;
            return result;
        }

        private ushort getHitIndex(FieldTypes type)
        {
            ushort hitIndex = 0;
            switch (type)
            {
                case FieldTypes.BB:
                    hitIndex = 0;
                    break;

                case FieldTypes.CV:
                    hitIndex = 1;
                    break;

                case FieldTypes.CA:
                    hitIndex = 2;
                    break;

                case FieldTypes.DD:
                    hitIndex = 3;
                    break;
            }
            return hitIndex;
        }
        private bool checkFleetIfDestroyed(Ship[] fleet)
        {
            int counter = 0;
            foreach (Ship ship in fleet)
            {
                if (ship.checkIfDestroyed())
                {
                    ++counter;
                }
            }
            return counter == 4;
        }
        private void checkHits(Position position, ushort hitIndex, Ship[] fleet)
        {
            if (fleet[hitIndex].getRotation())
            {
                checkRotatedHits(position, hitIndex, fleet);
            }

            else
            {
                checkNonRotatedHits(position, hitIndex, fleet);
            }
        }

        private void checkRotatedHits(Position position, ushort hitIndex, Ship[] fleet)
        {
            for (int i = 0; i < fleet[hitIndex].getLenght(); ++i)
            {
                //trosku hnus
                if (hitIndex == 1)
                {

                    if (fleet[hitIndex].getPosition().Y - i == position.Y && fleet[hitIndex].getPosition().X + 1 == position.X)
                    {
                        fleet[hitIndex].isHitted(i, true);
                    }
                    else if (fleet[hitIndex].getPosition().Y - i == position.Y)
                    {
                        fleet[hitIndex].isHitted(i, false);
                    }
                }
                else
                {
                    if (fleet[hitIndex].getPosition().Y - i == position.Y)
                    {
                        fleet[hitIndex].isHitted(i, false);
                    }
                }

            }
        }
        private void checkNonRotatedHits(Position position, ushort hitIndex, Ship[] fleet)
        {
            for (int i = 0; i < fleet[hitIndex].getLenght(); ++i)
            {
                if (hitIndex == 1)
                {
                    if (fleet[hitIndex].getPosition().X + i == position.X && fleet[hitIndex].getPosition().Y + 1 == position.Y)
                    {
                        fleet[hitIndex].isHitted(i, true);
                    }
                    else if (fleet[hitIndex].getPosition().X + i == position.X)
                    {
                        fleet[hitIndex].isHitted(i, false);
                    }

                }
                else
                {
                    if (fleet[hitIndex].getPosition().X + i == position.X)
                    {
                        fleet[hitIndex].isHitted(i, false);
                    }
                }

            }
        }

        private bool XAttacks(Position[] area, Field[,] tempField, Ship[] fleet)
        {
            bool result = false;
            switch (chosenShip)
            {
                case FieldTypes.BB:
                    result = bbAttack(area, tempField, fleet);
                    break;

                case FieldTypes.CA:
                    result = singleTarget(area[0], tempField, fleet);
                    break;

                case FieldTypes.CV:
                    CVScout(area, tempField);
                    result = true;
                    break;

                case FieldTypes.DD:
                    result = ddAttack(area, tempField, fleet);
                    break;
            }
            return true;
           
        }

        private bool singleTarget(Position position, Field[,] tempField, Ship[] fleet)
        {
            Console.WriteLine(position.X + "," + position.Y);
            ushort hitIndex;
            if (tempField[position.X, position.Y].type != FieldTypes.SEA)
            {
                Console.WriteLine("hit");
                hitIndex = getHitIndex(tempField[position.X, position.Y].type);
                checkHits(position, hitIndex, fleet);
                tempField[position.X, position.Y].type = FieldTypes.FIRE;
                tempField[position.X, position.Y].spotted = true;
                return true;
            }
            else
            {
                Console.WriteLine("miss");
                tempField[position.X, position.Y].spotted = true;
                return false;
            }

        }

        private bool bbAttack(Position[] area, Field[,] tempField, Ship[] fleet)
        {
            foreach (Position position in area)
            {
                singleTarget(position, tempField, fleet);
            }
            return true;
        }
        private bool CVScout(Position[] area, Field[,] tempField)
        {
            foreach (Position pos in area)
            {
                tempField[pos.X, pos.Y].spotted = true;
            }
            return true;
        }

        private bool ddAttack(Position[] area, Field[,] tempField, Ship[] fleet)
        {
            foreach (Position pos in area)
            {
                if (singleTarget(pos, tempField, fleet))
                {
                    return true;
                }
            }
            return false;
        }

        private Position[] chooseAttack(Ship[] fleet, Position position)
        {
            switch (chosenShip)
            {
                case FieldTypes.BB:
                    return fleet[0].attack(position, rotation);

                case FieldTypes.CV:
                    return fleet[1].attack(position, rotation);

                case FieldTypes.CA:
                    return fleet[2].attack(position, rotation);

                case FieldTypes.DD:
                    return fleet[3].attack(position, rotation);
                default:
                    return new Position[0];
            }
        }
    }


}



