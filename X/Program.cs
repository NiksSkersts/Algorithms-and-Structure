using System;
using System.Globalization;
using System.Linq;

Random random = Random.Shared;
int arraysize = 10;
int iterate = 0;
TimeSpan?[] ts = new TimeSpan?[arraysize];
DateTime?[] dt = new DateTime?[arraysize];


var Gcal = new GregorianCalendar();
var Hcal = new HebrewCalendar();
var Jcal = new JulianCalendar();
var Ccal = new ChineseLunisolarCalendar();
var Kcal = new KoreanCalendar();
var Pcal = new PersianCalendar();
var Ucal = new UmAlQuraCalendar();

var on = true;
var selection = 0;
var myEnumMemberCount = Enum.GetNames(typeof(Buttons)).Length - 1;
IterateThroughButtons();
while (on)
    switch (Console.ReadKey().Key)
    {
        case ConsoleKey.UpArrow:
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

void Print(TimeSpan? ts)
{
    if (!ts.HasValue) return;
    Console.WriteLine("Timespan: {0}," +
                      "\n\t Days: {1}," +
                      "\n\t Hours: {2}," +
                      "\n\t Minutes: {3}," +
                      "\n\t Seconds: {4}," +
                      "\n\t Milliseconds: {5}",
            ts,ts.Value.Days,ts.Value.Hours,ts.Value.Minutes,ts.Value.Seconds,ts.Value.Milliseconds);

}
void Init()
{
    if (iterate<arraysize)
    {
        ts[iterate] = new TimeSpan(random.NextInt64(630227520000000000,637703712000000000));
        dt[iterate] = new DateTime(random.NextInt64(630227520000000000,637703712000000000));
        iterate++;
    }
    else
    {
        iterate = 0;
    }
}
void PrintCalendar(Calendar calendar,DateTime dateTime)
{
    var calendarname = calendar.ToString().Split('.').LastOrDefault();
    Console.WriteLine();
    try
    {
        Console.WriteLine("Kalendārs: {0}," +
                          "\n\t Era: {1}" +
                          "\n\t Year: {2}" +
                          "\n\t Month: {3}" +
                          "\n\t Day of the year: {4}" +
                          "\n\t Day of the month: {5}" +
                          "\n\t Day of the week: {6}" +
                          "\n\t Hour: {7}" +
                          "\n\t Minute: {8}" +
                          "\n\t Seconds: {9}" +
                          "\n\t Milliseconds: {10}" +
                          "\n\t\t Warning!: Min supported DT: {11}, Max supported DT: {12}" +
                          "\n\t\t Is Leap Year: {13}",
            calendarname,
            calendar.GetEra(dateTime),
            calendar.GetYear(dateTime),
            calendar.GetMonth(dateTime),
            calendar.GetDayOfYear(dateTime),
            calendar.GetDayOfMonth(dateTime),
            calendar.GetDayOfWeek(dateTime),
            calendar.GetHour(dateTime),
            calendar.GetMinute(dateTime),
            calendar.GetSecond(dateTime),
            calendar.GetMilliseconds(dateTime),
            calendar.MinSupportedDateTime,
            calendar.MaxSupportedDateTime,
            calendar.IsLeapYear(dateTime.Year)
        );
    }
    catch (Exception e)
    {
        Console.WriteLine("Calendar {0} does not support this date {1}",calendarname,dateTime.ToString());
    }
    Console.WriteLine();
}
void SelectionExecute()
{
    switch (selection)
    {
        case 0:
            Init();
            break;
        case 1:
            for (int i = 0; i < arraysize; i++)
            {
                Print(ts[i]);
            }
            break;
        case 2:
            var timeout = 0;
            sec:
            var temp = dt[random.Next(0, 9)];
            if (timeout > 100)
            {
                Console.WriteLine("Something's Wrong! :((");
                break;
            }
            timeout++;
            if (temp == null) goto sec;
            PrintCalendar(Gcal,(DateTime) temp);
            PrintCalendar(Hcal,(DateTime) temp);
            PrintCalendar(Jcal,(DateTime) temp);
            PrintCalendar(Ccal,(DateTime) temp);
            PrintCalendar(Pcal,(DateTime) temp);
            PrintCalendar(Kcal,(DateTime) temp);
            PrintCalendar(Ucal,(DateTime) temp);
            break;
        case 3:
            break;
        case 4:
            break;
    }

    Console.WriteLine("Press any key");
    Console.ReadKey();
    IterateThroughButtons();
}
void CheckBounds()
{
    if (selection > myEnumMemberCount)
        selection = myEnumMemberCount;
    else if (selection < 0) selection = 0;
}
void IterateThroughButtons()
{
    Console.Clear();
    var i = 0;
    foreach (var button in Enum.GetNames(typeof(Buttons)))
    {
        if (selection == i)
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
    Init,
    Print,
    PrintCalendar
}