using System.Collections.Generic;

namespace ClientUI
{
    public class DayControlsService : IDayControlsService
    {

        private readonly List<DayControl> _dayControls = new List<DayControl>();

        public void Register(DayControl dayControl)
        {
            _dayControls.Add(dayControl);
        }

        public void RefreshAll()
        {
            foreach (DayControl dayControl in _dayControls)
            {
                dayControl.Refresh();
            }
        }
    }
}
