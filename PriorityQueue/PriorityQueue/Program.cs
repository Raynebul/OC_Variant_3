using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Text;
using System.Linq;
using System.Timers;

//Rule1: Если приоритет(А) > Приоритет(Б), будет запущена задача А (Б не будет)
//Rule2: Если приоритет(А) = Приоритет(Б), АиБ запускаются с использованием RR
//Rule3: Когда задача поступает в систему, она помещается в очередь с наивысшим приоритетом.
//Rule4: После того как задача израсходовала выделенное ей время в текущей очереди 
//(независимо от того сколько раз она освобождала CPU) приоритет такой задачи понижается (она двигается вниз очереди).
//Rule5: По прошествии некоторого периода S перевести все задачи в системе в наивысшую очередь.

namespace PriorityQueue
{

    public class PriorityQueue
    {
        public List<Task1> queue;
        public int Count { get { return queue.Count; } }
        public int Quant { get; }
        public int Priority { get; set; }

        public PriorityQueue(int priority, int quant)
        {
            queue = new List<Task1>();
            Priority = priority;
            Quant = quant;
        }

        public PriorityQueue(int priority)
        {
            queue = new List<Task1>();
            Priority = priority;
        }

        public void Enqueue(Task1 x)
        {
            queue.Add(x);
        }
        public void Dequeue()
        {
            queue.Remove(queue[0]);
        }
    }

    public class Task1
    {
        public string Name; // имя задачи
        public int id;
        public int Priority = 3; // приоритет задачи. Изначально ставится максимальный в соответствии с алгоритмом.
        public int Time; // время выполнения
        public float Done;

        public Task1(string name, int time)
        {
            Name = name;
            Time = time;
        }

        public int Execution(int q)
        {
            Done += q;
            if (Done >= Time)
                Finish();
            else
                Priority -= 1;
            return 0;
        }
        public int Finish()
        {
            Priority = -1;
            Console.WriteLine("The execution of '"+ Name+ "' is completed!");
            return 0;
        }

        public override string ToString()
        {
            return  "name - " + Name + "| priority - " + Priority + "| Done - " + Done + "| Time - " + Time;
        }
    }

    //class Task
    //{
    //    public string name;
    //    public int priority = 0;
    //    public int time;
    //    public bool e = true;
    //    public float done = 0.0f;

    //    public Task(string name, int time)
    //    {
    //        this.name = name;
    //        this.time = time;
    //    }

    //    public void DoTask(int q)
    //    {
    //        this.done += q;
    //        if (this.done >= this.time) TaskDone();
    //        else this.priority += 1;

    //    }

    //    public void TaskDone()
    //    {
    //        this.priority = -1;
    //        Console.WriteLine("*** " + this.name + " done! ***");
    //    }

    //    public override string ToString()
    //    {
    //        return name + " " + priority + " " + done + "/" + time;
    //    }
    //}

    class Program
    {
        static void Main(string[] args)
        {
            PriorityQueue Q1 = new PriorityQueue(0); // Базовая очередь.
            PriorityQueue Q2 = new PriorityQueue(1, 9); // Верхняя очередь
            PriorityQueue Q3 = new PriorityQueue(2, 6);
            PriorityQueue Q4 = new PriorityQueue(3, 3); //очередь с самым высоким приоритетом. Верхняя очередь.

            List<PriorityQueue> list = new List<PriorityQueue>(); // несколько уровней очередей

            list.Add(Q4);
            list.Add(Q3);
            list.Add(Q2);
            list.Add(Q1);

            Console.WriteLine("Emulation of task manager");
            int S = int.Parse(Console.ReadLine());
            Console.WriteLine(S);
            Random rnd = new Random();
            for (int l = 1; l <= S; l++)
            {
                string id = "task " + l.ToString();            
                Task1 proc = new Task1(id, rnd.Next(1, 20));
                Console.WriteLine(proc.ToString());
                list[0].queue.Add(proc);
            }

            for (int k = 0; k < 3; k++)
            {
                Console.WriteLine("Queue Q" + (4 - k).ToString() + " -----------------------");
                while (list[k].queue.Count != 0)
                {
                    list[k].queue[0].Execution(list[k].Quant);
                    Console.WriteLine(list[k].queue[0].ToString());
                    if (list[k].queue[0].Priority == -1)
                    {
                        list[k].queue[0].Priority -= 1;
                        list[k].queue.Remove(list[k].queue[0]);
                    }
                    else
                    {
                        list[k + 1].queue.Add(list[k].queue[0]);
                        list[k].queue.Remove(list[k].queue[0]);
                    }
                }

            }
            if (list[3].queue.Count != 0)
            {
                Console.WriteLine("Queue Q" + 1.ToString() + " -----------------------");
                for (int k = 0; k < list[3].Count; k++)
                {
                    list[3].queue[0].Finish();
                    Console.WriteLine(list[3].queue[0].ToString());
                    list[3].queue.Remove(list[3].queue[0]);
                }
            }


            //    int q = 1;
            //    string name = "task 1";
            //    List<Task> queue = new List<Task>();
            //    queue.Add(new Task(name, 10));

            //    int i = 1;
            //    int f = 0;
            //    do
            //    {
            //        for (int j = 0; j < 2; j++)
            //        {
            //            queue.Sort(CompareByPriority);

            //            for (int t = 0; t < queue.Count; t++)
            //                if (queue.FindAll(x => x.priority == t).Count != 0)
            //                {
            //                    foreach (Task task in queue.FindAll(x => x.priority == t))
            //                    {
            //                        task.DoTask(q);
            //                        if (task.priority == -1) queue.Remove(task);
            //                    }
            //                    break;
            //                }

            //            foreach (Task task in queue) Console.WriteLine(task.ToString());

            //            if (i < 4)
            //            {
            //                i++;
            //                name = "task " + i.ToString();
            //                queue.Insert(0, new Task(name, 10));
            //            }
            //            Console.WriteLine();
            //        }
            //        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

            //        if (queue.Count != 0)
            //        {
            //            queue.Insert(0, queue[queue.Count - 1]);
            //            queue[0].priority = 0;
            //            queue.RemoveAt(queue.Count - 1);
            //        }
            //        else break;
            //        f++;
            //    } while (f < 20);
            //}

            //public static int CompareByPriority(Task task1, Task task2)
            //{
            //    return task1.priority.CompareTo(task2.priority);
            //}
        }
    }
}
