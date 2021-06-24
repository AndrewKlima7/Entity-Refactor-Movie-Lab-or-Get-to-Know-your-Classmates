using System;
using System.Collections.Generic;
using System.Linq;
using CRUD_Get_to_Know_You_Lab.Models;

namespace CRUD_Get_to_Know_You_Lab
{
    class Program
    {
        static void Main(string[] args)
        {
            bool startOver = true;
            while (startOver == true)
            {
                GCClass2021Context gc = new GCClass2021Context();
                List<Student> students = gc.Students.ToList();
                bool again = true;
                while (again == true)
                {
                    Console.WriteLine("Welcome\n");
                    PrintClassIdandName(students, gc);
                    Console.WriteLine("\nPlease select a student by either ID or Name");
                    Student s = GetInput(gc);
                    if (s == null)
                    {
                        Console.WriteLine("That is not a current student (PRESS ENTER)");
                        Console.ReadKey();
                        Console.Clear();
                        again = true;
                    }
                    else
                    {
                        GetOutput(s);
                        again = GoAgain();
                    }
                }
                Console.Clear();
                bool update = true;
                while (update == true)
                {
                    Console.WriteLine("Would you like to update a student? Y/N");
                    string input = Console.ReadLine();

                    if (input.ToUpper() == "Y" || input.ToUpper() == "YES")
                    {
                        bool another2 = true;
                        while (another2 == true)
                        {
                            Console.Clear();
                            PrintClassIdandName(students, gc);
                            Console.WriteLine("Please select a student to update");
                            Student s = GetInput(gc);
                            if (s == null)
                            {
                                Console.WriteLine("That is not a current student (PRESS ENTER)");
                                Console.ReadKey();
                                Console.Clear();
                                update = true;
                            }
                            else
                            {
                                UpdateWhat(s, gc);
                                another2 = GoAgain();
                                update = false;
                            }
                        }
                        update = false;
                    }
                    else if (input.ToUpper() == "N" || input.ToUpper() == "NO")
                    {
                        update = false;
                    }
                    else
                    {
                        Console.WriteLine("Must input a valid response.");
                        update = true;
                    }

                }
                Console.Clear();
                bool another = true;
                while (another == true)
                {
                    Console.WriteLine("Would you like to add a new student? Y/N");
                    string input = Console.ReadLine();

                    if (input.ToUpper() == "Y" || input.ToUpper() == "YES")
                    {
                        bool add = true;
                        while (add == true)
                        {
                            Console.Clear();
                            Console.WriteLine("Input new student's name:");
                            string name = Console.ReadLine();
                            Console.WriteLine("Input new student's hometown:");
                            string hometown = Console.ReadLine();
                            Console.WriteLine("Input new student's favorite food:");
                            string favoriteFood = Console.ReadLine();

                            Student s = new Student() { Name = name, Location = hometown, FavoriteFood = favoriteFood };
                            AddStudent(s, gc);
                            add = GoAgain();
                            another = false;
                        }
                        another = false;
                    }
                    else if (input.ToUpper() == "N" || input.ToUpper() == "NO")
                    {
                        another = false;
                    }
                    else
                    {
                        Console.WriteLine("Must input a valid response.");
                        another = true;
                    }

                }
                Console.Clear();
                bool onceAgain = true;
                while (onceAgain == true)
                {
                    Console.WriteLine("Would you like to delete a student? Y/N");
                    string input = Console.ReadLine();

                    if (input.ToUpper() == "Y" || input.ToUpper() == "YES")
                    {
                        bool delete = true;
                        while (delete == true)
                        {
                            Console.Clear();
                            PrintClassIdandName(students, gc);
                            Console.WriteLine("Please select a student to delete");
                            Student s = GetInput(gc);
                            if (s == null)
                            {
                                Console.WriteLine("That is not a current student (PRESS ENTER)");
                                Console.ReadKey();
                                Console.Clear();
                                delete = true;
                            }
                            else
                            {
                                DeleteStudent(s, gc);
                                delete = GoAgain();
                                onceAgain = false;
                            }
                        }
                        onceAgain = false;
                    }
                    else if (input.ToUpper() == "N" || input.ToUpper() == "NO")
                    {
                        onceAgain = false;
                    }
                    else
                    {
                        Console.WriteLine("Must input a valid response.");
                        onceAgain = true;
                    }
                }
                startOver = StartOver();
            }
        }

