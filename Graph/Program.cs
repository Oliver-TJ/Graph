using System;
using System.Linq;

namespace Graph
{ 
    internal class Program
    {
        private static void Main(string[] args)
        {
            var Town = new Graph("Bury St Edmunds", "Stowmarket", "Ipswitch", "Framlingham", "Wickham Market",
                "Woodbridge");
            Town.getConnections("Ipswitch");
        }
    }

    internal class Graph
    {
        private int[,] _graphNodes = new int[6, 6];
        private string[] _graphNames = new string[6];
        private int tempVariable;
        // need to add search function
        public Graph(string node1, string node2, string node3, string node4, string node5, string node6)
        {
            _graphNames[0] = node1;
            _graphNames[1] = node2;
            _graphNames[2] = node3;
            _graphNames[3] = node4;
            _graphNames[4] = node5;
            _graphNames[5] = node6;

            // Populate Graph
            createConnection("Bury St Edmunds", "Stowmarket", 25);
            createConnection("Bury St Edmunds", "Ipswitch", 45);
            createConnection("Bury St Edmunds", "Woodbridge", 56);
            createConnection("Bury St Edmunds", "Framlingham", 57);
            createConnection("Ipswitch", "Stowmarket", 21);
            createConnection("Ipswitch", "Framlingham", 31);
            createConnection("Ipswitch", "Woodbridge", 15);
            createConnection("Woodbridge", "Wickham Market", 9);
            createConnection("Framlingham", "Wickham Market", 9);
        }
        
        public void userChoice()
        {
            Console.WriteLine("Main Menu:");
            Console.WriteLine("1. Choose two places and find the shortest distance between them");
            Console.WriteLine("2. Choose one place and show all connections to it");
            string ans = Console.ReadLine();
            if (ans == "1")
                search();
            else if (ans == "2")
                getItemForDisplay();
            else
                Console.WriteLine("ERROR - Please enter 1 or 2 (run the program again)");
        }

        private void getItemForDisplay()
        {}
        
        

        private void search()
        {
            Console.WriteLine("Please enter the first town");
            string t1 = Console.ReadLine();
            Console.WriteLine("Please enter the second town");
            string t2 = Console.ReadLine();
            int lowest = 100; // larger than any in the list
            if (getLink(t1, t2) < lowest)
            {
                lowest = getLink(t1, t2);
            }
            for (var i = 0; i < _graphNames.Length; i++)
            {
                if (getLink(t1, i) < lowest)
                {
                    for (var g = 0; g < _graphNames.Length; i++)
                    {
                        
                    }
                }
                
            }
        }



        public void getConnections(string nodeName)
        {
            Console.WriteLine($"The town of {nodeName} has the following connections:");
            for (var i = 0; i < _graphNames.Length; i++)
            {
                if (getLink(nodeName, _graphNames[i]) != 0 &&
                    getLink(nodeName, _graphNames[i]) != -1) // assuming that no connections have a distance of 0
                {
                    Console.WriteLine($"{_graphNames[i]}:\t {_graphNodes[getIndex(nodeName), i]}");
                }
            }
        }

        private int getIndex(string nodeName)
        {
            for (var i = 0; i < _graphNames.Length; i++)
                if (nodeName == _graphNames[i])
                    return i;
            return -1; // Shows if the input is not in the array
        }

        private int getLink(string nodeName1, string nodeName2)
        {
            if (getIndex(nodeName1) == getIndex(nodeName2))
            {
                return -1;
            }
            else
            {
                tempVariable = _graphNodes[getIndex(nodeName1), getIndex(nodeName2)];
                return tempVariable;
            }
        }

        public void createConnection(string _node1, string _node2, int linkValue)
        {
            _graphNodes[getIndex(_node1), getIndex(_node2)] = linkValue;
            _graphNodes[getIndex(_node2), getIndex(_node1)] = linkValue;
        }
    }
}