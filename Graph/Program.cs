using System;
using System.Linq;

namespace Graph
{ 
    internal class Program
    {
        private static void Main(string[] args)
        {
            var town = new Graph("Bury St Edmunds", "Stowmarket", "Ipswitch", "Framlingham", "Wickham Market",
                "Woodbridge");
            town.UserChoice();
            // Town.getConnections("Ipswitch");
        }
    }

    internal class Graph
    {
        private int[,] _graphNodes = new int[6, 6];
        private string[] _graphNames = new string[6];
        private int _tempVariable;
        // -- need to add search function --
        public Graph(string node1, string node2, string node3, string node4, string node5, string node6)
        {
            _graphNames[0] = node1;
            _graphNames[1] = node2;
            _graphNames[2] = node3;
            _graphNames[3] = node4;
            _graphNames[4] = node5;
            _graphNames[5] = node6;

            // Populate Graph
            CreateConnection("Bury St Edmunds", "Stowmarket", 25);
            CreateConnection("Bury St Edmunds", "Ipswitch", 45);
            CreateConnection("Bury St Edmunds", "Woodbridge", 56);
            CreateConnection("Bury St Edmunds", "Framlingham", 57);
            CreateConnection("Ipswitch", "Stowmarket", 21);
            CreateConnection("Ipswitch", "Framlingham", 31);
            CreateConnection("Ipswitch", "Woodbridge", 15);
            CreateConnection("Woodbridge", "Wickham Market", 9);
            CreateConnection("Framlingham", "Wickham Market", 9);
        }
        
        public void UserChoice()
        {
            Console.WriteLine("Main Menu:");
            Console.WriteLine("1. Choose two places and find the shortest distance between them");
            Console.WriteLine("2. Choose one place and show all connections to it");
            var ans = Console.ReadLine();
            if (ans == "1")
                Search();
            else if (ans == "2")
                GetItemForDisplay();
            else
                Console.WriteLine("ERROR - Please enter 1 or 2 (run the program again)");
        }

        private void GetItemForDisplay()
        {
            Console.WriteLine("Enter the town:  ");
            string townEntry = Console.ReadLine();
            GetConnections(townEntry);
        }



        private void Search() // Searches for the fastest route between two stations
        {
            Console.WriteLine("Please enter the first town");
            string t1 = Console.ReadLine();
            Console.WriteLine("Please enter the second town");
            string t2 = Console.ReadLine();
            var lowest = 100; // larger than any in the list
            var connections = 0;
            var c1 = "h"; // Throws an error if they are not changed
                             // -- need to fix --
            var c2 = "g";
            
            if (GetLink(t1, t2) < lowest && GetLink(t1, t2) != -1)
            {
                lowest = GetLink(t1, t2);
                connections = 0;
            }

            for (var i = 0; i < _graphNames.Length; i++)
            {
                if (GetLink(t1, _graphNames[i]) < lowest && GetLink(t1, _graphNames[i]) != -1)
                {
                    c1 = _graphNames[i];
                    connections = 1;
                    for (var g = 0; g < _graphNames.Length; i++)
                    {
                        if (GetLink(_graphNames[g], t2) != -1 &&
                            GetLink(t1, _graphNames[i]) + GetLink(_graphNames[g], t2) < lowest)
                        {
                            lowest = GetLink(_graphNames[i], t2) + GetLink(_graphNames[i], t2);
                            connections = 2;
                            c2 = _graphNames[g];
                            break;
                        }
                    }
                }
            

                if (connections == 1)
                {
                    Console.WriteLine($"The Shortest route from {t1} to {t2} are as follows:");
                    Console.WriteLine($"Get the train from {t1} to {c1} which takes {GetLink(t1, c1)}");
                    Console.WriteLine($"Then get the train from {c1} to {t2} which takes {GetLink(c1, t2)}");
                    Console.WriteLine($"The overall time is {GetLink(t1, c1) + GetLink(c1, t2)}");
                    break;
                }
                if (connections == 2)
                {
                    Console.WriteLine($"The Shortest route from {t1} to {t2} are as follows:");
                    Console.WriteLine($"Get the train from {t1} to {c1} which takes {GetLink(t1, c1)}");
                    Console.WriteLine($"Then get the train from {c1} to {c2} which takes {GetLink(c1, c2)}");
                    Console.WriteLine($"The overall time is {GetLink(t1, c1) + GetLink(c1, c2) + GetLink(c2, t2)}");
                    break;
                }

                if (connections == 0)
                {
                    Console.WriteLine($"The Shortest route from {t1} to {t2} are as follows:");
                    Console.WriteLine($"Get the train from {t1} to {t2} which takes {GetLink(t1, t2)} minutes in total");
                    break;
                }
            }
        }

        



        public void GetConnections(string nodeName)
        {
            Console.WriteLine($"The town of {nodeName} has the following connections:");
            for (var i = 0; i < _graphNames.Length; i++)
                if (GetLink(nodeName, _graphNames[i]) != 0 &&
                    GetLink(nodeName, _graphNames[i]) != -1) // assuming that no connections have a distance of 0
                    Console.WriteLine($"{_graphNames[i]}:\t {_graphNodes[GetIndex(nodeName), i]}");
        }

        private int GetIndex(string nodeName)
        {
            for (var i = 0; i < _graphNames.Length; i++)
                if (nodeName == _graphNames[i])
                    return i;
            return -1; // Shows if the input is not in the array
        }

        private int GetLink(string nodeName1, string nodeName2)
        {
            if (GetIndex(nodeName1) == GetIndex(nodeName2))
            {
                return -1;
            }
            _tempVariable = _graphNodes[GetIndex(nodeName1), GetIndex(nodeName2)];
            return _tempVariable;
        }

        public void CreateConnection(string node1, string node2, int linkValue)
        {
            _graphNodes[GetIndex(node1), GetIndex(node2)] = linkValue;
            _graphNodes[GetIndex(node2), GetIndex(node1)] = linkValue;
        }
    }
}