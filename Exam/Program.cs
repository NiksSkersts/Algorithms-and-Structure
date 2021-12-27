#region Setup
sec_check:
Console.WriteLine("Ievadiet mezglu skaitu");
var rc = Convert.ToInt32(Console.ReadLine());

Console.WriteLine("Ievadiet virsotņu nosaukumus vērtības atdalot ar simbolu [;]?: ");
var vertices = Console.ReadLine();
var verticesList = vertices.Split(';');
if (verticesList.Length > rc) {
    Console.WriteLine("Ievadīts nepareizs virsotņu daudzums!");
    goto sec_check;
}
#endregion


#region Init Matrix
var Matrix = new int[rc, rc];
for (var i = 0; i < rc-1; i++) {
    for (var j = 0; j < rc-1; j++) {
        Matrix[i, j] = 0;
    }
}

#endregion

#region Output start Matrix

Console.WriteLine("Jūsu izveidotais grafs:");
OutputMatrix(Matrix,verticesList);


void OutputMatrix(int[,] matrix, string[] vertices) {
    for (int i = 0; i < vertices.Length+1; i++) {
        if (i==0) {
            Console.Write(" # ");
        }
        else {
            Console.Write($" {vertices[i-1]} ");
        }
    }

    for (int i = 0; i < matrix.GetLength(0); i++) {
        for (int j = -1; j < matrix.GetLength(1); j++) {
            if (j is -1) {
                Console.WriteLine();
                Console.Write($" {vertices[i]} ");
            }
            if(j is not -1) {
                Console.Write($" {matrix[i,j]} ");
            }
        }
    }
}

Console.WriteLine();
#endregion

#region Add to Matrix

Console.WriteLine("Ievadiet šķautnes formā NosaukumsNo->NosaukumsUz attdalot ar simbolu[;]?: ");
var addToMatrix = Console.ReadLine();
var splitAddTomatrix = addToMatrix.Split(';');
foreach (var addOne in splitAddTomatrix) {
    var vertTemp = addOne.Split("->");
    int a=0;
    int b=0;
    for (int i = 0; i < verticesList.Length; i++) {
        if (vertTemp[0].Equals(verticesList[i])) {
            a = i;
            for (int j = 0; j < verticesList.Length; j++) {
                if (vertTemp[1].Equals(verticesList[j])) {
                    b = j;
                    j = 6;
                }
            }

            i = 6;
        }

        if (a!=b) {
            Matrix[a, b] = 1;
        }
    }
    
}
OutputMatrix(Matrix,verticesList);
Console.WriteLine();


#endregion

#region DispTree

Console.WriteLine("Šis viss veido koku!");
Console.WriteLine("izmantots DFS algoritms");

int v, e;
int len = 0;
v = verticesList.Length;
bool[] visited = new bool[v];

for (int i = 0; i < v; i++)
{
    if (!visited[i]) {
        len = 0;
        DFS(Matrix, verticesList.Length, visited, i);
        Console.WriteLine();
    }
}
void DFS(int[,] edges, int v, bool[] visited, int si) {
    len += 1;
    for (int i = 0; i < len; i++) {
        Console.Write(" - ");
    }
    visited[si] = true;
    Console.Write('>');
    Console.Write(verticesList[si]);
    Console.WriteLine();
    
    for (int i = 0; i < v; i++)
    {
        if (i == si)
            continue;
        if (!visited[i] && edges[si, i] == 1)
        {
            DFS(edges, v, visited, i);
        }
    }
}
#endregion

Console.ReadLine();