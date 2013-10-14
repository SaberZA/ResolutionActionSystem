using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResolutionActionSystem.Controllers
{
    public interface IControllable
    {
        void InitController();

        Controller GetController();

        void RegisterPropertyControls();
    }
}
