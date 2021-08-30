using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lodaky
{
    class AI
    {
        private Random rd = new Random();
        private FieldTypes[] priorities = new FieldTypes[4] { FieldTypes.CV, FieldTypes.DD, FieldTypes.BB, FieldTypes.CA };
        private Position positionToShootAt = new Position(-1, -1);
        public Position getRndPos()
        {
            Position pos = new Position(0, 0);
            pos.X = rd.Next(0, 10);
            pos.Y = rd.Next(0, 10);
            return pos;
        }
        public FieldTypes getShip()
        {
            return chooseShipByIndex(rd.Next(0, 4));
        }

        public FieldTypes chooseShipByIndex(int index)
        {
            switch (index)
            {
                case 0:
                    return FieldTypes.BB;
                case 1:
                    return FieldTypes.CV;
                case 2:
                    return FieldTypes.CA;
                case 3:
                    return FieldTypes.DD;
                default:
                    return FieldTypes.SEA;
            }
        }

        public List<Position>[] areThereAnyVisibleShips(Field[,] battlefield, Ship[] fleet)
        {
            List<Position>[] arrayOfLists = new List<Position>[2];
            List<Position> spottedShips = new List<Position>();
            List<Position> shipsOnFire = new List<Position>();
            for (int i = 0; i < Math.Sqrt(battlefield.Length); ++i)
            {
                for (int j = 0; j < Math.Sqrt(battlefield.Length); ++j)
                {
                    if (isFireaLiveShip(i, j, battlefield, fleet))
                    {
                        shipsOnFire.Add(new Position(i, j));
                    }

                    else if (battlefield[i, j].spotted && (battlefield[i, j].type != FieldTypes.SEA && battlefield[i, j].type != FieldTypes.FIRE))
                    {
                        spottedShips.Add(new Position(i, j));
                    }
                }
            }
            arrayOfLists[0] = shipsOnFire;
            arrayOfLists[1] = spottedShips;
            return arrayOfLists;
        }

        public bool isFireaLiveShip(int x, int y, Field[,] battlefield, Ship[] allyFleet)
        {
            if (battlefield[x, y].type != FieldTypes.FIRE)
            {
                return false;
            }
            foreach (Ship ship in allyFleet)
            {
                if (ship.getRotation())
                {
                    if (checkRotatedDestroyed(x, y, ship))
                    {
                        return false;
                    }
                }
                else
                {
                    if (checkUnrotatedDestroyed(x, y, ship))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private bool checkRotatedDestroyed(int x, int y, Ship ship)
        {
            for (int i = 0; i < ship.getLenght(); ++i)
            {
                if (ship.getType() == FieldTypes.CV)
                {

                    if (ship.getPosition().Y - i == y && ship.getPosition().X + 1 == x)
                    {
                        return ship.getDestroyed();

                    }
                    else if (ship.getPosition().Y - i == y)
                    {
                        return ship.getDestroyed();
                    }
                }
                else
                {
                    if (ship.getPosition().Y - i == y)
                    {
                        return ship.getDestroyed();
                    }
                }
            }
            return false;
        }
        private bool checkUnrotatedDestroyed(int x, int y, Ship ship)
        {
            for (int i = 0; i < ship.getLenght(); ++i)
            {
                if (ship.getType() == FieldTypes.CV)
                {
                    if (ship.getPosition().X + i == x && ship.getPosition().Y + 1 == y)
                    {
                        return ship.getDestroyed();
                    }
                    else if (ship.getPosition().X + i == x)
                    {
                        return ship.getDestroyed();
                    }
                }
                else
                {
                    if (ship.getPosition().X + i == x)
                    {
                        return ship.getDestroyed();
                    }
                }
            }
            return false;
        }
        public FieldTypes chooseAttack(Ship[] fleet, Field[,] battlefield, Ship[] allyFleet)
        {

            List<Position>[] priorityField = areThereAnyVisibleShips(battlefield, allyFleet);
            FieldTypes chosenShip = FieldTypes.SEA;

            if (priorityField[0].Count == 0 && priorityField[1].Count == 0)
            {
                return noVisibleShips(fleet, battlefield);
            }
            else
            {
                if (priorityField[0].Count == 0)
                {
                    return visibleShips(fleet, battlefield, priorityField);
                }
                else
                {
                    return shipsOnFire(fleet, battlefield, priorityField, allyFleet);
                }
            }
        }

        private FieldTypes visibleShips(Ship[] fleet, Field[,] battlefield, List<Position>[] priorityField)
        {
            positionToShootAt = priorityField[1][rd.Next(0, priorityField[1].Count())];
            Ship shipToChoose;
            FieldTypes chosenShip = FieldTypes.SEA;
            for (int i = 0; i < priorities.Length; ++i)
            {
                shipToChoose = rearengeIndexes(i, fleet);
                if (shipToChoose.getReloadCooldown() == 0 && !shipToChoose.getDestroyed())
                {
                    chosenShip = shipToChoose.getType();
                    return chosenShip;
                }
            }
            return chosenShip;
        }

        private FieldTypes shipsOnFire(Ship[] fleet, Field[,] battlefield, List<Position>[] priorityField, Ship[] allyFleet)
        {
            FieldTypes chosenShip = FieldTypes.SEA;
            Ship shipToChoose;
            Position positionToChoose = priorityField[0][rd.Next(0, priorityField[0].Count())];
            for (int i = 0; i < priorities.Length; ++i)
            {
                shipToChoose = rearengeIndexes(i, fleet);
                if (shipToChoose.getReloadCooldown() == 0 && !shipToChoose.getDestroyed())
                {
                    chosenShip = shipToChoose.getType();
                    positionToShootAt = getPositionAroundFire(positionToChoose, battlefield, allyFleet);
                    return chosenShip;
                }
            }
            return chosenShip;
        }
        private FieldTypes noVisibleShips(Ship[] fleet, Field[,] battlefield)
        {
            Ship shipToChoose;
            FieldTypes chosenShip = FieldTypes.SEA;
            for (int i = 0; i < priorities.Length; ++i)
            {
                shipToChoose = rearengeIndexes(i, fleet);
                if (shipToChoose.getReloadCooldown() == 0 && !shipToChoose.getDestroyed())
                {
                    chosenShip = shipToChoose.getType();
                    positionToShootAt = getRndPos();
                    return chosenShip;
                }
            }
            return chosenShip;
        }

        public Position getPositionToShootAt()
        {
            Position prewPos = positionToShootAt;
            positionToShootAt = new Position(-1, -1);
            return prewPos;
        }
        private Ship rearengeIndexes(int i, Ship[] fleet)
        {
            switch (i)
            {
                case 0:
                    return fleet[1];
                case 1:
                    return fleet[3];
                case 2:
                    return fleet[0];
                case 3:
                    return fleet[2];
                default:
                    return null;
            }
        }
        public bool getAiRotation()
        {
            return rd.Next(0, 2) == 1;
        }

        private Position fixedPosition(Position _position)
        {
            if (_position.X == 0)
            {
                _position.X = 1;
            }
            if (_position.Y == 0)
            {
                _position.Y = 1;
            }
            if (_position.X == 9)
            {
                _position.X = 8;
            }
            if (_position.Y == 9)
            {
                _position.Y = 8;
            }
            _position.X = _position.X - 1;
            _position.Y = _position.Y - 1;
            return _position;
        }
        private bool checkIfValidPosition(Position _position, Field[,] battlefield, Ship[] allyFleet)
        {
            if (battlefield[_position.X, _position.Y].type == FieldTypes.SEA && battlefield[_position.X, _position.Y].spotted)
            {
                return false;
            }
            if (battlefield[_position.X, _position.Y].type == FieldTypes.FIRE)
            {
                return false;
            }
            return true;
        }
        private Position getPositionAroundFire(Position _position, Field[,] battlefield, Ship[] allyFleet)
        {
            _position = fixedPosition(_position);
            Position currPos;
            List<Position> possiblePosition = new List<Position>();
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    currPos = new Position(_position.X + j, _position.Y + i);
                    if (checkIfValidPosition(currPos, battlefield, allyFleet))
                    {
                        possiblePosition.Add(currPos);
                    }
                }
            }
            if (possiblePosition.Count == 0)
            {
                return getRndPos();
            }
            return possiblePosition[rd.Next(0, possiblePosition.Count)];
        }
    }
}
