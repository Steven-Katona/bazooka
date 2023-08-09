using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FakeArcade2.GameStuff
{
    public interface IKineticBehavior
    {

        bool implementAction(Action result, bool bool_result);
        void Behave(Action EnemyBehavior);
    }
}
