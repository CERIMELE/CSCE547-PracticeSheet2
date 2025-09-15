# Hello!

In this repo, I will provide **10 questions**. We will do **Q1–Q5 in class** during the live coding session, and **Q6–Q10 on your own** in the allotted time. The focus is **arrays**, **method overloading**, and **method overriding**. 

---

## Theme A (Q1–Q5): Student Grades (In-Class)

### 1) Array Basics

Create an `int[] grades` with 5 test scores. Print:

* The first element
* The last element using `^1`
* The length of the array

```csharp
int[] grades = {90, 85, 70, 100, 95};
Console.WriteLine($"First grade: {grades[0]}");
Console.WriteLine($"Last grade: {grades[^1]}");
Console.WriteLine($"Total grades: {grades.Length}");
```

---

### 2) Average Grade with **Overloading** + **AddScores Overloads**

Create a `GradeCalculator` class with two **overloaded** methods to calculate averages:

* `double Average(int[] scores)`
* `double Average(double[] scores)`
* `double Average(params int[] scores)`

Then create a `StudentRecord` that **overloads** `AddScores` in two ways:

* `void AddScores(int score)` → adds a single score to the first empty slot
* `void AddScores(params int[] scores)` → adds many scores at once (e.g., `AddScores(90,85,70)`)

Use index/range syntax in `PrintFirstLastAndSlice()` to print the first, the last (`^1`), and the last three (`[^3..]`).

```csharp
public static class GradeCalculator
{
    public static double Average(params int[] scores)
    {
        if (scores == null || scores.Length == 0) return 0;
        long sum = 0;
        for (int i = 0; i < scores.Length; i++)
        {
            sum += scores[i];
        }
        return (double)sum / scores.Length;
    }

    public static double Average(double[] scores)
    {
        if (scores == null || scores.Length == 0) return 0;
        double sum = 0;
        for (int i = 0; i < scores.Length; i++)
        {
            sum += scores[i];
        }
        return sum / scores.Length;
    }
}

public class StudentRecord
{
    public string Name { get; set; } = string.Empty;
    public int[] Scores { get; } = new int[5];
    private int _count;

    // Overload #1: add one
    public void AddScores(int score)
    {
        if (_count < Scores.Length) Scores[_count++] = score;
    }

    // Overload #2: add many (params packs args into an int[] for you)
    public void AddScores(params int[] scores)
    {
        for (int i = 0; i < scores.Length; i++)
        {
            AddScores(scores[i]);
        }
    }

    public void PrintFirstLastAndSlice()
    {
        Console.WriteLine($"First: {Scores[0]}");
        Console.WriteLine($"Last: {Scores[^1]}");
        Console.WriteLine("Last 3: " + string.Join(", ", Scores[^3..]));
    }

    public double Average()
    {
        // Binds to GradeCalculator.Average(params int[] scores)
        return GradeCalculator.Average(Scores);
    }
}

// Usage demo
var sr = new StudentRecord { Name = "Alex" };
sr.AddScores(90);                  // single
sr.AddScores(85, 70, 100, 95);     // many
sr.PrintFirstLastAndSlice();

// Both calls below work:
Console.WriteLine("Average (array): " + GradeCalculator.Average(sr.Scores));
Console.WriteLine("Average (varargs): " + GradeCalculator.Average(90, 85, 70));
```

> **What “overloading” means here:** Both methods are named `AddScores`, but their **parameter lists are different**. The compiler picks which to call based on your arguments (one value vs many values).

---

### 3) Copy and Sort Grades (Array APIs)

Use `Array.Copy` and `Array.Sort` to demonstrate copying and sorting arrays **without mutating the original**.

```csharp
int[] grades = {90, 85, 70, 100, 95};
int[] copied = new int[grades.Length];
Array.Copy(grades, copied, grades.Length);
int[] sorted = (int[])grades.Clone();
Array.Sort(sorted);

Console.WriteLine("Original: " + string.Join(", ", grades));
Console.WriteLine("Copied:   " + string.Join(", ", copied));
Console.WriteLine("Sorted:   " + string.Join(", ", sorted));
```

---

### 4) Student with **Overriding** (2D array average)

Create a `Student` class that stores a **2D** array of test scores and calculates an average over **all cells**. Override `ToString()` to display results. Then create `HonorsStudent : Student` that **overrides** `Average()` to **add +5** to the result (cap at 100).

```csharp
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
int[,] aliceScores = { { 80, 90, 85 }, { 70, 75, 95 } };
Student alice = new Student("Alice", aliceScores);
Student honorsBob = new HonorsStudent("Bob", new int[,] { { 90, 92 }, { 88, 97 } });

Console.WriteLine(alice);
Console.WriteLine(honorsBob);


```

---

### 5) Jagged Arrays with **Overloaded** Printers

Use jagged arrays for students with different test counts. Implement **overloaded** `PrintScores` methods.

```csharp
public static class ScorePrinter {
    public static void PrintScores(int[] scores) {
        Console.WriteLine(string.Join(", ", scores));
    }

    public static void PrintScores(int[][] allScores) {
        for (int i = 0; i < allScores.Length; i++) {
            Console.WriteLine($"Student {i+1}: {string.Join(", ", allScores[i])}");
        }
    }
}

int[][] jagged = {
    new int[] {90, 85, 70},
    new int[] {88, 92},
    new int[] {75, 80, 95, 100}
};

ScorePrinter.PrintScores(jagged[0]); // single student (overload 1)
ScorePrinter.PrintScores(jagged);    // all students (overload 2)
```

