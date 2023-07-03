using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kringloopKleding
{
    internal class MessageBoxes
    {
        public void MessageBoxExist()
        {
            messageboxes.MessageBoxExist messageBoxExist = new messageboxes.MessageBoxExist();
            messageBoxExist.Show();
        }

        public void EmptyTextBoxes()
        {
            messageboxes.legenVakjes windowMessage = new messageboxes.legenVakjes();
            windowMessage.Show();
        }

        public void WaitAfhaling()
        {
            messageboxes.MessageBoxWait messageBoxWait = new messageboxes.MessageBoxWait();
            messageBoxWait.Show();
        }

        public void ReasonExist()
        {
            messageboxes.ReasonExist reasonExist = new messageboxes.ReasonExist();
            reasonExist.Show();
        }

    }
}
