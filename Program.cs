using System;

namespace QuizMasterApp
{
    // ==========================================
    // CHAPTER 10: CLASS AND OBJECT (Player Entity)
    // คลาสสำหรับเก็บข้อมูลผู้เล่นและสถิติส่วนตัว
    // ==========================================
    class Player
    {
        private string playerName;
        private int score;
        private int totalAttempts;

        public string PlayerName
        {
            get { return playerName; }
            set { playerName = value; }
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public int TotalAttempts
        {
            get { return totalAttempts; }
            set { totalAttempts = value; }
        }

        // Constructor
        public Player(string name)
        {
            playerName = name;
            score = 0;
            totalAttempts = 0;
        }

        // Method แสดงข้อมูลผู้เล่น
        public void DisplayPlayerInfo()
        {
            Console.WriteLine($"Player: {playerName} | Correct Answers: {score} / {totalAttempts}");
        }
    }

    // ==========================================
    // CHAPTER 10: CLASS AND OBJECT (Question Entity)
    // คลาสสำหรับจัดการคำถามและตัวเลือก
    // ==========================================
    class Question
    {
        public string QuestionText;
        public string[] Options;
        public int CorrectOptionIndex; // 1 to 4

        // Constructor
        public Question(string text, string[] options, int correctOption)
        {
            QuestionText = text;
            Options = options;
            CorrectOptionIndex = correctOption;
        }

        // Method แสดงคำถาม
        public void DisplayQuestion(int number)
        {
            Console.WriteLine($"\nQ{number}: {QuestionText}");
            for (int i = 0; i < Options.Length; i++)
            {
                Console.WriteLine($"    ({i + 1}) {Options[i]}");
            }
        }
    }

    class Program
    {
        // ==========================================
        // CHAPTER 2 & 9: VARIABLES & ARRAYS
        // พื้นที่จัดเก็บคำถามและอาเรย์เก็บรายชื่อผู้เล่น (Leaderboard)
        // ==========================================
        static Question[] questionBank = new Question[20];
        static int questionCount = 0;

        static Player[] playerLeaderboard = new Player[5]; // เก็บผู้เล่นได้สูงสุด 5 คน
        static int playerCount = 0;

        // Game Statistics variables
        static int totalGamesPlayed = 0;
        static int totalQuestionsAnswered = 0;
        static int totalCorrectAnswers = 0;

        static void Main(string[] args)
        {
            LoadDefaultQuestions();
            bool isRunning = true;

            // ==========================================
            // CHAPTER 6 & 7: ITERATION STATEMENTS (LOOPS)
            // วนลูปหลักของแอปพลิเคชัน (Main Menu Loop)
            // ==========================================
            while (isRunning)
            {
                DisplayMainMenu();
                string choice = Console.ReadLine();

                // ==========================================
                // CHAPTER 4 & 5: MAKING DECISIONS (SWITCH)
                // ตรวจสอบตัวเลือกเมนูหลัก
                // ==========================================
                switch (choice)
                {
                    case "1":
                        StartQuiz();
                        break;
                    case "2":
                        AddNewQuestion();
                        break;
                    case "3":
                        ViewQuestions();
                        break;
                    case "4":
                        ViewGameStatistics();
                        break;
                    case "5":
                        ViewLeaderboard(); // ฟังก์ชันใหม่สำหรับดูรายชื่อผู้เล่น
                        break;
                    case "6":
                        Console.WriteLine("\nThank you for playing Quiz Master! Goodbye.");
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("\n[Error] Invalid choice. Please enter a number between 1 and 6.");
                        break;
                }
            }
        }

        // ==========================================
        // CHAPTER 8: METHODS
        // ==========================================
        static void DisplayMainMenu()
        {
            Console.WriteLine("\n==================================================");
            Console.WriteLine("                    QUIZ MASTER                   ");
            Console.WriteLine("==================================================");
            Console.WriteLine("\n    Welcome to Quiz Master!\n");
            Console.WriteLine("    [1] Start Quiz");
            Console.WriteLine("    [2] Add New Question");
            Console.WriteLine("    [3] View Questions");
            Console.WriteLine("    [4] View Game Statistics");
            Console.WriteLine("    [5] View Player Leaderboard");
            Console.WriteLine("    [6] Exit\n");
            Console.WriteLine("==================================================");
            Console.Write("Enter your choice: ");
        }

        static void LoadDefaultQuestions()
        {
            questionBank[0] = new Question(
                "Which flowchart symbol is used for making decisions?",
                new string[] { "Rectangle", "Parallelogram", "Diamond", "Oval" },
                3
            );
            questionBank[1] = new Question(
                "Which C# data type is a Reference Type?",
                new string[] { "int", "double", "bool", "string" },
                4
            );
            questionBank[2] = new Question(
                "What is the result of the expression: 10 % 3 in C#?",
                new string[] { "3", "1", "0", "3.33" },
                2
            );
            questionBank[3] = new Question(
                "Which loop is guaranteed to execute at least once?",
                new string[] { "for loop", "while loop", "do-while loop", "foreach loop" },
                3
            );
            questionBank[4] = new Question(
                "What keyword is used to create a new instance (object) of a class?",
                new string[] { "class", "new", "static", "void" },
                2
            );

            questionCount = 5;
        }

