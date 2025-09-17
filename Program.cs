using System.Security.Cryptography.X509Certificates;

Console.WriteLine("Hello, World!");



var sr = new StudentRecord("Alex", 5);
System.Console.WriteLine(sr.Name);
sr.AddScores(90);
sr.AddScores(85, 78, 150, 95);
sr.PrintFirstLastAndSlice();

Console.WriteLine($"Average (array) : " + GradeCalculator.Average(sr.Scores));
Console.WriteLine("Average (varargs): " + GradeCalculator.Average(90, 85, 78));

int[] grades = { 90, 85, 78, 150, 95 };
int[] copy = new int[grades.Length];
Array.Copy(grades, copy, grades.Length);
int[] sorted = (int[])grades.Clone();
Array.Sort(sorted);

Console.WriteLine("Original : " + string.Join(", ", grades));
Console.WriteLine("Copy     : " + string.Join(", ", copy));
Console.WriteLine("Sorted   : " + string.Join(", ", sorted));

int[,] aliceScores = { { 80, 90, 85 }, { 70, 75, 95 } };
Student alice = new Student("Alice", aliceScores);
Student honorsBob = new HonorsStudent("Bob", new int[,] { { 90, 92 }, { 88, 97 } });

Console.WriteLine(alice);
Console.WriteLine(honorsBob);

int[][] jagged = {
    new int[] {90, 85, 70},
    new int[] {88, 92},
    new int[] {75, 80, 95, 100}
};

ScorePrinter.PrintScores(jagged[0]); // single student (overload 1)
ScorePrinter.PrintScores(jagged);    // all students (overload 2)
static class GradeCalculator
{
    public static double Average(params int[] scores)
    {
        if (scores == null || scores.Length == 0)
        {
            return 0;
        }
        long sum = 0;
        for (int i = 0; i < scores.Length; i++)
        {
            sum += scores[i];
        }
        return (double)sum / scores.Length;
    }

    public static double Average(double[] scores)
    {
        if (scores == null || scores.Length == 0)
        {
            return 0;
        }
        double sum = 0;
        for (int i = 0; i < scores.Length; i++)
        {
            sum += scores[i];
        }
        return sum / scores.Length;
    }
}

class StudentRecord
{
    public string Name { get; set; }

    public int[] Scores { get; set; }

    private int _count;

    public StudentRecord(string name, int size)
    {
        Name = name;
        Scores = new int[size];
        _count = 0;
    }

    public void AddScores(int score)
    {
        if (_count < Scores.Length)
        {
            Console.WriteLine(_count);
            Scores[_count] = score;
            _count += 1;
        }
    }

    public void AddScores(params int[] scores)
    {
        for (int i = 0; i < scores.Length; i++)
        {
           Console.WriteLine(scores[i]);
            AddScores(scores[i]);
        }
    }

    public void PrintFirstLastAndSlice()
    {
        Console.WriteLine($"First: {Scores[0]}");
        Console.WriteLine($"Last: {Scores[^1]}");
        Console.WriteLine($"Slice: {"Last 3: " + string.Join(", ", Scores[^3..])}");
    }

    public double Average()
    {
        return GradeCalculator.Average(Scores);
    }
}

public class Student
{
    public string Name { get; set; }
    private readonly int[,] _scores; // 2D rectangular array

    public Student(string name, int[,] scores)
    {
        Name = name ?? string.Empty;
        _scores = scores ?? new int[0, 0];
    }

    public virtual double Average()
    {
        int total = 0, count = 0;
        foreach (int s in _scores)
        {
            total += s;
            count++;
        }
        return count == 0 ? 0.0 : (double)total / count;
    }

    public override string ToString() => $"{Name}: Average = {Average():F2}";
}

public class HonorsStudent : Student
{
    public HonorsStudent(string name, int[,] scores) : base(name, scores) { }

    public override double Average()
    {
        var baseAvg = base.Average();
        return Math.Min(100.0, baseAvg + 5.0);
    }
}

// Demo


public static class ScorePrinter
{
    public static void PrintScores(int[] scores)
    {
        Console.WriteLine(string.Join(", ", scores));
    }

    public static void PrintScores(int[][] allScores)
    {
        for (int i = 0; i < allScores.Length; i++)
        {
            Console.WriteLine($"Student {i + 1}: {string.Join(", ", allScores[i])}");
        }
    }
}