        public static void PrintClassIdandName(List<Student> Class, GCClass2021Context gc)
        {
            if(Class.Count == 0)
            {
                Console.WriteLine("There are no students in this database");
            }
            foreach(Student s in Class)
            {
                Console.WriteLine(s.Id + " : " + s.Name);
            }
        }


        //Read
        public static Student SearchStudentById(int id, GCClass2021Context gc)
        {
            try
            {
                Student s = gc.Students.Find(id);
                return s;
            }
            catch (InvalidOperationException)
            {
                Student s = null;
                return s;
            }
        }

        public static Student SearchStudentByName(string name, GCClass2021Context gc)
        {
            try
            {
                Student s = gc.Students.Where(x => x.Name.ToLower() == name.ToLower()).ToList().First();
                return s;
            }
            catch(InvalidOperationException)
            {
                Student s = null;
                return s;
            }
        }

        //Create
        public static void AddStudent(Student newStudent, GCClass2021Context gc)
        {
            gc.Students.Add(newStudent);
            gc.SaveChanges();
            Console.WriteLine("New student was added");
            Console.WriteLine();
        }
        
        //Update
        public static void UpdateName(int id, GCClass2021Context gc, string updatedName)
        {
            Student s = gc.Students.Find(id);
            s.Name = updatedName;
            gc.Students.Update(s);
            gc.SaveChanges();
        }

        public static void UpdateLocation(int id, GCClass2021Context gc, string updatedLocation)
        {
            Student s = gc.Students.Find(id);
            s.Location = updatedLocation;
            gc.Students.Update(s);
            gc.SaveChanges();
        }

        public static void UpdateFood(int id, GCClass2021Context gc, string updatedFood)
        {
            Student s = gc.Students.Find(id);
            s.FavoriteFood = updatedFood;
            gc.Students.Update(s);
            gc.SaveChanges();
        }

        public static void UpdateWhat(Student s, GCClass2021Context gc)
        {
            bool again = true;
            while (again == true)
            {
                Console.Clear();
                Console.WriteLine($"What would you like to update about the student {s.Name}?");
                Console.WriteLine("1 : Name");
                Console.WriteLine("2 : Hometown");
                Console.WriteLine("3 : Favorite food");
                string input = Console.ReadLine();
                if (input.ToLower() == "name" || input.StartsWith('1'))
                {
                    Console.WriteLine($"\nWhat is the new name for {s.Name}");
                    UpdateName(s.Id, gc, Console.ReadLine());
                    again = false;
                }
                else if (input.ToLower() == "home" || input.StartsWith('2') || input.ToLower() == "hometown")
                {
                    Console.WriteLine($"\nWhat is the new hometown for {s.Name}");
                    UpdateLocation(s.Id, gc, Console.ReadLine());
                    again = false;
                }
                else if (input.ToLower() == "food" || input.StartsWith('3') || input.ToLower() == "favorite food" || input.ToLower() == "favorite")
                {
                    Console.WriteLine($"\nWhat is the new favorite food for {s.Name}");
                    UpdateFood(s.Id, gc, Console.ReadLine());
                    again = false;
                }
                else
                {
                    Console.WriteLine("That was not an option");
                    again = true;
                }
            }
            Console.WriteLine("\nThe student has been updated to: \n");
            Console.WriteLine(s.Id + " - " + s.Name);
            Console.WriteLine("Hometown: "+ s.Location);
            Console.WriteLine("Favorite food: " + s.FavoriteFood);
        }

