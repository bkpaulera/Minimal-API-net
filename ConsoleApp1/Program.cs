class Result
{

    /*
     * Complete the 'getMinFlips' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts STRING pwd as parameter.
     */

    public static int getMinFlips(string pwd)
    {
        int result = 0;
            


        return result;
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string pwd = Console.ReadLine();

        int result = Result.getMinFlips(pwd);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
