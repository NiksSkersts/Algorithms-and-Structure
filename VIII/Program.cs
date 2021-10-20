using System;
Random rnd = Random.Shared;
Btree t = new Btree();
for (int i = 0; i < 101; i++)
{
    t.Add(rnd.Next(0,1000));
}
bool on = true;
    int selection = 0;
    var myEnumMemberCount = Enum.GetNames(typeof(Buttons)).Length-1;
    IterateThroughButtons();
    while (on)
    {
        switch (Console.ReadKey().Key)
        {
            case ConsoleKey.UpArrow :
                selection++;
                CheckBounds();
                IterateThroughButtons();
                break;
            case ConsoleKey.DownArrow:
                selection--;
                CheckBounds();
                IterateThroughButtons();
                break;
            case ConsoleKey.Enter:
                SelectionExecute();
                break;
        }
    }

    void SelectionExecute()
    {
        switch (selection)
        {
            case 0:
                t.Print();
                break;
            case 1:
                Console.WriteLine("Skaitlis ko pievienot:");
                t.Add(Convert.ToInt32(Console.ReadLine()));
                break;
            case 2:
                Console.WriteLine("Skaitlis ko noņemt:");
                t.Remove(Convert.ToInt32(Console.ReadLine()));
                break;
            case 3:
                Console.WriteLine("Skaitlis ko atrast:");
                var j =t.Search(Convert.ToInt32(Console.ReadLine()));
                string rnode;
                string lnode;
                if (j== null)
                {
                    Console.WriteLine("Nav atrasts :(((");
                    break;
                }
                if (j.rightNode!=null)
                { 
                    rnode = j.rightNode?.number.ToString()?? "Empty";
                }
                else
                {
                    rnode = "Empty";
                }

                if (j.leftNode !=null)
                {
                    lnode = j.leftNode?.number.ToString() ?? "Empty";
                }
                else
                {
                    lnode = "Empty";
                }
                
                
                Console.WriteLine("Skaitlis: {0}" +
                                  "\t Kreisā puse: {1}" +
                                  "\t Labā puse: {2}",j.number.ToString(),lnode,rnode);
                break;
            case 4:
                t.Count();
                Console.WriteLine("Mezglu skaits kokā: {0}",t.count.ToString());
                break;
        }
        end:
        Console.WriteLine("Press any key");
        Console.ReadKey();
        IterateThroughButtons();
        
    }
    void CheckBounds()
    {
        if (selection > myEnumMemberCount)
        {
            selection = myEnumMemberCount;
        }else if (selection < 0)
        {
            selection = 0;
        }
    }
    void IterateThroughButtons()
    {
        Console.Clear();
        int i = 0;
        foreach (var button in Enum.GetNames(typeof(Buttons)))
        {
            if (selection==i)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            Console.WriteLine(button);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            i++;
        }
    }



internal enum Buttons
{
    Print,
    Add,
    Remove,
    Find,
    Walk

}
class Bnode
{
    public int number;
    public Bnode? rightNode;
    public Bnode? leftNode;

    public Bnode(int number)
    {
        this.number = number;
    }
    
}
    class Btree
    {
        public Bnode Root;
        public int count;
        private Bnode SearchPrevious;
        public void Add(int number)
        {
            if (Root == null)
            {
                Root = new Bnode(number);
                return;
            }
            Bnode previous = Root;
            Bnode current = Root;
            //bool left = false;
            while (current != null)
            {
                previous = current;
                current = number > current.number ? current.rightNode : current.leftNode;
            }
            Add(number, previous);
        }
        private void Print(Bnode n, string seperator)
        {
            if (n == null) return;
            Console.WriteLine("{0}{1}", seperator, n.number);
            Print(n.leftNode, "   " + seperator);
            Print(n.rightNode, "   " + seperator);
        }
        public void Print()
        {
            Print(Root, "->");
        }
        public Bnode Search(int number)
        {
            Bnode current = Root;
            Bnode previous = Root;
            while (current != null && current.number != number)
            {
                previous = current;
                current = number > current.number ? current.rightNode : current.leftNode;
            }
            SearchPrevious = previous;
            return current;
        }
        public void Remove(int number)
        {
            Bnode curr = Search(number);
            if (curr == null) return;
            bool left = number < SearchPrevious.number;

            //1.
            if (curr.rightNode == null)
            {
                if (left)
                {
                    SearchPrevious.leftNode = curr.leftNode;
                }
                else SearchPrevious.rightNode = curr.leftNode;
            }
            else if (curr.leftNode == null)
            {
                if (left)
                {
                    SearchPrevious.leftNode = curr.rightNode;
                }
                else SearchPrevious.rightNode = curr.rightNode;
            }
            //2.
            else if (curr.rightNode.leftNode == null)
            {
                if (left)
                {
                    curr.rightNode.leftNode = curr.leftNode;
                    SearchPrevious.leftNode = curr.rightNode;
                }
                else
                {
                    curr.rightNode.leftNode = curr.leftNode;
                    SearchPrevious.rightNode = curr.rightNode;
                }
            }
            else if (curr.rightNode.rightNode == null)
            {
                if (left)
                {
                    curr.leftNode.rightNode = curr.rightNode;
                    SearchPrevious.leftNode = curr.leftNode;
                }
                else
                {
                    curr.leftNode.rightNode = curr.rightNode;
                    SearchPrevious.rightNode = curr.leftNode;
                }
            }
            else
            {
                curr = curr.rightNode;
                SearchPrevious = curr;
                while (curr.leftNode != null)
                {
                    SearchPrevious = curr;
                    curr = curr.leftNode;
                }

                SearchPrevious.leftNode = curr.rightNode;
                curr.leftNode = Root.leftNode;
                curr.rightNode = Root.rightNode;
                Root = curr;
            }
        }
        public void Count()
        {
            count = 0;
            countnod(Root);
        }
        private void countnod(Bnode n)
        {
            if (n == null) return;
            count ++;
            countnod(n.leftNode);
            countnod(n.rightNode);
        }
        private void Add(int i, Bnode Parent)
        {
            if (i < Parent.number)
            {
                Parent.leftNode = new Bnode(i);
            }
            else
            {
                Parent.rightNode = new Bnode(i);
            }
        }
    }