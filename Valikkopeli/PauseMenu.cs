using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RayGuiCreator;


namespace Valikkopeli
{
    internal class PauseMenu
    {
        public event EventHandler BackButtonPressedEvent;
        
        public void Draw()
        {
            MenuCreator pause = new MenuCreator(40, 40, 32, 200, 2);
            if (pause.Button("Back"))
            {
                BackButtonPressedEvent.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
