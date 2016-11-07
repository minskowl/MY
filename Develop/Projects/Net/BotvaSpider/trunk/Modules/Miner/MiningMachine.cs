using System;
using BotvaSpider.Core;

namespace BotvaSpider.Automation.Mining
{
    class MiningMachine : MachineBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="MiningMachine"/> class.
        /// </summary>
        public MiningMachine()
        {
            logined = new MineLoginedState(this);

            Log = AppCore.LogMine;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        protected override void Run()
        {
        start:
            try
            {
                Login();
                
                //Controller.SaveScreenshot(@"c:\1.mht");
                if (CancelingAction) goto exit;
                if (state.State == MachineState.Error) goto start;

                do
                {
                  
                    UpdatePlayerStatus();

                    if (CancelingAction) goto exit;
                    if (state.State == MachineState.Error) goto start;

                    DoTraning();

                    SearchCrystal();


                    if (CancelingAction) goto exit;
                    if (state.State == MachineState.Error) goto start;

                } while (true);



            }
            catch (Exception ex)
            {
                if (HandleException(ex)) goto start;
            }
        exit:
            state = logouted;
            isRunning = false;
        }
    }
}