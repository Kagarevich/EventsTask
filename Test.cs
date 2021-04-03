using ClassLibrary1;

namespace EventsTask
{
    class Test
    {
        static public void Main()
        {
            Tariff tariff = new()
            {
                Value = 10,
                HouseId = 10,
                PeriodBegin = System.DateTime.Today,
                PeriodEnd = System.DateTime.Today,
                ServiceId = 10
            };
            Volume volume = new()
            {
                Value = 10,
                HouseId = 10,
                ServiceId = 10,
                Month = System.DateTime.Today
            };

            

            SubsidyCalculation subsidyCalculation = new();
            subsidyCalculation.OnNotify += SubsidyCalculation_OnNotify;
            subsidyCalculation.OnException += SubsidyCalculation_OnException;
            subsidyCalculation.CalculateSubsidy(volume, tariff);
        }

        static void SubsidyCalculation_OnNotify(object sender, string testMessage)
        {
            System.Console.WriteLine(testMessage);
        }

        static void SubsidyCalculation_OnException(object sender, System.Tuple<string, System.Exception> exception)
        {
            throw new System.Exception(exception.Item1 + "; " + exception.Item2 + "\n");
        }

    }
}
