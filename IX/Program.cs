using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

List<data> datas = new List<data>();

int randomNumber = 5000;
int arraySize = 100;
int stepSize = arraySize / 100;
Random random = Random.Shared;
int[] asc = new int[arraySize];
int[] desc = new int[arraySize];
int[] randomNumbers = new int[arraySize];
int[] uniquerand = new int[arraySize];
int[] aAsc = new int[arraySize];
int[] adesc = new int[arraySize];

Setup();
datas.Add(new data(
    "Bubble: ",
    BubbleSort(ref asc),
    BubbleSort(ref desc),
    BubbleSort(ref randomNumbers),
    BubbleSort(ref uniquerand),
    BubbleSort(ref aAsc),
    BubbleSort(ref adesc)
));
Setup();
datas.Add(new data(
    "Insertion: ",
    InsertionSort(ref asc),
    InsertionSort(ref desc),
    InsertionSort(ref randomNumbers),
    InsertionSort(ref uniquerand),
    InsertionSort(ref aAsc),
    InsertionSort(ref adesc)
));
Setup();
datas.Add(new data(
    "Shell: ",
    ShellSort(ref asc),
    ShellSort(ref desc),
    ShellSort(ref randomNumbers),
    ShellSort(ref uniquerand),
    ShellSort(ref aAsc),
    ShellSort(ref adesc)
));
Setup();
datas.Add(new data(
    "Quick: ",
    QuickSortIterative(ref asc),
    QuickSortIterative(ref desc),
    QuickSortIterative(ref randomNumbers),
    QuickSortIterative(ref uniquerand),
    QuickSortIterative(ref aAsc),
    QuickSortIterative(ref adesc)
));
Setup();
datas.Add(new data(
    "Heap: ",
    HeapSort(ref asc),
    HeapSort(ref desc),
    HeapSort(ref randomNumbers),
    HeapSort(ref uniquerand),
    HeapSort(ref aAsc),
    HeapSort(ref adesc)
));


var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
{
    HasHeaderRecord = false
};
using (var writer = new StreamWriter("C:\\Users\\skers\\Desktop\\results.csv"))
using (var csv = new CsvWriter(writer, csvConfig))
{
    csv.WriteField("Ascending");
    csv.WriteField("Descending");
    csv.WriteField("Rand");
    csv.WriteField("UniqueRand");
    csv.WriteField("Almost sorted rand ASC");
    csv.WriteField("Almost sorted rand Desc");
    csv.NextRecord();
    csv.WriteRecords(datas);
}
void Setup()
{
    
    //Gadījuma skaitļi
    FillArray(ref randomNumbers);

//Augošā secībā
    FillArray(ref asc);
    Sort(ref asc);

//Dilstošā secībā
    FillArray(ref desc);
    ReverseSort(ref desc);

//Unikāliem gadījuma skaitļiem
    FillArrayUnique(ref uniquerand);

//Gandrīz augoši unikāls
    FillArrayAAsc(ref aAsc);

//Gandrīz dilstoši unikāls
    FillArrayADesc(ref adesc);

}
void FillArrayAAsc(ref int[] a)
{
    FillArray(ref a);
    Sort(ref a);
    for (int i = 0; i < a.Length-1; i +=stepSize)
    {
        a[i] = random.Next(randomNumber);
    }
}
void FillArrayADesc(ref int[] a)
{
    FillArray(ref a);
    ReverseSort(ref a);
    for (int i = 0; i < a.Length-1; i +=stepSize)
    {
        a[i] = random.Next(randomNumber);
    }
}

void FillArray(ref int[] arr)
{
    for (var i = 0; i < arr.Length; i++)
    {
        arr[i] = random.Next(randomNumber);
    }
}

void FillArrayUnique(ref int[] arr)
{
    for (var i = 0; i < arr.Length; i++)
    {
        
        var number = random.Next(randomNumber);
        sec:
        if (arr.Contains(number))
        {
            number = random.Next(randomNumber);
            goto sec;
        }
        arr[i] = number;

    }
}
static int Sort(ref int[] arr)
{
    int counter = 0;
    for (int i = 0; i < arr.Length; i++)
    {
        int index = arr[i];
        int j = i;
        counter+=2;
        while (j > 0 && arr[j-1] > index)
        {
            arr[j] = arr[j - 1];
            j -= 1;
            counter += 2;
        }
        arr[j] = index;
        counter++;
    }

    return counter;
}
static int ReverseSort(ref int[] arr)
{
    int counter = 0;
    for (int i = 0; i < arr.Length; i++)
    {
        int index = arr[i];
        int j = i;
        counter+=2;
        while (j > 0 && arr[j - 1] < index)
        {
            arr[j] = arr[j - 1];
            j -= 1;
            counter += 2;
        }
        arr[j] = index;
        counter++;
    }

    return counter;
}

