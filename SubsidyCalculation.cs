using System;
using ClassLibrary1;

namespace EventsTask
{
    class SubsidyCalculation: ISubsidyCalculation
    {
        class ArgsException : Exception
        {
            public ArgsException(string message)
                : base(message)
            { }
        }
        
        public event EventHandler<string> OnNotify;

        public event EventHandler<Tuple<string, Exception>> OnException;
        
        public void ExceptionCall(string exceptionMessage)
        {
            ArgsException argsException = new(exceptionMessage);
            Tuple<string, Exception> exception = new("incorrect data", argsException);
            OnException.Invoke(this, exception);
        }

        public Charge CalculateSubsidy(Volume volumes, Tariff tariff)
        {
            Charge change = new Charge();
            OnNotify.Invoke(this, $"Start counting ({DateTime.Now})");
            if (volumes.HouseId == tariff.HouseId)
            {
                if (volumes.ServiceId == tariff.ServiceId)
                {
                    if (volumes.Month.Month <= tariff.PeriodEnd.Month && volumes.Month.Month >= tariff.PeriodBegin.Month) 
                    {
                        if (tariff.Value > 0)
                        {
                            if (volumes.Value >= 0)
                            {
                                change.ServiceId = tariff.ServiceId;
                                change.HouseId = tariff.HouseId;
                                change.Value = tariff.Value * volumes.Value;
                                change.Month = DateTime.Today;
                            }
                            else
                            {
                                ExceptionCall("incorrect volumes value");
                            }
                        } 
                        else
                        {
                            ExceptionCall("incorrect tariss value");
                        }
                    }
                    else
                    {
                        ExceptionCall("incorrect period");
                    }
                }
                else
                {
                    ExceptionCall("incorrect service ID");
                }
            }
            else
            {
                ExceptionCall("incorrect house ID");
            }
            OnNotify.Invoke(this, $"End counting ({DateTime.Now})");
            return change;
        }
    }
}
