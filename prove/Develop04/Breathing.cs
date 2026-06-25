public class Breathing : BaseActivity
{
    public Breathing()
    {
        _activityName = "Breathing";
        _activityDescription = "This activity will help you relax through slow breathing.";
    }

    public void Run()
    {
        DisplayStart();

        DisplayGetReady();
        DateTime endTime = DateTime.Now.AddSeconds(_activityDuration); 

        Console.WriteLine();
        int cycle =1;
        //breathing cycle
        while (DateTime.Now < endTime)
        {
            Console.WriteLine();
            Console.WriteLine($"--- Cycle {cycle} ---");
            Console.Write("Breathe in... ");
            CountDown(4);
            Console.WriteLine();

            Console.Write("Breathe out... ");
            CountDown(6);

            Console.WriteLine();
            cycle ++;
        }

        DisplayEnd();
    }
}