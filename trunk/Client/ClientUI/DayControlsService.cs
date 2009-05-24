using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientUI
{
    public class DayControlsService
    {
        public static readonly DayControlsService Instance = new DayControlsService();

        private List<DayControl> dayControls = new List<DayControl>();

        public void Register(DayControl dayControl)
        {
            this.dayControls.Add(dayControl);
        }

        public void RefreshAll()
        {
            foreach (DayControl dayControl in dayControls)
            {
                dayControl.Refresh();
            }
        }
    }
}
