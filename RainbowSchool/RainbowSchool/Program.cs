using System;
using System.IO;
namespace RainbowSchool
{
    class Teacher
    {
        int id;
        string name;
        string ClassSection;

        public Teacher(int id, string name, string ClassSection)
        {
            this.id = id;
            this.name = name;
            this.ClassSection = ClassSection;
        }
        public Teacher()
        {
            this.id = 0;
            this.name = "";
            this.ClassSection = "";
        }
        public int getId()
        {
            return this.id;
        }
        public void setId(int id)
        {
            this.id = id;
        }
        public string getName()
        {
            return this.name;
        }
        public void setName(string name)
        {
            this.name = name;
        }
        public string getClassAndSection()
        {
            return this.ClassSection;
        }
        public void setClassAndSection(string ClassSection)
        {
            this.ClassSection = ClassSection;
        }
    }
    class Program
    {
        static string filePath = "/Users/amjaadb/Projects/RainbowSchool.txt";
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Rainbow School system to manuplate teacher data");
            bool inprogress = true;
            while (inprogress)
            {
                Console.WriteLine("please enter your option number...");
                Console.WriteLine("option 1 ... store");
                Console.WriteLine("option 2 ... retrieve");
                Console.WriteLine("option 3 ... update");
                Console.WriteLine("option 4 ... exist program");
                string option = Console.ReadLine();
                if (option.Trim() != "4")
                {
                    switch (option.Trim())
                    {
                        case "1":
                            store();
                            break;
                        case "2":
                            retrieve();
                            break;
                        case "3":
                            update();
                            break;
                        default:
                            Console.WriteLine("please enter a valid option. ex 1");
                            break;
                    }
                }
                else if (option.Trim() == "4")
                {
                    inprogress = false;
                    Console.WriteLine("program terminated ");
                }
            }
        }
        static void store()
        {
            bool NotPipe = false;
            string Record = string.Empty, last = string.Empty;
            Teacher t1 = new Teacher(1, "", "");
            Int64 ID = 1;
            while (!NotPipe)
            {
                Console.WriteLine("please enter a valid name");
                string txt = Console.ReadLine().Trim();
                if (!txt.Contains("|"))
                {
                    t1.setName(txt);
                    NotPipe = true;
                }
            }
            NotPipe = false;
            while (!NotPipe)
            {
                Console.WriteLine("please enter a valid Class and section");
                string txt = Console.ReadLine().Trim();
                if (!txt.Contains("|"))
                {
                    t1.setClassAndSection(txt);
                    NotPipe = true;
                }
            }
            string[] lines = File.ReadAllLines(filePath);
            int cnt = lines.Length - 1;
            if (cnt >= 0)
            {
                last = lines[cnt];
                ID += Convert.ToInt64(last.Substring(0, last.IndexOf("|")).Trim());
            }
            t1.setId((int)ID);
            Record = t1.getId() + " | " + t1.getName() + " | " + t1.getClassAndSection();
            // open file and write 
            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
                fs.Seek(0, SeekOrigin.End);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(Record);
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
        static void retrieve()
        {
            // read
            String line;
            try
            {
                StreamReader sr = new StreamReader(filePath);
                line = sr.ReadLine();
                while (line != null)
                {
                    Console.WriteLine(line);
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
        static void update()
        {
            retrieve();
            Teacher t1 = new Teacher();
            string finalLine = string.Empty, id = string.Empty;
            bool IdExist = false;
            while (!IdExist)
            {
                Console.WriteLine("please enter a valid id you want to update or press 0 to exist update option");
                id = Console.ReadLine();
                if (id.Trim() == "0")
                {
                    return;
                }
                else
                {
                    foreach (string line in System.IO.File.ReadAllLines(filePath))
                    {
                        if (line.Contains(id + " | "))
                        {
                            finalLine = line;
                            Console.WriteLine(line);
                            IdExist = true;
                            t1.setId((int)Convert.ToInt64(id));
                        }
                    }
                }
            }
            string selection = string.Empty, Name = string.Empty,
            finalFullLine = string.Empty, ClassAndSection = string.Empty;
            bool IsSelection = false;
            while (!IsSelection)
            {
                bool NotPipe = false;
                Console.WriteLine("what do you want to update");
                Console.WriteLine("press 1 if you want to update Name only");
                Console.WriteLine("press 2 if you want to update Class and section only");
                Console.WriteLine("press 3 if you want to update Name, Class and section ");
                Console.WriteLine("press 0 if you want to exist update option ");
                selection = Console.ReadLine();
                switch (selection)
                {
                    case "0":
                        return;
                    case "1":
                        while (!NotPipe)
                        {
                            Console.WriteLine("please enter a valid name");
                            string txt = Console.ReadLine().Trim();
                            if (!txt.Contains("|"))
                            {
                                t1.setName(txt);
                                NotPipe = true;
                            }
                        }
                        finalFullLine = t1.getId() + " | " + t1.getName() + " " + finalLine.Substring(finalLine.LastIndexOf("|"));
                        IsSelection = true;
                        break;
                    case "2":
                        while (!NotPipe)
                        {
                            Console.WriteLine("please enter a valid Class and section");
                            string txt = Console.ReadLine().Trim();
                            if (!txt.Contains("|"))
                            {
                                t1.setClassAndSection(txt);
                                NotPipe = true;
                            }
                        }
                        finalFullLine = t1.getId() + finalLine.Substring(finalLine.IndexOf(" | "), finalLine.LastIndexOf("|")) + " " + t1.getClassAndSection();
                        IsSelection = true;
                        break;
                    case "3":
                        while (!NotPipe)
                        {
                            Console.WriteLine("please enter a valid name");
                            string txt = Console.ReadLine().Trim();
                            if (!txt.Contains("|"))
                            {
                                t1.setName(txt);
                                NotPipe = true;
                            }
                        }
                        NotPipe = false;
                        while (!NotPipe)
                        {
                            Console.WriteLine("please enter a valid Class and section");
                            string txt = Console.ReadLine().Trim();
                            if (!txt.Contains("|"))
                            {
                                t1.setClassAndSection(txt);
                                NotPipe = true;
                            }
                        }
                        finalFullLine = t1.getId() + " | " + t1.getName() + " | " + t1.getClassAndSection();
                        IsSelection = true;
                        break;
                    default:
                        IsSelection = false;
                        break;
                }
            }
            StreamReader reader = new StreamReader(filePath);
            string input = reader.ReadToEnd();

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                {
                    string output = input.Replace(finalLine, finalFullLine);
                    writer.Write(output);
                }
                writer.Close();
            }
        }
    }
}