        //Delete - procede with caution
        public static void DeleteStudent(Student s, GCClass2021Context gc)
        {
            try
            {
                gc.Students.Remove(s);
                gc.SaveChanges();
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("That is not a current student");
                Console.WriteLine();
            }
        }

        public static Student GetInput(GCClass2021Context gc)
        {
            string input = Console.ReadLine();
            try
            {
                int input2 = int.Parse(input); ;
                Student s = SearchStudentById(input2, gc);
                return s;
            }
            catch (FormatException)
            {
                Student s = SearchStudentByName(input, gc);
                return s;
            }
        }

        public static bool GetOutput(Student student)
        {
            Console.WriteLine($"\nThat student is {student.Name}");
            bool repeat = true;
            while (repeat == true)
            {
                Console.WriteLine("\nWould you like to learn about their hometown or favorite food? ");
                string learn = Console.ReadLine();

                if (learn.ToLower() == "hometown" || learn.ToLower().Contains("home"))
                {
                    bool another = true;
                    Console.WriteLine($"\n{student.Name}'s hometown is {student.Location}");
                    Console.WriteLine();

                    while (another == true)
                    {
                        Console.WriteLine("Would you like to learn more? Y/N ");
                        string more = Console.ReadLine();

                        if (more.ToLower() == "yes" || more.ToLower().Contains("y"))
                        {
                            Console.Clear();
                            Console.WriteLine($"\n{student.Name}'s favorite food is {student.FavoriteFood}");
                            another = false;
                            repeat = false;
                            continue;
                        }
                        else if (more.ToLower() == "no" || more.ToLower().Contains("n"))
                        {
                            Console.WriteLine("\nokay then");
                            Console.WriteLine();
                            another = false;
                            repeat = false;
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("\nNot valid");
                            Console.Clear();
                            another = true;
                        }
                    }
                }
                else if (learn.ToLower() == "favoritefood" || learn.ToLower().Contains("food"))
                {
                    bool another = true;
                    Console.WriteLine($"\n{student.Name}'s favorite food is {student.FavoriteFood}");
                    Console.WriteLine();
                    while (another == true)
                    {
                        Console.WriteLine("\nWould you like to learn more? Y/N");
                        string more = Console.ReadLine();

                        if (more.ToLower() == "yes" || more.ToLower().Contains("y"))
                        {
                            Console.Clear();
                            Console.WriteLine($"\n{student.Name}'s hometown is {student.Location}");
                            another = false;
                            repeat = false;
                            continue;
                        }
                        else if (more.ToLower() == "no" || more.ToLower().Contains("n"))
                        {
                            Console.WriteLine("\nokay then");
                            Console.WriteLine();
                            another = false;
                            repeat = false;
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("\nNot valid");
                            Console.Clear();
                            another = true;
                        }
                    }
                }
                else
                {
                    repeat = true;
                }
            }

            return false;
        }

        public static bool GoAgain()
        {
            Console.Write("Would you like to do another Y/N");
            string input = Console.ReadLine();

            if (input.ToUpper() == "Y" || input.ToUpper() == "YES")
            {
                Console.Clear();
                return true;
            }
            else if (input.ToUpper() == "N" || input.ToUpper() == "NO")
            {
                return false;
            }
            else
            {
                Console.WriteLine("Must input a valid response.");
                return GoAgain();
            }
        }

        public static bool StartOver()
        {
            Console.Write("Would you like start from the very beginning? Y/N");
            string input = Console.ReadLine();

            if (input.ToUpper() == "Y" || input.ToUpper() == "YES")
            {
                Console.Clear();
                return true;
            }
            else if (input.ToUpper() == "N" || input.ToUpper() == "NO")
            {
                return false;
            }
            else
            {
                Console.WriteLine("Must input a valid response.");
                return GoAgain();
            }
        }
    }
}
