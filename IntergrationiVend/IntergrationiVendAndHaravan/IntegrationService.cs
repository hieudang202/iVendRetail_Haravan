using IntergrationHaravan.Business;
using System.ServiceProcess;

using System.Timers;

namespace IntergrationHaravan
{
    public partial class IntegrationService : ServiceBase
    {

        private Timer timer = null;
        public IntegrationService()
        {
            InitializeComponent();
            IvendRetailService.GetProductFromiVend();
        }

        protected override void OnStart(string[] args)
        {
            timer = new Timer();
            timer.Interval = 6000;
            timer.Elapsed += timer_Tick;
            timer.Enabled = true;
            //BUS.readfiles(); // Khi nào cài đặt thì mớoik mở cái này, xóa cái dòng trên đi , sửa đi nhe
            Utilities.WriteLogError("Test log");
        }
       
        private void timer_Tick(object sender, ElapsedEventArgs args)
        {
            Utilities.WriteLogError("tets 2");
        }

        protected override void OnStop()
        {
            timer.Enabled = true;
            Utilities.WriteLogError("test 3");
        }
    }
}
