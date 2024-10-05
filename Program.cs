
using Microsoft.Data.Sqlite;

class Program{
    static void Main(){
        ClearDartRecords();
        InitializeDataBase();
        Random rand = new Random();
        while(true){
            Console.Write("How many darts would you like to throw at the board: ");
            int numThrows = Convert.ToInt32(Console.ReadLine());
            int hits = 0;
            for(var i = 0;i<numThrows;i++){
                int dart = rand.Next(1,6);
                switch(dart){
                    case 1:
                    case 2:
                    break;
                    case 3:
                    hits++;
                    break;
                    case 4:
                    case 5:
                    break;
                }

            }
            float Accuracy = hits > 0 ? (float)hits/numThrows*100:0;
            string formattedAccuracy = Accuracy.ToString("F2");
            Console.WriteLine();
            Console.WriteLine("Your Accuracy Was: "+formattedAccuracy+"%");
            InsertDartRecord(numThrows,formattedAccuracy);
            Console.WriteLine();
        
        }   
    }
    //Making a function to start the database
    static void InitializeDataBase(){
        //create a database with the connection string
        using var connection = new SqliteConnection("Data Source=darts.db");
            connection.Open();
        //create table statement
        string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS Darts (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    numofDarts INT,
                    accuracy FLOAT
                );";
        //defines the command coupling the create table query and the connection and executes it.
        using var command = new SqliteCommand(createTableQuery, connection);
        command.ExecuteNonQuery();
    }
    static void InsertDartRecord(int numThrows, string accuracy){
        using var connection = new SqliteConnection("Data Source=darts.db");
            connection.Open();
        string insertQuery = "INSERT INTO Darts (numofdarts, accuracy) VALUES (@numofdarts,@accuracy)";
        using var command = new SqliteCommand(insertQuery,connection);
            command.Parameters.AddWithValue("@numofdarts",numThrows);
            command.Parameters.AddWithValue("@accuracy",accuracy);
            command.ExecuteNonQuery();
    }
    static void ClearDartRecords(){
        //using var connection = new SqliteConnection("Data Source=darts.db");
            //connection.Open();
        //string deleteQuery = "DELETE FROM Darts;";
        //using var command = new SqliteCommand(deleteQuery, connection);
            //command.ExecuteNonQuery();
        Console.WriteLine();
        Console.WriteLine("ClearDarts=False");
        Console.WriteLine();
    }
}