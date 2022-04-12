using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace task_3
{
      public class Key
      {
              byte[] key;

              public Key(byte[] b)
              {
                  key = b;
              }

            public  byte[] GetKey()
            {
                return key;
            }

      }
        public class Hmac
    {
        byte[] hash;
        HMACSHA256 h;


        public Hmac(byte[] key)
        {
            h = new HMACSHA256(key);
        }

        internal object ComputeHash(byte[] vs)
        {
            hash = h.ComputeHash(vs);
            return hash;
        }
        public  string ToString()
        {
            return BitConverter.ToString(hash);
        }
    }

        public class Table
    {
        public int Tsize;
        string[][] rulesTable;
        public Table(string[] str, int size)
        {
            Tsize = size + 1;
            int half = size / 2;
            rulesTable = new string[size + 1][];
            for(int i = 0; i < size + 1; ++i)
            {
                rulesTable[i] = new string[size + 1];
            }
            for (int i = 1; i <= size; ++i)
            {
                rulesTable[0][i] = str[i-1];
                rulesTable[i][0] = str[i-1];

            }
            
            rulesTable[0][0] = "Your turn:";
            for (int i = 1; i <= size; ++i)
            {
                for(int t = 1; t <= size; ++t)
                {
                    if(i == t)
                    {
                        rulesTable[i][t] = "Draw";
                    }
                    else if((i + half >= str.Length && i > t && i - half - 1 < t) ||
                    (i + half < str.Length && (t < i || t > i + half)))
                    {
                        rulesTable[i][t] = "Win";
                    }
                    else
                    {
                        rulesTable[i][t] = "Lose";

                    }
                }
            }

        }
        public void PrintTable()
        {
            for(int i = 0; i < Tsize; ++i)
            {
                for(int t = 0; t < Tsize; ++t)
                {
                    Console.Write(rulesTable[i][t] + " ");
                }
                Console.WriteLine();
            }
        }

    }
        class Program
    {
        static void Main(string[] args)
        {
            //string[] variants = { "Камень", "Ножницы", "Бумага" };
            Key key = new Key(new byte[128]);


            var set = new HashSet<string>();
            foreach (var item in args)
            {
                if (!set.Add(item))
                {
                    Console.WriteLine("Wrong parametrs! There should be no duplicate elements!");
                    return;
                }
            }
            if (args.Length < 3)
            {
                Console.WriteLine("Wrong number of parametrs! Number of parametrs must be more than 3!");
                return;
            }
            if (args.Length % 2 == 0)
            {
                Console.WriteLine("Wrong number of parametrs! Number of parametrs must be odd!");
                return;
            }
            var pc = new byte[4];
          
            
            

            int player = 0;
            while (true)
            {
                var gen = RandomNumberGenerator.Create();
                gen.GetBytes(key.GetKey());
                gen.GetBytes(pc);
                

                var ipc = BitConverter.ToUInt32(pc, 0) % (args.Length); //make a turn
                Hmac hmac = new Hmac(key.GetKey());
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(args[ipc])); //count hmac

                Console.WriteLine("Computer make a move\nHMAC : " + hmac.ToString());
                Console.WriteLine("Your turn:\n0 : Exit");

                for (int i = 0; i < args.Length; i++)
                {
                    Console.WriteLine((i + 1) + " : " + args[i]);
                }
                 Console.WriteLine("? : Help");

                string temp = Console.ReadLine();
                if(temp == "?")
                {
                    Table tab = new Table(args, args.Length);
                    tab.PrintTable();
                    Console.WriteLine();
                    continue;
                }
                if (!int.TryParse(temp, out player) || player < 0 || player > args.Length)
                {
                    Console.WriteLine("Incorrect data");
                    continue;
                }
                else if (player == 0) { 
                    return; 
                }

                player -= 1;
                var check =( args.Length - 1) / 2;
                
                if (ipc == player)
                {
                   
                    Console.WriteLine("Turn of computer:");
                    Console.WriteLine(args[ipc]);
                    Console.WriteLine("It's a draw!");
                }
                else if ((player+check>=args.Length && player>ipc && player-check - 1 <ipc) || 
                    (player + check < args.Length && (ipc<player || ipc>player+check)))
                {
                    
                    Console.WriteLine("Turn of computer:");
                    Console.WriteLine(args[ipc]);
                    Console.WriteLine("You win!");
                }
                else
                {
                    
                    Console.WriteLine("Turn of computer:");
                    Console.WriteLine(args[ipc]);
                    Console.WriteLine("You lose!");
                }
                Console.WriteLine("Key : " + (BitConverter.ToUInt32(key.GetKey(), 0)));
                
            }
        }
        

    }
}
