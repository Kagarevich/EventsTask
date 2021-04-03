using System;
using ClassLibrary1;

namespace EventsTask
{
    class SubsidyCalculation: ISubsidyCalculation
    {
        public event EventHandler<string> OnNotify;
        public event EventHandler<Tuple<string, Exception>> OnException;

        public Charge CalculateSubsidy(Volume volumes, Tariff tariff)
        {
            throw new NotImplementedException();
        }
    }
}
