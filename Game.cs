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
        #region variables
        public GameStates currentGameState = GameStates.PLANNING;
        public Field[,] playersField;
        public Field[,] enemysField;
        public FieldTypes chosenShip;
        private ushort numberOfShips;
        private Ship[] allyFleet;
        private Ship[] enemyFleet;
        public bool rotation;
        private ushort rowLenght;
        private bool aiTurn = false;
        private AI ai;
        private bool[] chosenShips;
        public string enemyOutput;
        public string playersOutput;
        public bool playerHasWon = false;
        public bool AiTurn { get => aiTurn; set => aiTurn = value; }
        #endregion
        #region setup
        public Game()
        {
            numberOfShips = 4;
            rotation = false;
            rowLenght = 10;
            playersField = new Field[10, 10];
            enemysField = new Field[10, 10];
            allyFleet = new Ship[4];
            enemyFleet = new Ship[4];
            chosenShip = FieldTypes.SEA;
            ai = new AI();
            chosenShips = new bool[] { false, false, false, false };
            enemyOutput = "";
            playersOutput = "";
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

        public Ship[] getAllyFleet()
        {
            return allyFleet;
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
        #endregion
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
                    //Console.WriteLine("AI failed to place a ship");
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
            if (!isValidShip())
            {
                return false;
            }
            if (placeShip(position))
            {
                --numberOfShips;
                shipHasBeenPlaced();
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
        private bool isValidShip()
        {
            switch (chosenShip)
            {
                case FieldTypes.BB:
                    return !chosenShips[0];
                case FieldTypes.CV:
                    return !chosenShips[1];
                case FieldTypes.CA:
                    return !chosenShips[2];
                case FieldTypes.DD:
                    return !chosenShips[3];
                default:
                    return false;
            }
        }

        private void shipHasBeenPlaced()
        {
            switch (chosenShip)
            {
                case FieldTypes.BB:
                    chosenShips[0] = true;
                    break;
                case FieldTypes.CV:
                    chosenShips[1] = true;
                    break;
                case FieldTypes.CA:
                    chosenShips[2] = true;
                    break;
                case FieldTypes.DD:
                    chosenShips[3] = true;
                    break;
            }
        }
        #endregion
        #region utility
        private void fleetsReload()
        {
            foreach (Ship ship in allyFleet)
            {
                ship.reload();
            }
            foreach (Ship ship in enemyFleet)
            {
                ship.reload();
            }
        }

        private bool hasValidAttacks(Ship[] fleet)
        {
            foreach (Ship ship in fleet)
            {
                if (!ship.getDestroyed() && ship.getReloadCooldown() == 0)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
        #region attacking
        private Position[] chooseAttack(Ship[] fleet, Position position)
        {
            switch (chosenShip)
            {
                case FieldTypes.BB:
                    if (fleet[0].getReloadCooldown() == 0 && !fleet[0].getDestroyed())
                    {
                        fleet[0].shoot();
                        return fleet[0].attack(position, rotation);
                    }
                    break;

                case FieldTypes.CV:
                    if (fleet[1].getReloadCooldown() == 0 && !fleet[1].getDestroyed())
                    {
                        fleet[1].shoot();
                        return fleet[1].attack(position, rotation);
                    }
                    break;

                case FieldTypes.CA:
                    if (fleet[2].getReloadCooldown() == 0 && !fleet[2].getDestroyed())
                    {
                        fleet[2].shoot();
                        return fleet[2].attack(position, rotation);
                    }
                    break;

                case FieldTypes.DD:
                    if (fleet[3].getReloadCooldown() == 0 && !fleet[3].getDestroyed())
                    {
                        fleet[3].shoot();
                        return fleet[3].attack(position, rotation);
                    }
                    break;

                default:
                    return new Position[1] { new Position(-1, -1) };
            }
            return new Position[1] { new Position(-1, -1) };
        }
        public void battleRequest(Position position)
        {
            AiTurn = false;
            playersOutput = "";
            if (hasValidAttacks(allyFleet))
            {
                if (!playerAttacks(position))
                {
                    return;
                }
                if (hasValidAttacks(enemyFleet))
                {
                    if (playerHasWon)
                    {
                        return;
                    }
                    AiTurn = true;                  
                    aiAttacks();
                }
                fleetsReload();
            }
            if (!hasValidAttacks(allyFleet))
            {
                playersOutput += "\n We are reloading all of our ships!";
                while (!hasValidAttacks(allyFleet))
                {
                    if (playerHasWon)
                    {
                        return;
                    }
                    if (hasValidAttacks(enemyFleet))
                    {
                        AiTurn = true;
                        aiAttacks();
                    }

                    fleetsReload();
                }
            }
        }

        private bool playerAttacks(Position position)
        {
            Position[] area = chooseAttack(allyFleet, position);
            if (area[0].X == -1 && area[0].Y == -1)
            {
                return false;
            }
            bool result = XAttacks(area, enemysField, enemyFleet);
            if (checkFleetIfDestroyed(enemyFleet))
            {
                playersOutput = "Enemy fleet sunk! The victory is ours.";
                playerHasWon = true;
            }
            return true;
        }

        private bool aiAttacks()
        {
            bool prevRot = rotation;
            chosenShip = ai.chooseAttack(enemyFleet,playersField,allyFleet);
            if(chosenShip == FieldTypes.DD)
            {
                rotation = ai.getAiRotation();
            }
            Position[] area = chooseAttack(enemyFleet, ai.getPositionToShootAt());
            bool result = XAttacks(area, playersField, allyFleet);
            if (checkFleetIfDestroyed(allyFleet))
            {
                enemyOutput = "Ally fleet sunk! We will get them next time.";
                playerHasWon = true;
            }
            chosenShip = FieldTypes.SEA;
            rotation = prevRot;
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
            Report result;
            foreach (Ship ship in fleet)
            {
                result = ship.checkIfDestroyed();
                if (result.result)
                {
                    if (result.firstTime)
                    {
                        if (aiTurn)
                        {
                            enemyOutput = "Enemy forces have destroyed our " + ship.getType().ToString() + "!";
                        }
                        else
                        {
                            playersOutput = "We have destroyed enemy " + ship.getType().ToString() + "!";
                        }
                    }
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
                    result = caAttack(area[0], tempField, fleet);
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
        #endregion
        #region types of attack
        private bool singleTarget(Position position, Field[,] tempField, Ship[] fleet)
        {          
            ushort hitIndex;
            if (tempField[position.X, position.Y].type != FieldTypes.SEA)
            {             
                hitIndex = getHitIndex(tempField[position.X, position.Y].type);
                checkHits(position, hitIndex, fleet);
                tempField[position.X, position.Y].type = FieldTypes.FIRE;
                tempField[position.X, position.Y].spotted = true;
                return true;
            }
            else
            {              
                tempField[position.X, position.Y].spotted = true;
                return false;
            }

        }

        private bool caAttack(Position position,Field[,] tempField, Ship[] fleet)
        {
            bool result = false;
            if (singleTarget(position, tempField, fleet)) 
            {
                result = true;
            }
            caMessage(result,position, tempField);
            return result;
                
        }
        private bool bbAttack(Position[] area, Field[,] tempField, Ship[] fleet)
        {
            List<Position> hittedArea = new List<Position>();
            enemyOutput = "";
            foreach (Position position in area)
            {
                if (singleTarget(position, tempField, fleet))
                {
                    hittedArea.Add(position);                  
                }
            }
            bbMessage(hittedArea,tempField);
            return true;
        }
        private bool CVScout(Position[] area, Field[,] tempField)
        {
            foreach (Position pos in area)
            {
                tempField[pos.X, pos.Y].spotted = true;
            }
            cvMessage();
            return true;
        }

        private bool ddAttack(Position[] area, Field[,] tempField, Ship[] fleet)
        {
            Position hittedPosition = new Position(-1,-1);
            foreach (Position pos in area)
            {
                if (singleTarget(pos, tempField, fleet))
                {
                    hittedPosition = pos;
                    break;
                }
            }
            ddMessage(area, tempField, fleet, hittedPosition);
            return hittedPosition.X != -1;
        }

        #endregion
        #region messages
        private void caMessage(bool result, Position position, Field[,] tempField)
        {
            if (result)
            {
                if (aiTurn)
                {
                    enemyOutput = "They have hit our " + tempField[position.X, position.Y].type.ToString() + "at[" + position.X + "," + position.Y + "]!";
                }
                else
                {
                    playersOutput = "We have hit enemy warship at" + position.X + "," + position.Y + "!";
                }
            }
            else
            {
                if (aiTurn)
                {
                    enemyOutput = "Enemy missed their " + chosenShip.ToString() + "attack at [" + position.X + "," + position.Y + "]!";
                }
                else
                {
                    playersOutput = "We missed our cruiser salvo!";
                }
            }
        }
        private void bbMessage(List<Position> hittedArea, Field[,] tempField)
        {

            if (hittedArea.Count == 0)
            {
                if (aiTurn)
                {
                    enemyOutput = "The enemy missed their Battleship salvo!";
                }
                else
                {
                    playersOutput = "We missed our Battleship salvo!";
                }
                return;
            }
            if (AiTurn)
            {
                enemyOutput = "They hit our ";
            }
            else
            {
                playersOutput = "We have hit an enemy warship at";
            }


            foreach (Position position in hittedArea)
            {
                if (aiTurn)
                {
                    enemyOutput += tempField[position.X, position.Y].type.ToString() + "at" + position.X + "," + position.Y + " ";
                }
                else
                {
                    playersOutput += position.X + "," + position.Y + " ";
                }

            }
            if (aiTurn)
            {
                enemyOutput += "!";
            }
            else
            {
                playersOutput += "!";
            }


        }
        private void cvMessage()
        {

            if (aiTurn)
            {
                enemyOutput = "The enemy have scoutted our position!";
            }
            else
            {
                playersOutput = "We have scoutted enemy position.";
            }
        }
        private void ddMessage(Position[] area, Field[,] tempField, Ship[] fleet, Position hittedPosition)
        {
            if (hittedPosition.X != -1)
            {
                if (aiTurn)
                {
                    enemyOutput = "They torpedoed our " + tempField[hittedPosition.X, hittedPosition.Y].type.ToString() + "at[" + hittedPosition.X + "," + hittedPosition.Y + "]!";
                }
                else
                {
                    playersOutput = "We torpedoed enemy warship at [" + hittedPosition.X + "," + hittedPosition.Y + "]!";
                }
            }
            else
            {
                if (aiTurn)
                {
                    if (rotation)
                    {
                        enemyOutput = "Enemy missed torpedo on " + area[0].Y + "row!";
                    }
                    else
                    {
                        enemyOutput = "Enemy missed torpedo at " + area[0].X + "column!";
                    }

                }
                else
                {
                    playersOutput = "We missed our torpedo!";
                }
            }

        }
        #endregion
       
    }
}