void Print(ref int[] arr)
{
    foreach (var x in arr)
    {
        Console.Write(" {0} ",x.ToString());
    }
}

int BubbleSort(ref int[] input)
{
    int counter = 0;
    bool itemMoved;
    do
    {
        itemMoved = false;
        for (int i = 0; i < input.Count() - 1; i++)
        {
            if (input[i] <= input[i + 1]) continue;
            (input[i + 1], input[i]) = (input[i], input[i + 1]);
            itemMoved = true;
            counter += 3;
        }
    } while (itemMoved);

    return counter;
}
int InsertionSort(ref int[] input)
{
    int counter = 0;
    for (int i = 0; i < input.Length; i++)
    {
        var item = input[i];
        var currentIndex = i;
        counter += 2;
        
        while (currentIndex > 0 && input[currentIndex - 1] > item)
        {
            input[currentIndex] = input[currentIndex - 1];
            currentIndex--;
            counter += 2;
        }

        input[currentIndex] = item;
        counter++;
    }

    return counter;
}
int ShellSort(ref int[] data)
{
    int hSort = 1;
    int counter = 0;

    while (hSort < data.Length / 3)
    {
        hSort = (3 * hSort) + 1;
        counter++;
    }
        

    while (hSort >= 1)
    {
        for (int i = hSort; i < data.Length; i++)
        {
            for (int a = i; a >= hSort && (data[a] < data[a - hSort]); a -= hSort)
            {
                data[a] ^= data[a - hSort];
                data[a - hSort] ^= data[a];
                data[a] ^= data[a - hSort];
                counter += 3;
            }
        }

        hSort /= 3;
        counter++;
    }

    return counter;
}


int QuickSortIterative(ref int[] data)
{
    int startIndex = 0;
    int endIndex = data.Length - 1;
    int top = -1;
    int counter = 0;
    int[] stack = new int[data.Length];
    stack[++top] = startIndex;
    stack[++top] = endIndex;
    counter += 2;
    while (top >= 0)
    {
        endIndex = stack[top--];
        startIndex = stack[top--];
        counter += 2;
        int p = Partition(ref data, startIndex, endIndex,ref counter);

        if (p - 1 > startIndex)
        {
            stack[++top] = startIndex;
            stack[++top] = p - 1;
            counter += 2;
        }

        if (p + 1 < endIndex)
        {
            stack[++top] = p + 1;
            stack[++top] = endIndex;
            counter += 2;
        }
    }

    return counter;
}

int Partition(ref int[] data, int left, int right, ref int counter)
{
    int x = data[right];
    int i = (left - 1);
    counter += 2;

    for (int j = left; j <= right - 1; ++j)
    {
        if (data[j] > x) continue;
        ++i;
        Swap(ref data[i], ref data[j]);
        counter ++;
    }

    Swap(ref data[i + 1], ref data[right]);
    counter ++;

    return (i + 1);
}

void Swap(ref int a, ref int b)
{
    (a, b) = (b, a);
}



int HeapSort(ref int[] data)
{
    int heapSize = data.Length;
    int counter = 0;

    for (int p = (heapSize - 1) / 2; p >= 0; --p)
    {
        MaxHeapify(ref data, heapSize, p,ref counter);
    }
        

    for (int i = data.Length - 1; i > 0; --i)
    {
        (data[i], data[0]) = (data[0], data[i]);
        --heapSize;
        counter += 2;
        MaxHeapify(ref data, heapSize, 0,ref counter);
    }

    return counter;
}

void MaxHeapify(ref int[] data, int heapSize, int index, ref int counter)
{
    int left = (index + 1) * 2 - 1;
    int right = (index + 1) * 2;
    int largest = 0;
    counter += 2;
    if (left < heapSize && data[left] > data[index])
    {
        largest = left;
        counter++;
    }
    else
    {
        largest = index;
        counter++;
    }
        

    if (right < heapSize && data[right] > data[largest])
    {
        largest = right;
        counter++;
    }

    if (largest != index)
    {
        (data[index], data[largest]) = (data[largest], data[index]);
        counter++;

        MaxHeapify(ref data, heapSize, largest,ref counter);
    }
}

public class data
{
    public data(string rheader,int one, int two, int three, int four, int five, int six)
    {
        Rheader = rheader;
        One = one;
        Two = two;
        Three = three;
        Four = four;
        Five = five;
        Six = six;
    }
    public string Rheader { get; set; }
    public int One { get; set; }
    public int Two { get; set; }
    public int Three { get; set; }
    public int Four { get; set; }
    public int Five { get; set; }
    public int Six { get; set; }
}