        // [1] Start Quiz
        static void StartQuiz()
        {
            if (questionCount == 0)
            {
                Console.WriteLine("\nNo questions available! Please add questions first.");
                return;
            }

            Console.Write("\nEnter your Player Name: ");
            string playerName = Console.ReadLine();
            Player currentPlayer = new Player(playerName);

            Console.WriteLine($"\n--- STARTING QUIZ FOR {playerName.ToUpper()} ---");
            int currentScore = 0;

            for (int i = 0; i < questionCount; i++)
            {
                questionBank[i].DisplayQuestion(i + 1);

                int userAnswer = 0;
                bool validInput = false;

                // Input validation loop
                while (!validInput)
                {
                    Console.Write("Your answer (1-4): ");
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out userAnswer) && userAnswer >= 1 && userAnswer <= 4)
                    {
                        validInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input! Please enter 1, 2, 3, or 4.");
                    }
                }

                // Check answer using if-else
                if (userAnswer == questionBank[i].CorrectOptionIndex)
                {
                    Console.WriteLine(">> Correct!");
                    currentScore++;
                }
                else
                {
                    int correctIdx = questionBank[i].CorrectOptionIndex - 1;
                    Console.WriteLine($">> Wrong! The correct answer was ({questionBank[i].CorrectOptionIndex}) {questionBank[i].Options[correctIdx]}");
                }
            }

            // บันทึกคะแนนลงในออบเจกต์ Player
            currentPlayer.Score = currentScore;
            currentPlayer.TotalAttempts = questionCount;

            // บันทึกผู้เล่นลงในอาเรย์ Leaderboard (ถ้ายังมีที่ว่าง)
            if (playerCount < playerLeaderboard.Length)
            {
                playerLeaderboard[playerCount] = currentPlayer;
                playerCount++;
            }

            // Update overall statistics
            totalGamesPlayed++;
            totalQuestionsAnswered += questionCount;
            totalCorrectAnswers += currentScore;

            double percentage = ((double)currentScore / questionCount) * 100;
            Console.WriteLine("\n--------------------------------------------------");
            Console.WriteLine($"Quiz Finished! {playerName} scored {currentScore} out of {questionCount} ({percentage:F1}%)");
            Console.WriteLine("--------------------------------------------------");
        }

        // [2] Add New Question
        static void AddNewQuestion()
        {
            if (questionCount >= questionBank.Length)
            {
                Console.WriteLine("\n[Error] Question bank is full (Max 20 questions).");
                return;
            }

            Console.WriteLine("\n--- ADD NEW QUESTION ---");
            Console.Write("Enter the question text: ");
            string qText = Console.ReadLine();

            string[] options = new string[4];
            for (int i = 0; i < 4; i++)
            {
                Console.Write($"Enter option {i + 1}: ");
                options[i] = Console.ReadLine();
            }

            int correctOption = 0;
            while (correctOption < 1 || correctOption > 4)
            {
                Console.Write("Enter the correct option number (1-4): ");
                int.TryParse(Console.ReadLine(), out correctOption);
                if (correctOption < 1 || correctOption > 4)
                {
                    Console.WriteLine("Please enter a valid number between 1 and 4.");
                }
            }

            questionBank[questionCount] = new Question(qText, options, correctOption);
            questionCount++;

            Console.WriteLine("\n[Success] Question added successfully!");
        }

        // [3] View Questions
        static void ViewQuestions()
        {
            Console.WriteLine("\n--- ALL QUESTIONS IN BANK ---");
            if (questionCount == 0)
            {
                Console.WriteLine("No questions found.");
                return;
            }

            for (int i = 0; i < questionCount; i++)
            {
                questionBank[i].DisplayQuestion(i + 1);
                Console.WriteLine($"    [Answer: Option {questionBank[i].CorrectOptionIndex}]");
            }
        }

        // [4] View Game Statistics
        static void ViewGameStatistics()
        {
            Console.WriteLine("\n--- GAME STATISTICS ---");
            Console.WriteLine($"Total Questions in Bank : {questionCount}");
            Console.WriteLine($"Total Games Played      : {totalGamesPlayed}");
            Console.WriteLine($"Total Questions Answered: {totalQuestionsAnswered}");
            Console.WriteLine($"Total Correct Answers   : {totalCorrectAnswers}");

            double accuracy = (totalQuestionsAnswered > 0)
                ? ((double)totalCorrectAnswers / totalQuestionsAnswered) * 100
                : 0.0;

            Console.WriteLine($"Overall Accuracy        : {accuracy:F2}%");
        }

        // [5] View Player Leaderboard (ฟังก์ชันใหม่จาก Class Player)
        static void ViewLeaderboard()
        {
            Console.WriteLine("\n--- PLAYER LEADERBOARD ---");
            if (playerCount == 0)
            {
                Console.WriteLine("No players have played yet.");
                return;
            }

            for (int i = 0; i < playerCount; i++)
            {
                Console.Write($"{i + 1}. ");
                playerLeaderboard[i].DisplayPlayerInfo();
            }
        }
    }
}