---

## Theme B (Q6–Q10): Employee Salaries (Independent Practice)

### 6) Employee Array and **Overloaded** Print  

Create an `Employee` class with `Name` and `Salary`. Store employees in an array. Overload `PrintEmployees` to:

* Print all employees
* Print only employees above a salary threshold

Add **overloaded** `PrintBonuses` to print bonuses for:

* a single employee (`int[]`)
* all employees (`int[][]`)

---

### 7) **Override ToString**

Override `ToString()` in `Employee` to return `"Name: Salary"`. Print employees using `foreach`.

---

### 8) Array.Copy Behavior 

Copy the employee array using `Array.Copy`. Change a salary in the original and observe the copy’s behavior (**reference type** effect).


---

### 9) Sorting Employees

Sort employees by `Salary` using `Array.Sort` with a custom comparer. Print sorted results.

---

### 10) Program Flow  

1. Print welcome message
2. Create employee array
3. **2D array:** Create an `int[,]` for *quarterly raises* `[employee, quarter]` and print the **average** raise for **Q1**
4. Print all employees (uses overridden `ToString`)
5. Print employees above threshold
6. Show copy vs original reference behavior (employees)
7. Show jagged shallow-copy note for bonuses
8. Print employees sorted by salary
9. **Jagged print:** Show bonuses for employee 0 with the overloaded method

**Use these inputs so your output matches exactly:**

**Employees**
- Alice — 60000  
- Bob — 45000  
- Carol — 80000  

**Quarterly Raises** (`int[,]` with rows = employees, cols = Q1..Q4)
```
{
  {150, 100, 200,  50},
  {200, 150, 180, 100},
  {151, 120, 160,  90}
}
```

**Bonuses** (`int[][]`)
```
[
  [2100, 1500],
  [500],
  [2500, 1750]
]
```

**Expected Output Example:**

```
Welcome to the Employee Salary System.
Average raise in Q1: 167
Name: Alice, Salary: 60000
Name: Bob, Salary: 45000
Name: Carol, Salary: 80000
Employees above 50000:
Alice 60000
Carol 80000
After modifying original:
Original[0] Salary=65000
Copy[0] Salary=65000  // reference effect
Jagged shallow-copy check: bonusesCopy[0][0]=2100
Sorted by salary:
Bob 45000
Alice 65000
Carol 80000
Bonuses (employee 0):
2100, 1500
```

# Hints (Q6–Q10)

## 1) Arrays & Indexing

* **1D:**

  * `a[^1]` → last element
  * `a[^3..]` → last three elements

```csharp
Console.WriteLine(a[^1]);
Console.WriteLine(string.Join(", ", a[^3..]));
```

* **2D:** use lengths, not magic numbers

```csharp
int rows = m.GetLength(0), cols = m.GetLength(1);
for (int r = 0; r < rows; r++)
    for (int c = 0; c < cols; c++)
        Console.Write($"{m[r,c]} ");
```

* **Jagged:** each row can differ—check before indexing

```csharp
for (int i = 0; i < j.Length; i++) {
    var row = j[i];
    if (row is null || row.Length == 0) continue;
    Console.WriteLine(string.Join(", ", row));
}
```

---

## 2) Copy, Clone, Sort

* `Array.Copy(src, dst, len)` → copy **into an existing** array.
* `(T[])src.Clone()` → **shallow** copy (new outer array, same references inside for ref types).
* To keep originals, **clone before** `Array.Sort`.

```csharp
var copy = new Employee[employees.Length];
Array.Copy(employees, copy, employees.Length);     // shallow (Employee refs shared)

var bonusesCopy = (int[][])bonuses.Clone();        // shallow (inner int[] shared)
var sorted = (Employee[])employees.Clone();        // clone, then sort
Array.Sort(sorted, (a,b) => a.Salary.CompareTo(b.Salary));
```

---

## 3) Overloading

Same method name, **different parameters**.

```csharp
void PrintEmployees(Employee[] emps);
void PrintEmployees(Employee[] emps, int minSalary);

void PrintBonuses(int[] bonuses);
void PrintBonuses(int[][] allBonuses);
```

---

## 4) Overriding

* Base marks `virtual`; derived uses `override`. You can call `base.Method()`.
* `ToString()` is `virtual` on `object` → safe to override anywhere.

```csharp
public class Employee {
    public string Name { get; set; } = "";
    public int Salary { get; set; }
    public override string ToString() => $"Name: {Name}, Salary: {Salary}";
}
```

```csharp
public class HonorsEmployee : Employee {
    public override string ToString() => base.ToString() + " (Honors)";
}
```

---

## 5) Printing & Formatting

* **1D:** `string.Join(", ", arr)`
* **2D:** loop rows/cols (or `foreach` all cells if order doesn’t matter)

```csharp
Console.WriteLine(string.Join(", ", oneD));
for (int r = 0; r < m.GetLength(0); r++) {
    for (int c = 0; c < m.GetLength(1); c++)
        Console.Write($"{m[r,c],5}");
    Console.WriteLine();
}
```
