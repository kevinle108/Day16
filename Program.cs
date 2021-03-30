using System;

namespace Day16
{    
    public class Inheritance
    {
        public static void Main(string[] args)
        {
            Person[] persons =
            {
            new Teacher("Yung"),
            new Student("Kevin"),
            new Student("Tommy"),
            new Student("Huy")
        };

            for (int i = 0; i < persons.Length; i++)
            {
                if (i == 0)
                {
                    ((Teacher)persons[i]).Teach();
                }
                else
                {
                    ((Student)persons[i]).Study();
                }
            }
        }

        public class Person
        {
            protected string Name { get; set; }

            public Person(string name)
            {
                Name = name;
            }

            public void SayHello()
            {
                Console.WriteLine("Hello! My name is " + Name);
            }
        }

        public class Teacher : Person
        {
            public Teacher(string name) : base(name)
            {
            }

            public void Teach()
            {
                Console.WriteLine(Name + " teaches");
            }
        }

        public class Student : Person
        {
            public Student(string name) : base(name)
            {
            }

            public void Study()
            {
                Console.WriteLine(Name + " studies");
            }
        }
    }

}
