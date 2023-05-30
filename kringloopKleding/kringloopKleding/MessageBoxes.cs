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

        public void MessageBoxOkAdd()
        {
            messageboxes.MessageBoxOkAdd messageBoxAdd = new messageboxes.MessageBoxOkAdd();
            messageBoxAdd.Show();
        }

        public void EmptyTextBoxes()
        {
            messageboxes.legenVakjes windowMessage = new messageboxes.legenVakjes();
            windowMessage.Show();
        }

        public void MessageboxOk()
        {
            messageboxes.MessageBoxOk messageBoxOK = new messageboxes.MessageBoxOk();
            messageBoxOK.Show();
        }

    }
}
